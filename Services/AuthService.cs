using Microsoft.EntityFrameworkCore;
using hotel.Data;
using hotel.Models;

namespace hotel.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message, string Role)> LoginAsync(string? login, string? password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "Заполните логин и пароль", "");
            }

            // 1. Ищем пользователя (свойство Login, а не login)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

            if (user == null)
            {
                return (false, "Вы ввели неверный логин или пароль. Пожалуйста, проверьте ещё раз введённые данные", "");
            }

            // 2. Проверка на блокировку (IsBlocked имеет тип sbyte: 0 = разблокирован, 1 = заблокирован)
            if (user.IsBlocked != 0)
            {
                return (false, "Вы заблокированы. Обратитесь к администратору", "");
            }

            // 3. Проверка пароля (PasswordHash, а не password_hash)
            if (user.PasswordHash != password)
            {
                user.FailedAttempts++;

                if (user.FailedAttempts >= 3)
                {
                    user.IsBlocked = 1; // 👈 sbyte, поэтому 1, а не true
                    await _context.SaveChangesAsync();
                    return (false, "Вы заблокированы. Обратитесь к администратору", "");
                }

                await _context.SaveChangesAsync();
                return (false, "Вы ввели неверный логин или пароль. Пожалуйста, проверьте ещё раз введённые данные", "");
            }

            // 4. Успешный вход
            user.FailedAttempts = 0;
            await _context.SaveChangesAsync();

            return (true, "Вы успешно авторизовались", user.Role);
        }
    }
}