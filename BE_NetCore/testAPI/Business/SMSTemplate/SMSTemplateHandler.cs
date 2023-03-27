using AutoMapper;
using testAPI.Datatables;
using testAPI.Extention;
using testAPI.Repositories;
using testAPI.User;

namespace testAPI.Business.SMSTemplate
{
    public class SMSTemplateHandler : ISMSTemplateHandler
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SMSTemplateHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseData Create(SMSTemplateModel model)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_httpContextAccessor);
                var smsTemplate = unitOfWork.Repository<SysSMSTemplate>().FirstOrDefault(x => x.SmsTemplateName == model.SmsTemplateName);
                if (smsTemplate != null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Entity_Already);
                }
                var newSMSTemplate = _mapper.Map<SysSMSTemplate>(model);
                newSMSTemplate.Id = Guid.NewGuid();
                unitOfWork.Repository<SysSMSTemplate>().Insert(newSMSTemplate);
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
                var smsTemplate = unitOfWork.Repository<SysSMSTemplate>().FirstOrDefault(x => x.Id == id);
                if (smsTemplate == null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Not_Found);
                }
                unitOfWork.Repository<SysSMSTemplate>().Delete(smsTemplate);
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
                var smsTemplates = unitOfWork.Repository<SysSMSTemplate>().Get(x => ids.Contains(x.Id.ToString()));
                foreach (var item in smsTemplates)
                {
                    unitOfWork.Repository<SysSMSTemplate>().Delete(item);
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
                var smsTemplate = unitOfWork.Repository<SysSMSTemplate>().Get();
                var result = new List<SMSTemplateModel>();
                foreach (var item in smsTemplate)
                {
                    var tempSms = _mapper.Map<SMSTemplateModel>(item);
                    result.Add(tempSms);
                }
                return new ResponseDataObject<List<SMSTemplateModel>>(result, Code.Success, Constant.Success);
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
                var existData = unitOfWork.Repository<SysSMSTemplate>().GetById(id);
                if (existData == null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Not_Found);
                }
                var result = _mapper.Map<SMSTemplateModel>(existData);
                return new ResponseDataObject<SMSTemplateModel>(result, Code.Success, Constant.Success);
            }
            catch (Exception exception)
            {
                return new ResponseDataError(Code.ServerError, exception.Message);
            }
        }

        public ResponseData Update(Guid id, SMSTemplateModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.IdNotMatch);
                }
                using var unitOfWork = new UnitOfWork(_httpContextAccessor);
                var entity = unitOfWork.Repository<SysSMSTemplate>().FirstOrDefault(p => p.Id == id);
                if (entity == null)
                {
                    return new ResponseDataError(Code.NotFound, Constant.Not_Found);
                }

                _mapper.Map(model, entity);

                entity.LastModifiedOnDate = DateTime.Now;
                unitOfWork.Repository<SysSMSTemplate>().Update(entity);
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
