using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaS12.Models.ViewModels;
using PracticaS12.Models;

namespace PracticaS12.Controllers
{
    public class ComprasController : Controller
    {
        private readonly PracticaContext db = new PracticaContext();

        public ActionResult Create()
        {
            return View(new CompraCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompraCreateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var compra = new Principal
            {
                Descripcion = vm.Descripcion,
                Precio = Math.Round(vm.Precio, 5, MidpointRounding.AwayFromZero),
                Saldo = Math.Round(vm.Precio, 5, MidpointRounding.AwayFromZero),
                Estado = "Pendiente"
            };

            db.Principales.Add(compra);
            db.SaveChanges();

            TempData["Ok"] = "Compra registrada correctamente. Ya puedes registrar un abono.";
           
            return RedirectToAction("Create", "Abonos");
        }
    }
}