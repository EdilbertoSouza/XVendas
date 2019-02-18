using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XVendas.Model;

namespace XVendas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClientesListPage : ContentPage
	{
		public ClientesListPage ()
		{
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = App.Database.GetClientes();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            var cliente = new Cliente();
            await Navigation.PushAsync(new ClienteDetailPage(cliente, 1));
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var cliente = (Cliente)e.SelectedItem;
                await Navigation.PushAsync(new ClienteDetailPage(cliente, 2));
            }
        }

        async void OnDelClicked(object sender, EventArgs e)
        {
            var mi = (MenuItem)sender;
            var cliente = (Cliente)mi.CommandParameter;
            await Navigation.PushAsync(new ClienteDetailPage(cliente, 3));
        }

        async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
