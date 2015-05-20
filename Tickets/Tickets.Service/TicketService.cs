using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Contracts;
using Tickets.DataContract;
using Tickets.Model;
using Tickets.Repository;

namespace Tickets.Service
{
    public class TicketService : ITicketService
    {
        private IEventRepository _eventRepository;
        private static MessageResponseHistory<PurchaseTicketResponse> _reservationResponse = 
            new MessageResponseHistory<PurchaseTicketResponse>();

        public TicketService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public TicketService() : this (new EventRepository())
        {

        }
        
        public ReserveTicketResponse ReserveTicket(ReserveTicketRequest reserveTicketRequest)
        {
            throw new NotImplementedException();
        }

        public PurchaseTicketResponse PurchaseTicket(PurchaseTicketRequest purchaseTicketRequest)
        {
            throw new NotImplementedException();
        }
    }
}
