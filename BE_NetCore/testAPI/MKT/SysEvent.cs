using AutoMapper;
using System.ComponentModel.DataAnnotations;
using testAPI.Datatables;
using testAPI.Extention;
using testAPI.Repositories;
using testAPI.User;

namespace testAPI.MKT
{
    public class SysEvent
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string EventName { get; set; } = null!;
        [MaxLength(255)]
        public string? EventLogo { get; set; }
        [MaxLength(255)]
        public string? OrganizationLocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RegisterEndDate { get; set; }
        public int TicketsNumber { get; set; } = 0;
        public int TicketNumberChecked { get; set; } = 0;
        public int EventType { get; set; }
    }
    public class SysCustomer
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? CustomerName { get; set; }
        public string? Logo { get; set; }
        [MaxLength(255)]
        public string? Phone { get; set; }
        [MaxLength(255)]
        public string? Email { get; set; }
        public DateTime CheckTicketDate { get; set; }
        public int StatusTicket { get; set; }
    }
    public class SysUser
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? UserName { get; set;}
        [MaxLength(255)]
        public string? Account { get; set;}
        public string? PassWord { get; set;}
        public int Status { get; set; }
        public int Role { get; set; }
    }
    public class EventModel
    {
        public Guid Id { get; set; }
        public string EventName { get; set; } = null!;
        public string? EventLogo { get; set; }
        public string? OrganizationLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RegisterEndDate { get; set; }
        public int TicketsNumber { get; set; } = 0;
        public int TicketNumberChecked { get; set; } = 0;
        public int EventType { get; set; }
    }
    public interface IEventHandler
    {
        ResponseData Get();
        ResponseData GetById(Guid id);
        ResponseData Create(EventModel model);
        ResponseData Update(Guid id, EventModel model);
        ResponseData Delete(Guid id);
        ResponseData DeleteMany(List<string> ids);
    }
    public class EventHandler : IEventHandler
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData Create(EventModel model)
        {
            try
            {
                using UnitOfWork unitOfWork = new(_httpContextAccessor);
                var eventCurent = unitOfWork.Repository<SysEvent>().FirstOrDefault(x => x.EventName == model.EventName);
                if (eventCurent != null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Entity_Already);
                }
                model.StartDate = Convert.ToDateTime(model.StartDate.ToShortDateString());

                var eventNew = _mapper.Map<SysEvent>(model);
                eventNew.Id = Guid.NewGuid();
                unitOfWork.Repository<SysEvent>().Insert(eventNew);
                unitOfWork.Save();

                return new ResponseData(Code.Success, Constant.Empty);
            }
            catch (Exception exception)
            {
                return new ResponseDataError(Code.ServerError, exception.Message);
            }
        }

        public ResponseData Delete(Guid id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(_httpContextAccessor);
                var eventCurent = unitOfWork.Repository<SysEvent>().FirstOrDefault(x => x.Id == id);
                if (eventCurent == null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Not_Found);
                }
                unitOfWork.Repository<SysEvent>().Delete(eventCurent);
                unitOfWork.Save();
                return new ResponseData(Code.Success, Constant.Empty);
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
                var events = unitOfWork.Repository<SysEvent>().Get(x => ids.Contains(x.Id.ToString()));
                foreach (var item in events)
                {
                    unitOfWork.Repository<SysEvent>().Delete(item);
                }
                unitOfWork.Save();
                return new ResponseData(Code.Success, Constant.Empty);
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
                var eventData = unitOfWork.Repository<SysEvent>().Get();
                var result = new List<EventModel>();
                foreach (var item in eventData)
                {
                    var tempEvent = _mapper.Map<EventModel>(item);
                    result.Add(tempEvent);
                }
                return new ResponseDataObject<List<EventModel>>(result, Code.Success, Constant.Empty);
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
                var existData = unitOfWork.Repository<EventModel>().GetById(id);
                if (existData == null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Not_Found);
                }
                var result = _mapper.Map<EventModel>(existData);
                return new ResponseDataObject<EventModel>(result, Code.Success, Constant.Empty);
            }
            catch (Exception exception)
            {
                return new ResponseDataError(Code.ServerError, exception.Message);
            }
        }

        public ResponseData Update(Guid id, EventModel model)
        {
            try
            {
                using UnitOfWork unitOfWork = new(_httpContextAccessor);
                var existEvent = unitOfWork.Repository<EventModel>().GetById(id);
                if (existEvent == null)
                {
                    return new ResponseDataError(Code.BadRequest, Constant.Not_Found);
                }
                existEvent.StartDate = model.StartDate;
                existEvent.EndDate = model.EndDate;
                existEvent.EventName = model.EventName.Trim();
                existEvent.OrganizationLocation = model.OrganizationLocation?.Trim();
                existEvent.TicketsNumber = model.TicketsNumber;
                existEvent.TicketNumberChecked = model.TicketNumberChecked;
                existEvent.EventLogo = model.EventLogo?.Trim();
                existEvent.RegisterEndDate = model.RegisterEndDate;
                existEvent.EventType = model.EventType;
                unitOfWork.Repository<EventModel>().Update(existEvent);

                unitOfWork.Save();
                return new ResponseData(Code.Success, Constant.Empty);
            }
            catch (Exception exception)
            {
                return new ResponseDataError(Code.ServerError, exception.Message);
            }
        }
    }
}
