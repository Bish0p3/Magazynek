using Magazynek.Models;
using Magazynek.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Magazynek.ViewModels
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        private AsortymentyModel _selectedAsortyment;

        public AsortymentyModel SelectedAsortyment
        {
            get { return _selectedAsortyment; }
            set
            {
                if (_selectedAsortyment != value)
                {
                    _selectedAsortyment = value;
                    OnPropertyChanged();
                }
            }
        }

        public DetailsViewModel()
        {
            SelectedAsortyment = new AsortymentyModel();
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
