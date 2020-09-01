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
    public class FuncaoController : ControllerBase
    {
        private readonly IFuncaoDAL _dal;

        public FuncaoController(IFuncaoDAL dal)
        {
            _dal = dal;
        }
        [HttpGet]
        public IEnumerable<Funcao> GetFuncaos()
        {
            return _dal.GetAll();
        }
    }
}
