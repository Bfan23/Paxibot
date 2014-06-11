using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxibot.Server.Messages
{
    public class RegisterationRequestMessage :BaseMessage
    {
        public object GameDescription { get; set; }
    }
}
