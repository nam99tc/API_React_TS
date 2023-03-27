using System.ComponentModel.DataAnnotations;
using testAPI.Extention;

namespace testAPI.Business.School
{
    public interface ISchoolHandler
    {
        ResponseData Get();
        ResponseData GetById(Guid id);
        ResponseData Create(SchoolModel model);
        ResponseData Update(Guid id, SchoolModel model);
        ResponseData Delete(Guid id);
        ResponseData DeleteMany(List<string> ids);
    }
}
