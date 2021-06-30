using Ffitness.Data;
using Ffitness.Models.Stats;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Services
{
	public class StatisticsQueryService
	{
		private readonly ApplicationDbContext _context;

		public StatisticsQueryService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<BookedScheduledActivity>> getBookedScheduledActivitiesStatsAsync(int daysAhead)
		{
			var sql = "SELECT (SELECT CONCAT(Trainers.FirstName, ' ', Trainers.LastName) FROM Trainers WHERE Trainers.Id = ScheduledActivities.TrainerId) AS TrainerName, " +
				"(SELECT Activities.Name FROM Activities WHERE Activities.Id = ScheduledActivities.ActivityId) AS ActivityName, " +
				"CASE WHEN BookedSpots IS NULL THEN 0 ELSE BookedSpots END AS BookedSpots, " +
				"CASE WHEN BookedSpots IS NOT NULL THEN ScheduledActivities.Capacity ELSE ScheduledActivities.Capacity END AS RemainingSpots, " +
				" ScheduledActivities.Price, ScheduledActivities.StartTime, ScheduledActivities.EndTime " +
				"FROM ScheduledActivities " +
				"LEFT JOIN(SELECT Bookings.ScheduledActivityId, COUNT(*) AS BookedSpots " +
				"FROM Bookings GROUP BY Bookings.ScheduledActivityId) AS BookingsTable " +
				"ON BookingsTable.ScheduledActivityId = ScheduledActivities.Id " +
				"WHERE ScheduledActivities.StartTime >= '" + DateTime.UtcNow + "' " +
				"AND ScheduledActivities.StartTime <= '" + DateTime.UtcNow.AddDays(daysAhead) + "' " +
				"ORDER BY ScheduledActivities.StartTime";

			return await _context.BookedScheduledActivities.FromSqlRaw(sql).ToListAsync();
		}

		public async Task<List<PopularActivity>> getPopularActivitiesStatsAsync()
		{
			var sql = "SELECT TOP 10 ActivityId, " +
				"(SELECT Activities.Name FROM Activities WHERE Activities.Id = ActivityId) AS ActivityName, " +
				"CAST(CONVERT(DECIMAL(16,2), SUM(BookedSpots)) / CONVERT(DECIMAL(16,2), SUM(capacity) + SUM(BookedSpots)) * 100 AS DECIMAL(16,2)) AS OccupancyPercentage " +
				"FROM ScheduledActivities " +
				"INNER JOIN (SELECT ScheduledActivityId, COUNT(*) AS BookedSpots " +
				"FROM Bookings " +
				"GROUP BY ScheduledActivityId) AS BookingsCount " +
				"ON ScheduledActivities.Id = BookingsCount.ScheduledActivityId " +
				"GROUP BY ActivityId " +
				"ORDER BY OccupancyPercentage DESC";

			return await _context.PopularActivities.FromSqlRaw(sql).ToListAsync();
		}

		public async Task<List<PopularTrainer>> getPopularTrainersStatsAsync()
		{
			var sql = "SELECT TOP 10 TrainerId, " +
				"(SELECT CONCAT(Trainers.FirstName, ' ', Trainers.LastName) FROM Trainers WHERE Trainers.Id = TrainerId) AS TrainerName, " +
				"CAST(CONVERT(DECIMAL(16,2), SUM(BookedSpots)) / CONVERT(DECIMAL(16,2), SUM(capacity) + SUM(BookedSpots)) * 100 AS DECIMAL(16,2)) AS OccupancyPercentage " +
				"FROM ScheduledActivities " +
				"INNER JOIN (SELECT ScheduledActivityId, COUNT(*) AS BookedSpots " +
				"FROM Bookings " +
				"GROUP BY ScheduledActivityId) AS BookingsCount " +
				"ON ScheduledActivities.Id = BookingsCount.ScheduledActivityId " +
				"GROUP BY TrainerId " +
				"ORDER BY OccupancyPercentage DESC";

			return await _context.PopularTrainers.FromSqlRaw(sql).ToListAsync();
		}
	}
}
