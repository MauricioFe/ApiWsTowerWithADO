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
                relatos.UsuarioId = Convert.ToInt32(item["usuarioID"]);
                relatosList.Add(relatos);
            }
            conn.Close();
            return relatosList;
        }
        public void Add(Relatos relatos)
        {
            throw new NotImplementedException();
        }

        public void Update(Relatos relatos)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
