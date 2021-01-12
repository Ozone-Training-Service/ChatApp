using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class MessageHub : Hub
    {
        public  async  void NewMessage(Message msg)
        {
             await Clients.All.SendAsync("MessageReceived", msg);
            

        }

        public override Task OnConnectedAsync()
        {
         
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            MessageHub hub = new MessageHub();
            List<Message> messages = new List<Message>();
            messages.Add(new Message() { clientuniqueid = Guid.NewGuid().ToString(), date = DateTime.Now, message = "First", type = "sent" });

            var comments = messages;  // some sort of cache would be good here
            foreach (var comment in comments)
            {
                 Clients.Client(Context.ConnectionId).SendAsync("MessageRece, comment.message);
                Clients.All.SendAsync("MessageReceived", comment.message);
                // hub.NewMessage(comment);

            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}

public class Db{
    void Insert() { }
    List<Message> GetAll() { return null; }
}