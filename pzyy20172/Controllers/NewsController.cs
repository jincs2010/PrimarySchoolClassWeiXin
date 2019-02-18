using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pzyy20172.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult List()
        {
			try
			{
			new BLL.user_info().CheckLogin();

			var ex = Expressionable.Create<Model.news>();

			int _pageindex = kin.Utilities.WebHttp.GetRequestInt("txtPage") != 0 ? Convert.ToInt32(Request["txtPage"]) : 1;
			int _pageSize = kin.Utilities.WebHttp.GetRequestInt("txtPageSize") != 0 ? Convert.ToInt32(Request["txtPageSize"]) : 15;
			int _rowCount = 0;

			List<Model.news> list = new BLL.news().GetPageList(ex.ToExpression(), t => t.AddTime, true, _pageindex, _pageSize, ref _rowCount);

			ViewBag.TotalRowCount = _rowCount;
			ViewBag.PageSize = _pageSize;
			ViewBag.CurPageIndex = _pageindex;

			return View(list);
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());
				return Redirect("/Home/Page404/");
			}
		}

		public ActionResult Info()
		{
			try
			{
				new BLL.user_info().CheckLogin();

				BLL.news bllNews = new BLL.news();
				var ex1 = Expressionable.Create<Model.news>();
				var ex2 = Expressionable.Create<Model.news>();

				Model.news model = new Model.news();
				if (kin.Utilities.WebHttp.GetRequestInt("ID") > 0)
				{
					model = bllNews.GetByID(Convert.ToInt32(Request["ID"]));
					if (model == null)
						return Redirect("/Home/Page404/?msg="+Server.UrlEncode("数据不存在"));

					bllNews.Update(model);
				}

				//上一篇，下一篇
				ViewBag.PrevNew = "没有了";
				ViewBag.NextNew = "没有了";
				List<Model.news> listPrev = bllNews.GetList("AddTime<'" + model.AddTime + "'", "AddTime desc", 1);
				if (listPrev.Count > 0)
					ViewBag.PrevNew = "<a href=\"/News/Info/?ID=" + listPrev[0].ID + "\">" + listPrev[0].Title + "</a>";
				List<Model.news> listNext = bllNews.GetList("AddTime>'" + model.AddTime + "'", "AddTime", 1);
				if (listNext.Count > 0)
					ViewBag.NextNew = "<a href=\"/News/Info/?ID=" + listNext[0].ID + "\">" + listNext[0].Title + "</a>";

				return View(model);
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());
				return Redirect("/Home/Page404/");
			}
		}
	}
}