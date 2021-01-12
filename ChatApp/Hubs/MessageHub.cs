using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class MessageHub : Hub
    {
        List<Message> messages = new List<Message>();


        public MessageHub()
        {
            // from db
        }  
        public async void NewMessage(Message msg, IHubCallerClients Clients2)
        {
            if (Clients == null)// Intial call
            {
                await Clients2.All.SendAsync("MessageReceived", msg);
            }
            else
            {
                await Clients.All.SendAsync("MessageReceived", msg);
               // messages.Add(msg);
            }



        }

        public override Task OnConnectedAsync()
        {
            MessageHub hub = new MessageHub();
            //get DB MSG

            messages.Add(new Message() { clientuniqueid = Guid.NewGuid().ToString(), date = DateTime.Now, message = "FirstMSG", type = "sent" });
            messages.Add(new Message() { clientuniqueid = Guid.NewGuid().ToString(), date = DateTime.Now, message = "FirstMSG2", type = "sent" });
        
        var comments = messages;  // some sort of cache would be good here
            foreach (var comment in comments)
            {
                var id = Context.ConnectionId;
                var x = Clients.Client(id);
                // Clients.Client(id).SendAsync(comment.clientuniqueid, comment.message);

                //Clients.All.SendAsync("MessageReceived", comment.message);



                hub.NewMessage(comment, Clients);

            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // insert msg to db
            messages.Clear();
            return base.OnDisconnectedAsync(exception);
        }
    }
}

public class Db
{
    void Insert() { }
    List<Message> GetAll() { return null; }
}