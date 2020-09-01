using ApiWsTower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWsTower.Data
{
    public interface IUsuarioDAL
    {
        IEnumerable<Usuario> GetAll();
        Usuario Find(int id);
        Usuario Login(Usuario usuario);
    }
}
