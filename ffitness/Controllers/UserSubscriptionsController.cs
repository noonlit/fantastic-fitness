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
            return await _context.UserSubscriptions.Select(s => _mapper.Map<UserSubscriptionViewModel>(s)).ToListAsync();
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
        public async Task<ActionResult<IEnumerable<UserSubscriptionViewModel>>> GetCurrentUserSubscriptions()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await _context.UserSubscriptions
                .Where(s => s.UserId == user.Id)
                .Select(s => _mapper.Map<UserSubscriptionViewModel>(s))
                .ToListAsync();
        }

        [HttpGet("User/{subscriptionId}")]
        [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
        public async Task<ActionResult<UserSubscriptionViewModel>> GetCurrentUserSubscription(int subscriptionId)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _context.UserSubscriptions
                .Where(s => s.UserId == user.Id && s.SubscriptionId == subscriptionId)
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

            var subscriptionEntity = _mapper.Map<UserSubscription>(userSubscription);
            subscriptionEntity.User = user;
            subscriptionEntity.Subscription = await _context.Subscriptions.FindAsync(userSubscription.SubscriptionId);

            _context.UserSubscriptions.Add(subscriptionEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserSubscription", new { id = userSubscription.Id }, _mapper.Map<UserSubscriptionViewModel>(subscriptionEntity));
        }

        // PUT: api/UserSubscriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSubscription(int id, UserSubscriptionViewModel userSubscription)
        {
            if (id != userSubscription.Id)
            {
                return BadRequest();
            }

            var subscriptionEntity = _mapper.Map<UserSubscriptionViewModel>(userSubscription);

            _context.Entry(subscriptionEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSubscriptionExists(id))
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

        // POST: api/UserSubscriptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserSubscriptionViewModel>> PostUserSubscription(UserSubscriptionViewModel userSubscription)
        {
            var subscriptionEntity = _mapper.Map<UserSubscription>(userSubscription);
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
