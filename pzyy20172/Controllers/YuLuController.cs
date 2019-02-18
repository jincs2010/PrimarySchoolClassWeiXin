using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pzyy20172.Controllers
{
	public class YuLuController : Controller
	{
		// GET: YuLu
		public ActionResult List()
		{
			try
			{
				new BLL.user_info().CheckLogin();

				var ex = Expressionable.Create<Model.teacher_yulu>();
				ex.And(t => t.State == 0);

				int _pageindex = kin.Utilities.WebHttp.GetRequestInt("txtPage") != 0 ? Convert.ToInt32(Request["txtPage"]) : 1;
				int _pageSize = kin.Utilities.WebHttp.GetRequestInt("txtPageSize") != 0 ? Convert.ToInt32(Request["txtPageSize"]) : 15;
				int _rowCount = 0;

				List<Model.teacher_yulu> list = new BLL.teacher_yulu().GetPageList(ex.ToExpression(), t => t.AddTime, true, _pageindex, _pageSize, ref _rowCount);

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

		public ActionResult Delete()
		{
			try
			{
				JsonResult jr = new JsonResult();
				if (new BLL.user_info().IsCheckLogin() == false)
				{
					jr.Data = new { result = -1, msg = "您的身份非法！" };
					return jr;
				}

				if (kin.Utilities.WebHttp.GetRequestInt("ID") == 0)
				{
					jr.Data = new { result = -1, msg = "参数非法！" };
					return jr;
				}
				else
				{
					BLL.teacher_yulu bll = new BLL.teacher_yulu();

					Model.teacher_yulu model = bll.GetByID(Convert.ToInt32(Request["ID"]));
					model.State = 1;
					bll.Update(model);

					jr.Data = new { result = 1, msg = "删除成功！" };
					return jr;
				}
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());
				return Redirect("/Home/Page404/");
			}
		}
	}
}