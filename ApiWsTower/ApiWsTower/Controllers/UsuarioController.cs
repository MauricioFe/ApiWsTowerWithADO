using ApiWsTower.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiWsTower.Controllers
{
    public class UsuarioController : ApiController
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SessaoMobile;Integrated Security=True;");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        List<Usuario> usuarioList;
        [HttpGet]
        public IEnumerable<Usuario> GetAll()
        {
            cmd = new SqlCommand("Select u.id, u.nome, u.email, u.senha, u.telefone, f.funcao from usuario as u Inner Join Funcao as f on f.id = u.funcaoid", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            usuarioList = new List<Usuario>();
            try
            {
                conn.Open();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(item[0]);
                    usuario.Nome = item[1].ToString();
                    usuario.Email = item[2].ToString();
                    usuario.Senha = item[3].ToString();
                    usuario.Telefone = item[4].ToString();
                    usuario.Funcao = item[5].ToString();
                    usuarioList.Add(usuario);
                }
                return usuarioList;

            }
            catch (Exception e)
            {
                throw new Exception();
            }
            finally
            {
                conn.Close();
            }
        }
        [HttpGet]
        public Usuario GetById(int id)
        {
            cmd = new SqlCommand($"Select u.id, u.nome, u.email, u.senha, u.telefone, f.funcao from usuario as u Inner Join Funcao as f on f.id = u.funcaoid where u.id = {id}", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            try
            {
                conn.Open();
                adapter.Fill(dt);
                Usuario usuario = new Usuario();
                foreach (DataRow item in dt.Rows)
                {
                    usuario.Id = Convert.ToInt32(item[0]);
                    usuario.Nome = item[1].ToString();
                    usuario.Email = item[2].ToString();
                    usuario.Senha = item[3].ToString();
                    usuario.Telefone = item[4].ToString();
                    usuario.Funcao = item[5].ToString();
                }
                return usuario;

            }
            catch (Exception e)
            {
                throw new Exception();
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpPost]
        public Usuario Login([FromBody] Usuario usuario)
        {
            cmd = new SqlCommand($" Select * from usuario where email = '{usuario.Email}' and senha = '{usuario.Senha}'", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            try
            {
                conn.Open();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Usuario usuarioLogado = new Usuario();
                    foreach (DataRow item in dt.Rows)
                    {
                        usuarioLogado.Id = Convert.ToInt32(item[0]);
                        usuarioLogado.Nome = item[1].ToString();
                        usuarioLogado.Email = item[2].ToString();
                        usuarioLogado.Senha = item[3].ToString();
                        usuarioLogado.Telefone = item[4].ToString();
                        usuarioLogado.FuncaoId = Convert.ToInt32(item[5]);
                    }
                    return usuarioLogado;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
