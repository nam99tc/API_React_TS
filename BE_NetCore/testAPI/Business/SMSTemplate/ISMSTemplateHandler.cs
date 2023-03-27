using testAPI.Extention;

namespace testAPI.Business.SMSTemplate
{
    public interface ISMSTemplateHandler
    {
        ResponseData Get();
        ResponseData GetById(Guid id);
        ResponseData Create(SMSTemplateModel model);
        ResponseData Update(Guid id, SMSTemplateModel model);
        ResponseData Delete(Guid id);
        ResponseData DeleteMany(List<string> ids);
    }
}
