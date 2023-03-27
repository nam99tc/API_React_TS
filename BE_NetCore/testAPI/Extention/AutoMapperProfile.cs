using AutoMapper;
using testAPI.Business.EmailTemplate;
using testAPI.Business.Navigation;
using testAPI.Business.School;
using testAPI.Business.SMSTemplate;
using testAPI.Datatables;
using testAPI.User;

namespace testAPI.Extention
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SysDemoUser, UserModel>().ReverseMap();
            CreateMap<SysSMSTemplate, SMSTemplateModel>().ReverseMap();
            CreateMap<SysEmailTemplate, EmailTemplateModel>().ReverseMap();
            CreateMap<SysNavigation, NavigationModel>().ReverseMap();
            CreateMap<SysSchool, SchoolModel>().ReverseMap();
        }
    }
}
