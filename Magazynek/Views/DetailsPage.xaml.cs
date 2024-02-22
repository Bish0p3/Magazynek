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

    }
    private void Rezerwacja_OnClick(object sender, EventArgs e)
    {
        //var viewModel = (ItemsViewModel)BindingContext;
        //viewModel.SelectedAsortyment = selectedAsortyment;

        // Optionally, navigate to details page or perform other actions
        Navigation.PushAsync(new NowaRezerwacjaPage() { Title = "Nowa rezerwacja" });
    }
}