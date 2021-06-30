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
        public async Task<ActionResult<Trainer>> PostTrainer(TrainerWithActivitiesViewModel trainer)
        {
            if (ModelState.IsValid)
            {
                var trainerEntity = _mapper.Map<Trainer>(new Trainer());
                trainerEntity.Id = trainer.Id;
                trainerEntity.FirstName = trainer.FirstName;
                trainerEntity.LastName = trainer.LastName;
                trainerEntity.Description = trainer.Description;

                if (trainer.Activities.Count != 0)
                {
                    List<Activity> activities = new List<Activity>();
                    trainer.Activities.ForEach(activityId =>
                    {
                        var activity = _context.Activities.Find(activityId);
                        if (activity != null)
                        {
                            activities.Add(activity);
                        }
                    });

                    if (activities.Count == 0)
                    {
                        return BadRequest("The activities you provided are not available.");
                    }
                    trainerEntity.Activities = activities;
                }

                //string uniqueFileName = UploadedFile(model);
                 _context.Trainers.Add(trainerEntity);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTrainer", new { id = trainer.Id }, trainer);
            }
            return BadRequest(ModelState);
        }

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
            return await _context.Trainers
                .OrderBy(t => t.LastName)
                .Include(t => t.Activities)
                .Select(t => _mapper.Map<TrainerViewModel>(t))
                .ToListAsync();
        }

        // GET: api/Trainers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerViewModel>> GetTrainer(int id)
        {
            var trainer = await _context.Trainers.Include(t => t.Activities).FirstOrDefaultAsync(t => t.Id == id);

            if (trainer == null)
            {
                return NotFound();
            }

            return _mapper.Map<TrainerViewModel>(trainer) ;
        }

        // PUT: api/Trainers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainer(TrainerWithActivitiesViewModel trainerFromUi)
        {
            var trainerToUpdate = _context.Trainers
                .Include(t => t.Activities)
                .FirstOrDefault(t => t.Id == trainerFromUi.Id);

            if (trainerToUpdate == null) 
            { 
                return NotFound(); 
            }

            if (trainerFromUi.Activities.Count != 0)
            {
                var activitiesToRemove = trainerToUpdate.Activities.ToList();
                activitiesToRemove.ForEach(activity =>
                {
                    if (!trainerFromUi.Activities.Contains(activity.Id))
                    {
                        trainerToUpdate.Activities.Remove(activity);
                    }
                });
                trainerFromUi.Activities.ForEach(activityId =>
                {
                    var activityToAdd = _context.Activities.Find(activityId);
                    if (activityToAdd != null && !trainerToUpdate.Activities.Exists(a => a.Id == activityToAdd.Id))
                    {
                        trainerToUpdate.Activities.Add(activityToAdd);
                    }
                });
            } 
            else
            {
                trainerToUpdate.Activities.Clear();
            }
            var newActivities = trainerToUpdate.Activities;

            trainerToUpdate.Id = trainerFromUi.Id;
            trainerToUpdate.FirstName = trainerFromUi.FirstName;
            trainerToUpdate.LastName = trainerFromUi.LastName;
            trainerToUpdate.Description = trainerFromUi.Description;
            trainerToUpdate.Activities = newActivities;

            _context.Entry(trainerToUpdate).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                 throw;
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
/*
                var activitiesToRemove = trainerToUpdate.Activities.ToList();
                foreach (var activity in activitiesToRemove)
                {
                    if (!trainerFromUi.Activities.Contains(activity.Id))
                    {
                        trainerToUpdate.Activities.Remove(activity);
                        //_context.Entry(activity).State = EntityState.Modified;
                    }
                }*/

/*trainerToUpdate.Activities.ForEach(activity =>
{
    var activityToRemove = _context.Activities.Find(activity.Id);
    if (!trainerFromUi.Activities.Contains(activityToRemove.Id))
    {
        trainerToUpdate.Activities.Remove(activityToRemove);
    }
});*/