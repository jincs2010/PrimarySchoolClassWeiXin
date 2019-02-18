using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pzyy20172.Dal
{
	public class DbBase
	{
		public static string strConn = "Data Source=" + System.Web.HttpContext.Current.Server.MapPath("/_db/pyzz20172.db") + ";Version=3;";

		public static SqlSugarClient GetInstance()
		{
			SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = strConn, DbType = DbType.Sqlite, IsAutoCloseConnection = true });
			db.Ado.IsEnableLogEvent = true;
			db.Ado.LogEventStarting = (sql, pars) =>
			{
				Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
				Console.WriteLine();
			};
			return db;
		}

		public void CreateModelClassFile()
		{
			using (var db = GetInstance())
			{
				db.DbFirst.CreateClassFile("F:\\其它小项目\\pzyy20172\\pzyy20172.code\\Model", "pzyy20172.Model");
			}
		}
	}
}
