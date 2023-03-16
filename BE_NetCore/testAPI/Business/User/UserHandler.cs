using AutoMapper;
using testAPI.Datatables;
using testAPI.Extention;
using testAPI.Repositories;

namespace testAPI.User
{
    public class UserHandler : IUserHandler
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData Create(UserModel model)
        {
            try
            {
                using UnitOfWork unitOfWork = new(_httpContextAccessor);
                var user = unitOfWork.Repository<SysDemoUser>().FirstOrDefault(x => x.Username == model.Username);
                if (user != null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Entity_Already);
                }
                model.DateOfBirth = Convert.ToDateTime(model.DateOfBirth.ToShortDateString());

                var userNew = _mapper.Map<SysDemoUser>(model);
                userNew.Id = Guid.NewGuid();
                userNew.CreatedByUserId = userNew.Id;
                unitOfWork.Repository<SysDemoUser>().Insert(userNew);
                unitOfWork.Save();

                return new ResponseData(Code.Success, Constant.Empty);
            }
            catch (Exception exception)
            {
                return new ResponseDataError(Code.ServerError, exception.Message);
            }
        }

        public ResponseData Delete(Guid id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(_httpContextAccessor);
                var user = unitOfWork.Repository<SysDemoUser>().FirstOrDefault(x => x.Id == id);
                if(user == null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Not_Found);
                }
                unitOfWork.Repository<SysDemoUser>().Delete(user);
                unitOfWork.Save();
                return new ResponseData(Code.Success, Constant.Empty);
            }
            catch (Exception exception)
            {
                return new ResponseDataError(Code.ServerError, exception.Message);
            }
        }

        public ResponseData DeleteMany(List<string> ids)
        {
            try
            {
                using UnitOfWork unitOfWork = new(_httpContextAccessor);
                var users = unitOfWork.Repository<SysDemoUser>().Get(x => ids.Contains(x.Id.ToString()));
                foreach (var item in users)
                {
                    unitOfWork.Repository<SysDemoUser>().Delete(item);
                }
                unitOfWork.Save();
                return new ResponseData(Code.Success, Constant.Empty);
            }
            catch (Exception exception)
            {
                return new ResponseDataError(Code.ServerError, exception.Message);
            }
        }

        public ResponseData Get()
        {
            try
            {
                using UnitOfWork unitOfWork = new(_httpContextAccessor);
                var userData = unitOfWork.Repository<SysDemoUser>().Get();
                var result = new List<UserModel>();
                foreach (var item in userData)
                {
                    var tempUser = _mapper.Map<UserModel>(item);
                    result.Add(tempUser);
                }
                return new ResponseDataObject<List<UserModel>>(result, Code.Success, Constant.Empty);
            }
            catch (Exception exception)
            {
                return new ResponseDataError(Code.ServerError, exception.Message);
            }
        }

        public ResponseData GetById(Guid id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(_httpContextAccessor);
                var existData = unitOfWork.Repository<SysDemoUser>().GetById(id);
                if (existData == null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Not_Found);
                }
                var result = _mapper.Map<UserModel>(existData);
                return new ResponseDataObject<UserModel>(result, Code.Success, Constant.Empty);
            }
            catch (Exception exception)
            {
                return new ResponseDataError(Code.ServerError, exception.Message);
            }
        }

        public ResponseData Update(Guid id, UserModel model)
        {
            try
            {
                using UnitOfWork unitOfWork = new(_httpContextAccessor);
                var existUser = unitOfWork.Repository<SysDemoUser>().GetById(id);
                if (existUser == null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Not_Found);
                }
                existUser.LastModifiedOnDate = DateTime.Now;
                existUser.Username = model.Username.Trim();
                existUser.Fullname = model.Fullname.Trim();
                existUser.Phone = model.Phone.Trim();
                existUser.Address = model.Address.Trim();
                existUser.DateOfBirth = model.DateOfBirth;
                existUser.Email = model.Email.Trim();
                existUser.LastModifiedByUserId = model.Id;
                unitOfWork.Repository<SysDemoUser>().Update(existUser);

                unitOfWork.Save();
                return new ResponseData(Code.Success, Constant.Empty);
            }
            catch (Exception exception)
            {
                return new ResponseDataError(Code.ServerError, exception.Message);
            }
        }
    }
}
