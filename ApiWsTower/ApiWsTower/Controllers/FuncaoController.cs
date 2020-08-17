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
    public class FuncaoController : ApiController
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SessaoMobile;Integrated Security=True;");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        List<Funcao> funcaoList;
        [HttpGet]
        public IEnumerable<Funcao> GetAll()
        {
            cmd = new SqlCommand("Select * from funcao", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            funcaoList = new List<Funcao>();
            try
            {
                conn.Open();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Funcao funcao = new Funcao();
                    funcao.Id = Convert.ToInt32(item[0]);
                    funcao.funcao = item[1].ToString();
                    funcaoList.Add(funcao);
                }
                return funcaoList;

            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
    }
}
