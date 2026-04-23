using Microsoft.Data.SqlClient;
using PrisPilot.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrisPilot.Services.Peristence
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository() : base()
        {
        }

        public void Add(int cvr, string companyName, string email, int telephoneNumber, string address, byte[]? logoBytes, string contactPerson, int hourlyCost)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();

                using SqlCommand insertCmd = new SqlCommand(@"
                            INSERT INTO dbo.COSTUMER (Cvr, CompanyName, Email, PhoneNumber, Address, Logo, ContactPerson, HourlyCost)
                            VALUES (@Cvr, @CompanyName, @Email, @PhoneNumber, @Address, @Logo, @ContactPerson, @HourlyCost)", con);

                insertCmd.Parameters.Add("@Cvr", SqlDbType.Int).Value = cvr;
                insertCmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 100).Value = companyName;
                insertCmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;
                insertCmd.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = telephoneNumber;
                insertCmd.Parameters.Add("@Address", SqlDbType.NVarChar, 150).Value = address;
                insertCmd.Parameters.Add("@Logo", SqlDbType.VarBinary).Value = (object?)logoBytes ?? DBNull.Value;
                insertCmd.Parameters.Add("@ContactPerson", SqlDbType.NVarChar, 100).Value = contactPerson;
                insertCmd.Parameters.Add("@HourlyCost", SqlDbType.Int).Value = hourlyCost;

                insertCmd.ExecuteNonQuery();
            }
        }

        public override List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            return customers;
        }
    }
}
