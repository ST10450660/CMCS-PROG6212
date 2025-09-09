using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CMCS.Web.Hubs
{
    public class ClaimStatusHub : Hub
    {
        // Called after approve/reject to notify clients
        public async Task NotifyStatusChanged(int claimId, string newStatus)
        {
            await Clients.All.SendAsync("ClaimStatusChanged", claimId, newStatus);
        }
    }
}