using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tickets.Model
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Allocation { get; set; }
        public List<TicketReservation> ReservedTickets { get; set; }
        public List<TicketPurchase> PurchasedTickets { get; set; }

        public Event()
        {
            ReservedTickets = new List<TicketReservation>();
            PurchasedTickets = new List<TicketPurchase>();
        }

        public int AvailableAllocation()
        {
            int salesAndReservations = 0;

            PurchasedTickets.ForEach(t => salesAndReservations += t.TicketQuantity);

            ReservedTickets.FindAll(r => r.StillActive()).ForEach(r => salesAndReservations += r.TicketQuantity);

            return Allocation - salesAndReservations;
        }
    }
}
