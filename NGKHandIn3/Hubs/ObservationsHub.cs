using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace NGKHandIn3.Hubs
{
    public class ObservationsHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}