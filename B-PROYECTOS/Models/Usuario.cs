using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace B_PROYECTOS.Models
{
    public class Usuario
    {

        [Required]
        public string USU_NOMBRE { get; set; }
        [Required]
        public string USU_APELLIDO { get; set; }
        [Required]
        public string USU_CORREO { get; set; }
        [Required]
        public string USU_CONTRASENIA { get; set; }
        [Required]
        public string USU_ESTADO { get; set; }
        [Required]
        public string COD_ROL { get; set; }
        [Required]
        public string USU_FECHA_NAC { get; set; }
        [Required]

        public string USU_HORA_CONTRATADA { get; set; }

        [Required]
        public string USU_VALOR_HORA { get; set; }
        [Required]
        public string USU_COD_JEFE { get; set; }
    }
}
