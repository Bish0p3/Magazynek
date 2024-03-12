using Magazynek.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Magazynek.ViewModels
{
    public class DetailsViewModel : INotifyPropertyChanged
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

        private ItemModel _selectedAsortyment;
        public ItemModel SelectedAsortyment
        {
            get { return _selectedAsortyment; }
            set
            {
                if (_selectedAsortyment != value)
                {
                    _selectedAsortyment = value;
                    OnPropertyChanged(nameof(SelectedAsortyment));
                }
            }
        }



        public DetailsViewModel()
        {
            SelectedAsortyment = new ItemModel();

            // Initialize or set SelectedItem as needed
            // For example: SelectedItem = new Item { Name = "Default Name", Description = "Default Description" };
        }



        public void CheckIfShouldBeVisible()
        {
            if (SelectedAsortyment.Ilosciowa is DBNull)
            {
                IsReservationVisible = false;
            }
            else
            {
                IsReservationVisible = true;
            }
        }



        // INotifyPropertyChanged implementation (you can use a base class if available)
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
