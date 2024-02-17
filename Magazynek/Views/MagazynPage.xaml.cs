using Magazynek.ViewModels;
namespace Magazynek.Views;

public partial class MagazynPage : ContentPage
{
    public MagazynPage()
    {
        InitializeComponent();
        BindingContext = new ItemsViewModel();
    }

    private void Refresh_List(object sender, EventArgs e)
    {
        BindingContext = new ItemsViewModel();
    }

    private void Button_OpenFile(object sender, EventArgs e)
    {
        PickOptions options = new();
        ItemsViewModel itemsViewModel = new ItemsViewModel();
        _ = itemsViewModel.PickAndShow(options);
    }

}