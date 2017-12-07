using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TaskHub
{
    public class TaskMainHub : Hub<IHubClientMembers>
    {
        private readonly IUserTracker<TaskMainHub> _userTracker;

        public TaskMainHub(IUserTracker<TaskMainHub> userTracker) //: base(userTracker)
        {
            _userTracker = userTracker;
        }

        public Task SendMessage(string name, string message)
        {
            return Clients.All.BroadcastMessage(name, message);
        }

        public async Task ReceiveTask(FooTask fooTask)
        {
            await Clients.Group(fooTask.TaskType).BroadcastMessage(fooTask.TaskType, fooTask.TaskValue);
            //await Clients.Client("","")//.Where(). //.All.BroadcastMessage(fooTask.Name, fooTask.MyProperty.ToString());
        }

        public async Task SetSkill(string skill)
        {
            foreach (var user in _userTracker.UsersOnline())
            {
                if (user.ConnectionId == Context.ConnectionId)
                {
                    user.Skill = skill;
                    break;
                }
            }
            await Groups.RemoveAsync(Context.ConnectionId, "SMS");
            await Groups.RemoveAsync(Context.ConnectionId, "Regular");
            if (!string.IsNullOrWhiteSpace(skill))
            {
                await Groups.AddAsync(Context.ConnectionId, skill);
            }
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            _userTracker.AddUser(Context.Connection, new UserDetails(Context.Connection.ConnectionId, Context.Connection.User?.Identity?.Name ?? "RepSupreme"));
            await Clients.All.Counter(_userTracker.UsersOnline().Count());
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            _userTracker.RemoveUser(Context.Connection);
            await Clients.All.Counter(_userTracker.UsersOnline().Count());
        }
    }
}
