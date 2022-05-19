using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ChatController : Controller
    {
        private readonly string Connstr = "Data Source=daniell.database.windows.net;Initial Catalog=daniel;Persist Security Info=True;User ID=daniel;Password=5627Abcd;";
        public IActionResult Index()
        {
            var sqlList = new List<SqlModel>();
            SqlConnection sql = new SqlConnection(Connstr);
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Test];");
            command.Connection = sql;
            sql.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    sqlList.Add(new SqlModel()
                    {
                        id = reader.GetInt32(reader.GetOrdinal("id")),
                        name = reader.GetString(reader.GetOrdinal("name"))
                    });
                }
            }
            sql.Close();
            return View(sqlList);
        }

        public JsonResult Add(SqlModel sqlModel)
        {
            var sql = new SqlConnection(Connstr);
            string sqlStr = $"INSERT INTO [dbo].[Test] VALUES (N'{sqlModel.name}');";
            SqlCommand command = new SqlCommand(sqlStr);
            command.Connection = sql;
            sql.Open();
            command.ExecuteReader();
            var result = new { result = "OK" };
            return Json(result);
        }

        public JsonResult Delete(int id)
        {
            var sql = new SqlConnection(Connstr);
            string sqlStr = $"DELETE FROM [dbo].[Test] WHERE id = {id}";
            SqlCommand command = new SqlCommand(sqlStr);
            command.Connection = sql;
            sql.Open();
            command.ExecuteReader();
            var result = new { result = "OK" };
            return Json(result);
        }
    }
}
