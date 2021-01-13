using B_PROYECTOS.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace B_PROYECTOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionTareaController : ControllerBase
    {
        private string _connectionString;

        public AsignacionTareaController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        [HttpPost("{COD_TAREA}/{COD_USUARIO}")]
        public async Task<IActionResult> GUARDAR_ASIGNACION_TAREA(int COD_TAREA, int COD_USUARIO)
        {


            var QUERY = $"EXECUTE GUARDAR_ASIGNACION_TAREA @COD_TAREA  = {COD_TAREA} , @COD_USUARIO = {COD_USUARIO}   ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.ExecuteAsync(QUERY).Result;
                return Ok(data);
            }

        }

        [HttpGet("{COD_TAREA}")]
        public async Task<IActionResult> LISTAR_ASIGNACION(int COD_TAREA)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.QueryAsync($"EXECUTE LISTAR_ASIGNACION @COD_TAREA =  {COD_TAREA} ").Result.ToList();
                return Ok(data);
            }
        }

        [HttpDelete("{COD_TAREA}/{COD_USUARIO}")]
        public async Task<IActionResult> ELIMINAR_ASIGNACION_TAREA(int COD_TAREA  , int COD_USUARIO)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.QueryAsync($"EXECUTE ELIMINAR_ASIGNACION_TAREA @COD_TAREA  = {COD_TAREA} , @COD_USUARIO = {COD_USUARIO}").Result;
                return Ok(data);
            }
        }


    }
}
