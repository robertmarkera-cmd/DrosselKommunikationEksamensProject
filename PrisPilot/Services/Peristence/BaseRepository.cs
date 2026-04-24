using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisPilot.Services.Peristence
{
    public abstract class BaseRepository<TEntity>
    where TEntity : class, new()
    {
        protected readonly string ConnectionString;

        protected BaseRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        //public abstract void Add(TEntity entity);

        public abstract List<TEntity> GetAll();
    }
}
