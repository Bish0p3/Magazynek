using Magazynek.Models;
using Microsoft.Data.SqlClient;

namespace Magazynek.Services
{
    public class DatabaseService
    {
        public DatabaseService()
        {

        }

        public async Task<List<AsortymentyModel>> GetYourDataAsync()
        {
            string srvrdbname = "Nexo_demo_1";
            string srvrname = "KONKUTER\\MAGAZYNEK";
            string srvrusername = "borys";
            string srvrpassword = "admin";

            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};MultipleActiveResultSets=true; User Id={srvrusername};Password={srvrpassword};Persist Security Info=True;TrustServerCertificate=True;Encrypt=false";

            List<AsortymentyModel> data = new List<AsortymentyModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(sqlconn))
                {
                    await connection.OpenAsync();

                    string sqlQuery = "SELECT ModelDanychContainer.Asortymenty.Id, Symbol, Nazwa, Opis, CenaEwidencyjna, IloscDostepna, IloscZarezerwowanaIlosciowo, IloscZarezerwowanaDostawowo, Ilosc, Termin, Ilosciowa\r\nFROM ModelDanychContainer.Asortymenty\r\nFULL OUTER JOIN ModelDanychContainer.Rezerwacje ON ModelDanychContainer.Asortymenty.Id=ModelDanychContainer.Rezerwacje.Asortyment_Id\r\nFULL OUTER JOIN ModelDanychContainer.StanyMagazynowe ON ModelDanychContainer.Asortymenty.Id=ModelDanychContainer.StanyMagazynowe.Asortyment_Id";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                AsortymentyModel item = new AsortymentyModel
                                {
                                    Id = reader.GetInt32(0),
                                    Symbol = reader.GetString(1),
                                    Nazwa = reader.GetString(2),
                                    Opis = reader.GetString(3),
                                    CenaEwidencyjna = reader.GetValue(4),
                                    IloscDostepna = reader.GetValue(5),
                                    IloscZarezerwowanaIlosciowo = reader.GetValue(6),
                                    IloscZarezerwowanaDostawowo = reader.GetValue(7),
                                    Ilosc = reader.GetValue(8),
                                    Termin = reader.GetValue(9),
                                    Ilosciowa = reader.GetValue(10)
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
                // Handle the exception
            }

            return data;
        }
    }
}
