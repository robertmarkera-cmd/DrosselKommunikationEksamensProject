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

        public override void Add(Customer customer)
        {
            
        }

        public override List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            return customers;
        }
    }
}
