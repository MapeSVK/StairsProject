using StairsAndShit.Core.Entity;

namespace StairsAndShit.Infrastructure.Data
{
	public class DBSeed
	{
		public static void SeedDB(StairsAppContext ctx)
		{
			ctx.Database.EnsureDeleted();
			ctx.Database.EnsureCreated();

			string password = "1112";
			byte[] passwordHashJoe, passwordSaltJoe;
			CreatePasswordHash(password, out passwordHashJoe, out passwordSaltJoe);


			User userMichal = new User
			{
				Username = "michal",
				PasswordHash = passwordHashJoe,
				PasswordSalt = passwordSaltJoe,
				IsAdmin = true
			};

			ctx.Users.Add(userMichal);
			ctx.SaveChanges();
		}

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
