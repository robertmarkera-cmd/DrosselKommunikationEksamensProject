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

        public void Add(string cvr, string companyName, string email, string telephoneNumber, string address, byte[]? logoBytes, string contactPerson, double hourlyCost)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();

                using SqlCommand insertCmd = new SqlCommand(@"
                            INSERT INTO dbo.CUSTOMER (Cvr, CompanyName, Email, PhoneNumber, Address, Logo, ContactPerson, HourlyCost)
                            VALUES (@Cvr, @CompanyName, @Email, @PhoneNumber, @Address, @Logo, @ContactPerson, @HourlyCost)", con);

                insertCmd.Parameters.Add("@Cvr", SqlDbType.NVarChar, 8).Value = cvr;
                insertCmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 100).Value = companyName;
                insertCmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;
                insertCmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 20).Value = telephoneNumber;
                insertCmd.Parameters.Add("@Address", SqlDbType.NVarChar, 150).Value = (object?)address ?? DBNull.Value;
                insertCmd.Parameters.Add("@Logo", SqlDbType.VarBinary).Value = (object?)logoBytes ?? DBNull.Value;
                insertCmd.Parameters.Add("@ContactPerson", SqlDbType.NVarChar, 100).Value = contactPerson;
                insertCmd.Parameters.Add("@HourlyCost", SqlDbType.Float).Value = hourlyCost;

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