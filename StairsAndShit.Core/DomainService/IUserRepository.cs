using System.Collections.Generic;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Core.DomainService
{
	public interface IUserRepository<T>
	{
		User Authenticate(string username, string password);
		IEnumerable<User> GetAll();
		User GetById(int id);
		User Create(User user, string password);
		void Update(User user, string password = null);
		void Delete(int id);
	}
}
