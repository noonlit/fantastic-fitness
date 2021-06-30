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
using Ffitness.Services;

namespace Ffitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingStatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly StatisticsQueryService _queryService;

		public BookingStatsController(ApplicationDbContext context, IMapper mapper, StatisticsQueryService queryService)
        {
            _context = context;
            _mapper = mapper;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookedScheduledActivityViewModel>>> GetBookedScheduledActivitiesStats()
        {
            var result = await _queryService.getBookedScheduledActivitiesStatsAsync(7);

            return _mapper.Map<List<BookedScheduledActivity>, List<BookedScheduledActivityViewModel>>(result);
        }

        [HttpGet("PopularActivities")]
        public async Task<ActionResult<IEnumerable<PopularActivityViewModel>>> GetPopularActivitiesStats()
        {
            var result = await _queryService.getPopularActivitiesStatsAsync();

            return _mapper.Map<List<PopularActivity>, List<PopularActivityViewModel>>(result);
        }

        [HttpGet("PopularTrainers")]
        public async Task<ActionResult<IEnumerable<PopularTrainerViewModel>>> GetPopularTrainersStats()
        {
            var result = await _queryService.getPopularTrainersStatsAsync();

            return _mapper.Map<List<PopularTrainer>, List<PopularTrainerViewModel>>(result);
        }
    }
}
