using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tickets.Model;
using FluentAssertions;

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
            createdTicketPurchase.Should().BeOfType<TicketPurchase>();
            createdTicketPurchase.Event.Id.Should().Be(Event.Id);
            createdTicketPurchase.TicketQuantity.Should().Be(ticketQuantity);
        }
    }
}
