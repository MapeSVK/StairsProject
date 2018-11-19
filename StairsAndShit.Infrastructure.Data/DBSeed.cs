using StairsAndShit.Core.Entity;

namespace StairsAndShit.Infrastructure.Data
{
	public class DBSeed
	{
		public static void SeedDB(StairsAppContext ctx)
		{
			string password = "1112";
			byte[] passwordHash, passwordSalt;
			CreatePasswordHash(password, out passwordHash, out passwordSalt);


			User userMichal = new User
			{
				Username = "michal",
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt,
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
