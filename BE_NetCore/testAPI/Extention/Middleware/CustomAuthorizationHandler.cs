using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using testAPI.MKT;
using testAPI.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace testAPI.Extention.Middleware
{
    public class CustomAuthorizationHandler : IAuthorizationHandler
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthorizationHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            if (context!.User!.Identity!.IsAuthenticated)
            {
                using UnitOfWork unitOfWork = new(_httpContextAccessor);
                var existUser = unitOfWork.Repository<SysUser>().FirstOrDefault(x => x.UserName == context.User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (existUser != null)
                {

                }
                return;
            }
            return;
        }
    }
}
