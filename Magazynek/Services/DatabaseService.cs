using Magazynek.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;
using System.Xml;

namespace Magazynek.Services
{
    public class DatabaseService
    {
        private readonly string _databaseName;
        private readonly string _serverName;
        private readonly string _databaseUsername;
        private readonly string _databasePassword;
        private readonly string _SQLConnection;
        public DatabaseService()
        {
            _databaseName = "Nexo_demo_1";
            _serverName = "KONKUTER\\MAGAZYNEK";
            _databaseUsername = "borys";
            _databasePassword = "admin";
            _SQLConnection = $"Data Source={_serverName};Initial Catalog={_databaseName};MultipleActiveResultSets=true; User Id={_databaseUsername};Password={_databasePassword};Persist Security Info=True;TrustServerCertificate=True;Encrypt=false";

        }

        public async Task<List<ItemModel>> GetAsortymentDataAsync(int selectedWarehouse = 0)
        {
            List<ItemModel> data = new List<ItemModel>();
            string warehouseName = string.Empty;

            switch (selectedWarehouse)
            {
                case 0:
                    // magazyn główny
                    warehouseName = "100000";
                    break;

                case 1:
                    // w transporcie
                    warehouseName = "100004";
                    break;

                case 2:
                    // w produkcji
                    warehouseName = "100005";
                    break;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_SQLConnection))
                {
                    await connection.OpenAsync();

                    string sqlQuery = string.Format("SELECT Asortymenty.Id, Symbol, Nazwa, Opis, CenaEwidencyjna, IloscDostepna, IloscZarezerwowanaIlosciowo, IloscZarezerwowanaDostawowo, Ilosc, Termin, Ilosciowa\r\nFROM Nexo_Demo_1.ModelDanychContainer.Asortymenty\r\nFULL OUTER JOIN Nexo_Demo_1.ModelDanychContainer.Rezerwacje ON Asortymenty.Id=Rezerwacje.Asortyment_Id\r\nFULL OUTER JOIN Nexo_Demo_1.ModelDanychContainer.StanyMagazynowe ON Asortymenty.Id=StanyMagazynowe.Asortyment_Id\r\nWHERE Magazyn_Id={0}", warehouseName);

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                ItemModel item = new()
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
                                if (item.CenaEwidencyjna is not System.DBNull)
                                {
                                    item.CenaEwidencyjna = Convert.ToString(Convert.ToDouble(item.CenaEwidencyjna) + " zł");
                                }
                                if (item.IloscDostepna is not System.DBNull)
                                {
                                    item.IloscDostepna = Convert.ToString(Convert.ToDouble(item.IloscDostepna) + " szt.");
                                }
                                if (item.IloscZarezerwowanaIlosciowo is not System.DBNull)
                                {
                                    item.IloscZarezerwowanaIlosciowo = Convert.ToString(Convert.ToDouble(item.IloscZarezerwowanaIlosciowo) + " szt.");
                                }
                                if (item.IloscZarezerwowanaDostawowo is not System.DBNull)
                                {
                                    item.IloscZarezerwowanaDostawowo = Convert.ToString(Convert.ToDouble(item.IloscZarezerwowanaDostawowo) + " szt.");
                                }
                                if (item.Ilosc is not System.DBNull)
                                {
                                    item.Ilosc = Convert.ToString(Convert.ToDouble(item.Ilosc) + " szt.");
                                }
                                data.Add(item);
                            }
                        }
                    }
                    await connection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                // Handle the exception
            }

            return data;
        }


        public async Task<List<OrderModel>> GetUmowyDataAsync()
        {
            List<OrderModel> data = new();

            try
            {
                using (SqlConnection connection = new SqlConnection(_SQLConnection))
                {
                    await connection.OpenAsync();

                    string sqlQuery = "SELECT [Id],[MiejsceWydaniaWystawienia],[DataWprowadzenia],[NumerZewnetrzny],[DataWydaniaWystawienia],[TerminRealizacji],[KwotaDoZaplaty],[Symbol],[Tytul],[Podtytul],[Wystawil],[Odebral],[Uwagi], [NumerWewnetrzny_PelnaSygnatura]" +
                        "FROM [Nexo_Demo_1].[ModelDanychContainer].[Dokumenty]" +
                        "WHERE Symbol = 'ZK' OR Symbol = 'ZD'";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                OrderModel item = new()
                                {
                                    Id = reader[0],
                                    MiejsceWydaniaWystawienia = reader.GetString(1),
                                    DataWprowadzenia = reader["DataWprowadzenia"] as DateTime? ?? default,
                                    NumerZewnetrzny = reader.GetString(3),
                                    DataWydaniaWystawienia = reader["DataWydaniaWystawienia"] as DateTime? ?? default,
                                    TerminRealizacji = reader["TerminRealizacji"] as DateTime? ?? default,
                                    KwotaDoZaplaty = reader[6],
                                    Symbol = reader[7],
                                    Tytul = reader.GetString(8),
                                    Podtytul = reader[9],
                                    Wystawil = reader[10],
                                    Odebral = reader[11],
                                    Uwagi = reader[12],
                                    PelnaSyngatura = reader[13]
                                };
                                if (item.KwotaDoZaplaty is not System.DBNull)
                                {
                                    item.KwotaDoZaplaty = Convert.ToString(Convert.ToDouble(item.KwotaDoZaplaty) + " zł");
                                }
                                data.Add(item);
                            }
                        }
                    }
                    await connection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                // Handle the exception
            }

            return data;
        }

        public async Task MakeReservationAsync(string symbol, int asortyment_id, int ilosc_dostepna, bool ilosciowa, int ilosc_zrealizowana)
        {

            var sqlQuery = string.Format("BEGIN TRANSACTION\r" +
                                "\nUPDATE ModelDanychContainer.Asortymenty\r" +
                                "\nSET Symbol = '{0}'\r" +
                                "\nWHERE Id={1}; \r" +
                                "\nUPDATE ModelDanychContainer.StanyMagazynowe\r" +
                                "\nSET IloscDostepna = {2}\r" +
                                "\nWHERE Asortyment_Id = {1};\r" +
                                "\nINSERT INTO ModelDanychContainer.Rezerwacje ([Ilosc], [Termin], [Ilosciowa], [IloscZrealizowana], [TimeStamp], [Asortyment_Id])\r" +
                                "\nVALUES ({2}, NULL, {3}, {4},  DEFAULT, {1})\r" +
                                "\nUPDATE ModelDanychContainer.StanyMagazynowe\r" +
                                "\nSET IloscZarezerwowanaIlosciowo = {3}\r" +
                                "\nWHERE Id={1};\r" +
                                "\nROLLBACK", symbol, asortyment_id, ilosc_dostepna, ilosciowa, ilosc_zrealizowana);

            try
            {
                await using (SqlConnection connection = new SqlConnection(_SQLConnection))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    using (var command = new SqlCommand(sqlQuery, connection, transaction))
                    {
                        try
                        {
                            await command.ExecuteNonQueryAsync();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                        await transaction.CommitAsync();
                        await connection.CloseAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
    }
}
