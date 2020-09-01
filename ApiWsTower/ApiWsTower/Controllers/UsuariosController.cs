using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWsTower.Data;
using ApiWsTower.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWsTower.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioDAL _dal;

        public UsuariosController(IUsuarioDAL dal)
        {
            _dal = dal;
        }
        [HttpGet]
        public IEnumerable<Usuario> GetUsuarios()
        {
            return _dal.GetAll();
        }
        [HttpPost]
        [Route("login")]
        public IActionResult login(Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }
            var _usuario = _dal.Login(usuario);
            return new ObjectResult(_usuario);

        }
    }
}
