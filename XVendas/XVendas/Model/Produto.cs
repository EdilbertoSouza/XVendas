using SQLite.Net.Attributes;

namespace XVendas.Model
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Descricao { get; set; }
        [MaxLength(2)]
        public string Unid { get; set; }
        [MaxLength(18)]
        public double Preco { get; set; }
        [MaxLength(18)]
        public double Estoque { get; set; }
    }
}
