using Magazynek.Models;
using Magazynek.ViewModels;
using System.Collections.ObjectModel;

namespace Magazynek.Views;

public partial class NewOrderPage : ContentPage
{

    public NewOrderPage()
    {
        InitializeComponent();
        BindingContext = new NewOrderViewModel();

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await App.Current.MainPage.DisplayAlert("Nowe zam�wienie", "Zam�wienie zosta�o zapisane", "OK");
        await Navigation.PushAsync(new OrdersPage() { Title = "Zam�wienia" });
    }
}