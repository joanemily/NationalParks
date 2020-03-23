using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.DAL
{
    public interface ISiteDAO
    {
        List<Site> GetSites();
        bool HasAvailableSites(int campgroundId, DateTime arrivalDate, DateTime departureDate);
        List<Site> GetTop5AvailableSites(int campgroundId, DateTime arrivalDate, DateTime departureDate);
    }
}