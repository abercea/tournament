using SportLife.Bll.Contracts;
using SportLife.Dal.Contracts;
using SportLife.Dal.DomainModels;
using SportLife.Models.Models;
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

        public int AddNewDocument(Document doc)
        {

            doc.DateUploaded = DateTime.Now;

            _iDocDao.Add(doc);
            _iDocDao.SaveContext();

            return doc.DocumentId;
        }

        public ProfileViewModel GetProfiel(int userId)
        {
            var docs = _iDocDao.FindBy(d => d.UserId == userId).ToList();
            var mess = _iMesDao.FindBy(m => m.UserId == userId).ToList();
            var sk = GetSquashSkils();

            return new ProfileViewModel { Documents = docs, Messages = mess, Skils = sk };
        }

        private List<SkilViewModel> GetSquashSkils()
        {
            var r = new Random();

            var ss = new List<SkilViewModel>();
            skils.ForEach(s => ss.Add(new SkilViewModel { Name = s, Value = r.Next(80) + 20 }));

            return ss;
        }

        private List<string> skils = new List<string> { "CROSS", "DROP SHOT", "DRIVE", "BOAST", "VOLLEY", "SERVICE" };
    }
}
