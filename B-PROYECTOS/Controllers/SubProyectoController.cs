using B_PROYECTOS.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace B_PROYECTOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubProyectoController : ControllerBase
    {
        private readonly string _connectionString;
        public SubProyectoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        [HttpGet("{COD_PROYECTO}")]
        public async Task<IActionResult> LISTAR_SUB_PROYECTO(int COD_PROYECTO)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.QueryAsync($"EXECUTE LISTAR_SUB_PROYECTO @COD_PROYECTO = {COD_PROYECTO} ").Result.ToList();
                return Ok(data);
            }
        }

        [HttpGet("FILTRO/{COD_SUB_PROYECTO}")]
        public async Task<IActionResult> FILTRAR_SUB_PROYECTO(int COD_SUB_PROYECTO)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.QueryAsync($"EXECUTE FILTRAR_SUB_PROYECTO @COD_SUB_PROYECTO = {COD_SUB_PROYECTO} ").Result.ToList();
                return Ok(data);
            }
        }


      
        [HttpPost]
        public async Task<IActionResult> GUARDAR_SUB_PROYECTO([FromBody] SubProyecto x)
        {

            var FAC = 0;
            var RES = 0;

            if(x.SPR_FACTURABLE == true)
            {
                FAC = 1;
            }
            else
            {
                FAC = 0;
            }

            if (x.SPR_RESUMEN == true)
            {
                RES = 1;
            }
            else
            {
                RES = 0;
            }

            var QUERY = $"EXECUTE GUARDAR_SUB_PROYECTO " +
                        $"@SPR_DESCRIPCION = '{x.SPR_DESCRIPCION}', " +
                        $"@COD_PROYECTO = {x.COD_PROYECTO}, " +
                        $"@SPR__NOMBRE = '{x.SPR_NOMBRE}', " +
                        $"@SPR_FECHA_INICIO_ESTIMADA = '{x.SPR_FECHA_INICIO_ESTIMADA}', " +
                        $"@SPR_FECHA_FIN_ESTIMADA = '{x.SPR_FECHA_TERMINO_ESTIMADA}', " +
                        $"@SPR_FACTURABLE = {FAC}, " +
                        $"@SPR_RESUMEN = {RES}, " +
                        $"@COD_TIPO_SUB_PROYECTO = {x.COD_TIPO_SUB_PROYECTO}, " +
                        $"@COD_USUARIO_ENCARGADO = {x.COD_USUARIO_ENCARGADO}," +
                        $"@SPR_NRO_MEJORA = {x.SPR_NRO_MEJORA} ; ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.ExecuteAsync(QUERY).Result;
                return Ok(data);
            }

        }
        
        
        [HttpPut("{COD_SUB_PROYECTO}")]
        public async Task<IActionResult> MODIFICAR_SUB_PROYECTO (int COD_SUB_PROYECTO, [FromBody] SubProyecto x)
        {

            var FAC = 0;
            var RES = 0;

            if (x.SPR_FACTURABLE == true)
            {
                FAC = 1;
            }
            else
            {
                FAC = 0;
            }

            if (x.SPR_RESUMEN == true)
            {
                RES = 1;
            }
            else
            {
                RES = 0;
            }

            var QUERY = $"EXECUTE MODIFICAR_SUB_PROYECTO " +
            $"@SPR_DESCRIPCION = '{x.SPR_DESCRIPCION}', " +
            $"@COD_PROYECTO = {x.COD_PROYECTO}, " +
            $"@SPR__NOMBRE = '{x.SPR_NOMBRE}', " +
            $"@SPR_FECHA_INICIO_ESTIMADA = '{x.SPR_FECHA_INICIO_ESTIMADA}', " +
            $"@SPR_FECHA_FIN_ESTIMADA = '{x.SPR_FECHA_TERMINO_ESTIMADA}', " +
            $"@SPR_FACTURABLE = {FAC}, " +
            $"@SPR_RESUMEN = {RES}, " +
            $"@COD_TIPO_SUB_PROYECTO = {x.COD_TIPO_SUB_PROYECTO}, " +
            $"@COD_USUARIO_ENCARGADO = {x.COD_USUARIO_ENCARGADO}," +
            $"@COD_SUB_PROYECTO = {COD_SUB_PROYECTO} ," +
            $"@ESTADO = {x.ESTADO}, " +
            $"@SPR_NRO_MEJORA = {x.SPR_NRO_MEJORA} ; ";


   
            using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var data = conn.ExecuteAsync(QUERY).Result;
                    return Ok(data);
            }

        }

        [HttpDelete("{COD_SUB_PROYECTO}")]
        public async Task<IActionResult> ELIMINAR_SUB_PROYECTO(int COD_SUB_PROYECTO)
        {

            var QUERY = $"EXECUTE ELIMINAR_SUB_PROYECTO @COD_SUB_PROYECTO = {COD_SUB_PROYECTO}";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.ExecuteAsync(QUERY).Result;
                return Ok(data);

            }

        }
    }
}
