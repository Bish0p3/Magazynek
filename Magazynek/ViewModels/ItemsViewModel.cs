using Magazynek.Helpers;
using Magazynek.Models;
using Magazynek.Services;
using OfficeOpenXml;
using System.Collections.ObjectModel;
using System.Windows.Input;

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

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand Button_OpenFileCommand { get; }
        public ICommand ListView_ItemSelectedCommand { get; }
        public ICommand ListView_RefreshCommand { get; }
        #endregion

        #region CONSTRUCTOR
        public ItemsViewModel()
        {
            Button_OpenFileCommand = new Command(async () => await ExecuteButton_OpenFileCommand());
            ListView_ItemSelectedCommand = new Command<AsortymentyModel>(Execute_OnItemSelected);
            ListView_RefreshCommand = new Command(async () => await ExecuteRefreshCommand());

            // Initialize the ObservableCollection
            Asortyment = new ObservableCollection<AsortymentyModel>();

            // Call method to populate data
            _ = PopulateData();
        }
        #endregion

        #region METHODS
        private async Task ExecuteButton_OpenFileCommand()
        {
            PickOptions options = new();
            await PickAndShow(options);
        }

        private void Execute_OnItemSelected(AsortymentyModel selectedItem)
        {
            // You can raise an event or call a method in the View
            ItemSelected?.Invoke(this, new ItemSelectedEventArgs(selectedItem));
        }

        private async Task ExecuteRefreshCommand()
        {
            try
            {
                // Set IsRefreshing to true to show the refresh animation
                IsRefreshing = true;

                // Your refresh logic goes here
                await PopulateData();
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
            }
            finally
            {
                // Set IsRefreshing to false to hide the refresh animation
                IsRefreshing = false;
            }
        }

        public async Task PopulateData()
        {
            // ...
            await Task.Run(async () =>
            {
                // Call your data service here (e.g., SqlServerService)
                // to get data and add it to DataItems
                // ...
                // Initialize DatabaseService
                Asortyment.Clear();
                DatabaseService _databaseService = new DatabaseService();
                List<AsortymentyModel> data = await _databaseService.GetYourDataAsync();
                foreach (var item in data)
                {
                    Asortyment.Add(item);
                }
            });
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

        #region EVENTS
        // Define an event to be raised when an item is selected
        public event EventHandler<ItemSelectedEventArgs> ItemSelected;
        #endregion
    }

    // Define custom EventArgs if needed
    public class ItemSelectedEventArgs : EventArgs
    {
        public AsortymentyModel SelectedItem { get; }

        public ItemSelectedEventArgs(AsortymentyModel selectedItem)
        {
            SelectedItem = selectedItem;
        }
    }
}
