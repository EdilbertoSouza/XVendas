using SQLite.Net.Attributes;
using System;

namespace XVendas.Model
{
    public class Pedido
    {
        /*
         * identificar o cliente, a lista de produtos do pedido, e o total a pagar.
         * A cada pedido realizado, o sistema deve debitar a quantidade de produtos do estoque.
         */

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Emissao { get; set; }
        [MaxLength(18)]
        public int ClienteId { get; set; }
        [MaxLength(18)]
        public double Valor { get; set; }
    }
}
