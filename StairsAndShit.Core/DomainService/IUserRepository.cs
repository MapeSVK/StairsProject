using System.Collections.Generic;

namespace StairsAndShit.Core.DomainService
{
	public interface IUserRepository<T>
	{
		IEnumerable<T> GetAll();
		T Get(long id);
		void Add(T entity);
		void Edit(T entity);
		void Remove(long id);	
	}
}
