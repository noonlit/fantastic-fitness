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

namespace Ffitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ActivitiesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityViewModel>>> GetActivities()
        {
            var activities = await _context.Activities.Select(a => _mapper.Map<ActivityViewModel>(a)).ToListAsync();
            return activities;
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityViewModel>> GetActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                return NotFound();
            }
            return _mapper.Map<ActivityViewModel>(activity);
        }

        //GET: api/Activities/FilterByDifficulty/1
        [HttpGet]
        [Route("filterByDifficulty/{maxDifficulty}")]
        public async Task<ActionResult<IEnumerable<ActivityViewModel>>> FilterByDifficulty(int maxDifficulty)
        {
            return await _context.Activities.Where(a => a.DifficultyLevel <= maxDifficulty)
                                            .OrderBy(a => a.DifficultyLevel).Select(a => _mapper.Map<ActivityViewModel>(a)).ToListAsync();
        }

        //GET: api/Activities/FilterByType/yoga
        [HttpGet]
        [Route("filterByType/{activityType}")]
        public async Task<ActionResult<IEnumerable<ActivityViewModel>>> FilterByType(string activityType)
        {
            return await _context.Activities.Where(a => a.Type.Equals(activityType)).Select(a => _mapper.Map<ActivityViewModel>(a)).ToListAsync();
        }

        //GET: api/Activities/SortByName
        [HttpGet]
        [Route("sortByName")]
        public async Task<ActionResult<IEnumerable<ActivityViewModel>>> SortByName()
        {
            return await _context.Activities.OrderBy(a => a.Name).Select(a => _mapper.Map<ActivityViewModel>(a)).ToListAsync();
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, ActivityViewModel activity)
        {
            if (id != activity.Id)
            {
                return BadRequest();
            }

            _context.Entry(_mapper.Map<Activity>(activity)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
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

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity(ActivityViewModel activityRequest)
        {
            Activity activity = _mapper.Map<Activity>(activityRequest);
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivity", new { id = activity.Id }, activity);
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityExists(int id)
        {
            return _context.Activities.Any(a => a.Id == id);
        }
    }
}
