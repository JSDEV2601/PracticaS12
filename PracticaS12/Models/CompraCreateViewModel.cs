using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PracticaS12.Models.ViewModels
{
    public class CompraCreateViewModel
    {
        [Required, StringLength(500)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [Range(0.00001, 999999999, ErrorMessage = "El precio debe ser mayor que 0.")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; } 
    }
}