using System.ComponentModel.DataAnnotations;
using testAPI.Extention;

namespace testAPI.Business.EmailTemplate
{
    public interface IEmailTemplatehandler
    {
        ResponseData Get();
        ResponseData GetById(Guid id);
        ResponseData Create(EmailTemplateModel model);
        ResponseData Update(Guid id, EmailTemplateModel model);
        ResponseData Delete(Guid id);
        ResponseData DeleteMany(List<string> ids);
    }
}
