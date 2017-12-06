using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TaskHub
{
    public class TaskHub : Hub<IHubClientMembers>
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.BroadcastMessage(name, message);
            //Clients.Group("")
        }

        public void Receive(FooTask fooTask)
        {
            Clients.All.BroadcastMessage(fooTask.Name, fooTask.MyProperty.ToString());
        }
    }

    public interface IHubClientMembers
    {
        Task BroadcastMessage(string name, string message);
    }

    public class FooTask
    {
        public int MyProperty { get; set; }
        public string Name { get; set; }
    }
}
