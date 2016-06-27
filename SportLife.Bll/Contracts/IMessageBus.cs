using SportLife.Dal.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Bll.Contracts
{
    public interface IMessageBus
    {
        void save(ChatMessage msg);
        List<ChatMessage> GetLast();
    }
}
