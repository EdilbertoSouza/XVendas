using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XVendas.Data;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XVendas
{
    public partial class App : Application
    {
        static XVendasDB database;
        static SincronizarFB firebase;

        public App()
        {
            InitializeComponent();

            Resources = new ResourceDictionary();
            Resources.Add("primaryGreen", Color.FromHex("91CA47"));
            Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));

            var page = new NavigationPage(new MainPage());
            page.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
            page.BarTextColor = Color.White;

            MainPage = page;
        }

        public static XVendasDB Database
        {
            get
            {
                if (database == null)
                {
                    database = new XVendasDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XVendas.db"));
                }
                return database;
            }
        }

        public static SincronizarFB Firebase
        {
            get
            {
                if (firebase == null)
                {
                    firebase = new SincronizarFB("https://xvendas-uni7pos.firebaseio.com/");
                }
                return firebase;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
