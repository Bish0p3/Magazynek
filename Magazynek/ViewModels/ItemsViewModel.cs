using Magazynek.Helpers;
using Magazynek.Models;
using Magazynek.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Magazynek.ViewModels
{
    public class ItemsViewModel : ObservableObject
    {
        #region FIELDS
        private ObservableCollection<ItemModel> _asortyment;
        public ObservableCollection<ItemModel> Asortyment
        {
            get => _asortyment;
            set
            {
                _asortyment = value;
                OnPropertyChanged(nameof(Asortyment));
            }
        }

        private ObservableCollection<ItemModel> _asortymentSearched = new ObservableCollection<ItemModel>();
        public ObservableCollection<ItemModel> AsortymentSearched
        {
            get => _asortymentSearched;
            set
            {
                _asortymentSearched = value;
                OnPropertyChanged(nameof(AsortymentSearched));
            }
        }

        private ObservableCollection<WarehouseNamesModel> _magazynySelection;
        public ObservableCollection<WarehouseNamesModel> MagazynySelection
        {
            get { return _magazynySelection; }
            set
            {
                _magazynySelection = value;
                OnPropertyChanged(nameof(MagazynySelection));
            }
        }

        private int _selectedMagazyn = 0;
        public int SelectedMagazyn
        {
            get { return _selectedMagazyn; }
            set
            {
                if (_selectedMagazyn != value)
                {
                    _selectedMagazyn = value;
                    OnPropertyChanged(nameof(SelectedMagazyn));
                }
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

        private ItemModel _selectedAsortyment;
        public ItemModel SelectedAsortyment
        {
            get => _selectedAsortyment;
            set
            {
                _selectedAsortyment = value;
                OnPropertyChanged(nameof(SelectedAsortyment));
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                var linqResults = Asortyment.Where(s => s.Symbol.ToLower().Contains(_searchText.ToLower()));
                AsortymentSearched.Clear();
                AsortymentSearched = new ObservableCollection<ItemModel>(linqResults);
                OnPropertyChanged(nameof(SearchText));
            }
        }
        #endregion

        #region CONSTRUCTOR
        public ItemsViewModel()
        {
            ListView_RefreshCommand = new Command(async () => await ExecuteRefreshCommand());

            // Initialize the ObservableCollection
            Asortyment = new ObservableCollection<ItemModel>();

            MagazynySelection = new ObservableCollection<WarehouseNamesModel>
            {
                new WarehouseNamesModel { Nazwa = "Magazyn Główny" },
                new WarehouseNamesModel { Nazwa = "W transporcie" },
                new WarehouseNamesModel { Nazwa = "W produkcji" },
            };

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

        public async Task PopulateData(int selectedIndex=0)
        {
            // ...
            await Task.Run(async () =>
            {
                // Call your data service here (e.g., SqlServerService)
                // to get data and add it to DataItems
                // ...
                // Initialize DatabaseService
                Asortyment.Clear();
                AsortymentSearched.Clear();
                DatabaseService _databaseService = new();
                List<ItemModel> data = await _databaseService.GetAsortymentDataAsync(selectedIndex);
                foreach (var item in data)
                {
                    Asortyment.Add(item);
                    AsortymentSearched.Add(item);
                }
            });
        }

        #endregion



    }

}
