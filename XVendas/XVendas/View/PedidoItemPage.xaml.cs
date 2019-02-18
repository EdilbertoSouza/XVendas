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
	public partial class PedidoItemPage : ContentPage
	{
        private PedidoItem pedidoItem;
        private int operacao;
        private ListView listView;
        public ObservableCollection<PedidoItemAux> pedidoItensAux { get; set; }

        public PedidoItemPage()
        {
            InitializeComponent();
        }

        public PedidoItemPage(PedidoItem pi, int o, ref ListView aux)
        {
            InitializeComponent();

            operacao = o;
            pedidoItem = pi;
            listView = aux;
            pedidoItensAux = (ObservableCollection<PedidoItemAux>)aux.ItemsSource;
        }    

        protected override void OnAppearing()
        {
            int index = 0;
            List<Produto> produtos = App.Database.GetProdutos();
            foreach (var produto in produtos)
            {
                IteProduto.Items.Add(produto.Id + " - " + produto.Descricao);
                if (pedidoItem.ProdutoId == produto.Id)
                {
                    IteProduto.SelectedIndex = index;
                }
                index++;
            }

            if (operacao > 1) // Alteração, Exclusão ou Consulta
            {
                IteQuant.Text = pedidoItem.Quant.ToString();
                ItePreco.Text = pedidoItem.Preco.ToString();
                IteValor.Text = pedidoItem.Valor.ToString();
            }
        }

        async void OnConfirmClicked(object sender, EventArgs e)
        {
            int index = IteProduto.SelectedIndex;
            if (index != -1)
                pedidoItem.ProdutoId = Convert.ToInt32(IteProduto.Items[index].Split('-')[0]);
            else
            {
                await DisplayAlert("Erro","Selecione o Produto para Continuar","Voltar");
                return;
            }
            pedidoItem.Quant = Convert.ToDouble(IteQuant.Text);
            pedidoItem.Preco = Convert.ToDouble(ItePreco.Text);
            pedidoItem.Valor = pedidoItem.Quant * pedidoItem.Preco;

            PedidoItemAux pedidoItemAux = new PedidoItemAux(pedidoItem);

            if (operacao == 1) //Inclusão
            {
                pedidoItensAux.Add(pedidoItemAux);
            }                
            else if (operacao == 2) //Alteração
            {                
                index = FindIndex(pedidoItem.Id);
                if (index != -1)
                {
                    pedidoItensAux[index].ProdutoId = pedidoItem.ProdutoId;
                    pedidoItensAux[index].Quant     = pedidoItem.Quant;
                    pedidoItensAux[index].Preco     = pedidoItem.Preco;
                    pedidoItensAux[index].Valor     = pedidoItem.Valor;
                    pedidoItensAux[index].Produto   = pedidoItemAux.Produto;
                }
            }
            else if (operacao == 3) //Exclusão
            {
                int id = FindIndex(pedidoItem.Id);
                pedidoItensAux.RemoveAt(id);
            }
            if (operacao != 4)
            {
                listView.EndRefresh();
            }
            await Navigation.PopAsync();
        }

        private int FindIndex(int id)
        {
            int index = -1;
            foreach (var item in pedidoItensAux)
            {
                if (item.Id == id)
                    index = item.ItemId - 1;
            }
            return index;
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}