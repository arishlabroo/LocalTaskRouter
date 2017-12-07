using System.Threading.Tasks;

namespace TaskHub
{
    public interface IHubClientMembers
    {
        Task BroadcastMessage(string name, string message);
        Task Counter(int count);
    }
}
