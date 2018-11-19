using System.Collections.Generic;
using System.Linq;
using StairsAndShit.Core.DomainService;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Core.ApplicationService.Impl
{
	public class UserService : IUserService<User>
	{
		readonly IUserRepository<User> _userRepository;

		public UserService(IUserRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		public void Create(User newModel)
		{
			_userRepository.Add(newModel);
		}

		public List<User> GetAll(Filter filter)
		{
			return _userRepository.GetAll(filter).ToList();
		}

		public void Update(User modelUpdate)
		{
			_userRepository.Edit(modelUpdate);
		}

		public User GetById(int id)
		{
			return _userRepository.Get(id);
		}

		public void Delete(int id)
		{
			_userRepository.Remove(id);
		}
	}
}
