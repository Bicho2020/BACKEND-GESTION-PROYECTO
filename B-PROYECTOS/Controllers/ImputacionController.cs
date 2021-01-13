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
    public class ImputacionController : ControllerBase
    {
        private readonly string _connectionString;
        public ImputacionController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        [HttpPost]
        public async Task<IActionResult> GUARDAR_IMPUTACION([FromBody] Imputacion x)
        {
            var CODIGO_IMPUTACION = x.IMP_MES.ToString() + x.IMP_ANIO.ToString();

            var QUERY = $"EXECUTE GUARDAR_IMPUTACION @COD_IMPUTACION  = '{CODIGO_IMPUTACION}'," +
                $"@MES = {x.IMP_MES}," +
                $"@ANIO = {x.IMP_ANIO}," +
                $"@ESTADO = {x.IMP_ESTADO} ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.ExecuteAsync(QUERY).Result;
                return Ok(data);
            }

        }

        [HttpGet("{ANIO}")]
        public async Task<IActionResult> LISTAR_IMPUTACION(int ANIO)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.QueryAsync($"EXECUTE LISTAR_IMPUTACION @FECHA =  {ANIO} ").Result.ToList();
                return Ok(data);
            }
        }

        [HttpGet]
        public async Task<IActionResult> LISTAR_ANIOS()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.QueryAsync($"EXECUTE LISTAR_ANIOS").Result.ToList();
                return Ok(data);
            }
        }

        [HttpPut("{CODIGO_IMPUTACION}/{ESTADO}")]
        public async Task<IActionResult> CAMBIAR_ESTADO_IMPUTACION(string CODIGO_IMPUTACION , int ESTADO)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.QueryAsync($"EXECUTE CAMBIAR_ESTADO_IMPUTACION @ESTADO =  {ESTADO} , @COD_IMPUTACION = {CODIGO_IMPUTACION}").Result.ToList();
                return Ok(data);
            }
        }

        [HttpPost("GENERAR/{ANIO}")]
        public async Task<IActionResult> GENERAR_ANIO_IMPUTACION(int ANIO)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.QueryAsync($"EXECUTE GENERAR_ANIO_IMPUTACION @ANIO = {ANIO}").Result;
                return Ok(data);
            }
        }
    }
}
