using AutoMapper;
using testAPI.Datatables;
using testAPI.User;

namespace testAPI.Extention
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SysDemoUser, UserModel>().ReverseMap();
        }
    }
}
