using B_PROYECTOS.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace B_PROYECTOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly string _connectionString;
        public TareaController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        [HttpPost]
        public async Task<IActionResult> GUARDAR_TAREA([FromBody] Tarea x)
        {
            var QUERY = $"EXECUTE GUARDAR_TAREA @COD_SUB_PROYECTO = {x.COD_SUB_PROYECTO} ," +
                $"@TAR_DESCRIPCION = '{x.TAR_DESCRIPCION}' ," +
                $"@TAR_HORAS_PLANIFICADAS = {x.TAR_HORAS_PLANIFICADAS} ," +
                $"@TAR_HORAS_IMPUTADAS = {x.TAR_HORAS_IMPUTADAS} ," +
                $"@TAR_HORAS_ACTUALES = {x.TAR_HORAS_ACTUALES} ," +
                $"@TAR_FECHA_INICIO_ESTIMADA = '{x.TAR_FECHA_INICIO_ESTIMADA}'," +
                $"@TAR_FECHA_FIN_ESTIMADA = '{x.TAR_FECHA_FIN_ESTIMADA}'";


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.ExecuteAsync(QUERY).Result;
                return Ok(data);
            }


        }

        [HttpGet("{COD_SUB_PROYECTO}")]
        public async Task<IActionResult> LISTAR_TAREAS(int COD_SUB_PROYECTO)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.QueryAsync($"EXECUTE LISTAR_TAREAS @COD_SUB_PROYECTO = {COD_SUB_PROYECTO}").Result.ToList();
                return Ok(data);
            }
        }


        [HttpPut("{COD_TAREA}")]
        public async Task<IActionResult> MODIFICAR_TAREA(int COD_TAREA  , [FromBody] Tarea x)
        {
            var QUERY = $"EXECUTE MODIFICAR_TAREA " +
                        $"@COD_SUB_PROYECTO = {x.COD_SUB_PROYECTO}, " +
                        $"@TAR_DESCRIPCION = '{x.TAR_DESCRIPCION}', " +
                        $"@TAR_HORAS_PLANIFICADAS = {x.TAR_HORAS_PLANIFICADAS}, " +
                        $"@TAR_HORAS_IMPUTADAS = {x.TAR_HORAS_IMPUTADAS}, " +
                        $"@TAR_HORAS_ACTUALES = {x.TAR_HORAS_ACTUALES}, " +
                        $"@TAR_FECHA_INICIO_ESTIMADA = '{x.TAR_FECHA_INICIO_ESTIMADA}', " +
                        $"@TAR_FECHA_FIN_ESTIMADA = '{x.TAR_FECHA_FIN_ESTIMADA}', " +
                        $"@TAR_ESTADO = {x.TAR_ESTADO} ," +
                        $"@COD_TAREA = {COD_TAREA} ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.ExecuteAsync(QUERY).Result;
                return Ok(data);
            }

        }


        [HttpDelete("{COD_TAREA}")]
        public async Task<IActionResult> ELIMINAR_TAREA(int COD_TAREA)
        {
            var QUERY = $"EXECUTE  ELIMINAR_TAREA @COD_TAREA = {COD_TAREA} ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.ExecuteAsync(QUERY).Result;
                return Ok(data);
            }

        }
    }
}
