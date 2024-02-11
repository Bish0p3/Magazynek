using Magazynek.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Magazynek.Services
{
    public class DatabaseService
    {
        public DatabaseService()
        {


        }
        public List<AsortymentyModel> GetYourData()
        {
            string srvrdbname = "Nexo_demo_1";
            string srvrname = "192.168.2.164";
            string srvrusername = "borys";
            string srvrpassword = "admin";

            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};MultipleActiveResultSets=true; User Id={srvrusername};Password={srvrpassword};Persist Security Info=True;TrustServerCertificate=True;Connection Timeout=30;";

            List<AsortymentyModel> data = new List<AsortymentyModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(sqlconn))
                {
                    connection.Open();

                    string sqlQuery = "SELECT Id, Nazwa FROM ModelDanychContainer.Asortymenty";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AsortymentyModel item = new AsortymentyModel
                                {
                                    Id = reader.GetInt32(0),
                                    Nazwa = reader.GetString(1),
                                    // Map other properties as needed
                                };
                                data.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return data;
        }
    }
}
