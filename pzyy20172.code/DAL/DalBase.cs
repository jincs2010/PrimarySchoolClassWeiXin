using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace pzyy20172.Dal
{
	public class DalBase<T, TJoin> where T : class, new()
	{
		/// <summary>
		/// 增加一条数据，并返回自增列，待测试
		/// </summary>
		public int Add(T model)
		{
			using (var db = DbBase.GetInstance())
			{
				return db.Insertable(model).ExecuteReturnIdentity();
			}
		}

		//待测试
		public int GetMaxID(Expression<Func<T, object>> expression)
		{
			using (var db = DbBase.GetInstance())
			{
				//string sql = "select Max(ID) from Comment";
				//dynamic identity = conn.ExecuteScalar(sql);
				//return Convert.ToInt32(identity);

				//var sum = db.Queryable<T>().Sum(it => it.Id);
				var max = db.Queryable<T>().Max(expression);
				return Convert.ToInt32(max);
			}
		}


		/// <summary>
		/// 更新一条数据,待测试
		/// </summary>
		public int Update(T model)
		{
			using (var db = DbBase.GetInstance())
			{
				return db.Updateable(model).ExecuteCommand();
			}
		}


		/// <summary>
		/// 删除一条数据,待测试
		/// </summary>
		public int Delete(T model)
		{
			using (var db = DbBase.GetInstance())
			{
				return db.Deleteable<T>().Where(model).ExecuteCommand();
			}
		}

		/// <summary>
		/// 根据主键删除一条数据,待测试
		/// </summary>
		public int Delete(int ID)
		{
			using (var db = DbBase.GetInstance())
			{
				return db.Deleteable<T>().In(ID).ExecuteCommand();
			}
		}

		/// <summary>
		/// 根据Where批量删除一批数据,待测试
		/// </summary>
		public int DeleteByWhere(string strWhere)
		{
			using (var db = DbBase.GetInstance())
			{
				return db.Deleteable<T>().Where(strWhere).ExecuteCommand();
			}
		}
		/// <summary>
		/// 根据Lambda批量删除一批数据,待测试
		/// </summary>
		/// <param name="Twhere"></param>
		/// <returns></returns>
		public int DeleteByWhere(Expression<Func<T, bool>> Twhere)
		{
			using (var db = DbBase.GetInstance())
			{
				return db.Deleteable<T>().Where(Twhere).ExecuteCommand();
			}
		}
		/// <summary>
		/// 根据Model删除一个数据,待测试
		/// </summary>
		/// <param name="deleteObj"></param>
		/// <returns></returns>
		public int DeleteByWhere(T deleteObj)
		{
			using (var db = DbBase.GetInstance())
			{
				return db.Deleteable<T>().Where(deleteObj).ExecuteCommand();
			}
		}

		/// <summary>
		/// 得到一个对象实体,待测试
		/// </summary>
		public T GetByID(int ID)
		{
			using (var db = DbBase.GetInstance())
			{
				return db.Queryable<T>().InSingle(ID);

				//StringBuilder strSql = new StringBuilder();
				//strSql.Append("select * from Comment ");
				//strSql.Append(" where ID=@ID");

				//List<T> list = conn.Query<T>(strSql.ToString(), new { ID = ID }).ToList();
				//if (list.Count > 0)
				//	return list[0];
				//else
				//	return null;
			}
		}

		/// <summary>
		/// 得到一个对象实体,待测试
		/// </summary>
		public T GetSingle(string strWhere)
		{
			using (var db = DbBase.GetInstance())
			{
				return db.Queryable<T>().Where(strWhere).First();
			}
		}

		//OK
		public T GetSingle(Expression<Func<T, bool>> Twhere)
		{
			using (var db = DbBase.GetInstance())
			{
				return db.Queryable<T>().Where(Twhere).First();
			}
		}

		/// <summary>
		/// 获得数据列表，根据Where和OrderBy,待测试
		/// </summary>
		public List<T> GetList(string strWhere = "", string strOrderBy = "", int topnum = 0)
		{
			using (var db = DbBase.GetInstance())
			{
				var qa = db.Queryable<T>();
				if (!string.IsNullOrEmpty(strWhere))
					qa.Where(strWhere);
				if (!string.IsNullOrEmpty(strOrderBy))
					qa.OrderBy(strOrderBy);
				if (topnum > 0)
					qa.Take(topnum);

				return qa.ToList();
			}
		}

		/// <summary>
		/// 获得数据列表，根据Lambda，OK。注意topnum为能为0
		/// </summary>
		/// <returns></returns>
		public List<T> GetList(Expression<Func<T, bool>> Twhere, Expression<Func<T, object>> Torderby, bool IsDesc, int topnum)
		{
			using (var db = DbBase.GetInstance())
			{
				OrderByType orderByType = IsDesc ? OrderByType.Desc : OrderByType.Asc;

				var qa = db.Queryable<T>();
				if(Twhere!=null)
					qa.Where(Twhere);
				if (Torderby != null)
					qa.OrderBy(Torderby, orderByType);
				if (topnum > 0)
					qa.Take(topnum);

				return qa.ToList();
			}
		}

		/// <summary>
		/// 获得数据列表,待测试
		/// </summary>
		public List<T> GetList(string strWhere, string strOrderBy)
		{
			using (var db = DbBase.GetInstance())
			{
				var qa = db.Queryable<T>();
				if (!string.IsNullOrEmpty(strWhere))
					qa.Where(strWhere);
				if (!string.IsNullOrEmpty(strOrderBy))
					qa.OrderBy(strOrderBy);

				return qa.ToList();
			}
		}

		//OK
		public List<T> GetPageList(string strWhere, string strOrderBy, int pageIndex, int pageSize, ref int totalCount)
		{
			using (var db = DbBase.GetInstance())
			{
				int startnum = (pageIndex - 1) * pageSize;
				int endnum = startnum + pageSize;

				var qa = db.Queryable<T>();
				if (!string.IsNullOrEmpty(strWhere))
					qa.Where(strWhere);
				if (!string.IsNullOrEmpty(strOrderBy))
					qa.OrderBy(strOrderBy);

				return qa.ToPageList(pageIndex, pageSize, ref totalCount);
			}
		}

		/// <summary>
		/// 待测试
		/// </summary>
		public List<T> GetPageList(Expression<Func<T, bool>> Twhere, Expression<Func<T, object>> Torderby, bool IsDesc, int pageIndex, int pageSize, ref int totalCount)
		{
			using (var db = DbBase.GetInstance())
			{
				int startnum = (pageIndex - 1) * pageSize;
				int endnum = startnum + pageSize;
				OrderByType orderByType = IsDesc ? OrderByType.Desc : OrderByType.Asc;

				var qa = db.Queryable<T>();
				if (Twhere!=null)
					qa.Where(Twhere);
				if (Torderby!=null)
					qa.OrderBy(Torderby, orderByType);

				return qa.ToPageList(pageIndex, pageSize, ref totalCount);
			}
		}

		/// <summary>
		/// 待测试
		/// </summary>
		public Int32 GetCount(string strWhere)
		{
			using (var db = DbBase.GetInstance())
			{
				var qa = db.Queryable<T>();
				if (!string.IsNullOrEmpty(strWhere))
					qa.Where(strWhere);

				return qa.Count();
				//string strSql = string.IsNullOrEmpty(strWhere) ? string.Empty : " Where " + strWhere;
				//string query = "select Count(*) from Comment " + strSql;
				//dynamic identity = conn.ExecuteScalar(query);
				//return Convert.ToInt32(identity);
			}
		}
		//待测试
		public Int32 GetCount(Expression<Func<T, bool>> Twhere)
		{
			using (var db = DbBase.GetInstance())
			{
				var qa = db.Queryable<T>();
				if (Twhere != null)
					qa.Where(Twhere);

				return qa.Count();
			}
		}


		#region 多表查询
		//Lambda参数必须使用主体名m1
		public List<TJoin> GetPageListEx(Expression<Func<T, bool>> Twhere, Expression<Func<T, object>> Torderby, bool IsDesc,
			int pageIndex, int pageSize, ref int totalCount)
		{
			return null;
		}

		//public TJoin GetByIDEx(int ID)
		//{
		//	int intTotalCount = 0;
		//	List<TJoin> list = GetPageListEx(m1 => m1.ID == ID, m1 => m1.ID, false, 1, 1, ref intTotalCount);
		//	if (list.Count > 0)
		//		return list[0];
		//	else
		//		return null;
		//}


		public TJoin GetSingleEx(Expression<Func<T, bool>> Twhere, Expression<Func<T, object>> Torderby, bool IsDesc)
		{
			int intTotalCount = 0;
			List<TJoin> list = GetPageListEx(Twhere, Torderby, IsDesc, 1, 1, ref intTotalCount);
			//if (list.Count > 0)
			return list[0];
			//else
			//	return new TJoin();
		}

		public List<TJoin> GetListEx(Expression<Func<T, bool>> Twhere, Expression<Func<T, object>> Torderby, bool IsDesc)
		{
			int intTotalCount = 0;
			return GetPageListEx(Twhere, Torderby, IsDesc, 1, 0, ref intTotalCount);
		}

		#endregion

	}
}
