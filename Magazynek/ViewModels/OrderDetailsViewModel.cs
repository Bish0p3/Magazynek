using Magazynek.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Magazynek.ViewModels
{
    public class OrderDetailsViewModel : INotifyPropertyChanged
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



        public OrderDetailsViewModel()
        {
            SelectedOrder = new OrderModel();

            // Initialize or set SelectedItem as needed
            // For example: SelectedItem = new Item { Name = "Default Name", Description = "Default Description" };
        }


        // INotifyPropertyChanged implementation (you can use a base class if available)
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
