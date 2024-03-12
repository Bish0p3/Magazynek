using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Magazynek.Helpers;
using Magazynek.Models;
using Magazynek.Services;
using System.Collections.ObjectModel;

namespace Magazynek.ViewModels
{
    internal class OrdersViewModel : ObservableObject
    {

        #region FIELDS

        private ObservableCollection<OrderModel> _umowy;
        public ObservableCollection<OrderModel> Umowy
        {
            get => _umowy;
            set
            {
                _umowy = value;
                OnPropertyChanged(nameof(Umowy));
            }
        }

        private ObservableCollection<OrderModel> _umowySearched = new ObservableCollection<OrderModel>();
        public ObservableCollection<OrderModel> UmowySearched
        {
            get => _umowySearched;
            set
            {
                _umowySearched = value;
                OnPropertyChanged(nameof(UmowySearched));
            }
        }

        private OrderModel _selectedUmowa;
        public OrderModel SelectedUmowa
        {
            get => _selectedUmowa;
            set
            {
                _selectedUmowa = value;
                OnPropertyChanged(nameof(SelectedUmowa));
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

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                var linqResults = Umowy.Where(s => s.Tytul.ToLower().Contains(_searchText.ToLower()));
                UmowySearched = new ObservableCollection<OrderModel>(linqResults);
                OnPropertyChanged(nameof(SearchText));
            }
        }
        #endregion

        #region CONSTRUCTOR
        public OrdersViewModel()
        {
            ListView_RefreshCommand = new Command(async () => await ExecuteRefreshCommand());

            // Initialize the ObservableCollection
            Umowy = new ObservableCollection<OrderModel>();


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
                Umowy.Clear();
                UmowySearched.Clear();
                DatabaseService _databaseService = new DatabaseService();
                List<OrderModel> data = await _databaseService.GetUmowyDataAsync();
                foreach (var item in data)
                {
                    Umowy.Add(item);
                    UmowySearched.Add(item);
                }
            });
        }
        #endregion
    }
}
