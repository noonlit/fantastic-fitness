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
        public async Task<ActionResult<IEnumerable<BookingStatsViewModel>>> GetStats(string startDate, string endDate)
        {
            var startDateDt = DateTime.Parse(startDate);
            var endDateDt = DateTime.Parse(endDate);
            // TODO

            var result = await _context.BookingStats
                .FromSqlRaw("SELECT COUNT(*) AS BookingsCount FROM Bookings")
                .ToListAsync();
            return _mapper.Map<List<BookingStats>, List<BookingStatsViewModel>>(result);
        }

    }
}
