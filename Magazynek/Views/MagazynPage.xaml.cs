using Magazynek.Models;
using Magazynek.ViewModels;
using System.Collections.ObjectModel;

namespace Magazynek.Views;

public partial class MagazynPage : ContentPage
{
    public MagazynPage()
    {
        InitializeComponent();
        BindingContext = new ItemsViewModel();
        list.SelectedItem = null;
    }

    // ListView item selected
    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is AsortymentyModel selectedAsortyment)
        {
            var viewModel = (ItemsViewModel)BindingContext;
            viewModel.SelectedAsortyment = selectedAsortyment;

            // Change view to detailsPage
            Navigation.PushAsync(new DetailsPage(selectedAsortyment) { Title = "Szczeg�y elementu"});
        }
    }

    // Searchbar
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue is not null)
        {
            var viewModel = (ItemsViewModel)BindingContext;
            viewModel.SearchText = e.NewTextValue;
        }
    }

    // No highlight fix
    private void ListItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null) return;

        if (sender is ListView lv) lv.SelectedItem = null;
    }
}