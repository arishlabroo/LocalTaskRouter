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
    }

    public interface IHubClientMembers
    {
        Task BroadcastMessage(string name, string message);
    }
}
