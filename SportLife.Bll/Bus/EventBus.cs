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

        public EventBus(IEventDao iEventDao)
        {
            _iEventDao = iEventDao;
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
    }
}
