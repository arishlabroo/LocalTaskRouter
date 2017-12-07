using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

namespace TaskHub
{
    public class HubWithPresence<T> : Hub<T> where T : class
    {
        private IUserTracker<HubWithPresence<T>> _userTracker;

        public HubWithPresence(IUserTracker<HubWithPresence<T>> userTracker)
        {
            _userTracker = userTracker;
        }

        public IEnumerable<UserDetails> GetUsersOnline()
        {
            return _userTracker.UsersOnline();
        }
    }
}
