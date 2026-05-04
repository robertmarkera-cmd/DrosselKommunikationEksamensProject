using Microsoft.Data.SqlClient;
using PrisPilot.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrisPilot.Services.Peristence
{
    public class FixedPriceProductRepository : BaseRepository<FixedPriceProduct>
    {
        public FixedPriceProductRepository() : base()
        {
        }

        public override FixedPriceProduct Add(FixedPriceProduct fixedPriceProduct)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();

                using SqlCommand insertCmd = new SqlCommand(@"
                            INSERT INTO dbo.CUSTOMER (Name, Description, Price, Frequency)
                            VALUES (@Name, @Description, @Price, @Frequency)" + "SELECT @@IDENTITY", con);

                insertCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 80).Value = fixedPriceProduct.Name;
                insertCmd.Parameters.Add("@Description", SqlDbType.NVarChar, 1200).Value = fixedPriceProduct.Description;
                insertCmd.Parameters.Add("@Price", SqlDbType.Int).Value = fixedPriceProduct.Price;
                insertCmd.Parameters.Add("@Frequency", SqlDbType.Int).Value = fixedPriceProduct.Frequency;
                fixedPriceProduct.FixedPriceProductID = Convert.ToInt32(insertCmd.ExecuteScalar());
            }
            return fixedPriceProduct;
        }

        public override List<FixedPriceProduct> GetAll()
        {
            List<FixedPriceProduct> fixedPriceProducts = new List<FixedPriceProduct>();
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand("SELECT FixedPriceProductID, Name, Description, Price, Frequency FROM FixedPriceProduct", con);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FixedPriceProduct fixedPriceProduct = new FixedPriceProduct
                    {
                        FixedPriceProductID = reader.GetInt32(0),
                        Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString()! : string.Empty,
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString()! : string.Empty,
                        Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0,
                        Frequency = reader["Frequency"] != DBNull.Value ? Convert.ToInt32(reader["Frequency"]) : 0
                    };
                    fixedPriceProducts.Add(fixedPriceProduct);
                }
            }
            return fixedPriceProducts;
        }
    }
}
