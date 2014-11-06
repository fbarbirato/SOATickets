using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Model
{
    public class TicketPurchaseFactory
    {
        public static TicketPurchase CreateTicket(Event Event, int ticketQuantity)
        {
            var ticket = new TicketPurchase
            {
                Id = Guid.NewGuid(),
                Event = Event,
                TicketQuantity = ticketQuantity
            };

            return ticket;
        }
    }
}
