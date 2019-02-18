using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XVendas.Model;

namespace XVendas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidosListPage : ContentPage
    {
        private int opcao;
        public ObservableCollection<Pedido> pedidos { get; set; }
        public double total;

        public PedidosListPage(int opcao)
        {
            InitializeComponent();
            this.opcao = opcao;
            pedidos = new ObservableCollection<Pedido>();
            if (opcao == 1) //por emissão
            {
                LblCliente.IsVisible = false;
                PedCliente.IsVisible = false;
                LblEmissaoIni.IsVisible = true;
                LblEmissaoFin.IsVisible = true;
                PedEmissaoIni.IsVisible = true;
                PedEmissaoFin.IsVisible = true;
            }
            else
            {
                LblCliente.IsVisible = true;
                PedCliente.IsVisible = true;
                LblEmissaoIni.IsVisible = false;
                LblEmissaoFin.IsVisible = false;
                PedEmissaoIni.IsVisible = false;
                PedEmissaoFin.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (opcao == 2) //por cliente
            {
                if (PedCliente.Items.Count == 0)
                {
                    List<Cliente> clientes = App.Database.GetClientes();
                    foreach (var cliente in clientes)
                        PedCliente.Items.Add(cliente.Id + " - " + cliente.Nome);
                }
            }
            listView.ItemsSource = this.pedidos;
        }

        async private void OnConsultarClicked(object sender, EventArgs e)
        {
            List<Pedido> pedidos;
            if (opcao == 1) //por emissão
            {
                DateTime EmissaoIni = PedEmissaoIni.Date;
                DateTime EmissaoFin = PedEmissaoFin.Date;

                if (EmissaoIni == null || EmissaoFin == null)
                {
                    await DisplayAlert("Erro", "Selecione um Período para continuar", "Voltar");
                    return;
                }
                pedidos = App.Database.GetPedidos(EmissaoIni, EmissaoFin);
            }
            else //por cliente
            {
                int clienteId = 0;
                int index = PedCliente.SelectedIndex;

                if (index != -1)
                    clienteId = Convert.ToInt32(PedCliente.Items[index].Split('-')[0]);
                else
                {
                    await DisplayAlert("Erro", "Selecione um Cliente para continuar", "Voltar");
                    return;
                }
                pedidos = App.Database.GetPedidos(clienteId);
            }
            App.Firebase.ListenerPedidos();

            total = 0;
            this.pedidos.Clear();
            foreach (var pedido in pedidos)
            {
                total += pedido.Valor;
                this.pedidos.Add(pedido);
            }
            PedValor.Text = String.Format("{0:#.##}", total);
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            Pedido pedido = new Pedido();
            await Navigation.PushAsync(new PedidoDetailPage(pedido, 1));
        }

        async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Pedido pedido = (Pedido)e.SelectedItem;
            if (pedido != null)
                await Navigation.PushAsync(new PedidoDetailPage(pedido, 4));
        }

    }
}