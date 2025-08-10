using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PracticaS12.Models;
using PracticaS12.Models.ViewModels;

namespace PracticaS12.Controllers
{
    public class AbonosController : Controller
    {
        private readonly PracticaContext db = new PracticaContext();

     
        private List<SelectListItem> GetComprasPendientesSelectList()
        {
            var compras = db.Principal
                .Where(p => p.Estado == "Pendiente")
                .OrderByDescending(p => p.IdCompra)
                .Select(p => new { p.IdCompra, p.Descripcion, p.Saldo })
                .ToList(); 

           
            return compras
                .Select(p => new SelectListItem
                {
                    Value = p.IdCompra.ToString(),
                    Text = $"#{p.IdCompra} - {p.Descripcion} (Saldo: {p.Saldo:n2})"
                })
                .ToList();
        }

        // GET
        public ActionResult Create()
        {
            var vm = new AbonoCreateViewModel
            {
                ComprasPendientes = GetComprasPendientesSelectList()
            };
            return View(vm);
        }

        [HttpGet]
        public ActionResult GetSaldo(long idCompra)
        {
            var compra = db.Principal.FirstOrDefault(p => p.IdCompra == idCompra);
            if (compra == null) return HttpNotFound();

            return Json(new
            {
                saldo = compra.Saldo,
                descripcion = compra.Descripcion
            }, JsonRequestBehavior.AllowGet);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AbonoCreateViewModel vm)
        {
          
            vm.ComprasPendientes = GetComprasPendientesSelectList();

            if (!ModelState.IsValid) return View(vm);

            var compra = db.Principal.FirstOrDefault(p => p.IdCompra == vm.IdCompra);
            if (compra == null)
            {
                ModelState.AddModelError("", "La compra seleccionada no existe.");
                return View(vm);
            }
            if (compra.Estado != "Pendiente")
            {
                ModelState.AddModelError("", "La compra no está pendiente.");
                return View(vm);
            }
            if (vm.Monto > compra.Saldo)
            {
                ModelState.AddModelError("Monto", "El abono no puede ser mayor que el saldo anterior.");
                return View(vm);
            }

            var abono = new Abono
            {
                IdCompra = compra.IdCompra,
                Monto = vm.Monto,
                Fecha = DateTime.Now
            };
            db.Abonos.Add(abono);

            compra.Saldo = Math.Round(compra.Saldo - vm.Monto, 5, MidpointRounding.AwayFromZero);
            if (compra.Saldo == 0m) compra.Estado = "Cancelado";

            db.SaveChanges();

            return RedirectToAction("Index", "Consulta");
        }
    }
}