using Microsoft.Data.SqlClient;
using PrisPilot.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrisPilot.Services.Peristence
{
    public class VariablePriceProductRepository : BaseRepository<VariablePriceProduct>
    {
        public VariablePriceProductRepository() : base()
        {
        }

        public override VariablePriceProduct Add(VariablePriceProduct variablePriceProduct)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();

                using SqlCommand cmd = new SqlCommand(@"
                            INSERT INTO FixedPriceProduct (Name, Description)
                            VALUES (@Name, @Description)" + "SELECT @@IDENTITY", con);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 80).Value = variablePriceProduct.Name;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 1200).Value = variablePriceProduct.Description;
                variablePriceProduct.VariablePriceProductID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return variablePriceProduct;
        }

        public override List<VariablePriceProduct> GetAll()
        {
            List<VariablePriceProduct> variablePriceProducts = new List<VariablePriceProduct>();
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand("SELECT VariablePriceProductID, Name, Description FROM VariablePriceProduct", con);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    VariablePriceProduct variablePriceProduct = new VariablePriceProduct
                    {
                        VariablePriceProductID = reader.GetInt32(0),
                        Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString()! : string.Empty,
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString()! : string.Empty
                    };
                    variablePriceProducts.Add(variablePriceProduct);
                }
            }
            return variablePriceProducts;
        }
    }
}
