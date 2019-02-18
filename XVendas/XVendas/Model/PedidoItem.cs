using SQLite.Net.Attributes;

namespace XVendas.Model
{
    public class PedidoItem
    {
        /*
         * identificar o cliente, a lista de produtos do pedido, e o total a pagar.
         * A cada pedido realizado, o sistema deve debitar a quantidade de produtos do estoque.
         */

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(18)]
        public int ItemId { get; set; }
        [MaxLength(18)]
        public int PedidoId { get; set; }
        [MaxLength(18)]
        public int ProdutoId { get; set; }
        [MaxLength(18)]
        public double Quant { get; set; }
        [MaxLength(18)]
        public double Preco { get; set; }
        [MaxLength(18)]
        public double Valor { get; set; }
    }
}
