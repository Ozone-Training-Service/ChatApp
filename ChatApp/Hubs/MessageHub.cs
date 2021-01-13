using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                Healper ob = new Healper();
                ob.insert(msg);
            }



        }

        public override Task OnConnectedAsync()
        {
            Healper ob = new Healper();
            MessageHub hub = new MessageHub();
            //get DB MSG

            messages = ob.GetAll();

           // messages.Add(new Message() { clientuniqueid = Guid.NewGuid().ToString(), date = DateTime.Now, message = "FirstMSG", type = "sent" });
           // messages.Add(new Message() { clientuniqueid = Guid.NewGuid().ToString(), date = DateTime.Now, message = "FirstMSG2", type = "sent" });
        
        var comments =messages;  // some sort of cache would be good here
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


public class Healper{
public void insert(Message message)
    {
        try
        {
         
            var Query = "insert into Message2 values('"+Guid.NewGuid().ToString()+"','" + message.clientuniqueid + "','" + message.type + "','" + message.message + "','" + message.date.ToString() + "')";
            SqlConnection con = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=ChatApp;Integrated Security=True");
            con.Open();
            SqlCommand cm = new SqlCommand(Query, con);
            cm.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public List<Message> GetAll()
    {
        List<Message> messages = new List<Message>();

        var Query = "select * from  Message2 ";
        SqlConnection con = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=ChatApp;Integrated Security=True");
        con.Open();
        SqlCommand cm = new SqlCommand(Query, con);
        SqlDataReader reader = cm.ExecuteReader();
        while (reader.Read())
        {
            messages.Add(new Message() { 
            Id = reader.GetString(0),
            clientuniqueid=reader.GetString(1),
           type=reader.GetString(2),
           message=reader.GetString(3),
           date=reader.GetString(4)

        });
           
        }
        con.Close();
        return messages;
    }
}