using System;
using Microsoft.AspNetCore.Identity;
using Ffitness.Models;

namespace Ffitness.Data
{
    public class SeedUsers
    {
        private static string Characters = "abcdefghijklmnopqrstuvwxyz123456890";
        private static Random random = new Random();
        public static void Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager, int count)
        {

            context.Database.EnsureCreated();

            for (int i = 0; i < count; ++i)
            {
                var email = generateRandomString(3, 10) + "@" + generateRandomString(2, 3);
                var user = new ApplicationUser
                {
                    FirstName = generateRandomString(2, 50),
                    LastName = generateRandomString(2, 50),
                    Email = email,
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true
                };

                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, "P@ssw0rd1!");
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        private static string generateRandomString(int min, int max)
        {
            string s = "";

            for (int j = 0; j < random.Next(min, max); ++j)
            {
                s += Characters[random.Next(Characters.Length)];
            }

            return s;
        }
    }
}
