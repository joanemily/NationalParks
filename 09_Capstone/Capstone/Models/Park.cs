using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;


namespace Capstone.Models
{
    public class Park
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime EstablishDate { get; set; }
        public int Area { get; set; }
        public int AnnualVisitors { get; set; }
        public string Description { get; set; }
        public object ParksSqlDAO { get; private set; }
 
    }
}
