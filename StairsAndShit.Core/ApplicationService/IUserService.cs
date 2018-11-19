using System.Collections.Generic;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Core.ApplicationService
{
	public interface IUserService<T>
	{
		void Create(T newModel);

		List<T> GetAll(Filter filter);

		void Update(T modelUpdate);

		T GetById(int id);

		void Delete(int id);

	}
}
