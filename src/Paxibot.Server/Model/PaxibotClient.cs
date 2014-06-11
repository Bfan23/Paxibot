using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxibot.Server.Model
{
    public class PaxibotClient
    {
        public Socket Socket { get; set; }
        public string Id { get; set; }
    }
}
