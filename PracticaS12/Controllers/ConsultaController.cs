using PracticaS12.Models;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;

namespace PracticaS12.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly PracticaContext db = new PracticaContext();

        public ActionResult Index()
        {
            var data = db.Principales
            .AsNoTracking()
            .OrderByDescending(p => p.Saldo)
            .ThenByDescending(p => p.IdCompra)
            .ToList();


            return View(data);
        }

        public ActionResult CheckEntities()
        {
            var objectContext = ((IObjectContextAdapter)db).ObjectContext;
            var metadataWorkspace = objectContext.MetadataWorkspace;

            var entityTypes = metadataWorkspace.GetItems<EntityType>(DataSpace.CSpace);
            var entityNames = entityTypes.Select(e => e.Name).ToList();

            return Json(entityNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TestContext()
        {
            var tipoContexto = db.GetType().FullName;

            // Obtener propiedades DbSet del contexto
            var dbSetProps = db.GetType().GetProperties()
                .Where(p => p.PropertyType.IsGenericType &&
                       p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .Select(p => p.Name)
                .ToList();

            return Json(new { TipoContexto = tipoContexto, DbSets = dbSetProps }, JsonRequestBehavior.AllowGet);
        }
    }
}