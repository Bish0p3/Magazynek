using Magazynek.Helpers;
using Magazynek.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Magazynek.ViewModels
{
    public class ItemDetailsViewModel : ObservableObject
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



        public ItemDetailsViewModel()
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
    }
}
