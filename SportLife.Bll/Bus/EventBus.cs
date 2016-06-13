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
                    evDb = dbModel;
                    _iEventDao.SaveContext();

                    return true;
                }
                else
                {
                    dbModel.CreatorId = user.UserId;

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
    }
}
