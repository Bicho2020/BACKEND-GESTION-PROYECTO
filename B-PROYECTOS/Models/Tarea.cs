using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace B_PROYECTOS.Models
{
    public class Tarea
    {
        [Required]
        public string COD_SUB_PROYECTO { get; set; }
        [Required]
        public string TAR_DESCRIPCION { get; set; }
        [Required]
        public string TAR_HORAS_PLANIFICADAS { get; set; }
        [Required]
        public string TAR_HORAS_IMPUTADAS { get; set; }
        [Required]
        public string TAR_HORAS_ACTUALES { get; set; }
        [Required]
        public string TAR_FECHA_INICIO_ESTIMADA { get; set; }
        [Required]
        public string TAR_FECHA_FIN_ESTIMADA { get; set; }
        public int TAR_ESTADO { get; set; }

    }
}

