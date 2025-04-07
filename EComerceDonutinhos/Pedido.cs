namespace EComerceDonutinhos
{
    public class Pedido
    {
        public int Id { get; set; } 
        public string Status { get; set; } 
        public List<PedidoItem> Itens { get; set; } = new List<PedidoItem>();  

        public DateTime DataCadastro { get; set; }
    }
}
