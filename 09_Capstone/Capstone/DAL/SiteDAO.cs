using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class SiteDAO : ISiteDAO
    {
        private string connectionString;

        public SiteDAO(string connString)
        {
            this.connectionString = connString;
        }

        /// <summary>
        /// Gets all sites from table
        /// </summary>
        /// <returns>Site list</returns>
        public List<Site> GetSites()
        {
            List<Site> list = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();

                    // Create the command for the sql statement
                    string sql = "Select * from site";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Execute the query and get the result set in a reader
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // For each row, create a new country and add it to the list
                    while (rdr.Read())
                    {
                        list.Add(RowToObject(rdr));
                    }

                }
            }
            catch (SqlException ex)
            {
                ErrorLog.LogError(ex);
                throw;
            }

            return list;
        }

        public List<Site> GetTop5AvailableSites(int campgroundId, DateTime arrivalDate, DateTime departureDate)
        {
            List<Site> list = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();

                  

                    string sql =
                    @"SELECT DISTINCT TOP 5 s.*
                    FROM site s
                    LEFT JOIN reservation r ON s.site_id = r.site_id
                    WHERE s.campground_id = @campgroundId
                    AND s.site_id NOT IN(SELECT site_id FROM reservation WHERE(@arrivalDate BETWEEN from_date AND to_date) OR from_date IS NULL)
                    AND s.site_id NOT IN(SELECT site_id FROM reservation WHERE(@departureDate BETWEEN from_date AND to_date) OR from_date IS NULL)
                    AND s.site_id NOT IN(SELECT site_id FROM reservation WHERE(from_date BETWEEN @arrivalDate AND @departureDate) OR from_date IS NULL)";



                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@campgroundId", campgroundId);
                    cmd.Parameters.AddWithValue("@arrivalDate", arrivalDate);
                    cmd.Parameters.AddWithValue("@departureDate", departureDate);

                    // Execute the query and get the result set in a reader
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // For each row, create a new country and add it to the list
                    while (rdr.Read())
                    {
                        list.Add(RowToObject(rdr));
                    }

                }
            }
            catch (SqlException ex)
            {
                ErrorLog.LogError(ex);
                throw;
            }

            return list;
        }

        public bool HasAvailableSites(int campgroundId, DateTime arrivalDate, DateTime departureDate)
        {
            List<Site> list = GetTop5AvailableSites(campgroundId, arrivalDate, departureDate);

            if (list.Count == 0)
            {
                return false; //no sites, need to prompt user if they want a different date
            }
            else
            {
                return true; //yay, there are sites
            }
        }

        /// <summary>
        /// Creates the Site object from the data row
        /// </summary>
        /// <param name="rdr">data row</param>
        /// <returns>Site object</returns>
        private static Site RowToObject(SqlDataReader rdr)
        {
            Site site = new Site()
            {
                SiteId = Convert.ToInt32(rdr["site_id"]),
                CampgroundId = Convert.ToInt32(rdr["campground_id"]),
                SiteNumber = Convert.ToInt32(rdr["site_number"]),
                MaxOccupancy = Convert.ToInt32(rdr["max_occupancy"]),
                IsAccessible = Convert.ToBoolean(rdr["accessible"]),
                MaxRVLength = Convert.ToInt32(rdr["max_rv_length"]),
                HasUtilities = Convert.ToBoolean(rdr["utilities"])
            };

            return site;
        }

    }
}
