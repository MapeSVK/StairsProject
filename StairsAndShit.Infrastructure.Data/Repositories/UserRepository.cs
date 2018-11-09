using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StairsAndShit.Core.DomainService;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Infrastructure.Data
{
	public class UserRepository: IUserRepository<User>
	{
		private readonly StairsAppContext db;

		public UserRepository(StairsAppContext context)
		{
			db = context;
		}

		public IEnumerable<User> GetAll()
		{
			return db.Users.ToList();
		}

		public User Get(long id)
		{
			return db.Users.FirstOrDefault(u => u.Id == id);
		}

		public void Add(User entity)
		{
			db.Users.Add(entity);
			db.SaveChanges();
		}

		public void Edit(User entity)
		{
			db.Entry(entity).State = EntityState.Modified;
			db.SaveChanges();
		}

		public void Remove(long id)
		{
			var item = db.Users.FirstOrDefault(u => u.Id == id);
			db.Users.Remove(item);
			db.SaveChanges();
		}
	}
}
