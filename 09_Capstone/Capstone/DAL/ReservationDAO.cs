using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationDAO : IReservationDAO
    {
        private string connectionString;

        public ReservationDAO(string connString)
        {
            this.connectionString = connString;
        }

        /// <summary>
        /// Gets all reservations from table
        /// </summary>
        /// <returns>Reservation list</returns>
        public List<Reservation> GetReservations()
        {
            List<Reservation> list = new List<Reservation>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();

                    // Create the command for the sql statement
                    string sql = "Select * from reservation";
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
        /// Books a new reservation.
        /// </summary>
        /// <param name="newReservation">The reservation object.</param>
        /// <returns>The id of the new reservation (if successful).</returns>
        public int AddReservation(Reservation newReservation)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT reservation (site_id, name, from_date, to_date, create_date) VALUES (@site_id, @name, @from_date, @to_date, @create_date); SELECT @@identity";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@site_id", newReservation.SiteId);
                    cmd.Parameters.AddWithValue("@name", newReservation.Name);
                    cmd.Parameters.AddWithValue("@from_date", newReservation.FromDate);
                    cmd.Parameters.AddWithValue("@to_date", newReservation.ToDate);
                    cmd.Parameters.AddWithValue("@create_date", newReservation.CreateDate);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());

                    return result;
                }
            }
            catch (SqlException ex)
            {
                ErrorLog.LogError(ex);
                throw;
            }

            

        }

        /// <summary>
        /// Creates the Reservation object from the data row
        /// </summary>
        /// <param name="rdr">data row</param>
        /// <returns>Reservation object</returns>
        private static Reservation RowToObject(SqlDataReader rdr)
        {
            Reservation reservation = new Reservation()
            {
                ReservationId = Convert.ToInt32(rdr["reservation_id"]),
                SiteId = Convert.ToInt32(rdr["site_id"]),
                Name = Convert.ToString(rdr["name"]),
                FromDate = Convert.ToDateTime(rdr["from_date"]),
                ToDate = Convert.ToDateTime(rdr["to_date"])
            };

            //Create date can be null
            if (rdr["create_date"] is DBNull)
            {
                reservation.CreateDate = null;
            }
            else
            {
                reservation.CreateDate = Convert.ToDateTime(rdr["create_date"]);
            }

            return reservation;
        }
    }
}
