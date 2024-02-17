using Magazynek.Models;
using OfficeOpenXml; // NuGet Package

namespace Magazynek.Helpers
{

    public class ExcelReader
    {
        static ExcelReader()
        {
            // Set the LicenseContext when the class is loaded
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial
        }
        public List<ExcelDataModel> ReadExcelFile(string filePath)
        {
            List<ExcelDataModel> data = new List<ExcelDataModel>();
            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var currentSheet = package.Workbook.Worksheets; // Assuming data is in the first worksheet
                    var worksheet = currentSheet.First();

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        data.Add(new ExcelDataModel
                        {
                            Name = worksheet.Cells[row, 1].Value?.ToString(),
                            Description = worksheet.Cells[row, 2].Value?.ToString(),
                            Quantity = Convert.ToInt32(worksheet.Cells[row, 3].Value),
                            Price = Convert.ToDecimal(worksheet.Cells[row, 4].Value)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening Excel file: {ex.Message}");
            }

            return data;
        }
    }

}
