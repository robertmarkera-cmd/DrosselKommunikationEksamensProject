using Microsoft.Data.SqlClient;
using PrisPilot.Models;
using System;
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

        public void Add()
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();

                using SqlCommand insertCmd = new SqlCommand(@"
                            INSERT INTO dbo.CUSTOMER (Cvr, CompanyName, Email, PhoneNumber, Address, Logo, ContactPerson)
                            VALUES (@Cvr, @CompanyName, @Email, @PhoneNumber, @Address, @Logo, @ContactPerson)" + "SELECT @@IDENTITY", con);

                insertCmd.Parameters.Add("@Cvr", SqlDbType.NVarChar, 8).Value = cvr;
                insertCmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 100).Value = companyName;
                insertCmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;
                insertCmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 20).Value = telephoneNumber;
                insertCmd.Parameters.Add("@Address", SqlDbType.NVarChar, 150).Value = (object?)address ?? DBNull.Value;
                insertCmd.Parameters.Add("@Logo", SqlDbType.VarBinary).Value = (object?)logoBytes ?? DBNull.Value;
                insertCmd.Parameters.Add("@ContactPerson", SqlDbType.NVarChar, 100).Value = contactPerson;

                insertCmd.ExecuteNonQuery();
            }
        }

        public override List<FixedPriceProduct> GetAll()
        {
            List<FixedPriceProduct> fixedPriceProducts = new List<FixedPriceProduct>();
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand("SELECT Cvr, CompanyName, Email, PhoneNumber, Address, Logo, ContactPerson FROM dbo.CUSTOMER", con);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FixedPriceProduct fixedPriceProduct = new FixedPriceProduct
                    {
                        Cvr = reader["Cvr"] != DBNull.Value ? reader["Cvr"].ToString()! : string.Empty,
                        CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString()! : string.Empty,
                        Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString()! : string.Empty,
                        TelephoneNumber = reader["PhoneNumber"] != DBNull.Value ? reader["PhoneNumber"].ToString()! : string.Empty,
                        Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString()! : string.Empty,
                        Logo = reader["Logo"] != DBNull.Value ? (byte[])reader["Logo"] : null,
                        ContactPerson = reader["ContactPerson"] != DBNull.Value ? reader["ContactPerson"].ToString()! : string.Empty
                    };
                    fixedPriceProducts.Add(fixedPriceProduct);
                }
            }
            return fixedPriceProducts;
        }
    }
}
