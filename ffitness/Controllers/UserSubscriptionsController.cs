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
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Ffitness.ViewModels;

namespace Ffitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscriptionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSubscriptionsController(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/UserSubscriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSubscriptionViewModel>>> GetUserSubscriptions()
        {
            var result = await _context.UserSubscriptions
               .Include(s => s.User)
               .Include(s => s.Subscription)
               .OrderBy(s => s.UserId)
               .ThenBy(s => s.StartTime)
               .ToListAsync();

            return _mapper.Map<List<UserSubscription>, List<UserSubscriptionViewModel>>(result);
        }

        // GET: api/UserSubscriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSubscriptionViewModel>> GetUserSubscription(int id)
        {
            var userSubscription = await _context.UserSubscriptions.FindAsync(id);

            if (userSubscription == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserSubscriptionViewModel>(userSubscription);
        }

        [HttpGet("User")]
        [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
        public async Task<ActionResult<List<UserSubscriptionViewModel>>> GetCurrentUserSubscriptions()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _context.UserSubscriptions
                .Where(s => s.UserId == user.Id)
                .Include(s => s.Subscription)
                .ToListAsync();

            return _mapper.Map<List<UserSubscription>, List<UserSubscriptionViewModel>>(result);
        }

        [HttpGet("User/{subscriptionId}")]
        [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
        public async Task<ActionResult<UserSubscriptionViewModel>> GetCurrentUserSubscription(int subscriptionId)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _context.UserSubscriptions
                .Where(s => s.UserId == user.Id && s.SubscriptionId == subscriptionId)
                .Include(s => s.Subscription)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                return new EmptyResult();
            }

            return _mapper.Map<UserSubscriptionViewModel>(result);
        }

        [HttpPost("User/{subscriptionId}")]
        [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
        public async Task<ActionResult<UserSubscriptionViewModel>> CreateCurrentUserSubscription(UserSubscriptionViewModel userSubscription)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var existingSubscriptions = await _context.UserSubscriptions
                .Where(s => s.UserId == user.Id)
                .Include(s => s.Subscription)
                .ToListAsync();

            var overlappingSubscription = existingSubscriptions.Any(s => s.EndTime > userSubscription.StartTime);

            if (overlappingSubscription)
			{
                return BadRequest($"This subscription would overlap your current one. Please check your subscriptions status in your account dashboard.");
            }

            var subscriptionEntity = _mapper.Map<UserSubscription>(userSubscription);
            subscriptionEntity.User = user;
            subscriptionEntity.Subscription = await _context.Subscriptions.FindAsync(userSubscription.SubscriptionId);

            _context.UserSubscriptions.Add(subscriptionEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserSubscription", new { id = userSubscription.Id }, _mapper.Map<UserSubscriptionViewModel>(subscriptionEntity));
        }

        // POST: api/UserSubscriptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserSubscriptionViewModel>> PostUserSubscription(UserSubscriptionViewModel userSubscription)
        {
            var subscriptionEntity = _mapper.Map<UserSubscription>(userSubscription);

            var existingSubscriptions = await _context.UserSubscriptions
                .Where(s => s.UserId == subscriptionEntity.UserId)
                .Include(s => s.Subscription)
                .ToListAsync();

            var overlappingSubscription = existingSubscriptions.Any(s => s.EndTime > userSubscription.StartTime);

            if (overlappingSubscription)
            {
                return BadRequest($"This subscription would overlap a current one.");
            }

            _context.UserSubscriptions.Add(subscriptionEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserSubscription", new { id = userSubscription.Id }, userSubscription);
        }

        // DELETE: api/UserSubscriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSubscription(int id)
        {
            var userSubscription = await _context.UserSubscriptions.FindAsync(id);
            if (userSubscription == null)
            {
                return NotFound();
            }

            _context.UserSubscriptions.Remove(userSubscription);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserSubscriptionExists(int id)
        {
            return _context.UserSubscriptions.Any(e => e.Id == id);
        }
    }
}
