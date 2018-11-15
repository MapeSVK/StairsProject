using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StairsAndShit.Core.ApplicationService.Impl;
using StairsAndShit.Core.DomainService;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.RestApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private IUserRepository<User> _userRepository;
		private IMapper _mapper;
		private readonly AppSettings _appSettings;

		public UsersController(
			IUserRepository<User> userRepository,
			IMapper mapper,
			IOptions<AppSettings> appSettings)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_appSettings = appSettings.Value;
		}

		[AllowAnonymous]
		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromBody]LoginInputModel loginInputModel)
		{
			var user = _userRepository.Authenticate(loginInputModel.Username, loginInputModel.Password);

			if (user == null)
				return BadRequest(new { message = "Username or password is incorrect" });

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[] 
				{
					new Claim(ClaimTypes.Name, user.Id.ToString())
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			// return basic user info (without password) and token to store client side
			return Ok(new {
				Id = user.Id,
				Username = user.Username,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Token = tokenString
			});
		}

		[AllowAnonymous]
		[HttpPost("register")]
		public IActionResult Register([FromBody]LoginInputModel loginInputModel)
		{
			// map dto to entity
			var user = _mapper.Map<User>(loginInputModel);

			try 
			{
				// save 
				_userRepository.Create(user, loginInputModel.Password);
				return Ok();
			} 
			catch(Exception ex)
			{
				// return error message if there was an exception
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var users =  _userRepository.GetAll();
			var userLogins = _mapper.Map<IList<LoginInputModel>>(users);
			return Ok(userLogins);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var user =  _userRepository.GetById(id);
			var userLogins = _mapper.Map<LoginInputModel>(user);
			return Ok(userLogins);
		}
		
		[HttpPut("{id}")]
		public IActionResult Update(int id, [FromBody]LoginInputModel loginInputModel)
		{
			// map dto to entity and set id
			var user = _mapper.Map<User>(loginInputModel);
			user.Id = id;

			try 
			{
				// save 
				_userRepository.Update(user, loginInputModel.Password);
				return Ok();
			} 
			catch(Exception ex)
			{
				// return error message if there was an exception
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_userRepository.Delete(id);
			return Ok();
		}
	}
}
