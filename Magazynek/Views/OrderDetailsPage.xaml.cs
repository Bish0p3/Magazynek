using Magazynek.ViewModels;
namespace Magazynek.Views;

public partial class OrderDetailsPage : ContentPage
{
    public OrderDetailsPage(object selectedOrder)
    {
        InitializeComponent();

        BindingContext = new OrderDetailsViewModel();

        var viewModel = (OrderDetailsViewModel)BindingContext;
        viewModel.SelectedOrder = (Models.OrderModel)selectedOrder;
        _ = viewModel.PopulateData((Models.OrderModel)selectedOrder);
    }
    private void Rezerwacja_OnClick(object sender, EventArgs e)
    {
        // JAK PRZEKAZAC DANE
        Navigation.PushAsync(new NewOrderPage() { Title = "Nowa rezerwacja" });
    }
}