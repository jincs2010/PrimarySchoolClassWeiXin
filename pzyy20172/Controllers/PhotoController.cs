using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlSugar;

namespace pzyy20172.Controllers
{
	public class PhotoController : Controller
	{
		#region 相册

		// GET: Photo
		public ActionResult AlbumList()
		{
			try
			{
				new BLL.user_info().CheckLogin();

				var ex = Expressionable.Create<Model.photo_album>().And(t => t.ID != 0);
				int _pageindex = kin.Utilities.WebHttp.GetRequestInt("txtPage") != 0 ? Convert.ToInt32(Request["txtPage"]) : 1;
				int _pageSize = kin.Utilities.WebHttp.GetRequestInt("txtPageSize") != 0 ? Convert.ToInt32(Request["txtPageSize"]) : 15;
				int _rowCount = 0;

				List<Model.photo_album> list = new BLL.photo_album().GetPageList(ex.ToExpression(), t => t.UpTime, true, _pageindex, _pageSize, ref _rowCount);

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

		public ActionResult AlbumAdd()
		{
			new BLL.user_info().CheckLogin();
			return View();
		}
		public ActionResult AlbumAddUpdate()
		{
			try
			{
				JsonResult jr = new JsonResult();
				if (new BLL.user_info().IsCheckLogin() == false)
				{
					jr.Data = new { result = -1, msg = "您的身份非法！" };
					return jr;
				}

				Model.photo_album model = new Model.photo_album();
				model.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				model.AlbumName = kin.Utilities.WebHttp.GetRequest("AlbumName");
				model.Descript = kin.Utilities.WebHttp.GetRequest("Descript");
				model.PhotoCount = 0;
				model.ToUserID = Common.Cookie.UserID;
				model.UpTime = model.AddTime;
				new BLL.photo_album().Add(model);

				jr.Data = new { result = 1, msg = "相册创建成功！" };
				return jr;
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());
				return Redirect("/Home/Page404/");
			}
		}

		#endregion

		#region 照片

		public ActionResult PhotoList()
		{
			try
			{
				new BLL.user_info().CheckLogin();

				int intAlbumID = kin.Utilities.WebHttp.GetRequestInt("ID");
				if (intAlbumID == 0)
					return Redirect("/Home/Page404/?msg=" + Server.UrlEncode("参数非法！"));

				Model.photo_album mAlbum = new BLL.photo_album().GetByID(intAlbumID);
				if (intAlbumID == 0)
					return Redirect("/Home/Page404/?msg=" + Server.UrlEncode("相册不存在！"));

				ViewBag.AlbumName = mAlbum.AlbumName;

				var ex = Expressionable.Create<Model.photo_list>();
				ex.And(t => t.ToPhtoAlbumID == intAlbumID);
				ex.And(t => t.State == 0);

				int _pageindex = kin.Utilities.WebHttp.GetRequestInt("txtPage") != 0 ? Convert.ToInt32(Request["txtPage"]) : 1;
				int _pageSize = kin.Utilities.WebHttp.GetRequestInt("txtPageSize") != 0 ? Convert.ToInt32(Request["txtPageSize"]) : 10;
				int _rowCount = 0;

				List<Model.photo_list> list = new BLL.photo_list().GetPageList(ex.ToExpression(), t => t.AddTime, true, _pageindex, _pageSize, ref _rowCount);

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

		public ActionResult PhotoAdd()
		{
			new BLL.user_info().CheckLogin();
			return View();
		}
		public ActionResult PhotoAddUpdate()
		{
			try
			{
				JsonResult jr = new JsonResult();
				if (new BLL.user_info().IsCheckLogin() == false)
				{
					jr.Data = new { result = -1, msg = "您的身份非法！" };
					return jr;
				}

				Model.photo_list model = new Model.photo_list();
				model.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
				model.ToPhtoAlbumID = kin.Utilities.WebHttp.GetRequestInt("AlbumID");
				model.Url = kin.Utilities.WebHttp.GetRequest("Url");
				model.ToUserID = Common.Cookie.UserID;
				model.State = 0;
				new BLL.photo_list().Add(model);

				//更新相册统计信息
				new BLL.photo_album().UpdateStat(model.ToPhtoAlbumID, model.Url);

				jr.Data = new { result = 1, msg = "照片上传成功！" };
				return jr;
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());
				return Redirect("/Home/Page404/");
			}
		}
		public ActionResult PhotoDelete()
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
					BLL.photo_list bll = new BLL.photo_list();

					Model.photo_list model = bll.GetByID(Convert.ToInt32(Request["ID"]));
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

		#endregion

	}
}