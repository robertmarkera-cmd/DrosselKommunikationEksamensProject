using Microsoft.Data.SqlClient;
using PrisPilot.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrisPilot.Services.Peristence
{
    public class TimeSpentRepository : BaseRepository<TimeSpent>
    {
        public TimeSpentRepository() : base()
        {
        }

        // Adds a TimeSpent row (composite key of  QuoteID and VariablePriceProductID)
        public override TimeSpent Add(TimeSpent timeSpent)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand(
                    "INSERT INTO TIMESPENT (QuoteID, VariablePriceProductID, HoursUsed) " +
                    "VALUES (@QuoteID, @VariablePriceProductID, @HoursUsed)", con);

                cmd.Parameters.Add("@QuoteID", SqlDbType.Int).Value = timeSpent.QuoteID;
                cmd.Parameters.Add("@VariablePriceProductID", SqlDbType.Int).Value = timeSpent.VariablePriceProductID;
                cmd.Parameters.Add("@HoursUsed", SqlDbType.Int).Value = timeSpent.HoursUsed;

                cmd.ExecuteNonQuery();
            }
            return timeSpent;
        }

        public override List<TimeSpent> GetAll()
        {
            List<TimeSpent> list = new List<TimeSpent>();
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand(
                    "SELECT QuoteID, VariablePriceProductID, HoursUsed FROM TIMESPENT", con);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new TimeSpent
                    {
                        QuoteID = reader["QuoteID"] != DBNull.Value ? Convert.ToInt32(reader["QuoteID"]) : 0,
                        VariablePriceProductID = reader["VariablePriceProductID"] != DBNull.Value ? Convert.ToInt32(reader["VariablePriceProductID"]) : 0,
                        HoursUsed = reader["HoursUsed"] != DBNull.Value ? Convert.ToInt32(reader["HoursUsed"]) : 0
                    });
                }
            }
            return list;
        }

        // Get all TimeSpent rows for a specific quote
        public List<TimeSpent> GetByQuoteID(int quoteId)
        {
            List<TimeSpent> list = new List<TimeSpent>();
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand(
                    "SELECT QuoteID, VariablePriceProductID, HoursUsed FROM TIMESPENT WHERE QuoteID = @QuoteID", con);
                cmd.Parameters.Add("@QuoteID", SqlDbType.Int).Value = quoteId;
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new TimeSpent
                    {
                        QuoteID = Convert.ToInt32(reader["QuoteID"]),
                        VariablePriceProductID = Convert.ToInt32(reader["VariablePriceProductID"]),
                        HoursUsed = reader["HoursUsed"] != DBNull.Value ? Convert.ToInt32(reader["HoursUsed"]) : 0
                    });
                }
            }
            return list;
        }

        // Get all TimeSpent rows for a specific variable price product
        public List<TimeSpent> GetByVariablePriceProductID(int variablePriceProductId)
        {
            List<TimeSpent> list = new List<TimeSpent>();
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand(
                    "SELECT QuoteID, VariablePriceProductID, HoursUsed FROM TIMESPENT WHERE VariablePriceProductID = @VariablePriceProductID", con);
                cmd.Parameters.Add("@VariablePriceProductID", SqlDbType.Int).Value = variablePriceProductId;
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new TimeSpent
                    {
                        QuoteID = Convert.ToInt32(reader["QuoteID"]),
                        VariablePriceProductID = Convert.ToInt32(reader["VariablePriceProductID"]),
                        HoursUsed = reader["HoursUsed"] != DBNull.Value ? Convert.ToInt32(reader["HoursUsed"]) : 0
                    });
                }
            }
            return list;
        }

        // Update HoursUsed, specific object needed to create composite key
        public void Update(TimeSpent timeSpent)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand(
                    "UPDATE TIMESPENT SET HoursUsed = @HoursUsed WHERE QuoteID = @QuoteID AND VariablePriceProductID = @VariablePriceProductID", con);
                cmd.Parameters.Add("@HoursUsed", SqlDbType.Int).Value = timeSpent.HoursUsed;
                cmd.Parameters.Add("@QuoteID", SqlDbType.Int).Value = timeSpent.QuoteID;
                cmd.Parameters.Add("@VariablePriceProductID", SqlDbType.Int).Value = timeSpent.VariablePriceProductID;
                cmd.ExecuteNonQuery();
            }
        }

        // Delete a TimeSpent row by composite key
        public void Delete(int quoteId, int variablePriceProductId)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand(
                    "DELETE FROM TIMESPENT WHERE QuoteID = @QuoteID AND VariablePriceProductID = @VariablePriceProductID", con);
                cmd.Parameters.Add("@QuoteID", SqlDbType.Int).Value = quoteId;
                cmd.Parameters.Add("@VariablePriceProductID", SqlDbType.Int).Value = variablePriceProductId;
                cmd.ExecuteNonQuery();
            }
        }
    }
}