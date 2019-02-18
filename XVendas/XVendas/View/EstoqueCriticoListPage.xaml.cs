using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XVendas.Model;

namespace XVendas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstoqueCriticoListPage : ContentPage
    {
        public EstoqueCriticoListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = App.Database.GetEstoqueCritico();
        }

        async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var produto = (Produto)e.SelectedItem;
                await Navigation.PushAsync(new ProdutoDetailPage(produto, 4));
            }
        }
    }
}