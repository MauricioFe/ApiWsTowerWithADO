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
    public class RelatosDAL : IRelatosDAL
    {
        private readonly string _conn;

        public RelatosDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter adapter;
        public Relatos Find(int id)
        {
            Relatos relatos = new Relatos();
            conn = new SqlConnection(_conn);
            cmd = new SqlCommand($"select * from Relatos where id = {id}", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                relatos.Id = Convert.ToInt32(item["id"]);
                relatos.Relato = item["relato"].ToString();
                relatos.Imagem = item["imagem"].ToString();
                relatos.Latitude = decimal.Parse(item["latitude"].ToString());
                relatos.Longitude = decimal.Parse(item["longitude"].ToString());
                relatos.UsuarioId = Convert.ToInt32(item["usuarioID"]);
            }
            conn.Close();
            return relatos;
        }

        public IEnumerable<Relatos> GetAll()
        {
            List<Relatos> relatosList = new List<Relatos>();
            Relatos relatos;
            conn = new SqlConnection(_conn);
            cmd = new SqlCommand("select * from Relatos", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                relatos = new Relatos();
                relatos.Id = Convert.ToInt32(item["id"]);
                relatos.Relato = item["relato"].ToString();
                relatos.Imagem = item["imagem"].ToString();
                relatos.Latitude = decimal.Parse(item["latitude"].ToString());
                relatos.Longitude = decimal.Parse(item["longitude"].ToString());
                if (item["usuarioid"].ToString() == "")
                {
                    relatos.UsuarioId = null;
                }
                else
                {
                    relatos.UsuarioId = int.Parse(item["usuarioid"].ToString());
                }
                
                relatosList.Add(relatos);
            }
            conn.Close();
            return relatosList;
        }
        public void Add(Relatos relatos)
        {
            conn = new SqlConnection(_conn);
            cmd = new SqlCommand($"insert into Relatos values ('{relatos.Relato}', '{relatos.Imagem}', {relatos.Latitude.ToString().Replace(",", ".")}, {relatos.Longitude.ToString().Replace(",", ".")}, {relatos.UsuarioId})", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Update(Relatos relatos, int id)
        {
            conn = new SqlConnection(_conn);
            cmd = new SqlCommand($"update Relatos set relato = '{relatos.Relato}', imagem = '{relatos.Imagem}', latitude = {relatos.Latitude.ToString().Replace(",", ".")}, longitude = {relatos.Longitude.ToString().Replace(",", ".")}, usuarioid = {relatos.UsuarioId} where id = {relatos.Id}", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Remove(int id)
        {
            conn = new SqlConnection(_conn);
            cmd = new SqlCommand($"delete from Relatos where id = {id}", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
