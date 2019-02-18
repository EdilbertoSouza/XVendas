using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XVendas.Model;

namespace XVendas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClienteDetailPage : ContentPage
	{
        private Cliente cliente;
        private int operacao;

        public ClienteDetailPage(Cliente c, int o)
        {
            InitializeComponent();
            this.cliente = c;
            this.operacao = o;
            this.NomeCliente.Text = cliente.Nome;
            this.EmailCliente.Text = cliente.Email;
        }

        async void OnConfirmClicked(object sender, EventArgs e)
        {
            cliente.Nome = this.NomeCliente.Text;
            cliente.Email = this.EmailCliente.Text;
            if (operacao == 3)
                App.Database.DeleteCliente(cliente);
            else
                App.Database.SaveCliente(cliente);
            await Navigation.PopAsync();
        }

        async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}