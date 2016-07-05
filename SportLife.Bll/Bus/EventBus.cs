using SportLife.Bll.Contracts;
using SportLife.Bll.Converter;
using SportLife.Dal.Contracts;
using SportLife.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Bll.Bus
{
    public class EventBus : IEventBus
    {
        private readonly IEventDao _iEventDao;
        private readonly IUserDao _iUserDao;

        public EventBus(IEventDao iEventDao, IUserDao iUserDao)
        {
            _iEventDao = iEventDao;
            _iUserDao = iUserDao;
        }

        public bool AdEdit(EventViewModel ev, UserViewModel user)
        {
            try
            {
                var dbModel = EventConverter.FromViewModelToDbModel(ev);

                if (ev.EventId > 0)
                {
                    var evDb = _iEventDao.FindBy(e => e.EventId == ev.EventId).FirstOrDefault();
                    evDb.EventDate = dbModel.EventDate;
                    evDb.EventName = dbModel.EventName;
                    evDb.EventDescription = dbModel.EventDescription;
                    evDb.IsOpened = dbModel.IsOpened;
                    evDb.IsVisible = dbModel.IsVisible;
                    evDb.EventPlace = dbModel.EventPlace;
                    evDb.MaxNumberOfParticipants = dbModel.MaxNumberOfParticipants;
                    evDb.EventFee = ev.EventFee;
                    _iEventDao.SaveContext();

                    return true;
                }
                else
                {
                    dbModel.CreatorId = user.UserId;
                    dbModel.DateCreation = DateTime.Now;
                    if (dbModel != null)
                    {
                        _iEventDao.Add(dbModel);
                        _iEventDao.SaveContext();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public List<EventViewModel> GetAllEvents()
        {
            var lastMonth = DateTime.Now;
            lastMonth = lastMonth.AddDays(-31);
            var events = _iEventDao.FindBy(e => e.DateCreation > lastMonth || !e.DateCreation.HasValue).Take(20).ToList();

            return events.Select(EventConverter.FromDbModelToViewModel).ToList();
        }


        public EventViewModel GetById(int id)
        {
            return EventConverter.FromDbModelToViewModel(_iEventDao.FindBy(e => e.EventId == id).FirstOrDefault());
        }

        public string Delete(int id)
        {
            var evToDelete = _iEventDao.FindBy(e => e.EventId == id).FirstOrDefault();
            if (evToDelete != null)
            {
                _iEventDao.Delete(evToDelete);
                _iEventDao.SaveContext();

                return evToDelete.EventName.ToUpper();
            }

            return string.Empty;
        }


        public EventMainDetalsViewModel GetEventDetails(int eventId)
        {
            var ev = _iEventDao.FindBy(e => e.EventId == eventId).FirstOrDefault();

            var players = ev.RegisteredPlayers.ToList().Select(UserConverter.FromUserDbModelToViewModel).ToList();
            EventMainDetalsViewModel model = new EventMainDetalsViewModel { RegisterdPlayers = players, Event = EventConverter.FromDbModelToViewModel(ev) };

            return model;
        }


        public bool JoinEvent(int eventId, int userId)
        {
            try
            {
                var eventDb = _iEventDao.FindBy(e => e.EventId == eventId).FirstOrDefault();
                var user = _iUserDao.FindBy(u => u.UserId == userId).FirstOrDefault();

                eventDb.RegisteredPlayers.Add(user);
                _iEventDao.SaveContext();

                return true;
            }catch(Exception e){

                return false;
            }
        }
    }
}
