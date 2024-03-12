using Magazynek.ViewModels;
namespace Magazynek.Views;

public partial class ItemDetailsPage : ContentPage
{
    public ItemDetailsPage(object selectedAsortyment)
    {
        InitializeComponent();

        BindingContext = new ItemDetailsViewModel();

        var viewModel = (ItemDetailsViewModel)BindingContext;
        viewModel.SelectedAsortyment = (Models.ItemModel)selectedAsortyment;
        viewModel.CheckIfShouldBeVisible();
    }
    private void Rezerwacja_OnClick(object sender, EventArgs e)
    {
        // JAK PRZEKAZAC DANE
        Navigation.PushAsync(new NewOrderPage() { Title = "Nowa rezerwacja" });
    }
}