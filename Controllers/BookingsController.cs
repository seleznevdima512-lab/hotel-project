using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hotel.Data;
using hotel.Models.DTOs;

namespace hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BookingsController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetBookings([FromQuery] int? id)
        {
            if (id <= 0)
                return BadRequest(new { error = "Параметр id должен быть целым положительным числом." });

            try
            {
                var bookings = await _context.Bookings
                    .Include(b => b.OrganizationCardNavigation)
                    .Where(b => !id.HasValue || b.BookingNo == id)
                    .ToListAsync();

                return Ok(bookings.Select(b => new BookingResponse
                {
                    Id = b.BookingNo,
                    Caption = $"{b.OrganizationCardNavigation?.Name ?? "Неизвестная организация"} - {b.Person}",
                    Contact = b.Person,
                    FormattedDate = b.ApplicationDate.ToString("dd.MM.yyyy")
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Внутренняя ошибка сервера: {ex.Message}" });
            }
        }
    }
}