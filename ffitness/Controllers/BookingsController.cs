using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ffitness.Data;
using Ffitness.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Ffitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _manager;

		public BookingsController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
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

        [HttpGet("ScheduledActivity/{activityId}")]
        public async Task<ActionResult<Booking>> GetBookingForCurrentUserAndActivity(int activityId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var booking = await _context.Bookings.Where(b => b.UserId == userId && b.ScheduledActivityId == activityId).FirstOrDefaultAsync();

            if (booking == null)
            {
                return new EmptyResult();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost("BookSpot")]
        public async Task<ActionResult<Booking>> BookSpot(ScheduledActivity activity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var booking = new Booking { ScheduledActivityId = activity.Id, UserId = userId };

            _context.Bookings.Add(booking);
            activity.Capacity--;
            _context.Entry(activity).State = EntityState.Modified;

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

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            var activity = _context.ScheduledActivities.Find(booking.ScheduledActivityId);
            activity.Capacity--;
            _context.Entry(activity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
