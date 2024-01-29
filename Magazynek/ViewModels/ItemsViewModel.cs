using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magazynek.Helpers;
using Magazynek.Models;

namespace Magazynek.ViewModels
{
    public class ItemsViewModel : ObservableObject
    {
        private ObservableCollection<ExcelDataModel> _items;
        public ObservableCollection<ExcelDataModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        public async Task<FileResult> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        using var stream = await result.OpenReadAsync();
                        var image = ImageSource.FromStream(() => stream);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
        }

        public ItemsViewModel()
        {

            // Replace "your_excel_file_path.xlsx" with the actual path to your Excel file
            var excelFilePath = PickAndShow;
            FileInfo fileInfo = new FileInfo(Path.GetFullPath(excelFilePath.ToString()));
            var excelReader = new ExcelReader();
            Items = new ObservableCollection<ExcelDataModel>(excelReader.ReadExcelFile(fileInfo.ToString()));

            //// Initialize the ObservableCollection and add some sample products
            //Items = new ObservableCollection<Item>
            //{
            //    new Item { Photo = "", 
            //        Name = "Krzesło", 
            //        Description = "Po prostu krzesło", 
            //        Quantity = 10, 
            //        Price = 19.99M },
            //    new Item { 
            //        Photo = "", 
            //        Name = "Jabłko", 
            //        Description = "Kraj pochodzenia: Polska", 
            //        Quantity = 5, 
            //        Price = 29.99M },
            //    // Add more products as needed
            //};
        }
    }
}

