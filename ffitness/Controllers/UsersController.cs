using AutoMapper;
using Ffitness.Data;
using Ffitness.Models;
using Ffitness.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(ApplicationDbContext context, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("listRoles")]
        public IQueryable<IdentityRole> ListRoles()
        {
            var roles = _roleManager.Roles;
            return roles;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUserViewModel>>> GetUsers()
        {
            var result = await _context.Users.ToListAsync();

            var mappedResult = new List<ApplicationUserViewModel>();

            foreach (ApplicationUser user in result)
			{
                var roles = await _userManager.GetRolesAsync(user);
                var mappedUser = _mapper.Map<ApplicationUserViewModel>(user);
                mappedUser.Roles = roles.ToList();
                mappedResult.Add(mappedUser);
			}

            return mappedResult;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserViewModel>> GetUser(String id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var mappedUser = _mapper.Map<ApplicationUserViewModel>(user);
            var roles = await _userManager.GetRolesAsync(user);
            mappedUser.Roles = roles.ToList();
            return mappedUser;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(String id, ApplicationUserViewModel user)
        {
            if (!user.Id.Equals(id))
            {
                return BadRequest();
            }

            // only change a few exposed properties
            var userEntity = await _context.Users.FindAsync(id);
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.BirthDate = user.BirthDate;
            userEntity.Email = user.Email;
            userEntity.Gender = (ApplicationUser.GenderType)user.Gender;

            // attempt to change password if necessary
            if (user.PlainPassword != null)
            {
                userEntity.PasswordHash = _userManager.PasswordHasher.HashPassword(userEntity, user.PlainPassword);
            }

            _context.Entry(userEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostUser(ApplicationUserViewModel newUser)
        {
            var user = new ApplicationUser
            {
                Email = newUser.Email,
                UserName = newUser.UserName,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                BirthDate = newUser.BirthDate,
                Gender = (ApplicationUser.GenderType) newUser.Gender,
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true // a hack, but we're not implementing email confirmation
            };

            var result = await _userManager.CreateAsync(user, newUser.PlainPassword);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, newUser.Roles);
                var mappedUser = _mapper.Map<ApplicationUserViewModel>(user);
                mappedUser.Roles = newUser.Roles;

                return CreatedAtAction("GetUser", new { id = user.Id }, mappedUser);
            }

            return BadRequest(result.Errors);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(String id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id.Equals(id));
        }
    }
}
