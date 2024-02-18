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
}