﻿using System;
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

        public bool CanPurchaseTicketWith(Guid reservationId)
        {
            if (HasReservationWith(reservationId))
            {
                return GetReservationWith(reservationId).StillActive();
            }

            return false;
        }

        private TicketReservation GetReservationWith(Guid reservationId)
        {
            if (!HasReservationWith(reservationId))
            {
                throw new ApplicationException(String.Format("No reservation ticket with matching id of '{0}'", reservationId.ToString()));
            }

            return ReservedTickets.FirstOrDefault(t => t.Id == reservationId);
        }

        private bool HasReservationWith(Guid reservationId)
        {
            return ReservedTickets.Exists(t => t.Id == reservationId);
        }

        public string DetermineWhyATicketCannotBePurchasedWith(Guid reservationId)
        {
            string reservationIssue = "";

            if (HasReservationWith(reservationId))
            {
                TicketReservation reservation = GetReservationWith(reservationId);
                if (reservation.HasExpired())
                {
                    reservationIssue = String.Format("Ticket reservation '{0}' has expired", reservationId.ToString());
                }
                else if (reservation.HasBeenRedeemed)
                {
                    reservationIssue = String.Format("Ticket reservation '{0}' has already been redeemed", reservationId.ToString());
                }
            }
            else
            {
                reservationIssue = String.Format("There is no ticket reservation with the Id '{0}'", reservationId.ToString());
            }

            return reservationIssue;
        }
    }
}
