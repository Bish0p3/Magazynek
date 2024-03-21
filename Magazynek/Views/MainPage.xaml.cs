namespace Magazynek.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    private void Magazyny_OnClick(object sender, EventArgs e)
    {
        Navigation.PushAsync(new WarehousesPage() { Title = "Magazyny" });
    }
    private void Rezerwacje_OnClick(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OrdersPage() { Title = "Zamówienia" });
    }
    private void Ustawienia_OnClick(object sender, EventArgs e)
    {
        Navigation.PushAsync(new WarehousesPage() { Title = "Ustawienia" });
    }
    private void Wyloguj_OnClick(object sender, EventArgs e)
    {
        Application.Current.MainPage = new Magazynek.Views.LoginPage();
    }
    private void Author_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AuthorPage() { Title = "O autorze" });
    }
    private void Help_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new HelpPage() { Title = "Strona pomocy" });
    }
}