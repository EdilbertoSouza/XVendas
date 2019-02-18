using System;
using System.Collections.Generic;
using System.Text;

namespace XVendas.Model
{
    public class PedidoItemAux
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public double Quant { get; set; }
        public double Preco { get; set; }
        public double Valor { get; set; }
        public string Produto { get; set; }
        public string Info { get; set; }

        public PedidoItemAux(PedidoItem pedidoItem)
        {
            this.Id         = pedidoItem.Id;
            this.ItemId     = pedidoItem.ItemId;
            this.PedidoId   = pedidoItem.PedidoId;
            this.ProdutoId  = pedidoItem.ProdutoId;
            this.Quant      = pedidoItem.Quant;
            this.Preco      = pedidoItem.Preco;
            this.Valor      = pedidoItem.Valor;

            if (pedidoItem.ProdutoId > 0)
            {
                string produtoId = pedidoItem.ProdutoId.ToString();
                Produto produto = App.Database.GetProduto(pedidoItem.ProdutoId);
                string produtoDs = produto.Descricao;
                string produtoUn = produto.Unid;
                string quant = pedidoItem.Quant.ToString();
                string preco = pedidoItem.Preco.ToString();
                string valor = pedidoItem.Valor.ToString();

                this.Produto = produtoId + " - " + produtoDs;
                this.Info = String.Format("{0:D} {1:D} x {2:#.##} = {3:#.##}", quant, produtoUn, preco, valor);
            }
        }

        public PedidoItem getPedidoItem()
        {
            PedidoItem pedidoItem = new PedidoItem();

            pedidoItem.Id       = this.Id;
            pedidoItem.ItemId   = this.ItemId;
            pedidoItem.PedidoId = this.PedidoId;
            pedidoItem.ProdutoId= this.ProdutoId;
            pedidoItem.Quant    = this.Quant;
            pedidoItem.Preco    = this.Preco;
            pedidoItem.Valor    = this.Valor;

            return pedidoItem;
        }

    }
}
