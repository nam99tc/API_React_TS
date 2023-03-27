using AutoMapper;
using testAPI.Datatables;
using testAPI.Extention;
using testAPI.Repositories;

namespace testAPI.Business.EmailTemplate
{
    public class EmailTemplatehandler : IEmailTemplatehandler
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmailTemplatehandler(IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseData Create(EmailTemplateModel model)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_httpContextAccessor);
                var emailTemplate = unitOfWork.Repository<SysEmailTemplate>().FirstOrDefault(x => x.EmailTemplateName == model.EmailTemplateName);
                if (emailTemplate != null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Entity_Already);
                }
                var newEmailTemplate = _mapper.Map<SysEmailTemplate>(model);
                newEmailTemplate.Id = Guid.NewGuid();
                unitOfWork.Repository<SysEmailTemplate>().Insert(newEmailTemplate);
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
                var emailTemplate = unitOfWork.Repository<SysEmailTemplate>().FirstOrDefault(x => x.Id == id);
                if (emailTemplate == null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Not_Found);
                }
                unitOfWork.Repository<SysEmailTemplate>().Delete(emailTemplate);
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
                var emailTemplates = unitOfWork.Repository<SysEmailTemplate>().Get(x => ids.Contains(x.Id.ToString()));
                foreach (var item in emailTemplates)
                {
                    unitOfWork.Repository<SysEmailTemplate>().Delete(item);
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
                var emailTemplate = unitOfWork.Repository<SysEmailTemplate>().Get();
                var result = new List<EmailTemplateModel>();
                foreach (var item in emailTemplate)
                {
                    var tempEmail = _mapper.Map<EmailTemplateModel>(item);
                    result.Add(tempEmail);
                }
                return new ResponseDataObject<List<EmailTemplateModel>>(result, Code.Success, Constant.Success);
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
                var existData = unitOfWork.Repository<SysEmailTemplate>().GetById(id);
                if (existData == null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Not_Found);
                }
                var result = _mapper.Map<EmailTemplateModel>(existData);
                return new ResponseDataObject<EmailTemplateModel>(result, Code.Success, Constant.Success);
            }
            catch (Exception exception)
            {
                return new ResponseDataError(Code.ServerError, exception.Message);
            }
        }

        public ResponseData Update(Guid id, EmailTemplateModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.IdNotMatch);
                }
                using var unitOfWork = new UnitOfWork(_httpContextAccessor);
                var entity = unitOfWork.Repository<SysEmailTemplate>().FirstOrDefault(p => p.Id == id && p.EmailTemplateName == model.EmailTemplateName);
                if (entity == null)
                {
                    return new ResponseDataError(Code.NotFound, Constant.Not_Found);
                }

                _mapper.Map(model, entity);

                entity.LastModifiedOnDate = DateTime.Now;
                unitOfWork.Repository<SysEmailTemplate>().Update(entity);
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
