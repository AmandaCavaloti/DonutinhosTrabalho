using EComerceDonutinhos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EComerceDonutinhos.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Donut> donuts = _context.Donuts.ToList();

            return View(donuts);
        }

        [HttpPost]
        public IActionResult EnviarPedido(Pedido pedido)
        {
            if (pedido.Itens == null || pedido.Itens.All(i => i.Quantidade == 0))
            {
                TempData["Mensagem"] = "Selecione pelo menos um donut antes de enviar.";
                return RedirectToAction("Index");
            }

            //Só cadastra os selecionados
            pedido.Itens = pedido.Itens.Where(i => i.Quantidade > 0).ToList();
            pedido.DataCadastro = DateTime.Now;
            pedido.Status = "Pendente";

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            TempData["Mensagem"] = "Pedido enviado com sucesso!";
            return RedirectToAction("Index");
        }

       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
