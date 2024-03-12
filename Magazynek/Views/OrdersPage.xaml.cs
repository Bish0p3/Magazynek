using Magazynek.Models;
using Magazynek.ViewModels;

namespace Magazynek.Views;

public partial class OrdersPage : ContentPage
{
    public OrdersPage()
    {
        InitializeComponent();
        BindingContext = new UmowyViewModel();
        list.SelectedItem = null;
    }

    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is OrderModel selectedUmowa)
        {
            var viewModel = (UmowyViewModel)BindingContext;
            viewModel.SelectedUmowa = selectedUmowa;

            // Change view to detailsPage
            Navigation.PushAsync(new DetailsPage(selectedUmowa) { Title = "Szczegó³y elementu" });
        }
    }

    // Searchbar
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue is not null)
        {
            var viewModel = (UmowyViewModel)BindingContext;
            viewModel.SearchText = e.NewTextValue;
        }
    }


    // No highlight fix
    private void ListItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null) return;

        if (sender is ListView lv) lv.SelectedItem = null;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = (UmowyViewModel)BindingContext;
        await viewModel.PopulateData();
    }
}