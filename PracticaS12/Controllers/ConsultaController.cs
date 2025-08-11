using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaS12.Models;

namespace PracticaS12.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly PracticaContext db = new PracticaContext();

        public ActionResult Index()
        {
            var data = db.Principal
    .AsNoTracking()
    .OrderByDescending(p => p.Saldo > 0m) 
    .ThenByDescending(p => p.IdCompra)
    .ToList();


            return View(data);
        }
    }
}