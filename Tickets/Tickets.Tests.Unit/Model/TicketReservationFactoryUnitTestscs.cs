using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tickets.Model;

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
            Assert.IsInstanceOfType(createdTicketReservation, typeof(TicketReservation));
            Assert.AreEqual(Event.Id, createdTicketReservation.Event.Id);
            Assert.AreEqual(ticketQuantity, createdTicketReservation.TicketQuantity);
        }
    }
}
