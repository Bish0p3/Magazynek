using Magazynek.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazynek.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<AsortymentyModel> Asortyment { get; set; }

        public bool IsDatabaseConnected()
        {
            try
            {
                // Attempt to query the database by executing a simple query
                // You can change this query based on your actual database schema
                var result = Asortyment.FirstOrDefault();

                // If the query executes successfully, the database is connected
                return true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                // If an exception occurs, the database is not connected
                return false;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=KONKUTER\\MAGAZYNEK;Database=Nexo_demo_1;User Id=borys_admin;Password=admin");

        //Server=your_server_address;Database=your_database_name;User Id=your_username;Password=your_password;

    }
}
