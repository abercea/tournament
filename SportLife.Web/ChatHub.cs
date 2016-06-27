using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using SportLife.Bll.Contracts;
namespace SignalRChat
{
    public class ChatHub : Hub
    {
       

        public void Send(string name, string message, int userId)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }
}