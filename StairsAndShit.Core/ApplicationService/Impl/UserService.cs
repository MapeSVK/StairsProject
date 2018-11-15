using System;
using System.Collections.Generic;
using System.IO;
using StairsAndShit.Core.DomainService;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Core.ApplicationService.Impl
{
	public class UserService : IUserService
	{
		
		
		
		
		
		
		/*/* DEPENDENCY INJECTION #1#
		readonly IUserRepository<User> _userRepository;

		public UserService(IUserRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}
		
		/* password hashing and salting #1#
		private String password;
		private byte[] passwordHash, passwordSalt;
		
		
		/* CREATE NEW USER #1#
		public User CreateUser(User newUser)
		{
			if (newUser.Username == null)
			{
				throw new InvalidDataException("You need to choose username");
			}
			

			var createUser = _userRepository.Add(newUser);
		    
			return newUser;
		}
		
		
		/* HASHING AND SALTING PASSWORD #1#
		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}*/
		
	}
}
