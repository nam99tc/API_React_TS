using testAPI.Extention;

namespace testAPI.Business.Navigation
{
    public interface INavigationHandler
    {
        ResponseData Get();
        ResponseData GetById(Guid id);
        ResponseData Create(NavigationModel model);
        ResponseData Update(Guid id, NavigationModel model);
        ResponseData Delete(Guid id);
        ResponseData DeleteMany(List<string> ids);
    }
}
