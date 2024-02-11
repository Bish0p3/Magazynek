using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Magazynek.Helpers;
using Magazynek.Models;
using Magazynek.Services;
using OfficeOpenXml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Magazynek.ViewModels
{
    public class ItemsViewModel : ObservableObject
    {

        #region FIELDS
        private ObservableCollection<AsortymentyModel> _asortyment;
        public ObservableCollection<AsortymentyModel> Asortyment
        {
            get => _asortyment;
            set
            {
                _asortyment = value;
                OnPropertyChanged(nameof(Asortyment));
            }
        }

        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public ICommand Button_OpenFileCommand { get; }
        #endregion

       

        #region CONSTRUCTOR
        public ItemsViewModel()
        {
            Button_OpenFileCommand = new Command(ExecuteButton_OpenFileCommand);

            // Initialize the ObservableCollection
            Asortyment = new ObservableCollection<AsortymentyModel>();



            // Call method to populate data
            PopulateData();
        }
        #endregion

        #region METHODS
        private void ExecuteButton_OpenFileCommand()
        {
            PickOptions options = new();
            _ = PickAndShow(options);
        }

        private void PopulateData()
        {
            // Call your data service here (e.g., SqlServerService)
            // to get data and add it to DataItems
            // ...
            // Initialize DatabaseService
            DatabaseService _databaseService = new DatabaseService();
            List<AsortymentyModel> data = _databaseService.GetYourData();
            foreach (var item in data)
            {
                Asortyment.Add(item);
            }
            // For demonstration purposes, adding dummy data
            Asortyment.Add(new AsortymentyModel { Id = 1, Nazwa = "Item 1" });
            Asortyment.Add(new AsortymentyModel { Id = 2, Nazwa = "Item 2" });
            // Add more items as needed
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
                string message = ex.Message;
            }
        }
        #endregion


    }
}

