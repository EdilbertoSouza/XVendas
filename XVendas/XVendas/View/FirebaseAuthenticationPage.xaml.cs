using System;
using Xamarin.Forms;
using XVendas.Interface;

namespace XVendas
{
    public partial class FirebaseAuthenticationPage : ContentPage
	{
        public FirebaseAuthenticationPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (CheckValidations())
            {                
                //var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaxxxxxxxxxxxxxxXE"));
                //var auth = await authProvider.SignInWithEmailAndPasswordAsync("pal@gmail.com", "abc@1234");
                //var token = await DependencyService.Get<IFirebaseAuthenticator>().RegisterWithEmailPassword(txtEmail.Text, txtPassword.Text);
            }

        }

        async void Login_Cicked(object sender, System.EventArgs e)
        {
            if (CheckValidations())
            {
                try
                {
                    var token = await DependencyService.Get<IFirebaseAuthenticator>().LoginWithEmailPassword(txtEmail.Text.Trim(), txtPassword.Text.Trim());
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Atenção", ex.Message, "ok");
                }
            }

        }
        private bool CheckValidations()
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                DisplayAlert("Atenção", "Entre com o seu email", "ok");
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                DisplayAlert("Atenção", "Entre com a sua senha", "ok");
                return false;
            }
            return true;
        }
    }

}