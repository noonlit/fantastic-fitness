using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ffitness.Data;
using Ffitness.Models;
using System.Security.Claims;

namespace Ffitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

		public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        [HttpGet("ScheduledActivity/{scheduledActivityId}")]
        public async Task<ActionResult<Booking>> GetUserBooking(int scheduledActivityId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var booking = await _context.Bookings.Where(b => b.UserId == userId && b.ScheduledActivityId == scheduledActivityId).FirstOrDefaultAsync();

            if (booking == null)
            {
                return new EmptyResult();
            }

            return booking;
        }

        [HttpPost("BookSpot")]
        public async Task<ActionResult<Booking>> BookSpot(ScheduledActivity activity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var booking = new Booking { ScheduledActivityId = activity.Id, UserId = userId };

            _context.Bookings.Add(booking);
            activity.Capacity--;
            _context.Entry(activity).State = EntityState.Modified;

            var bookingLog = new Models.UserActions.Booking
            {
                ScheduledActivityId = activity.Id,
                UserId = userId,
                UserAction = Models.UserActions.Booking.Action.BookSpot,
                CreatedAt = DateTime.UtcNow
            };

            _context.UserActionsBooking.Add(bookingLog);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        [HttpGet("/Bookings/Current")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsForCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return new List<Booking>();
            }

            var bookingIds = _context.Bookings.Select(b => b.UserId);

            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            var activity = _context.ScheduledActivities.Find(booking.ScheduledActivityId);
            activity.Capacity++;
            _context.Entry(activity).State = EntityState.Modified;


            var bookingLog = new Models.UserActions.Booking
            {
                ScheduledActivityId = activity.Id,
                UserId = userId,
                UserAction = Models.UserActions.Booking.Action.CancelBooking,
                CreatedAt = DateTime.UtcNow
            };

            _context.UserActionsBooking.Add(bookingLog);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
