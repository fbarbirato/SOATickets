using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Model
{
    public class TicketReservationFactory
    {
        public static TicketReservation CreateReservation(Event Event, int ticketQuantity)
        {
            var reservation = new TicketReservation 
            {
                Id = Guid.NewGuid(),
                Event = Event,
                ExpiryTime = DateTime.Now.AddMinutes(1),
                TicketQuantity = ticketQuantity
            };

            return reservation;
        }
    }
}
