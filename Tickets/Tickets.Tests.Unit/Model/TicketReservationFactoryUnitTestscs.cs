using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tickets.Model;
using FluentAssertions;

namespace Tickets.Tests.Unit.Model
{
    [TestClass]
    public class TicketReservationFactoryUnitTestscs
    {
        [TestMethod]
        public void CreateReservation_PassingEventAndTicketQuantity_CreatesTicketReservation()
        {
            //arrange
            var Event = new Event
            {
                Id = Guid.NewGuid()
            };
            var ticketQuantity = 10;

            //act
            var createdTicketReservation = TicketReservationFactory.CreateReservation(Event, ticketQuantity);

            //assert
            createdTicketReservation.Should().BeOfType<TicketReservation>();
            createdTicketReservation.Event.Id.Should().Be(Event.Id);
            createdTicketReservation.TicketQuantity.Should().Be(ticketQuantity);
        }
    }
}
