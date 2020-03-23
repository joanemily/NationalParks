using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Campground
    {
        #region Properties
        public int Id { get; set; }
        public int ParkId { get; set; }
        public string Name { get; set; }
        public int OpenMonths { get; set; }
        public int ClosedMonths { get; set; }
        public decimal DailyFee { get; set; }
        #endregion

        #region Methods

        public string DisplayMonths(int month)
        {
            
            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";

            }
            return null;
        }

        /// <summary>
        /// Calculates the total cost to stay at this camgpround
        /// </summary>
        /// <param name="arrivalDate"></param>
        /// <param name="departureDate"></param>
        /// <returns>decimal for total cost of stay</returns>
        public decimal TotalStayCost(DateTime arrivalDate, DateTime departureDate)
        {
            return this.DailyFee * Convert.ToDecimal((departureDate - arrivalDate).TotalDays);
        }

        /// <summary>
        /// Checks if the campground is open (in-season)
        /// </summary>
        /// <param name="arrivalDate"></param>
        /// <param name="departureDate"></param>
        /// <returns>True if the reservation request is in season</returns>
        public bool IsCampgroundOpen(DateTime arrivalDate, DateTime departureDate)
        {
            if (arrivalDate.Month >= OpenMonths & departureDate.Month <= ClosedMonths)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
