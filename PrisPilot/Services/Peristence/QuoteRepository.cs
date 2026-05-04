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

                using SqlCommand Cmd = new SqlCommand(@" INSERT INTO QUOTE (Date, HourlyCost, TotalPrice)"
                                                               + "VALUES (@Date, @HourlyCost, @TotalPrice)"
                                                               + "SELECT @@IDENTITY", con);

                Cmd.Parameters.Add("@Date", SqlDbType.DateTime2).Value = quote.Date;
                Cmd.Parameters.Add("@HourlyCost", SqlDbType.Int).Value = quote.HourlyCost;
                Cmd.Parameters.Add("@TotalPrice", SqlDbType.Float).Value = quote.TotalPrice;
                quote.QuoteID = Convert.ToInt32(Cmd.ExecuteScalar());
                                
            }
            return quote;
        }
        public override List<Quote> GetAll()
        {
            List<Quote> quotes = new List<Quote>();

            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand(
                    "SELECT QuoteID, Date, HourlyCost, TotalPrice FROM QUOTE", con);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Quote quote = new Quote
                    {
                        QuoteID = reader.GetInt32(0),
                        Date = (DateTime)reader["Date"],
                        HourlyCost = (int)reader["HourlyCost"],
                        TotalPrice = (Double)reader["TotalPrice"],
                    };

                    quotes.Add(quote);
                }
            }

            return quotes;
        }

    }
}
    

