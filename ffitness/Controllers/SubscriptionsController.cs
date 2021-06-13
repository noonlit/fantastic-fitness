using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ffitness.Data;
using Ffitness.Models;
using AutoMapper;
using Ffitness.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Ffitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubscriptionsController(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Subscriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionViewModel>>> GetSubscriptions()
        {
            return await _context.Subscriptions
                .Include(s => s.Bookings)
                .Select(s => _mapper.Map<SubscriptionViewModel>(s))
                .ToListAsync();                
        }

        // GET: api/Subscriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionViewModel>> GetSubscription(int id)
        {
            var subscription = await _context.Subscriptions
                .Include(s => s.Bookings)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subscription == null)
            {
                return NotFound();
            }

            return _mapper.Map<SubscriptionViewModel>(subscription);
        }

        // PUT: api/Subscriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscription(int id, SubscriptionWithBookingViewModel subscriptionFromUi)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (id != subscriptionFromUi.Id)
            {
                return BadRequest();
            }

            var subscriptionToUpdate = _context.Subscriptions
                .Include(s => s.Bookings)
                .FirstOrDefault(s => s.Id == subscriptionFromUi.Id);

            if(subscriptionToUpdate == null)
            {
                return NotFound();
            }

            if (subscriptionFromUi.BookingIds.Count != 0)
            {
                var bookingsToRemove = subscriptionToUpdate.Bookings.ToList();
                bookingsToRemove.ForEach(s =>
                {
                    if (!subscriptionFromUi.BookingIds.Contains(s.Id))
                    {
                        subscriptionToUpdate.Bookings.Remove(s);//anulez rezervarea
                    }
                });
                subscriptionFromUi.BookingIds.ForEach(sId =>
                {
                    var bookingToAdd = _context.Bookings.Find(sId);
                    if (bookingToAdd != null && !subscriptionToUpdate.Bookings.Exists(b => b.Id == bookingToAdd.Id))
                    {
                        subscriptionToUpdate.Bookings.Add(bookingToAdd);//adaug o noua rezervare
                    }
                });
            }
            else 
            {
                subscriptionToUpdate.Bookings.Clear();
            }

            /*var newBookings = subscriptionToUpdate.Bookings;
            subscriptionToUpdate.Bookings = newBookings;*/
            //subscriptionToUpdate.Id = subscriptionFromUi.Id;
            //subscriptionToUpdate.SubscriptionStart = subscriptionToUpdate.SubscriptionStart;
            subscriptionToUpdate.User = user;

            _context.Entry(subscriptionToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionExists(id))
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

        // POST: api/Subscriptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subscription>> PostSubscription(SubscriptionWithBookingViewModel subscription)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var subscriptionEntity = new Subscription();
            //subscriptionEntity.Id = subscription.Id;
            subscriptionEntity.Duration = subscription.Duration;
            subscriptionEntity.SubscriptionStart = subscription.SubscriptionStart;
            subscriptionEntity.SubscriptionPrice = subscription.SubscriptionPrice;
            subscriptionEntity.User = user;
            subscriptionEntity.UserId = user.Id;
            subscriptionEntity.Active = true;

            if(subscription.BookingIds.Count != 0)
            {
                List<Booking> bookings = new List<Booking>();
                subscription.BookingIds.ForEach(bId =>
                {
                    var booking = _context.Bookings.Find(bId);
                    if (booking != null)
                    {
                        bookings.Add(booking);
                    }
                });

                if (bookings.Count == 0)
                {
                    return BadRequest("The bookings you provided are not available");
                }
                subscriptionEntity.Bookings = bookings;
            }

            _context.Subscriptions.Add(subscriptionEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubscription", new { id = subscription.Id }, subscription);
        }

        // DELETE: api/Subscriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubscriptionExists(int id)
        {
            return _context.Subscriptions.Any(e => e.Id == id);
        }
    }
}
