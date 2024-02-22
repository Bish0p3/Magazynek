namespace Magazynek.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
    private void Magazyny_OnClick(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MagazynPage() { Title = "Magazyny" });
    }
    private void Rezerwacje_OnClick(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MagazynPage() { Title = "Magazyny" });
    }
    private void Ustawienia_OnClick(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MagazynPage() { Title = "Magazyny" });
    }
    private void Wyloguj_OnClick(object sender, EventArgs e)
    {
        //Navigation.PushAsync(new MagazynPage() { Title = "Magazyny" });
    }
}