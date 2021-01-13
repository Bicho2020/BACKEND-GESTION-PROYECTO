using B_PROYECTOS.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace B_PROYECTOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly string _connectionString;
        public ProyectoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }


        [HttpGet]
        public async Task<IActionResult> LISTAR_PROYECTO()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.QueryAsync("EXECUTE LISTAR_PROYECTO").Result.ToList();
                return Ok(data);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GUARDAR_PROYECTO([FromBody] Proyecto x)
        {

            var QUERY = $"EXECUTE GUARDAR_PROYECTO " +
                $"@PRO_DESCRIPCION = '{x.PRO_DESCRIPCION}' ," +
                $"@COD_CLIENTE = '{x.COD_CLIENTE}' ," +
                $"@COD_USUARIO_JEFE = {x.COD_USUARIO_JEFE} ," +
                $"@PRO_FECHA = '{x.PRO_FECHA}' ," +
                $"@PRO_ESTADO  = {x.PRO_ESTADO} , " +
                $"@PRO_CLIENTE_NOMBRE = '{x.PRO_CLIENTE_NOMBRE}' , " +
                $"@PRO_NOMBRE = '{x.PRO_NOMBRE}' ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.ExecuteAsync(QUERY).Result;
                return Ok(data);
            }

        }

        [HttpPut("{COD_PROYECTO}")]
        public async Task<IActionResult> MODIFICAR_PROYECTO (int COD_PROYECTO, [FromBody] Proyecto x)
        {

            var QUERY = $"EXECUTE MODIFICAR_PROYECTO " +
                $"@COD_PROYECTO = {COD_PROYECTO} ," +
                $"@PRO_DESCRIPCION = '{x.PRO_DESCRIPCION}' ," +
                $"@COD_CLIENTE = '{x.COD_CLIENTE}' ," +
                $"@COD_USUARIO_JEFE  = {x.COD_USUARIO_JEFE} ," +
                $"@PRO_FECHA = '{x.PRO_FECHA}'," +
                $"@PRO_ESTADO = {x.PRO_ESTADO}," +
                $"@PRO_CLIENTE_NOMBRE = '{ x.PRO_CLIENTE_NOMBRE}'," +
                $"@PRO_NOMBRE = '{x.PRO_NOMBRE}' ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.ExecuteAsync(QUERY).Result;
                return Ok(data);
            }

        }
        [HttpGet("{COD_PROYECTO}")]
        public async Task<IActionResult> LISTAR_UN_PROYECTO(int COD_PROYECTO)
        {

            var QUERY = $"EXECUTE LISTAR_UN_PROYECTO @COD_PROYECTO = {COD_PROYECTO}";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.QueryAsync(QUERY).Result.ToList();
                if (data.Count == 0)
                {
                    return Ok(0);
                }
                else
                {
                    return Ok(data[0]);
                }

            }

        }

        [HttpDelete("{COD_PROYECTO}")]
        public async Task<IActionResult> ELIMINAR_USUARIO(int COD_PROYECTO)
        {

            var QUERY = $"EXECUTE ELIMINAR_PROYECTO @COD_PROYECTO = {COD_PROYECTO}";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.ExecuteAsync(QUERY).Result;
                return Ok(data);

            }

        }
    }
}
