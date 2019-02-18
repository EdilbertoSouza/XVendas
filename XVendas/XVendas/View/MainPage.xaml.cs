using System;
using Xamarin.Forms;
using XVendas.Model;
using XVendas.View;

namespace XVendas
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //FirebaseAuthentication();
        }

        async void FirebaseAuthentication()
        {
            await Navigation.PushAsync(new FirebaseAuthenticationPage());
        }

        async void ClientesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientesListPage());
        }

        async void ProdutosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProdutosListPage());
        }

        async void VendasClicked(object sender, EventArgs e)
        {
            Pedido pedido = new Pedido();
            await Navigation.PushAsync(new PedidoDetailPage(pedido, 1));
        }

        async void ConsPedEmiClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PedidosListPage(1));
        }

        async void ConsPedCliClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PedidosListPage(2));
        }

        async void ConsEstCriClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EstoqueCriticoListPage());
        }

        void SincronizarClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new SincronizarPage());
            App.Firebase.SincronizarTudo();
        }

    }
}
