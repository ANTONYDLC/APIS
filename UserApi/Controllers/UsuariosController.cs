using Microsoft.AspNetCore.Mvc;
using UserApi.Data;
using UserApi.Models;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        [HttpPost]
        public IActionResult Crear([FromBody] Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre) ||
                string.IsNullOrWhiteSpace(usuario.Email) ||
                string.IsNullOrWhiteSpace(usuario.Pais))
            {
                return BadRequest("Todos los campos son obligatorios.");
            }

            usuario.Id = FakeDb.Usuarios.Count + 1;
            FakeDb.Usuarios.Add(usuario);
            return Ok(usuario);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(FakeDb.Usuarios);
        }
    }
}
