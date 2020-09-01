using ApiWsTower.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWsTower.Data
{
    public interface IRelatosDAL
    {
        IEnumerable<Relatos> GetAll();
        Relatos Find(int id);
        void Add(Relatos relatos);
        void Update(Relatos relatos, int id);
        void Remove(int id);
    }
}
