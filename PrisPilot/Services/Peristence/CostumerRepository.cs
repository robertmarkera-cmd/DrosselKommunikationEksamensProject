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

        public override void Add(Costumer costumer)
        {
            
        }

        public override List<Costumer> GetAll()
        {
            List<Costumer> costumers = new List<Costumer>();
            return costumers;
        }
    }
}
