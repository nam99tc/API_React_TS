using testAPI.Extention;

namespace testAPI.User
{
    public interface IUserHandler
    {
        ResponseData Get();
        ResponseData GetById(Guid id);
        ResponseData Create(UserModel model);
        ResponseData Update(Guid id, UserModel model);
        ResponseData Delete(Guid id);
        ResponseData DeleteMany(List<string> ids);
    }
}
