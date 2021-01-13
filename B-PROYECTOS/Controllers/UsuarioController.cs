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
    public class UsuarioController : ControllerBase
    {
        private readonly string _connectionString;
        public UsuarioController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        [HttpGet]
        public async Task<IActionResult> LISTAR_USUARIOS()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.QueryAsync("EXECUTE LISTAR_USUARIOS").Result.ToList();
                return Ok(data);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GUARDAR_USUARIO([FromBody] Usuario x)
        {

            var QUERY = $"EXECUTE GUARDAR_USUARIO " +
                $"@NOMBRE = '{x.USU_NOMBRE}' ," +
                $"@APELLIDO = '{x.USU_APELLIDO}' ," +
                $"@CORREO = '{x.USU_CORREO}' ," +
                $"@CONTRASENIA = '{x.USU_CONTRASENIA}' ," +
                $"@ESTADO = {x.USU_ESTADO}," +
                $"@ROL = {x.COD_ROL}," +
                $"@FECHA_NAC = '{x.USU_FECHA_NAC}', " +
                $"@HORA_CONTRATADA = {x.USU_HORA_CONTRATADA}," +
                $"@VALOR_HORA = {x.USU_VALOR_HORA}," +
                $"@JEFE = {x.USU_COD_JEFE} ";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var data = conn.ExecuteAsync(QUERY).Result;
                    return Ok(data);
                }

        }
        [HttpPost("login/{correo}/{password}")]
        public async Task<IActionResult> LOGIN(string correo , string password)
        {
            var QUERY = $"EXECUTE LOGIN @CORREO = '{correo}' , @CONTRASENIA = '{password}'; ";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {

                    await conn.OpenAsync();
                    var data = conn.QueryAsync(QUERY).Result.ToList();
                    return Ok(data[0]);
                }
        }
        [HttpPut("{COD_USUARIO}")]
        public async Task<IActionResult> MODIFICAR_USUARIO(int COD_USUARIO, [FromBody] Usuario x)
        {

            var QUERY = $"EXECUTE MODIFICAR_USUARIO " +
                $"@NOMBRE = '{x.USU_NOMBRE}' ," +
                $"@APELLIDO = '{x.USU_APELLIDO}' ," +
                $"@CORREO = '{x.USU_CORREO}' ," +
                $"@CONTRASENIA = '{x.USU_CONTRASENIA}' ," +
                $"@ESTADO = {x.USU_ESTADO}," +
                $"@ROL = {x.COD_ROL}," +
                $"@COD_USUARIO = {COD_USUARIO}," +
                $"@FECHA_NAC = '{x.USU_FECHA_NAC}', " +
                $"@HORA_CONTRATADA = {x.USU_HORA_CONTRATADA}," +
                $"@VALOR_HORA = {x.USU_VALOR_HORA}," +
                $"@JEFE = {x.USU_COD_JEFE} ";


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.ExecuteAsync(QUERY).Result;
                return Ok(data);
            }

        }
        [HttpGet("{COD_USUARIO}")]
        public async Task<IActionResult> LISTAR_UN_USUARIO(int COD_USUARIO)
        {

            var QUERY = $"EXECUTE LISTAR_UN_USUARIO @COD_USUARIO = {COD_USUARIO}";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.QueryAsync(QUERY).Result.ToList();
                if(data.Count == 0)
                {
                    return Ok(0);
                }
                else
                {
                    return Ok(data[0]);
                }
               
            }

        }

        [HttpDelete("{COD_USUARIO}")]
        public async Task<IActionResult> ELIMINAR_USUARIO(int COD_USUARIO)
        {

            var QUERY = $"EXECUTE ELIMINAR_USUARIO @COD_USUARIO = {COD_USUARIO}";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = conn.ExecuteAsync(QUERY).Result;
                return Ok(data);
  
            }

        }
    }
}
