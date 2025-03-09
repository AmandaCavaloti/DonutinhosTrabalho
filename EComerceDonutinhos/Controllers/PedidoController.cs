using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EComerceDonutinhos.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Donut) // Inclui os donuts nos itens
                .ToListAsync();

            return View(pedidos);
        }
    }
}