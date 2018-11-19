using System.Collections.Generic;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Core.DomainService
{
	public interface IUserRepository<T>
	{
		IEnumerable<T> GetAll(Filter filter);
        
		T Get(int id);
        
		void Add(T newModel);
        
		void Edit(T modelUpdate);
        
		void Remove(int id);
	}
}
