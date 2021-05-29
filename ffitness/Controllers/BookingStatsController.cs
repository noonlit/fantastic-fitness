using AutoMapper;
using Ffitness.Data;
using Ffitness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ffitness.Models;
using Ffitness.ViewModels.Stats;
using Ffitness.Models.Stats;

namespace Ffitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingStatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public BookingStatsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookedScheduledActivityViewModel>>> GetStats()
        {
            var sql = "SELECT (SELECT CONCAT(Trainers.FirstName, ' ', Trainers.LastName) FROM Trainers WHERE Trainers.Id = ScheduledActivities.TrainerId) AS TrainerName, " +
                "(SELECT Activities.Name FROM Activities WHERE Activities.Id = ScheduledActivities.ActivityId) AS ActivityName, " +
                "CASE WHEN BookedSpots IS NULL THEN 0 ELSE BookedSpots END AS BookedSpots, " +
                "CASE WHEN BookedSpots IS NOT NULL THEN ScheduledActivities.Capacity - BookedSpots ELSE ScheduledActivities.Capacity END AS RemainingSpots, " +
                " ScheduledActivities.Price, ScheduledActivities.StartTime, ScheduledActivities.EndTime " +
                "FROM ScheduledActivities " +
                "LEFT JOIN(SELECT Bookings.ScheduledActivityId, COUNT(*) AS BookedSpots " +
                "FROM Bookings GROUP BY Bookings.ScheduledActivityId) AS BookingsTable " +
                "ON BookingsTable.ScheduledActivityId = ScheduledActivities.Id " +
                "WHERE ScheduledActivities.StartTime >= '" + DateTime.UtcNow.AddDays(-7) + "'";
                


            var result = await _context.BookedScheduledActivities
                .FromSqlRaw(sql)
                .ToListAsync();


            return _mapper.Map<List<BookedScheduledActivity>, List<BookedScheduledActivityViewModel>>(result);
        }

    }
}
