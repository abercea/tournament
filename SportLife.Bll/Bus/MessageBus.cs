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
        private IDocumentDao _iDocDao;

        public MessageBus(IMessDao d, IDocumentDao iDocDao)
        {
            _iMesDao = d;
            _iDocDao = iDocDao;
        }

        public void save(ChatMessage msg)
        {
            _iMesDao.Add(msg);
            _iMesDao.SaveContext();
        }

        public List<ChatMessage> GetLast()
        {
            return _iMesDao.FindBy(m => m.DateCreated < DateTime.Now).Take(5).ToList();
           // return _iMesDao.GetAll().ToList();
        }

        public void AddNewDocument(Document doc){

            doc.DateUploaded = DateTime.Now;

            _iDocDao.Add(doc);
            _iDocDao.SaveContext();
        }
    }
}
