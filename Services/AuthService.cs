using Microsoft.EntityFrameworkCore;
using hotel.Data;
using hotel.Models;

namespace hotel.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context) => _context = context;

        public async Task<(bool Success, string Message, string Role)> LoginAsync(string? login, string? password)
        {
            const string BadLoginMsg = "Вы ввели неверный логин или пароль. Пожалуйста, проверьте ещё раз введённые данные";
            const string BlockedMsg = "Вы заблокированы. Обратитесь к администратору";

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return (false, "Заполните логин и пароль", "");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

            if (user == null)
                return (false, BadLoginMsg, "");

            if (user.IsBlocked != 0)
                return (false, BlockedMsg, "");

            if (user.PasswordHash != password)
            {
                user.FailedAttempts++;

                if (user.FailedAttempts >= 3)
                {
                    user.IsBlocked = 1;
                    await _context.SaveChangesAsync();
                    return (false, BlockedMsg, "");
                }

                await _context.SaveChangesAsync();
                return (false, BadLoginMsg, "");
            }
            user.FailedAttempts = 0;
            await _context.SaveChangesAsync();
            return (true, "Вы успешно авторизовались", user.Role);
        }
    }
}