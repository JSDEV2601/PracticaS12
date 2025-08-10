using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaS12.Models
{
    [Table("Principal")]
    public class Principal
    {
        [Key]
        [Column("Id_Compra")]
        public long IdCompra { get; set; }

        [Required]
        [Column(TypeName = "decimal")] 
        public decimal Precio { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Saldo { get; set; }

        [Required, StringLength(500)]
        public string Descripcion { get; set; }

        [Required, StringLength(100)]
        public string Estado { get; set; }
    }
}