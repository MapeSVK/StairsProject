using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Infrastructure.Data
{
	public class DBInitializer
	{
		public static class DbInitializer
    {
        // This method will create and seed the database.
        public static void Initialize(StairsAppContext context)
        {
            // Create the database, if it does not already exists. If the database
            // already exists, no action is taken (and no effort is made to ensure it
            // is compatible with the model for this context).
            context.Database.EnsureCreated();

            // Look for any TodoItems
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            List<Product> items = new List<Product>
            {
                new Product() { Name = "stairs1", Desc = "desc1", Price = 7, Id = 1, Type = 'O', ImageLink = "image"},
                new Product() { Name = "stairs2", Desc = "desc2", Price = 8, Id = 2, Type = 'O', ImageLink = "image" }
            };

            // Create two users with hashed and salted passwords
            string password = "1234";
            byte[] passwordHashJoe, passwordSaltJoe, passwordHashAnn, passwordSaltAnn;
            CreatePasswordHash(password, out passwordHashJoe, out passwordSaltJoe);
            CreatePasswordHash(password, out passwordHashAnn, out passwordSaltAnn);

            List<User> users = new List<User>
            {
                new User {
                    Username = "UserJoe",
                    PasswordHash = passwordHashJoe,
                    PasswordSalt = passwordSaltJoe,
                    IsAdmin = false
                },
                new User {
                    Username = "AdminAnn",
                    PasswordHash = passwordHashAnn,
                    PasswordSalt = passwordSaltAnn,
                    IsAdmin = true
                }
            };

            context.Products.AddRange(items);
            context.Users.AddRange(users);
            context.SaveChanges();

        }

        // This method computes a hashed and salted password using the HMACSHA512 algorithm.
        // The HMACSHA512 class computes a Hash-based Message Authentication Code (HMAC) using 
        // the SHA512 hash function. When instantiated with the parameterless constructor (as
        // here) a randomly Key is generated. This key is used as a password salt.

        // The computation is performed as shown below:
        //   passwordHash = SHA512(password + Key)

        // A password salt randomizes the password hash so that two identical passwords will
        // have significantly different hash values. This protects against sophisticated attempts
        // to guess passwords, such as a rainbow table attack.
        // The password hash is 512 bits (=64 bytes) long.
        // The password salt is 1024 bits (=128 bytes) long.
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
	}
}
