using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Magazynek.Helpers;
using Magazynek.Models;
using OfficeOpenXml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Magazynek.ViewModels
{
    public class ItemsViewModel : ObservableObject
    {


        private ObservableCollection<Item> _items;

        public ICommand Button_OpenFileCommand { get; }
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public async Task PickAndShow(PickOptions options)
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

                FileInfo fileInfo = new FileInfo(Path.GetFullPath(result.FullPath));

                using var package = new ExcelPackage(new FileInfo(fileInfo.ToString()));
                var worksheet = package.Workbook.Worksheets.First();



                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    Items.Add(new Item
                    {
                        Photo = "",
                        Name = worksheet.Cells[row, 1].Value?.ToString(),
                        Description = worksheet.Cells[row, 2].Value?.ToString(),
                        Quantity = Convert.ToInt32(worksheet.Cells[row, 3].Value),
                        Price = Convert.ToDecimal(worksheet.Cells[row, 4].Value)
                    });
                }
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }
        }

        public ItemsViewModel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or 
            Items = new ObservableCollection<Item>();

            Items.Add(new Item
            {
                Photo = "",
                Name = "Krzesło",
                Description = "Po prostu krzesło",
                Quantity = 10,
                Price = 19.99M
            });

            Button_OpenFileCommand = new Command(ExecuteButton_OpenFileCommand);



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

        private void ExecuteButton_OpenFileCommand()
        {
            PickOptions options = new();
            _ = PickAndShow(options);
        }
    }
}

