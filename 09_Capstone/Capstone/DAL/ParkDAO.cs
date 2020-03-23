using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ParkDAO : IParkDAO
    {
        private string connectionString;

        public ParkDAO(string connString)
        {
            this.connectionString = connString;
        }

        /// <summary>
        /// Gets all parks from table sorted alphabetically by name
        /// </summary>
        /// <returns>Park list</returns>
        public List<Park> GetParks()
        {
            List<Park> list = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();

                    // Create the command for the sql statement
                    string sql = "Select * from park ORDER BY name ASC";
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

        /// <summary>
        /// Creates the Park object from the data row
        /// </summary>
        /// <param name="rdr">data row</param>
        /// <returns>Park object</returns>
        private static Park RowToObject(SqlDataReader rdr)
        {
            Park park = new Park()
            {
                Id = Convert.ToInt32(rdr["park_id"]),
                Name = Convert.ToString(rdr["name"]),
                Location = Convert.ToString(rdr["location"]),
                EstablishDate = Convert.ToDateTime(rdr["establish_date"]),
                Area = Convert.ToInt32(rdr["area"]),
                AnnualVisitors = Convert.ToInt32(rdr["visitors"]),
                Description = Convert.ToString(rdr["description"])
            };

            return park;
        }

    }
}
