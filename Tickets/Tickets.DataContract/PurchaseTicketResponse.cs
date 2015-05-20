using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.DataContract
{
    [DataContract]
    public class PurchaseTicketResponse : Response
    {
        [DataMember]
        public string TicketId { get; set; }

        [DataMember]
        public string EventName { get; set; }

        [DataMember]
        public string EventId { get; set; }

        [DataMember]
        public int NoOfTickets { get; set; }
    }
}
