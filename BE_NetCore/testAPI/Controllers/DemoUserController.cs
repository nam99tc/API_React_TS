using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using testAPI.Extention;
using testAPI.User;

namespace testAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class DemoUserController : ControllerBase
    {
        private readonly IUserHandler _handler;
        public DemoUserController(IUserHandler handler)
        {
            _handler = handler;
        }
        [HttpGet]
        public ResponseData GetUser()
        {
            return _handler.Get();
        }
        [HttpGet]
        [Route("{id}")]
        public ResponseData GetUserById(Guid id)
        {
            return _handler.GetById(id);
        }
        [HttpPut]
        [Route("{id}")]
        public ResponseData Update(Guid id, [FromBody] UserModel model)
        {
            return _handler.Update(id, model);
        }

        [HttpPost]
        public ResponseData Create([FromBody] UserModel model)
        {
            return _handler.Create(model);
        }

        [HttpDelete]
        [Route("{id}")]
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
