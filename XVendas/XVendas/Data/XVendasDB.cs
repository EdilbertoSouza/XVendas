using System.Collections.Generic;
using SQLite.Net;
using XVendas.Model;
using PCLExt.FileStorage.Folders;
using PCLExt.FileStorage;
using Xamarin.Forms;
using System.Linq;
using System;
using Firebase.Database;
using Firebase.Database.Query;

namespace XVendas.Data
{
    public class XVendasDB
    {
        readonly SQLiteConnection database;

        public XVendasDB(string dbPath)
        {
            //Base local com SQLite
            //database = new SQLiteConnection(dbPath);
            IFolder pasta = new LocalRootFolder();
            IFile arquivo = pasta.CreateFile("XVendas.db", CreationCollisionOption.OpenIfExists);
            var config = DependencyService.Get<IConfig>();
            database = new SQLiteConnection(config.Plataforma, arquivo.Path);
            database.CreateTable<Cliente>();
            database.CreateTable<Produto>();
            database.CreateTable<Pedido>();
            //database.DropTable<PedidoItem>();
            database.CreateTable<PedidoItem>();
        }

        public List<Cliente> GetClientes()
        {
            return database.Table<Cliente>().OrderBy(c => c.Nome).ToList();
        }

        public Cliente GetCliente(int id)
        {
            return database.Table<Cliente>().FirstOrDefault(c => c.Id == id);
        }

        public void SaveCliente(Cliente cliente)
        {
            if (cliente.Id != 0)
            {
                database.Update(cliente);
            }
            else
            {
                database.Insert(cliente);
            }
        }

        public void DeleteCliente(Cliente cliente)
        {
            database.Delete(cliente);
        }

        public List<Produto> GetProdutos()
        {
            return database.Table<Produto>().OrderBy(p => p.Descricao).ToList();
        }

        public List<Produto> GetEstoqueCritico()
        {
            return database.Table<Produto>().Where(p => p.Estoque < 10).OrderBy(p => p.Estoque).ToList();
        }

        public Produto GetProduto(int id)
        {
            return database.Table<Produto>().FirstOrDefault(p => p.Id == id);            
        }

        public void SaveProduto(Produto produto)
        {
            if (produto.Id != 0)
            {
                database.Update(produto);
            }
            else
            {
                database.Insert(produto);
            }
        }

        public void DeleteProduto(Produto produto)
        {
            database.Delete(produto);
        }

        // Pedidos e Itens de Pedido
        public List<Pedido> GetPedidos()
        {
            return database.Table<Pedido>().OrderBy(p => p.Id).ToList();
        }

        public List<Pedido> GetPedidos(int ClienteId)
        {
            return database.Table<Pedido>().Where(p => p.ClienteId == ClienteId).OrderBy(p => p.Id).ToList();
        }

        public List<Pedido> GetPedidos(DateTime dtInicial, DateTime dtFinal)
        {
            return database.Table<Pedido>().Where(p => p.Emissao >= dtInicial && p.Emissao <= dtFinal).OrderBy(p => p.Id).ToList();
        }        

        public Pedido GetPedido(int id)
        {
            return database.Table<Pedido>().FirstOrDefault(p => p.Id == id);
        }

        public void SavePedido(Pedido pedido)
        {
            if (pedido.Id != 0)
            {
                database.Update(pedido);
            }
            else
            {
                database.Insert(pedido);
            }
        }

        public void DeletePedido(Pedido pedido)
        {
            database.Delete(pedido);
        }

        public List<PedidoItem> GetPedidoItens()
        {
            return database.Table<PedidoItem>().OrderBy(pi => pi.Id).ToList();
        }

        public List<PedidoItem> GetPedidoItens(int PedidoId)
        {
            return database.Table<PedidoItem>().Where(pi => pi.PedidoId == PedidoId).OrderBy(pi => pi.ItemId).ToList();
        }

        public void SavePedidoItem(PedidoItem pedidoItem)
        {
            if (pedidoItem.Id != 0)
            {
                database.Update(pedidoItem);
            }
            else
            {
                database.Insert(pedidoItem);
            }
        }

        public void DeletePedidoItem(PedidoItem pedidoItem)
        {
            database.Delete(pedidoItem);
        }

        public void SaidaEstoque(int produtoId, double quant)
        {
            database.Execute("UPDATE Produto SET Estoque = Estoque - ? WHERE Id = ?", quant, produtoId);            
        }

        public void EntradaEstoque(int produtoId, double quant)
        {
            database.Execute("UPDATE Produto SET Estoque = Estoque + ? WHERE Id = ?", quant, produtoId);
        }

    }
}