using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using testAPI.Business.SMSTemplate;
using testAPI.Extention;

namespace testAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class SMSTemplateController : Controller
    {
        private readonly ISMSTemplateHandler _handler;
        public SMSTemplateController(ISMSTemplateHandler handler)
        {
            _handler = handler;
        }
        [HttpGet]
        public ResponseData GetSMSTemplate()
        {
            return _handler.Get();
        }
        [HttpGet]
        [Route("{id}")]
        public ResponseData GetSMSTemplateById(Guid id)
        {
            return _handler.GetById(id);
        }
        [HttpPut]
        [Route("{id}")]
        public ResponseData Update(Guid id, [FromBody] SMSTemplateModel model)
        {
            return _handler.Update(id, model);
        }

        [HttpPost]
        public ResponseData Create([FromBody] SMSTemplateModel model)
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
