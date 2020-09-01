using ApiWsTower.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWsTower.Data
{
    public class FuncaoDAL : IFuncaoDAL
    {
        private readonly string _conn;

        public FuncaoDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter adapter;
        public IEnumerable<Funcao> GetAll()
        {
            List<Funcao> usuarioList = new List<Funcao>();
            Funcao usuario;
            conn = new SqlConnection(_conn);
            cmd = new SqlCommand("select * from Funcao", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                usuario = new Funcao();
                usuario.Id = Convert.ToInt32(item["id"]);
                usuario.Funcoes = item["funcao"].ToString();

                usuarioList.Add(usuario);
            }
            conn.Close();
            return usuarioList;
        }
    }
}
