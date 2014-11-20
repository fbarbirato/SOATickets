using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tickets.Model;
using FizzWare.NBuilder;
using FluentAssertions;

namespace Tickets.Tests.Unit.Model
{
    [TestClass]
    public class EventUnitTests
    {
        [TestMethod]
        public void AvailableAllocation_100Allocation10PurchasedAnd15ReservedWith10StillActive_Returns80TicketsAvailable()
        {
            //arrange
            var testEvent = new Event
            {
                Id = Guid.NewGuid(),
                Name = "Test Event",
                Allocation = 100,
                PurchasedTickets = Builder<TicketPurchase>.CreateListOfSize(2)
                                        .All()
                                        .With(t => t.TicketQuantity = 5)
                                        .Build().ToList(),
                ReservedTickets = Builder<TicketReservation>.CreateListOfSize(3)
                                        .All()
                                        .With(r => r.TicketQuantity = 5)
                                        .TheFirst(2)
                                        .With(r => r.ExpiryTime = DateTime.Now.AddHours(1))
                                        .And(r => r.HasBeenRedeemed = false)
                                        .TheNext(1)
                                        .With(r => r.ExpiryTime = DateTime.Now.AddHours(-1))
                                        .And(r => r.HasBeenRedeemed = true)
                                        .Build().ToList()
            };

            //act
            int result = testEvent.AvailableAllocation();

            //assert
            result.Should().Be(80);
        }

        [TestMethod]
        public void CanPurchaseTicketWith_HasReservationAndStillActive_ReturnsTrue()
        {
            //arrange
            var reservationId = Guid.NewGuid();

            var testEvent = new Event
            {
                Id = reservationId,
                Name = "Test Event",
                Allocation = 100,
                ReservedTickets = new List<TicketReservation>()
                {
                    new TicketReservation
                    {
                        Id = reservationId,
                        ExpiryTime = DateTime.Now.AddHours(2),
                        TicketQuantity = 2,
                        HasBeenRedeemed = false
                    }
                }
            };

            //act
            bool result = testEvent.CanPurchaseTicketWith(reservationId);

            //assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void CanPurchaseTicketWith_HasReservationButNotStillActive_ReturnsFalse()
        {
            //arrange
            var reservationId = Guid.NewGuid();

            var testEvent = new Event
            {
                Id = reservationId,
                Name = "Test Event",
                Allocation = 100,
                ReservedTickets = new List<TicketReservation>()
                {
                    new TicketReservation
                    {
                        Id = reservationId,
                        ExpiryTime = DateTime.Now.AddHours(-1),
                        TicketQuantity = 2,
                        HasBeenRedeemed = true
                    }
                }
            };

            //act
            bool result = testEvent.CanPurchaseTicketWith(reservationId);

            //assert
            result.Should().BeFalse();
        }


        [TestMethod]
        public void CanPurchaseTicketWith_DoesNotHaveReservation_ReturnsFalse()
        {
            //arrange
            var reservationId = Guid.NewGuid();

            var testEvent = new Event
            {
                Id = reservationId,
                Name = "Test Event",
                Allocation = 100,
                ReservedTickets = new List<TicketReservation>()
                {
                    new TicketReservation
                    {
                        Id = Guid.NewGuid(),
                        ExpiryTime = DateTime.Now.AddHours(20),
                        TicketQuantity = 2,
                        HasBeenRedeemed = false
                    },
                    new TicketReservation
                    {
                        Id = Guid.NewGuid(),
                        ExpiryTime = DateTime.Now.AddHours(12),
                        TicketQuantity = 14,
                        HasBeenRedeemed = false
                    }
                }
            };

            //act
            bool result = testEvent.CanPurchaseTicketWith(reservationId);

            //assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DetermineWhyATicketCannotBePurchasedWith_HasExpired_ReturnsMessage()
        {
            //arrange
            var reservationId = Guid.NewGuid();

            var testEvent = new Event
            {
                Id = reservationId,
                Name = "Test Event",
                Allocation = 100,
                ReservedTickets = new List<TicketReservation>()
                {
                    new TicketReservation
                    {
                        Id = reservationId,
                        ExpiryTime = DateTime.Now.AddHours(-1),
                        TicketQuantity = 2,
                        HasBeenRedeemed = false
                    }
                }
            };

            //act
            var result = testEvent.DetermineWhyATicketCannotBePurchasedWith(reservationId);

            //assert
            result.Should().Be(String.Format("Ticket reservation '{0}' has expired", reservationId.ToString()));
        }

        [TestMethod]
        public void DetermineWhyATicketCannotBePurchasedWith_HasBeenRedeemed_ReturnsMessage()
        {
            //arrange
            var reservationId = Guid.NewGuid();

            var testEvent = new Event
            {
                Id = reservationId,
                Name = "Test Event",
                Allocation = 100,
                ReservedTickets = new List<TicketReservation>()
                {
                    new TicketReservation
                    {
                        Id = reservationId,
                        ExpiryTime = DateTime.Now.AddHours(20),
                        TicketQuantity = 2,
                        HasBeenRedeemed = true
                    }
                }
            };

            //act
            var result = testEvent.DetermineWhyATicketCannotBePurchasedWith(reservationId);

            //assert
            result.Should().Be(String.Format("Ticket reservation '{0}' has already been redeemed", reservationId.ToString()));
        }

        [TestMethod]
        public void DetermineWhyATicketCannotBePurchasedWith_HasNoTicketReservation_ReturnsMessage()
        {
            //arrange
            var reservationId = Guid.NewGuid();

            var testEvent = new Event
            {
                Id = reservationId,
                Name = "Test Event",
                Allocation = 100,
                ReservedTickets = new List<TicketReservation>()
                {
                    new TicketReservation
                    {
                        Id = Guid.NewGuid(),
                        ExpiryTime = DateTime.Now.AddHours(20),
                        TicketQuantity = 2,
                        HasBeenRedeemed = false
                    }
                }
            };

            //act
            var result = testEvent.DetermineWhyATicketCannotBePurchasedWith(reservationId);

            //assert
            result.Should().Be(String.Format("There is no ticket reservation with the Id '{0}'", reservationId.ToString()));
        } 
    }
}
