using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tickets.Model;
using FizzWare.NBuilder;

namespace Tickets.Tests.Unit.Model
{
    [TestClass]
    public class EventUnitTests
    {
        [TestMethod]
        public void AvailableAllocation_100Allocation10PurchasedAnd15ReservedWith10StillActive_Returns80TicketsAvailable()
        {
            //arrange
            var Event = new Event
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
            int result = Event.AvailableAllocation();

            //assert
            Assert.AreEqual(80, result);
        }
    }
}
