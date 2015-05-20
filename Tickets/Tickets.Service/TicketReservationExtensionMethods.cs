using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DataContract;
using Tickets.Model;

namespace Tickets.Service
{
    public static class TicketReservationExtensionMethods
    {
        public static ReserveTicketResponse ConvertToReserveTicketResponse(this TicketReservation ticketReservation)
        {
            var response = new ReserveTicketResponse
            {
                EventId = ticketReservation.Event.Id.ToString(),
                EventName = ticketReservation.Event.Name,
                NoOfTickets = ticketReservation.TicketQuantity,
                ExpirationDate = ticketReservation.ExpiryTime,
                ReservationNumber = ticketReservation.Id.ToString()
            };

            return response;
        }
    }
}
