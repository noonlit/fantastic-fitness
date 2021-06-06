using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ffitness.Data;
using Ffitness.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Ffitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public BookingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
        public async Task<ActionResult<Booking>> GetUserBooking(int scheduledActivityId)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var booking = await _context.Bookings.Where(b => b.UserId == user.Id && b.ScheduledActivityId == scheduledActivityId).FirstOrDefaultAsync();

            if (booking == null)
            {
                return new EmptyResult();
            }

            return booking;
        }

        [HttpPost("BookSpot")]
        [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
        public async Task<ActionResult<Booking>> BookSpot(ScheduledActivity activity)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userId = user.Id;

            var booking = new Booking { ScheduledActivityId = activity.Id, UserId = user.Id };

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
        [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsForCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
            {
                return new List<Booking>();
            }

            return await _context.Bookings
                .Where(b => b.UserId == user.Id)
                .ToListAsync();
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
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
                UserId = user.Id,
                UserAction = Models.UserActions.Booking.Action.CancelBooking,
                CreatedAt = DateTime.UtcNow
            };

            _context.UserActionsBooking.Add(bookingLog);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
