using System.Threading;
using Microsoft.AspNet.SignalR;

namespace Yogam.AMC.Web.Hubs
{
    public class ProgressbarHub : Hub
    {
        public void SendProgress()
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(50);
                Clients.Caller.sendMessage(i + "%");
            }
        }    
    }
}