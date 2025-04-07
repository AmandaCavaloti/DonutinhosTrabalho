namespace EComerceDonutinhos
{
    public class PedidoItem
    {
        public int Id { get; set; } 
        public int DonutId { get; set; } 
        public int PedidoId { get; set; } 
        public int Quantidade { get; set; }  
        public Donut Donut { get; set; }
    }
}
