namespace EComerceDonutinhos
{
    public class Donut
    {
        public int Id { get; set; }
        public string Sabor { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public List<PedidoItem> Itens { get; set; } = new List<PedidoItem>();  
    }
}
