using Magazynek.ViewModels;

namespace Magazynek.Views
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new AppShell();
        }
    }
}