using Magazynek.Models;
using Magazynek.ViewModels;
namespace Magazynek.Views;

public partial class MagazynPage : ContentPage
{
    public MagazynPage()
    {
        InitializeComponent();
        BindingContext = new ItemsViewModel();
    }
    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is AsortymentyModel selectedAsortyment)
        {
            var viewModel = (ItemsViewModel)BindingContext;
            viewModel.SelectedAsortyment = selectedAsortyment;

            // Optionally, navigate to details page or perform other actions
            Navigation.PushAsync(new DetailsPage(selectedAsortyment) { Title = "Szczegó³y elementu"});
        }
    }
}