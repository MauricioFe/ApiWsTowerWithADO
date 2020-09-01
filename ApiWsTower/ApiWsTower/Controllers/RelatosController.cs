using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWsTower.Data;
using ApiWsTower.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiWsTower.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatosController : ControllerBase
    {
        private readonly IRelatosDAL _dal;

        public RelatosController(IRelatosDAL dal)
        {
            _dal = dal;
        }
        // GET: api/<ValuesController1>
        [HttpGet]
        public IEnumerable<Relatos> Get()
        {
            return _dal.GetAll();
        }

        // GET api/<ValuesController1>/5
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {

            return new ObjectResult(_dal.Find(id));
        }

        // POST api/<ValuesController1>
        [HttpPost]
        public IActionResult Post([FromBody] Relatos relatos)
        {
            if (relatos == null)
            {
                return BadRequest();
            }
            _dal.Add(relatos);
            return Ok("Inserido com sucesso");
        }

        // PUT api/<ValuesController1>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Relatos relatos)
        {
            if (relatos == null)
            {
                return BadRequest();
            }
            var _relatos = _dal.Find(id);
            if (_relatos == null)
            {
                return NotFound();
            }
            _relatos = relatos;
            _dal.Update(_relatos);
            return Ok("Editado com sucesso");
        }

        // DELETE api/<ValuesController1>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            
            var usuario = _dal.Find(id);
            if (usuario == null)
            {
                return NotFound("Relato não encontrado");
            }
            _dal.Remove(id);
            return Ok("Removido com sucesso");
        }
    }
}
