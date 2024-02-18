using Magazynek.ViewModels;
namespace Magazynek.Views;

public partial class MagazynPage : ContentPage
{
    public MagazynPage()
    {
        InitializeComponent();
        BindingContext = new ItemsViewModel();
    }
}