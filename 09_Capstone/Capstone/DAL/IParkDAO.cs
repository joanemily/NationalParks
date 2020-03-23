using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAL
{
    public interface IParkDAO
    {
        List<Park> GetParks();
    }
}