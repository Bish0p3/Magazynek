using Magazynek.Helpers;
using Magazynek.Models;
using Magazynek.Services;
using Magazynek.Views;
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

        public ICommand ListView_RefreshCommand { get; }

        private AsortymentyModel _selectedAsortyment;
        public AsortymentyModel SelectedAsortyment
        {
            get => _selectedAsortyment;
            set
            {
                _selectedAsortyment = value;
                OnPropertyChanged(nameof(SelectedAsortyment));
            }
        }
        #endregion

        #region CONSTRUCTOR
        public ItemsViewModel()
        {
            ListView_RefreshCommand = new Command(async () => await ExecuteRefreshCommand());

            // Initialize the ObservableCollection
            Asortyment = new ObservableCollection<AsortymentyModel>();

            // Call method to populate data
            _ = PopulateData();
        }
        #endregion

        #region METHODS
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
                Console.Write(ex.ToString());
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

        #endregion



    }

}
