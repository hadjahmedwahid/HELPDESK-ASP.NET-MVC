using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace HelpDesk.Hubs
{
    public class HelpDeskHub : Hub
    {
        public static void RealTime()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<HelpDeskHub>();
            context.Clients.All.realTime();
        }
    }
}