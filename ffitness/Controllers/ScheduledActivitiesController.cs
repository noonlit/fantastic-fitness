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

namespace Ffitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduledActivitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ScheduledActivitiesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ScheduledActivities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduledActivity>>> GetScheduledActivities()
        {
            return await _context.ScheduledActivities.Include(a => a.Activity).Include(t => t.Trainer).ToListAsync();
        }

        // GET: api/ScheduledActivities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduledActivity>> GetScheduledActivity(int id)
        {
            var scheduledActivity = await _context.ScheduledActivities.FindAsync(id);

            if (scheduledActivity == null)
            {
                return NotFound();
            }

            return scheduledActivity;
        }

        // PUT: api/ScheduledActivities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScheduledActivity(int id, ScheduledActivity scheduledActivity)
        {
            if (id != scheduledActivity.Id)
            {
                return BadRequest();
            }

            _context.Entry(scheduledActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduledActivityExists(id))
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

        // POST: api/ScheduledActivities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScheduledActivity>> PostScheduledActivity(ScheduledActivity scheduledActivity)
        {
            _context.ScheduledActivities.Add(scheduledActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScheduledActivity", new { id = scheduledActivity.Id }, scheduledActivity);
        }

        // DELETE: api/ScheduledActivities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScheduledActivity(int id)
        {
            var scheduledActivity = await _context.ScheduledActivities.FindAsync(id);
            if (scheduledActivity == null)
            {
                return NotFound();
            }

            _context.ScheduledActivities.Remove(scheduledActivity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScheduledActivityExists(int id)
        {
            return _context.ScheduledActivities.Any(e => e.Id == id);
        }
    }
}
