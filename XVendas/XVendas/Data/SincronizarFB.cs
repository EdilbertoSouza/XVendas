using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using XVendas.Model;

namespace XVendas.Data
{
    public class SincronizarFB
    {        
        readonly FirebaseClient firebase;

        public SincronizarFB(String fbPath)
        {
            //Base remota com Realtime Database do Firebase
            firebase = new FirebaseClient(fbPath);
        }

        public void SincronizarTudo()
        {
            SincronizarProdutos();
            SincronizarClientes();
            SincronizarPedidos();
            ListenerProdutos();
            ListenerClientes();
            ListenerPedidos();
        }

        public void SincronizarProdutos()
        {
            List<Produto> produtos = App.Database.GetProdutos();
            foreach (var produto in produtos)
            {
                firebase.Child("produtos").Child(produto.Id.ToString).PutAsync(produto);
            }
        }

        public void SincronizarClientes()
        {
            List<Cliente> clientes = App.Database.GetClientes();
            foreach (var cliente in clientes)
            {
                firebase.Child("clientes").Child(cliente.Id.ToString).PutAsync(cliente);
            }
        }

        public void SincronizarPedidos()
        {
            List<Pedido> pedidos = App.Database.GetPedidos();
            foreach (var pedido in pedidos)
            {
                firebase.Child("pedidos").Child(pedido.Id.ToString).PutAsync(pedido);

                List<PedidoItem> pedidoItens = App.Database.GetPedidoItens(pedido.Id);
                foreach (var pedidoItem in pedidoItens)
                {
                    firebase.Child("pedidos").Child(pedido.Id.ToString).
                        Child("itens").Child(pedidoItem.ItemId.ToString).PutAsync(pedidoItem);
                }                
            }
        }

        public void ListenerProdutos()
        {
            firebase.Child("produtos").AsObservable<Produto>()
                .Subscribe(d =>
                {
                    if (d.EventType == FirebaseEventType.InsertOrUpdate)
                    {
                        if (d.Object != null)
                        {
                            Produto produto = App.Database.GetProduto(d.Object.Id);
                            if (produto != null && produto.Id == 0)
                            {
                                produto.Descricao = d.Object.Descricao;
                                produto.Preco = d.Object.Preco;
                                produto.Unid = d.Object.Unid;
                                produto.Estoque = d.Object.Estoque;
                                App.Database.SaveProduto(produto);
                            }
                            else
                                App.Database.SaveProduto(d.Object);
                        }
                    }
                    else if (d.EventType == FirebaseEventType.Delete)
                    {
                        App.Database.DeleteProduto(d.Object);
                    }
                });
        }

        public void ListenerClientes()
        {
            firebase.Child("clientes").AsObservable<Cliente>()
                .Subscribe(d =>
                {
                    if (d.EventType == FirebaseEventType.InsertOrUpdate)
                    {
                        if (d.Object != null)
                        {
                            Cliente cliente = App.Database.GetCliente(d.Object.Id);
                            if (cliente != null && cliente.Id == 0)
                            {
                                cliente.Nome = d.Object.Nome;
                                cliente.Email = d.Object.Email;
                                App.Database.SaveCliente(cliente);
                            }
                            else
                                App.Database.SaveCliente(d.Object);
                        }
                    }
                    else if (d.EventType == FirebaseEventType.Delete)
                    {
                        App.Database.DeleteCliente(d.Object);
                    }
                });
        }

        public void ListenerPedidos()
        {
            firebase.Child("pedidos").AsObservable<Pedido>()
                .Subscribe(d =>
                {
                    if (d.EventType == FirebaseEventType.InsertOrUpdate)
                    {
                        if (d.Object != null)
                        {
                            Pedido pedido = App.Database.GetPedido(d.Object.Id);
                            if (pedido != null && pedido.Id == 0)
                            {
                                pedido.ClienteId = d.Object.ClienteId;
                                pedido.Emissao = d.Object.Emissao;
                                pedido.Valor = d.Object.Valor;
                                App.Database.SavePedido(pedido);
                            }
                            else
                                App.Database.SavePedido(d.Object);
                        }
                    }
                    else if (d.EventType == FirebaseEventType.Delete)
                    {
                        App.Database.DeletePedido(d.Object);
                    }
                });
        }

    }
}
