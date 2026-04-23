using Microsoft.Data.SqlClient;
using PrisPilot.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrisPilot.Services.Peristence
{
    public class CostumerRepository : BaseRepository<Costumer>
    {
        public CostumerRepository() : base()
        {
        }

        public void Add(int cvr, string companyName, string email, int telephoneNumber, string address)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();

                using SqlCommand insertCmd = new SqlCommand(@"
                            INSERT INTO dbo.COSTUMER (Cvr, CompanyName, Email, PhoneNumber, Address, Logo)
                            VALUES (@Cvr, @CompanyName, @Email, @PhoneNumber, @Address, @Logo)", con);
                insertCmd.Parameters.Add("@Cvr", SqlDbType.NVarChar, 50).Value = cvr;
                insertCmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 8).Value = companyName;
                insertCmd.Parameters.Add("@Email", SqlDbType.Bit).Value = email;
                insertCmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 20).Value = telephoneNumber;
                insertCmd.Parameters.Add("@Address", SqlDbType.NVarChar, 20).Value = address;

                insertCmd.Parameters.Add("@Logo", SqlDbType.NVarChar, 20).Value = address;
                insertCmd.ExecuteNonQuery();
            }
        }

        public override List<Costumer> GetAll()
        {
            List<Costumer> costumers = new List<Costumer>();
            return costumers;
        }
    }
}
