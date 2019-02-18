using pzyy20172.Dal;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace pzyy20172.BLL
{
	public partial class user_info : Dal.DalBase<Model.user_info, Model.user_info>
	{
		#region 多表查询
		//Lambda参数必须使用主体名t
		//new public List<Model.news> GetPageListEx(Expression<Func<Model.news, bool>> Twhere, Expression<Func<Model.news, object>> Torderby, bool IsDesc, int pageIndex, int pageSize, ref int totalCount)
		//{
		//	using (var db = DbBase.GetInstance())
		//	{
		//		if (pageSize == 0) pageSize = 10000;    //以10000条代替全部，应该足够了

		//		List<Model.newsJoin> list = db.Queryable<Model.news, Model.StockBase, Model.SysUser>((t, m2, m3) => new object[] {
		//			JoinType.Left,t.Code==m2.code,
		//			JoinType.Left,t.ToUserID==m3.ID
		//		})
		//	   .Where(Twhere)
		//	   .OrderBy(Torderby, IsDesc ? OrderByType.Desc : OrderByType.Asc)
		//	   .Select((t, m2, m3) => new Model.newsJoin { newsModel = t, StockBaseModel = m2, SysUserModel = m3 })
		//	   .ToPageList(pageIndex, pageSize, ref totalCount);

		//		return list;
		//	}
		//}

		//public Model.newsJoin GetByIDEx(int ID)
		//{
		//	int intTotalCount = 0;
		//	List<Model.newsJoin> list = GetPageListEx(t => t.ID == ID, t => t.ID, false, 1, 1, ref intTotalCount);
		//	if (list.Count > 0)
		//		return list[0];
		//	else
		//		return null;
		//}

		#endregion

		#region 重写

		new public int Add(Model.user_info model)
		{
			int i = new Dal.DalBase<Model.user_info, Model.user_info>().Add(model);
			SetListByCache();
			return i;
		}
		new public int Update(Model.user_info model)
		{
			int i = new Dal.DalBase<Model.user_info, Model.user_info>().Update(model);
			SetListByCache();
			return i;
		}

		#endregion

		public int Login(Model.user_info model)
		{
			Common.Cookie.UserID = model.ID;

			return 1;
		}

		#region 缓存

		/// <summary>
		/// 缓存
		/// </summary>
		/// <returns></returns>
		public List<Model.user_info> GetListByCache()
		{
			var cache = Common.CacheHelper.GetCache("data_user_info");//先读取  
			if (cache == null)//如果没有该缓存  
			{
				List<Model.user_info> list = GetList("", "");
				Common.CacheHelper.SetCache("data_user_info", list, 24 * 60 * 60);  //添加缓存  
				return list;
			}
			var result = (List<Model.user_info>)cache;  //有就直接返回该缓存  
			return result;
		}

		public void SetListByCache()
		{
			List<Model.user_info> list = GetList("", "");
			Common.CacheHelper.SetCache("data_user_info", list, 24 * 60 * 60);  //添加缓存  
		}
		#endregion

		/// <summary>
		/// 检查登录，并做相关处理
		/// </summary>
		public void CheckLogin()
		{
			if (Common.Cookie.UserID == 0)
			{
				HttpContext.Current.Response.Redirect("/Home/Page404/?msg=" + HttpContext.Current.Server.UrlEncode("没有登录信息！"));
			}
		}
		public bool IsCheckLogin()
		{
			return Common.Cookie.UserID == 0 ? false : true;
		}

		public string GetNameByID(int userid)
		{
			List<Model.user_info> list = GetListByCache();
			Model.user_info model = list.Where(t => t.ID == userid).FirstOrDefault();
			if (model == null) return "";
			return model.ToStudentName + model.UserName;
		}
		public string GetWeiXinIconByID(int userid)
		{
			List<Model.user_info> list = GetListByCache();
			Model.user_info model = list.Where(t => t.ID == userid).FirstOrDefault();

			if (model == null) return "";
			return model.UserName + "(" + model.WeiXinIcon + ")";
		}
	}
}
