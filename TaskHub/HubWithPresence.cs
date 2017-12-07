// using System.Collections.Generic;
// using Microsoft.AspNetCore.SignalR;

// namespace TaskHub
// {
//     public class HubWithPresence<T> : Hub<T> where T : class
//     {
//         private IUserTracker<HubWithPresence<T>> _userTracker;

//         public HubWithPresence(IUserTracker<HubWithPresence<T>> userTracker)
//         {
//             _userTracker = userTracker;
//         }

//         protected void AddUser()
//         {
//             _userTracker.AddUser(Context.Connection, new UserDetails(Context.Connection.ConnectionId, Context.Connection.User?.Identity?.Name ?? "RepSupreme"));
//         }

//         protected void RemoveUser()
//         {
//             _userTracker.RemoveUser(Context.Connection);
//         }

//         protected void UpdateUserSkill()
//         {

//         }

//         public IEnumerable<UserDetails> GetUsersOnline()
//         {
//             return _userTracker.UsersOnline();
//         }
//     }
// }
