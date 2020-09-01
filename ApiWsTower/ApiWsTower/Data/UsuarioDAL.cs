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
    public class UsuarioDAL : IUsuarioDAL
    {
        private readonly string _conn;

        public UsuarioDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter adapter;
        public Usuario Find(int id)
        {
            Usuario usuario = new Usuario();
            conn = new SqlConnection(_conn);
            cmd = new SqlCommand($"select * from Usuario where id = {id}", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                usuario.Id = Convert.ToInt32(item["id"]);
                usuario.Nome = item["nome"].ToString();
                usuario.Email = item["email"].ToString();
                usuario.Senha = item["senha"].ToString();
                usuario.Telefone = item["telefone"].ToString();
                usuario.Funcao_id = Convert.ToInt32(item["funcaoID"]);
            }
            conn.Close();
            return usuario;
        }

        public IEnumerable<Usuario> GetAll()
        {
            List<Usuario> usuarioList = new List<Usuario>();
            Usuario usuario;
            conn = new SqlConnection(_conn);
            cmd = new SqlCommand("select * from Usuario", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                usuario = new Usuario();
                usuario.Id = Convert.ToInt32(item["id"]);
                usuario.Nome = item["nome"].ToString();
                usuario.Email = item["email"].ToString();
                usuario.Senha = item["senha"].ToString();
                usuario.Telefone = item["telefone"].ToString();
                usuario.Funcao_id = Convert.ToInt32(item["funcaoID"]);
                usuarioList.Add(usuario);
            }
            conn.Close();
            return usuarioList;
        }

        public Usuario Login(Usuario usuario)
        {
            Usuario _usuario;
            conn = new SqlConnection(_conn);
            cmd = new SqlCommand($"select * from Usuario where email = '{usuario.Email}' and senha = '{usuario.Senha}'", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            _usuario = usuario;
            foreach (DataRow item in dt.Rows)
            {
                _usuario.Id = Convert.ToInt32(item["id"]);
                _usuario.Nome = item["nome"].ToString();
                _usuario.Email = item["email"].ToString();
                _usuario.Senha = "";
                _usuario.Telefone = item["telefone"].ToString();
                _usuario.Funcao_id = Convert.ToInt32(item["funcaoID"]);
            }
            conn.Close();
            if (_usuario == null)
            {
                return null;
            }
            return _usuario;
        }
    }
}
