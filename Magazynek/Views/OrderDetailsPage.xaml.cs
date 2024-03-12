using Magazynek.ViewModels;
namespace Magazynek.Views;

public partial class DetailsPage : ContentPage
{
    public DetailsPage(object selectedAsortyment)
    {
        InitializeComponent();

        BindingContext = new DetailsViewModel();

        var viewModel = (DetailsViewModel)BindingContext;
        viewModel.SelectedAsortyment = (Models.AsortymentyModel)selectedAsortyment;
        viewModel.CheckIfShouldBeVisible();
    }
    private void Rezerwacja_OnClick(object sender, EventArgs e)
    {
        // JAK PRZEKAZAC DANE
        Navigation.PushAsync(new NewOrderPage() { Title = "Nowa rezerwacja" });
    }
}