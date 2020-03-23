using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class MainMenu : CLIMenu
    {
        // DAOs - Interfaces to our data objects can be stored here...
        protected IParkDAO parkDAO;
        protected ICampgroundDAO campgroundDAO;
        protected IReservationDAO reservationDAO;
        protected ISiteDAO siteDAO;
        //private string Selection = null;


        /// <summary>
        /// Constructor adds items to the top-level menu. YOu will likely have parameters for one or more DAO's here...
        /// </summary>
        public MainMenu(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, IReservationDAO reservationDAO, ISiteDAO siteDAO) : base("Main Menu")
        {
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDAO;
            this.reservationDAO = reservationDAO;
            this.siteDAO = siteDAO;
        }



        protected override void SetMenuOptions()
        {

            List<Park> parks = parkDAO.GetParks();

            

            foreach (Park park in parks)
            {
                this.menuOptions.Add(park.Id.ToString(), park.Name);

            }
            this.menuOptions.Add("Q", "Quit");

            
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string Selection)
        {
            List<Park> parks = parkDAO.GetParks();
            int parkId = 0;
            parkId = int.Parse(Selection);

            Console.Clear();

            Park park = null;
            foreach(Park p in parks)
            {
                if (p.Id == parkId)
                {
                    park = p;
                    break;
                }
            }

            ParkInfoMenu sm = new ParkInfoMenu(parkDAO, campgroundDAO, reservationDAO, siteDAO, park);
            sm.Run();
            
            return true;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
        }

        private void PrintHeader()
        {
            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("National Parks"));

            ResetColor();
        }
    }
}
