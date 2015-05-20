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
        private static MessageResponseHistory<PurchaseTicketResponse> _reservationResponseHistory = 
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
            //Validate Request

            var response = new ReserveTicketResponse();

            try
            {
                var Event = _eventRepository.FindBy(new Guid(reserveTicketRequest.EventId));

                TicketReservation reservation;

                if (Event.CanReserveTicket(reserveTicketRequest.TicketQuantity))
                {
                    reservation = Event.ReserveTicket(reserveTicketRequest.TicketQuantity);

                    _eventRepository.Save(Event);

                    response = reservation.ConvertToReserveTicketResponse();
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = string.Format("There are {0} ticket(s) available.",
                        Event.AvailableAllocation());
                }

            }
            catch (Exception ex)
            {
                response.Message = ErrorLog.GenerateErrorRefMessageAndLog(ex);
                response.Success = false;
            }
            
            return response;
        }

        public PurchaseTicketResponse PurchaseTicket(PurchaseTicketRequest purchaseTicketRequest)
        {
            //Validate Request

            PurchaseTicketResponse response = new PurchaseTicketResponse();

            try
            {
                // Check for a duplicate transaction using the Idempotent Pattern,
                // the Domain Logic could cope but we can't be sure.
                if (_reservationResponseHistory.IsAUniqueRequest(purchaseTicketRequest.CorrelationId))
                {
                    TicketPurchase ticket;
                    var Event = _eventRepository.FindBy(new Guid(purchaseTicketRequest.EventId));

                    if (Event.CanPurchaseTicketWith(new Guid(purchaseTicketRequest.ReservationId)))
                    {
                        ticket = Event.PurchaseTicketWith(new Guid(purchaseTicketRequest.ReservationId));

                        _eventRepository.Save(Event);

                        response = ticket.ConvertToPurchaseTicketResponse();
                        response.Success = true;
                    }
                    else
                    {
                        response.Message = Event.DetermineWhyATicketCannotbePurchasedWith(new Guid(purchaseTicketRequest.ReservationId));
                        response.Success = false;
                    }

                    _reservationResponseHistory.LogResponse(purchaseTicketRequest.CorrelationId, response);
                }
                else
                {
                    // Retrieve last response
                    response = _reservationResponseHistory.RetrievePreviousResponseFor(purchaseTicketRequest.CorrelationId);
                }
            }
            catch (Exception ex)
            {
                // Shield Exceptions
                response.Message = ErrorLog.GenerateErrorRefMessageAndLog(ex);
                response.Success = false;
            }

            return response;
        }  
    }
}
