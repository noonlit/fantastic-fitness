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
using Microsoft.AspNetCore.Hosting;
using Ffitness.ViewModels;
using System.IO;

namespace Ffitness.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper _mapper;
        public TrainersController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }


        // POST: 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trainer>> PostTrainer(TrainerViewModel trainer)
        {
            if (ModelState.IsValid)
            {
                //string uniqueFileName = UploadedFile(model);
                _context.Trainers.Add(_mapper.Map<Trainer>(trainer));
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTrainer", new { id = trainer.Id }, trainer);
            }
            return BadRequest(ModelState);
        }

     /*   [HttpPost]
        public async Task<ActionResult> AllocateActivities(NewFavouritesForUserViewModel newFavouriteRequest)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            Favourites favouritesForYear = _context.Favourites.Where(f => f.Year == newFavouriteRequest.Year && f.User.Id == user.Id).FirstOrDefault();

            if (favouritesForYear != null)
            {
                return BadRequest($"You already have a favourites list for year {newFavouriteRequest.Year}.");
            }

            List<Movie> movies = new List<Movie>();
            newFavouriteRequest.MovieIds.ForEach(mid =>
            {
                var movie = _context.Movies.Find(mid);
                if (movie != null)
                {
                    movies.Add(movie);
                }
            });

            if (movies.Count == 0)
            {
                return BadRequest("The movies you provided do not exist.");
            }

            var favourites = new Favourites
            {
                User = user,
                Movies = movies,
                Year = newFavouriteRequest.Year
            };

            _context.Favourites.Add(favourites);
            await _context.SaveChangesAsync();
            return Ok();
        }
*/
        /* private string UploadedFile(TrainerViewModel model)
         {
             string uniqueFileName = null;

             if (model.ProfileImage != null)
             {
                 string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                 uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                 string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                 using (var fileStream = new FileStream(filePath, FileMode.Create))
                 {
                     model.ProfileImage.CopyTo(fileStream);
                 }
             }
             return uniqueFileName;
         }*/

        // GET: api/Trainers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerViewModel>>> GetTrainers()
        {
           /* return await _context.Trainers
                .Select(t => _mapper.Map<TrainerViewModel>(t))
                .ToListAsync();*/

            return await _context.Trainers
                .Include(t => t.Activities)
                .OrderBy(t => t.LastName)
                .Select(t => _mapper.Map<TrainerViewModel>(t))
                .ToListAsync();
        }

        // GET: api/Trainers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerViewModel>> GetTrainer(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);

            if (trainer == null)
            {
                return NotFound();
            }

            return _mapper.Map<TrainerViewModel>(trainer) ;
        }

        // PUT: api/Trainers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainer(int id, Trainer trainer)
        {
            if (id != trainer.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainerExists(id))
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

        // DELETE: api/Trainers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }

            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainerExists(int id)
        {
            return _context.Trainers.Any(e => e.Id == id);
        }
    }
}
