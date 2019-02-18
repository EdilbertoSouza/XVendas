using DLToolkit.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XVendas.Model;

namespace XVendas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PedidoDetailPage : ContentPage
	{
        private Pedido pedido;
        private int operacao;
        public ObservableCollection<PedidoItemAux> pedidoItensAux { get; set; }

        public PedidoDetailPage()
        {
            InitializeComponent();
        }

        public PedidoDetailPage(Pedido p, int o)
        {
            InitializeComponent();

            operacao = o;
            pedido = p;
            pedidoItensAux = new ObservableCollection<PedidoItemAux>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            int index = 0;
            List<Cliente> clientes = App.Database.GetClientes();
            foreach (var cliente in clientes)
            {
                PedClienteId.Items.Add(cliente.Id + " - " + cliente.Nome);
                if (pedido.ClienteId == cliente.Id)
                    PedClienteId.SelectedIndex = index;
                index++;
            }

            if (operacao == 1)
                PedEmissao.Date = DateTime.Now;
            else
            {
                pedido.Valor = 0;
                PedId.Text = pedido.Id.ToString();
                PedEmissao.Date = pedido.Emissao;
                List<PedidoItem> pedidoItens = App.Database.GetPedidoItens(pedido.Id);
                foreach (var pedidoItem in pedidoItens)
                {
                    pedido.Valor += pedidoItem.Valor;
                    PedidoItemAux pedidoItemAux = new PedidoItemAux(pedidoItem);
                    pedidoItensAux.Add(pedidoItemAux);
                }
                PedValor.Text = pedido.Valor.ToString();
            }
            listView.ItemsSource = pedidoItensAux;
        }
        
        async void OnConfirmClicked(object sender, EventArgs e)
        {
            pedido.Emissao = PedEmissao.Date;
            
            int index = PedClienteId.SelectedIndex;
            if (index != -1)
                pedido.ClienteId = Convert.ToInt32(PedClienteId.Items[index].Split('-')[0]);
            else
            {
                await DisplayAlert("Erro", "Selecione um Cliente para continuar", "Voltar");
                return;
            }
            pedido.Valor = Convert.ToDouble(PedValor.Text);

            if (operacao == 1 || operacao == 2)
            {
                App.Database.SavePedido(pedido);
                int itemId = 1;
                foreach (var pedidoItemAux in pedidoItensAux)
                {
                    if (pedidoItemAux.ProdutoId > 0)
                    {
                        PedidoItem pedidoItem = pedidoItemAux.getPedidoItem();
                        if (pedidoItem.ItemId < 1)
                            pedidoItem.ItemId = itemId++;
                        if (operacao == 1)
                            pedidoItem.PedidoId = pedido.Id;
                        else
                        {
                            App.Database.EntradaEstoque(pedidoItem.ProdutoId, pedidoItem.Quant);
                            App.Database.DeletePedidoItem(pedidoItem);
                            pedidoItem.Id = -1;
                        }                            
                        App.Database.SavePedidoItem(pedidoItem);
                        App.Database.SaidaEstoque(pedidoItem.ProdutoId, pedidoItem.Quant);
                    }
                }
            }
            if (operacao == 3)
            {
                App.Database.DeletePedido(pedido);
                foreach (var pedidoItemAux in pedidoItensAux)
                {
                    if (pedidoItemAux.ProdutoId > 0)
                    {
                        PedidoItem pedidoItem = pedidoItemAux.getPedidoItem();
                        App.Database.EntradaEstoque(pedidoItem.ProdutoId, pedidoItem.Quant);
                        App.Database.DeletePedidoItem(pedidoItem);
                    }
                }
            }
            await Navigation.PopAsync();
        }

        async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void OnAddItemClicked(object sender, EventArgs e)
        {
            PedidoItem pedidoItem = new PedidoItem();
            if (operacao != 4)
            {
                await Navigation.PushAsync(new PedidoItemPage(pedidoItem, 1, ref listView));
            }
            PedValor.Text = CalculateValue().ToString();
        }

        async private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            PedidoItemAux pedidoItemAux = (PedidoItemAux)e.SelectedItem;
            PedidoItem pedidoItem = pedidoItemAux.getPedidoItem();
            if (operacao != 4 && pedidoItem != null)
                await Navigation.PushAsync(new PedidoItemPage(pedidoItem, 2, ref listView));
            PedValor.Text = CalculateValue().ToString();
        }

        async void OnDelItemClicked(object sender, EventArgs e)
        {
            var mi = (MenuItem)sender;
            var pedidoItem = (PedidoItem)mi.CommandParameter;
            if (operacao != 4 && pedidoItem != null)
                await Navigation.PushAsync(new PedidoItemPage(pedidoItem, 3, ref listView));
            PedValor.Text = CalculateValue().ToString();
        }

        private double CalculateValue()
        {
            double valor = 0.00;
            foreach (var item in pedidoItensAux)
            {
                if (item.ProdutoId > 0)
                    valor += item.Valor;
            }
            return valor;
        }

    }
}