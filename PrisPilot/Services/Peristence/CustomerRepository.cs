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

        public override Customer Add(Customer customer)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();

                using SqlCommand insertCmd = new SqlCommand(@"
                            INSERT INTO dbo.CUSTOMER (Cvr, CompanyName, Email, PhoneNumber, Address, Logo, ContactPerson)
                            VALUES (@Cvr, @CompanyName, @Email, @PhoneNumber, @Address, @Logo, @ContactPerson)", con);

                insertCmd.Parameters.Add("@Cvr", SqlDbType.NVarChar, 8).Value = customer.Cvr;
                insertCmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 100).Value = customer.CompanyName;
                insertCmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = customer.Email;
                insertCmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 20).Value = customer.TelephoneNumber;
                insertCmd.Parameters.Add("@Address", SqlDbType.NVarChar, 150).Value = (object?)customer.Address ?? DBNull.Value;
                insertCmd.Parameters.Add("@Logo", SqlDbType.VarBinary).Value = (object?)customer.Logo ?? DBNull.Value;
                insertCmd.Parameters.Add("@ContactPerson", SqlDbType.NVarChar, 100).Value = customer.ContactPerson;
                insertCmd.ExecuteNonQuery();
            }
            return customer;
        }

        public override List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using SqlCommand cmd = new SqlCommand("SELECT Cvr, CompanyName, Email, PhoneNumber, Address, Logo, ContactPerson FROM dbo.CUSTOMER", con);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        Cvr = reader["Cvr"] != DBNull.Value ? reader["Cvr"].ToString()! : string.Empty,
                        CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString()! : string.Empty,
                        Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString()! : string.Empty,
                        TelephoneNumber = reader["PhoneNumber"] != DBNull.Value ? reader["PhoneNumber"].ToString()! : string.Empty,
                        Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString()! : string.Empty,
                        Logo = reader["Logo"] != DBNull.Value ? (byte[])reader["Logo"] : null,
                        ContactPerson = reader["ContactPerson"] != DBNull.Value ? reader["ContactPerson"].ToString()! : string.Empty
                    };
                    customers.Add(customer);
                }
            }
            return customers;
        }
    }
}