using AutoMapper;
using testAPI.Datatables;
using testAPI.Extention;
using testAPI.Repositories;

namespace testAPI.Business.School
{
    public class SchoolHandler : ISchoolHandler
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SchoolHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseData Create(SchoolModel model)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_httpContextAccessor);
                var school = unitOfWork.Repository<SysSchool>().FirstOrDefault(x => x.SchoolName == model.SchoolName || x.Code == model.Code);
                if (school != null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Entity_Already);
                }
                var newSchool = _mapper.Map<SysSchool>(model);
                newSchool.Id = Guid.NewGuid();
                unitOfWork.Repository<SysSchool>().Insert(newSchool);
                unitOfWork.Save();

                return new ResponseData(Code.Success, Constant.Success);
            }
            catch (Exception ex)
            {
                return new ResponseDataError(Code.BadRequest, ex.Message);
            }
        }

        public ResponseData Delete(Guid id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(_httpContextAccessor);
                var school = unitOfWork.Repository<SysSchool>().FirstOrDefault(x => x.Id == id);
                if (school == null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Not_Found);
                }
                unitOfWork.Repository<SysSchool>().Delete(school);
                unitOfWork.Save();
                return new ResponseData(Code.Success, Constant.Success);
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
                var schools = unitOfWork.Repository<SysSchool>().Get(x => ids.Contains(x.Id.ToString()));
                foreach (var item in schools)
                {
                    unitOfWork.Repository<SysSchool>().Delete(item);
                }
                unitOfWork.Save();
                return new ResponseData(Code.Success, Constant.Success);
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
                var schools = unitOfWork.Repository<SysSchool>().Get();
                var result = new List<SchoolModel>();
                foreach (var item in schools)
                {
                    var school = _mapper.Map<SchoolModel>(item);
                    result.Add(school);
                }
                return new ResponseDataObject<List<SchoolModel>>(result, Code.Success, Constant.Success);
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
                var existData = unitOfWork.Repository<SysSchool>().GetById(id);
                if (existData == null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Not_Found);
                }
                var result = _mapper.Map<SchoolModel>(existData);
                return new ResponseDataObject<SchoolModel>(result, Code.Success, Constant.Success);
            }
            catch (Exception exception)
            {
                return new ResponseDataError(Code.ServerError, exception.Message);
            }
        }

        public ResponseData Update(Guid id, SchoolModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.IdNotMatch);
                }
                using var unitOfWork = new UnitOfWork(_httpContextAccessor);
                var entity = unitOfWork.Repository<SysSchool>().FirstOrDefault(p => p.Id == id);
                if (entity == null)
                {
                    return new ResponseDataError(Code.NotFound, Constant.Not_Found);
                }
                if (unitOfWork.Repository<SysSchool>().FirstOrDefault(p => p.SchoolName == model.SchoolName || p.Code == model.Code) != null)
                {
                    return new ResponseDataError(Code.NotFound, "School already");
                }

                _mapper.Map(model, entity);

                entity.LastModifiedOnDate = DateTime.Now;
                unitOfWork.Repository<SysSchool>().Update(entity);
                unitOfWork.Save();
                return new ResponseData(Code.Success, Constant.Success);
            }
            catch (Exception exception)
            {
                return new ResponseDataError(Code.ServerError, exception.Message);
            }
        }
    }
}
