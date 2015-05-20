using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DataContract;
using Tickets.Model;

namespace Tickets.Service
{
    public static class TicketPurchaseExtensionMethods
    {
        public static PurchaseTicketResponse ConvertToPurchaseTicketResponse(this TicketPurchase ticketPurchase)
        {
            var response = new PurchaseTicketResponse
            {
                TicketId = ticketPurchase.Id.ToString(),
                EventName = ticketPurchase.Event.Name,
                EventId = ticketPurchase.Event.Id.ToString(),
                NoOfTickets = ticketPurchase.TicketQuantity
            };

            return response;
        }
    }
}
