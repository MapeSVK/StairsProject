using AutoMapper;

namespace StairsAndShit.Core.Entity
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<User, LoginInputModel>();
			CreateMap<LoginInputModel, User>();
		}
	}
}
