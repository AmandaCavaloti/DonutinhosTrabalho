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


        [HttpPost]
        public IActionResult Editar(Pedido pedido)
        {
            if (pedido.Itens == null || pedido.Itens.All(i => i.Quantidade == 0))
            {
                TempData["Mensagem"] = "Selecione pelo menos um donut antes de salvar.";
                return RedirectToAction("Index");
            }

            var pedidoExistente = _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Donut)
                .FirstOrDefault(p => p.Id == pedido.Id);

            if (pedidoExistente == null)
            {
                TempData["Mensagem"] = "Pedido não encontrado.";
                return RedirectToAction("Index");
            }

            // Atualiza os itens: remove os antigos e adiciona os novos
            _context.PedidoItens.RemoveRange(pedidoExistente.Itens);
            pedidoExistente.Itens = pedido.Itens.Where(i => i.Quantidade > 0).ToList();

            pedidoExistente.Status = pedido.Status ?? pedidoExistente.Status;
            pedidoExistente.DataCadastro = pedidoExistente.DataCadastro; // mantém a data antiga

            _context.SaveChanges();

            TempData["Mensagem"] = "Pedido atualizado com sucesso!";
            return RedirectToAction("Index");
        }




        [HttpPost]
        public IActionResult Concluir(int id)
        {
            var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido == null)
            {
                TempData["Mensagem"] = "Pedido não encontrado.";
                return RedirectToAction("Index");
            }

            pedido.Status = "Finalizado";
            _context.SaveChanges();

            TempData["Mensagem"] = "Pedido marcado como finalizado!";
            return RedirectToAction("Index");
        }
    }
}