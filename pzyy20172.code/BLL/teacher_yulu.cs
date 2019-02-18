using pzyy20172.Dal;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace pzyy20172.BLL
{
	public partial class teacher_yulu : Dal.DalBase<Model.teacher_yulu, Model.teacher_yulu>
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
	}
}
