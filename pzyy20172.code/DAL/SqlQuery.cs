using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pzyy20172.Dal
{
	/// <summary>
	/// 用SQL语句来查询，适用一些复杂的查询，很难用Lambda来表达的
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class SqlQuery<T> where T : class, new()
	{
		/// <summary>
		/// SQL字符串查询分页
		/// </summary>
		/// <param name="sql">完整的SQL语句，如select * from userinfo</param>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public static List<T> GetPageListBySql(string sql, int pageIndex, int pageSize, ref int totalCount)
		{
			using (var db = DbBase.GetInstance())
			{
				List<T> list = db.SqlQueryable<T>(sql).ToPageList(pageIndex, pageSize,ref totalCount);
				return list;
			}
		}

		/// <summary>
		/// SQL字符串查询
		/// </summary>
		/// <param name="sql">完整的SQL语句，如select * from userinfo</param>
		/// <returns></returns>
		public static List<T> GetListBySql(string sql)
		{
			using (var db = DbBase.GetInstance())
			{
				List<T> list = db.SqlQueryable<T>(sql).ToList();
				return list;
			}
		}
	}

}
