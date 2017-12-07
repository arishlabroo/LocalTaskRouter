using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TaskHub
{
    public class InMemoryUserTracker<THub> : IUserTracker<THub>
    {
        private readonly ConcurrentDictionary<HubConnectionContext, UserDetails> _usersOnline
            = new ConcurrentDictionary<HubConnectionContext, UserDetails>();

        public IEnumerable<UserDetails> UsersOnline()
            => _usersOnline.Values.AsEnumerable();

        public void AddUser(HubConnectionContext connection, UserDetails userDetails)
        {
            _usersOnline.TryAdd(connection, userDetails);
        }

        public void RemoveUser(HubConnectionContext connection)
        {
            if (_usersOnline.TryRemove(connection, out var userDetails))
            {
            }
        }
    }
}
