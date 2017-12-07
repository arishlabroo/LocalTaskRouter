using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TaskHub
{
    public class TaskMainHub : HubWithPresence<IHubClientMembers>
    {
        private readonly IUserTracker<TaskMainHub> _userTracker;

        public TaskMainHub(IUserTracker<TaskMainHub> userTracker) : base(userTracker)
        {
            _userTracker = userTracker;
        }

        public Task Send(string name, string message)
        {
            var x = GetUsersOnline().Count();
            return Clients.All.BroadcastMessage(name, message + x.ToString());
        }

        public async Task Receive(FooTask fooTask)
        {
            //return Clients.All.InvokeAsync("BroadcastMessage", new object[] { fooTask.Name, fooTask.MyProperty });
            await Clients.All.BroadcastMessage(fooTask.Name, fooTask.MyProperty.ToString());
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            _userTracker.AddUser(Context.Connection, new UserDetails(Context.Connection.ConnectionId, Context.Connection.User?.Identity?.Name ?? "RepSupreme"));
            await Clients.All.Counter(GetUsersOnline().Count());
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            _userTracker.RemoveUser(Context.Connection);
        }
    }
}
