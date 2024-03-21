using Magazynek.Helpers;
using Magazynek.Models;
using Magazynek.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Magazynek.ViewModels
{
    public class OrderDetailsViewModel : ObservableObject
    {
        private bool _isReservationVisible;
        public bool IsReservationVisible
        {
            get { return _isReservationVisible; }
            set
            {
                _isReservationVisible = value;
                OnPropertyChanged(nameof(IsReservationVisible));
            }
        }

        private OrderModel _selectedOrder;
        public OrderModel SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                if (_selectedOrder != value)
                {
                    _selectedOrder = value;
                    OnPropertyChanged(nameof(SelectedOrder));
                }
            }
        }

        private ObservableCollection<ItemModel> _selectedOrderItemsList;
        public ObservableCollection<ItemModel> SelectedOrderItemsList
        {
            get => _selectedOrderItemsList;
            set
            {
                _selectedOrderItemsList = value;
                OnPropertyChanged(nameof(SelectedOrderItemsList));
            }
        }


        public OrderDetailsViewModel()
        {
            SelectedOrder = new OrderModel();
            SelectedOrderItemsList = new ObservableCollection<ItemModel>();

            // Initialize or set SelectedItem as needed
            // For example: SelectedItem = new Item { Name = "Default Name", Description = "Default Description" };
        }

        public async Task PopulateData(OrderModel selectedOrder)
        {
            string order_id = selectedOrder.Id.ToString();

            await Task.Run(async () =>
            {
                // Call your data service here (e.g., SqlServerService)
                // to get data and add it to DataItems
                // ...
                // Initialize DatabaseService
                //SelectedOrderItemsList.Clear();
                DatabaseService _databaseService = new();
                List<ItemModel> data = await _databaseService.GetSelectedOrderItemsList(order_id);
                foreach (var item in data)
                {
                    SelectedOrderItemsList.Add(item);
                }
            });
        }
    }
}
