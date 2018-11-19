using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StairsAndShit.Core.DomainService;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Infrastructure.Data
{
	public class UserRepository : IUserRepository<User>
	{
		private readonly StairsAppContext _ctx;

		public UserRepository(StairsAppContext ctx)
		{
			_ctx = ctx;
		}

		public IEnumerable<User> GetAll(Filter filter)
		{
			return _ctx.Users.ToList();
		}

		public User Get(int id)
		{
			return _ctx.Users.FirstOrDefault(b => b.Id == id);
		}

		public void Add(User entity)
		{
			_ctx.Users.Add(entity);
			_ctx.SaveChanges();
		}

		public void Edit(User entity)
		{
			_ctx.Entry(entity).State = EntityState.Modified;
			_ctx.SaveChanges();
		}

		public void Remove(int id)
		{
			var item = _ctx.Users.FirstOrDefault(b => b.Id == id);
			_ctx.Users.Remove(item);
			_ctx.SaveChanges();
		}
	}

}
