using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tickets.Model;

namespace Tickets.Tests.Unit.Model
{
    [TestClass]
    public class TicketReservationUnitTests
    {
        [TestMethod]
        public void HasExpired_1MinuteAfterExpiryTime_ReturnsTrue()
        {
            //arrange
            var reservation = new TicketReservation
            {
                ExpiryTime = DateTime.Now.AddMinutes(-1)
            };

            //act
            bool result = reservation.HasExpired();

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasExpired_1MinuteBeforeExpiryTime_ReturnsFalse()
        {
            //arrange
            var reservation = new TicketReservation
            {
                ExpiryTime = DateTime.Now.AddMinutes(1)
            };

            //act
            bool result = reservation.HasExpired();

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StillActive_HasNotBeenRedeemedAndHasNotExpired_ReturnsTrue()
        {
            //arrange
            var reservation = new TicketReservation
            {
                HasBeenRedeemed = false,
                ExpiryTime = DateTime.Now.AddHours(1)
            };

            //act
            bool result = reservation.StillActive();

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StillActive_HasBeenRedeemedAndHasNotExpired_ReturnsFalse()
        {
            //arrange
            var reservation = new TicketReservation
            {
                HasBeenRedeemed = true,
                ExpiryTime = DateTime.Now.AddHours(1)
            };

            //act
            bool result = reservation.StillActive();

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StillActive_HasNotBeenRedeemedAndHasExpired_ReturnsFalse()
        {
            //arrange
            var reservation = new TicketReservation
            {
                HasBeenRedeemed = false,
                ExpiryTime = DateTime.Now.AddHours(-1)
            };

            //act
            bool result = reservation.StillActive();

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StillActive_HasBeenRedeemedAndHasExpired_ReturnsFalse()
        {
            //arrange
            var reservation = new TicketReservation
            {
                HasBeenRedeemed = false,
                ExpiryTime = DateTime.Now.AddHours(-1)
            };

            //act
            bool result = reservation.StillActive();

            //assert
            Assert.IsFalse(result);
        }
    }
}
