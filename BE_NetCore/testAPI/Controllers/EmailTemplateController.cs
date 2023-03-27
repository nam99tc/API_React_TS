using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using testAPI.Business.EmailTemplate;
using testAPI.Extention;

namespace testAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class EmailTemplateController : Controller
    {
        private readonly IEmailTemplatehandler _handler;
        public EmailTemplateController(IEmailTemplatehandler handler)
        {
            _handler = handler;
        }
        [HttpGet]
        public ResponseData GetEmailTemplate()
        {
            return _handler.Get();
        }
        [HttpGet]
        [Route("{id}")]
        public ResponseData GetEmailTemplateById(Guid id)
        {
            return _handler.GetById(id);
        }
        [HttpPut]
        [Route("{id}")]
        public ResponseData Update(Guid id, [FromBody] EmailTemplateModel model)
        {
            return _handler.Update(id, model);
        }

        [HttpPost]
        public ResponseData Create([FromBody] EmailTemplateModel model)
        {
            return _handler.Create(model);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public ResponseData Delete(Guid id)
        {
            return _handler.Delete(id);
        }

        [HttpDelete]
        [Route("DeleteMany")]
        public ResponseData DeleteMany([FromBody] List<string> ids)
        {
            return _handler.DeleteMany(ids);
        }
    }
}
