using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
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
                .AsNoTracking()
                .Where(p => p.Saldo > 0m)
                .OrderByDescending(p => p.IdCompra)
                .Select(p => new { p.IdCompra, p.Descripcion, p.Saldo })
                .ToList();

            return compras.Select(p => new SelectListItem
            {
                Value = p.IdCompra.ToString(),
                Text = $"#{p.IdCompra} - {p.Descripcion} (Saldo: {p.Saldo:n2})"
            }).ToList();
        }

      
        public ActionResult Create()
        {
            var vm = new AbonoCreateViewModel
            {
                ComprasPendientes = GetComprasPendientesSelectList()
            };
            return View("Create", vm); 
        }

        
        [HttpGet]
        public ActionResult GetSaldo(long idCompra)
        {
            var compra = db.Principal.AsNoTracking().FirstOrDefault(p => p.IdCompra == idCompra);
            if (compra == null) return HttpNotFound();
            return Json(new { saldo = compra.Saldo, descripcion = compra.Descripcion }, JsonRequestBehavior.AllowGet);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AbonoCreateViewModel vm)
        {
            vm.ComprasPendientes = GetComprasPendientesSelectList();
            if (!ModelState.IsValid) return View("Create", vm);

            var compra = db.Principal.FirstOrDefault(p => p.IdCompra == vm.IdCompra);
            if (compra == null) { ModelState.AddModelError("", "La compra seleccionada no existe."); return View("Create", vm); }
            if (compra.Saldo <= 0m) { ModelState.AddModelError("", "La compra no está pendiente."); return View("Create", vm); }
            if (vm.Monto > compra.Saldo) { ModelState.AddModelError("Monto", "El abono no puede ser mayor que el saldo anterior."); return View("Create", vm); }

            db.Abonos.Add(new Abono { IdCompra = compra.IdCompra, Monto = vm.Monto, Fecha = DateTime.Now });
            compra.Saldo = Math.Round(compra.Saldo - vm.Monto, 5, MidpointRounding.AwayFromZero);
            compra.Estado = (compra.Saldo <= 0m) ? "Cancelado" : "Pendiente";
            if (compra.Saldo < 0m) compra.Saldo = 0m;

            db.SaveChanges();
            return RedirectToAction("Index", "Consulta");
        }
    }

}

 


