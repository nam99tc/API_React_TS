using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using testAPI.Business.School;
using testAPI.Extention;

namespace testAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class SchoolController : Controller
    {
        private readonly ISchoolHandler _handler;
        public SchoolController(ISchoolHandler handler)
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
        public ResponseData Update(Guid id, [FromBody] SchoolModel model)
        {
            return _handler.Update(id, model);
        }

        [HttpPost]
        public ResponseData Create([FromBody] SchoolModel model)
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
