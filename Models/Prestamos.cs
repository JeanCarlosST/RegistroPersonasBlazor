using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroPersonasBlazor.Models
{
    public class Prestamos
    {
        [Key]
        public int PrestamoID { get; set; }

        [Required(ErrorMessage = "Ingrese una fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Selecciona una persona")]
        public int PersonaID { get; set; }

        public String Concepto { get; set; }

        [Required(ErrorMessage = "Ingrese un monto")]
        public float Monto { get; set; }

        [Required(ErrorMessage = "Ingrese un balance")]
        public float Balance { get; set; }

    }
}
