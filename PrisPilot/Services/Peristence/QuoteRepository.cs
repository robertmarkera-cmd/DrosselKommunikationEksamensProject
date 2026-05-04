using Microsoft.Data.SqlClient;
using PrisPilot.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrisPilot.Services.Peristence
{
    public class QuoteRepository : BaseRepository<Quote>
    {
        public QuoteRepository() : base()
        {

        }
        public override Quote Add(Quote quote)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();

                using SqlCommand insertCmd = new SqlCommand(@" INSERT INTO dbo.QUOTE (Date, HourlyCost, TotalPrice)"
                                                               + "VALUES (@Date, @HourlyCost, @TotalPrice)"
                                                               + "SELECT @@IDENTITY", con);

                insertCmd.Parameters.Add("@Date", SqlDbType.DateTime2).Value = quote.Date;
                insertCmd.Parameters.Add("@HourlyCost", SqlDbType.Int).Value = quote.HourlyCost;
                insertCmd.Parameters.Add("@TotalPrice", SqlDbType.Float).Value = quote.TotalPrice;
                quote.QuoteID = Convert.ToInt32(insertCmd.ExecuteScalar());
                insertCmd.ExecuteNonQuery();
            }
        }
        public override List<Quote> GetAll()
        {
            List<Quote> quotes = new List<Quote>();

            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand(
                    "SELECT QuoteID, Date, HourlyCost, TotalPrice FROM dbo.QUOTE", con);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var quote = new Quote
                    {

                        Date = reader["Date"] != DBNull.Value ? Convert.ToDateTime(reader["Date"]) : DateTime.MinValue,
                        HourlyCost = reader["HourlyCost"] != DBNull.Value ? Convert.ToInt32(reader["HourlyCost"]) : 0,
                        TotalPrice = reader["TotalPrice"] != DBNull.Value ? Convert.ToDouble(reader["TotalPrice"]) : 0.0
                    };

                    quotes.Add(quote);
                }
            }

            return quotes;
        }

    }
}
    

