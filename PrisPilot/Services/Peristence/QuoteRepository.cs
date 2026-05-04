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

                using SqlCommand Cmd = new SqlCommand(@"
                        INSERT INTO QUOTE (Date, HourlyCost, TotalPrice)"
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

        // Link a FixedPriceProduct to a Quote
        public void AddFixedPriceProductToQuote(int quoteId, int fixedPriceProductId)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand(
                    "INSERT INTO FIXEDPRICEPRODUCT_QUOTE (FixedPriceProductID," +
                    "QuoteID) VALUES (@FixedPriceProductID, @QuoteID)", con);
                cmd.Parameters.Add("@FixedPriceProductID", SqlDbType.Int).Value = fixedPriceProductId;
                cmd.Parameters.Add("@QuoteID", SqlDbType.Int).Value = quoteId;
                cmd.ExecuteNonQuery();
            }
        }

        // Remove link between Quote and FixedPriceProduct
        public void RemoveFixedPriceProductFromQuote(int quoteId, int fixedPriceProductId)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand(
                    "DELETE FROM FIXEDPRICEPRODUCT_QUOTE" +
                    "WHERE FixedPriceProductID = @FixedPriceProductID AND QuoteID = @QuoteID", con);
                cmd.Parameters.Add("@FixedPriceProductID", SqlDbType.Int).Value = fixedPriceProductId;
                cmd.Parameters.Add("@QuoteID", SqlDbType.Int).Value = quoteId;
                cmd.ExecuteNonQuery();
            }
        }

        // Get all FixedPriceProduct associated to a Quote
        public List<FixedPriceProduct> GetFixedPriceProductsForQuote(int quoteId)
        {
            List<FixedPriceProduct> results = new List<FixedPriceProduct>();
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand(@"
                    SELECT fp.FixedPriceProductID, fp.Name, fp.Description, fp.Price, fp.Frequency
                    FROM FIXEDPRICEPRODUCT fp
                    JOIN FIXEDPRICEPRODUCT_QUOTE fq ON fp.FixedPriceProductID = fq.FixedPriceProductID
                    WHERE fq.QuoteID = @QuoteID", con);
                cmd.Parameters.Add("@QuoteID", SqlDbType.Int).Value = quoteId;
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FixedPriceProduct fp = new FixedPriceProduct
                    {
                        FixedPriceProductID = reader.GetInt32(0),
                        Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString()! : string.Empty,
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString()! : string.Empty,
                        Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0,
                        Frequency = reader["Frequency"] != DBNull.Value ? Convert.ToInt32(reader["Frequency"]) : 0
                    };
                    results.Add(fp);
                }
            }
            return results;
        }

    }
}
    

