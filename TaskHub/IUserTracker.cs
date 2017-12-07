using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TaskHub
{
    public interface IUserTracker<out THub>
    {
        IEnumerable<UserDetails> UsersOnline();
        void AddUser(HubConnectionContext connection, UserDetails userDetails);
        void RemoveUser(HubConnectionContext connection);
    }
}
