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
    public class RelatosController : ApiController
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SessaoMobile;Integrated Security=True;");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        List<Relatos> relatoList;
        [HttpGet]
        public IEnumerable<Relatos> GetAll()
        {
            cmd = new SqlCommand("select * from relatos", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            relatoList = new List<Relatos>();
            try
            {
                conn.Open();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Relatos relato = new Relatos();
                    relato.Id = Convert.ToInt32(item[0]);
                    relato.Relato = item[1].ToString();
                    relato.Imagem = item[2].ToString();
                    relato.Latitude = Decimal.Parse(item[3].ToString());
                    relato.Longitude = Decimal.Parse(item[4].ToString());
                    relato.UsuarioId = Convert.ToInt32(item[5].ToString() == ""? "0": item[5].ToString());
                    relatoList.Add(relato);
                }
                return relatoList;

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
        public Relatos GetById(int id)
        {
            cmd = new SqlCommand($" select * from relatos where id = {id}", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            try
            {
                conn.Open();
                adapter.Fill(dt);
                Relatos relato = new Relatos();
                foreach (DataRow item in dt.Rows)
                {
                    relato.Id = Convert.ToInt32(item[0]);
                    relato.Relato = item[1].ToString();
                    relato.Imagem = item[2].ToString();
                    relato.Latitude = Decimal.Parse(item[3].ToString());
                    relato.Longitude = Decimal.Parse(item[4].ToString());
                    relato.UsuarioId = Convert.ToInt32(item[5].ToString() == "" ? "0" : item[5].ToString());
                }
                return relato;

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
        public string Post([FromBody] Relatos relato)
        {
            cmd = new SqlCommand($"Insert Into Relatos values ('{relato.Relato}', '{relato.Imagem}', {relato.Latitude.ToString().Replace(",","." )}, {relato.Longitude.ToString().Replace(",", ".")}, {relato.UsuarioId})", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return "Inserido com sucesso";

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

        public string Put(int id, [FromBody] Relatos relato)
        {
            cmd = new SqlCommand($"Update Relatos set relato = '{relato.Relato}', imagem ='{relato.Imagem}', latitude = {relato.Latitude.ToString().Replace(",", ".")}, longitude = {relato.Longitude.ToString().Replace(",", ".")}, usuarioid = {relato.UsuarioId} where id = {id}", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return "Editado com sucesso";
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

        public string Delete(int id)
        {
            cmd = new SqlCommand($"Delete From usuario where id = {id}", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return "Excluido com sucesso";
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
    }
}
