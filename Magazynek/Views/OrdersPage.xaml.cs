using Magazynek.Models;
using Magazynek.ViewModels;

namespace Magazynek.Views;

public partial class OrdersPage : ContentPage
{
    public OrdersPage()
    {
        InitializeComponent();
        BindingContext = new OrdersViewModel();
        list.SelectedItem = null;
    }

    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is OrderModel selectedUmowa)
        {
            var viewModel = (OrdersViewModel)BindingContext;
            viewModel.SelectedUmowa = selectedUmowa;

            // Change view to detailsPage
            Navigation.PushAsync(new OrderDetailsPage(selectedUmowa) { Title = "Szczegó³y elementu" });
        }
    }

    // Searchbar
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue is not null)
        {
            var viewModel = (OrdersViewModel)BindingContext;
            viewModel.SearchText = e.NewTextValue;
        }
    }


    // No highlight fix
    private void ListItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null) return;

        if (sender is ListView lv) lv.SelectedItem = null;
    }

    // Warehouse selection
    private async void WarehousePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            var viewModel = (OrdersViewModel)BindingContext;
            await viewModel.PopulateData(selectedIndex);
        }
    }
    private void Rezerwacja_OnClick(object sender, EventArgs e)
    {
        // JAK PRZEKAZAC DANE
        Navigation.PushAsync(new NewOrderPage() { Title = "Nowa rezerwacja" });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = (OrdersViewModel)BindingContext;
        await viewModel.PopulateData();
    }
}