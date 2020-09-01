using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWsTower.Models
{
    public class Relatos
    {
        public int ID { get; set; }
        public string  Relato { get; set; }
        public string  Imagem { get; set; }
        public Decimal  Latitude{ get; set; }
        public Decimal Longitude { get; set; }
        public Nullable<int> UsuarioId { get; set; }
    }
}
