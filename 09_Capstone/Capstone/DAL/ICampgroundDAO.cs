using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAL
{
    public interface ICampgroundDAO
    {
        List<Campground> GetCampgrounds();
    }
}