using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PracticaS12.Models.ViewModels
{
    public class AbonoCreateViewModel
    {
        [Display(Name = "Compra")]
        [Required(ErrorMessage = "Seleccione una compra.")]
        public long IdCompra { get; set; }

        [Display(Name = "Saldo Anterior")]
        public decimal SaldoAnterior { get; set; }

        [Display(Name = "Monto del Abono")]
        [Required(ErrorMessage = "Ingrese el monto del abono.")]
        [Range(0.01, 9999999999, ErrorMessage = "El abono debe ser mayor que 0.")]
        public decimal Monto { get; set; }

        public IEnumerable<SelectListItem> ComprasPendientes { get; set; }
        public string Descripcion { get; set; }
    }
}