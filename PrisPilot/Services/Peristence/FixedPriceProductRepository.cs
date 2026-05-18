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

                using SqlCommand cmd = new SqlCommand(@"
                            INSERT INTO FixedPriceProduct (Name, Description, Price, Frequency)
                            VALUES (@Name, @Description, @Price, @Frequency)" + "SELECT @@IDENTITY", con);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 80).Value = fixedPriceProduct.Name;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 1200).Value = fixedPriceProduct.Description;
                cmd.Parameters.Add("@Price", SqlDbType.Int).Value = fixedPriceProduct.Price;
                cmd.Parameters.Add("@Frequency", SqlDbType.Int).Value = fixedPriceProduct.Frequency;
                fixedPriceProduct.FixedPriceProductID = Convert.ToInt32(cmd.ExecuteScalar());
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
                        Name = reader["Name"] as string ?? string.Empty,
                        Description = reader["Description"] as string ?? string.Empty,
                        Price = reader["Price"] as int? ?? 0,
                        Frequency = reader["Frequency"] as int? ?? 0
                    };
                    fixedPriceProducts.Add(fixedPriceProduct);
                }
            }
            return fixedPriceProducts;
        }
    }
}
