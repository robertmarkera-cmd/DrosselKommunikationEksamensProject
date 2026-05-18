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
                            INSERT INTO CUSTOMER (Cvr, CompanyName, Email, PhoneNumber, Address, Logo, ContactPerson)
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
                using SqlCommand cmd = new SqlCommand("SELECT Cvr, CompanyName, Email, PhoneNumber, Address, Logo, ContactPerson FROM CUSTOMER", con);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        Cvr = reader["Cvr"] as string ?? string.Empty,
                        CompanyName = reader["CompanyName"] as string ?? string.Empty,
                        Email = reader["Email"] as string ?? string.Empty,
                        TelephoneNumber = reader["PhoneNumber"] as string ?? string.Empty,
                        Address = reader["Address"] as string ?? string.Empty,
                        Logo = reader["Logo"] as byte[] ?? null,
                        ContactPerson = reader["ContactPerson"] as string ?? string.Empty
                    };
                    customers.Add(customer);
                }
            }
            return customers;
        }
    }
}