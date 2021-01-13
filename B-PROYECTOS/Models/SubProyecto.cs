using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace B_PROYECTOS.Models
{
    public class SubProyecto
    {
        [Required]
        public string SPR_DESCRIPCION { get; set; }
        [Required]
        public string COD_PROYECTO { get; set; }
        [Required]
        public string SPR_NOMBRE { get; set; }
        [Required]
        public string SPR_FECHA_INICIO_ESTIMADA { get; set; }
        [Required]
        public string SPR_FECHA_TERMINO_ESTIMADA { get; set; }
        [Required]
        public Boolean SPR_FACTURABLE { get; set; }
        [Required]
        public Boolean SPR_RESUMEN { get; set; }
        public string COD_TIPO_SUB_PROYECTO { get; set; }
        [Required]
        public string COD_USUARIO_ENCARGADO { get; set; }
        public int ESTADO { get; set; }
        public int SPR_NRO_MEJORA { get; set; }

    }
}
