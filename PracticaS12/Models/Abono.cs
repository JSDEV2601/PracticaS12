using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaS12.Models
{
    [Table("Abonos")]
    public class Abono
    {
        [Key]
        [Column("Id_Abono")]
        public long IdAbono { get; set; }

        [Required]
        [Column("Id_Compra")]
        public long IdCompra { get; set; }

        [Required]
        [Column(TypeName = "decimal")] 
        [Range(0.01, 999.99, ErrorMessage = "El abono debe ser mayor que 0.")]
        public decimal Monto { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [ForeignKey(nameof(IdCompra))]
        public virtual Principal Compra { get; set; }
    }
}