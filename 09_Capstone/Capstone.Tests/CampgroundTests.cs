using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class CampgroundTests
    {
        /// <summary>
        /// Tests that the total cost to stay is correct 
        /// based on the campground's daily fee and the 
        /// duration of the requested stay.
        /// </summary>
        [TestMethod]
        public void TotalStayCostTests()
        {
            //Arrange
            Campground campground = new Campground()
            {
                Id = 1,
                ParkId = 1,
                Name = "Testing",
                OpenMonths = 1,
                ClosedMonths = 12,
                DailyFee = 30.00M
            };

            //Act 
            decimal actualResult = campground.TotalStayCost(Convert.ToDateTime("02/24/2020"), Convert.ToDateTime("03/01/2020"));

            //Assert
            Assert.AreEqual(180.0M, actualResult);
        }

        [TestMethod]
        public void IsCampgroundOpenTests()
        {
            //Arrange
            Campground campground = new Campground()
            {
                Id = 1,
                ParkId = 1,
                Name = "Testing",
                OpenMonths = 6,
                ClosedMonths = 8,
                DailyFee = 30.00M
            };

            //Act and Assert - For open
            Assert.IsTrue(campground.IsCampgroundOpen(Convert.ToDateTime("07/01/2020"), Convert.ToDateTime("07/30/2020")));
            Assert.IsTrue(campground.IsCampgroundOpen(Convert.ToDateTime("06/01/2020"), Convert.ToDateTime("08/31/2020")));

            //Act and Assert = For closed
            Assert.IsFalse(campground.IsCampgroundOpen(Convert.ToDateTime("01/01/2020"), Convert.ToDateTime("05/30/2020")));
            Assert.IsFalse(campground.IsCampgroundOpen(Convert.ToDateTime("01/01/2020"), Convert.ToDateTime("08/31/2020")));
            Assert.IsFalse(campground.IsCampgroundOpen(Convert.ToDateTime("08/31/2020"), Convert.ToDateTime("10/30/2020")));


        }
    }
}
