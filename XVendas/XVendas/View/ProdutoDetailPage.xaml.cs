using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XVendas.Model;

namespace XVendas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProdutoDetailPage : ContentPage
	{
        private Produto produto;
        private int operacao;

        public ProdutoDetailPage(Produto c, int o)
        {
            InitializeComponent();
            this.produto = c;
            this.operacao = o;
            this.DescProduto.Text = produto.Descricao;
            this.UnidProduto.Text = produto.Unid;
            this.PrecoProduto.Text = produto.Preco.ToString();
            this.Estoque.Text = produto.Estoque.ToString();
        }

        async void OnConfirmClicked(object sender, EventArgs e)
        {
            produto.Descricao   = this.DescProduto.Text;
            produto.Unid        = this.UnidProduto.Text;
            produto.Preco       = Convert.ToDouble(this.PrecoProduto.Text);
            produto.Estoque     = Convert.ToDouble(this.Estoque.Text);
            if (operacao == 3)
                App.Database.DeleteProduto(produto);
            else
                App.Database.SaveProduto(produto);
            await Navigation.PopAsync();
        }

        async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}