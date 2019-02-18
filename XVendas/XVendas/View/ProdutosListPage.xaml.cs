using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XVendas.Model;

namespace XVendas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProdutosListPage : ContentPage
	{
		public ProdutosListPage ()
		{
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = App.Database.GetProdutos();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            var produto = new Produto();
            await Navigation.PushAsync(new ProdutoDetailPage(produto, 1));
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var produto = (Produto)e.SelectedItem;
                await Navigation.PushAsync(new ProdutoDetailPage(produto, 2));
            }
        }

        async void OnDelClicked(object sender, EventArgs e)
        {
            var mi = (MenuItem)sender;
            var produto = (Produto)mi.CommandParameter;
            await Navigation.PushAsync(new ProdutoDetailPage(produto, 3));
        }

        async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}
