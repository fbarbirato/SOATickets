using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tickets.Model;

namespace Tickets.Tests.Unit.Model
{
    [TestClass]
    public class TicketPurchaseFactoryUnitTests
    {
        [TestMethod]
        public void CreateTicket_PassingEventAndTicketQuantity_CreatesTicketPurchase()
        {
            //arrange
            var Event = new Event
            {
                Id = Guid.NewGuid()
            };
            var ticketQuantity = 10;

            //act
            var createdTicketPurchase = TicketPurchaseFactory.CreateTicket(Event, ticketQuantity);

            //assert
            Assert.IsInstanceOfType(createdTicketPurchase, typeof(TicketPurchase));
            Assert.AreEqual(Event.Id, createdTicketPurchase.Event.Id);
            Assert.AreEqual(ticketQuantity, createdTicketPurchase.TicketQuantity);
        }
    }
}
