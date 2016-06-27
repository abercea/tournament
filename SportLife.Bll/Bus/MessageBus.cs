using SportLife.Bll.Contracts;
using SportLife.Dal.Contracts;
using SportLife.Dal.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Bll.Bus
{
    public class MessageBus : IMessageBus
    {
        private IMessDao _iMesDao;

        public MessageBus(IMessDao d)
        {
            _iMesDao = d;
        }

        public void save(ChatMessage msg)
        {
            _iMesDao.Add(msg);
            _iMesDao.SaveContext();
        }

        public List<ChatMessage> GetLast()
        {
            // return _iMesDao.FindBy(m => m.DateCreated < DateTime.Now).ToList();
            return _iMesDao.GetAll().ToList();
        }
    }
}
