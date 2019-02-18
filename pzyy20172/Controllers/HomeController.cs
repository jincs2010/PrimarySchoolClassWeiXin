using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pzyy20172.Controllers
{
	public class HomeController : Controller
	{

		public ActionResult Index()
		{
			new BLL.user_info().CheckLogin();

			return View();
		}

		#region 登录及验证

		/// <summary>
		/// 身份绑定
		/// </summary>
		/// <returns></returns>
		public ActionResult Bind()
		{
			try
			{
				BLL.user_info bllUser = new BLL.user_info();

				//检查openid是否存在，存在则直接进入主页
				string strOpenID = kin.Utilities.WebHttp.GetRequest("OpenID");
				if (strOpenID == "")
				{
					return Redirect("/Home/Page404/?msg=" + Server.UrlEncode("非法请求！错误码：001"));
				}

				Model.user_info mUser = bllUser.GetSingle(t => t.OpenID == strOpenID);
				if (mUser != null)
				{
					bllUser.Login(mUser);
					Response.Redirect("/Home/Index/");
				}

				return View();
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());
				return Redirect("/Home/Page404/");
			}
		}
		public ActionResult BindUpdate()
		{
			try
			{
				JsonResult jr = new JsonResult();

				string strNickName = kin.Utilities.WebHttp.GetRequest("txtNickName");
				string strName = kin.Utilities.WebHttp.GetRequest("txtUserName");
				//int intID = kin.Utilities.WebHttp.GetRequestInt("txtIDCard");
				string strOpenID = kin.Utilities.WebHttp.GetRequest("OpenID");
				if (strOpenID == "")
				{
					jr.Data = new { result = 0, msg = "非法请求！错误码：002" };
					return jr;
				}
				if (strName == "")
				{
					jr.Data = new { result = 0, msg = "请输入孩子姓名？" };
					return jr;
				}

				BLL.user_info bllUser = new BLL.user_info();

				Model.student_teacher model = new BLL.student_teacher().GetSingle(t => t.Name == strName && t.Type == 0);
				if (model == null)
				{
					jr.Data = new { result = -1, msg = "孩子姓名不存在！" };
					return jr;
				}
				//if (model.ID != intID)
				//{
				//	jr.Data = new { result = -1, msg = "学号不正确！" };
				//	return jr;
				//}

				Model.user_info mUser = new Model.user_info();
				mUser.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				mUser.OpenID = strOpenID;
				mUser.ToStudentName = model.Name;
				mUser.Type = model.Type;
				mUser.UserName = strNickName;
				mUser.WeiXinIcon = "";
				mUser.ID = bllUser.Add(mUser);

				bllUser.Login(mUser);
				jr.Data = new { result = 1, msg = "绑定成功！" };

				return jr;
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());

				JsonResult jr = new JsonResult();
				jr.Data = new { result = -1, msg = "系统错误" };
				return jr;
			}
		}

		public ActionResult BindUpdateTeacher()
		{
			try
			{
				JsonResult jr = new JsonResult();

				string strTeacherName = kin.Utilities.WebHttp.GetRequest("txtTeacherName");
				//int intID = kin.Utilities.WebHttp.GetRequestInt("txtIDCard");
				string strOpenID = kin.Utilities.WebHttp.GetRequest("OpenID");
				if (strOpenID == "")
				{
					jr.Data = new { result = 0, msg = "非法请求！错误码：003" };
					return jr;
				}
				if (strTeacherName == "")
				{
					jr.Data = new { result = 0, msg = "请输入老师姓名？" };
					return jr;
				}

				BLL.user_info bllUser = new BLL.user_info();

				Model.student_teacher model = new BLL.student_teacher().GetSingle(t => t.Name == strTeacherName && t.Type == 1);
				if (model == null)
				{
					jr.Data = new { result = -1, msg = "老师信息不存在！" };
					return jr;
				}

				Model.user_info mUser = new Model.user_info();
				mUser.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				mUser.OpenID = strOpenID;
				mUser.ToStudentName = "";
				mUser.Type = model.Type;
				mUser.UserName = strTeacherName.Substring(0, 1) + "老师";
				mUser.WeiXinIcon = "";
				mUser.ID = bllUser.Add(mUser);

				bllUser.Login(mUser);
				jr.Data = new { result = 1, msg = "绑定成功！" };

				return jr;
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());

				JsonResult jr = new JsonResult();
				jr.Data = new { result = -1, msg = "系统错误" };
				return jr;
			}
		}
		#endregion

		//通讯录
		public ActionResult Contact()
		{
			try
			{
				//new BLL.user_info().CheckLogin();
				List<Model.student_teacher> list = new BLL.student_teacher().GetList(t => t.ID > 0, t => t.ID, false, 0);
				return View(list);
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());
				return Redirect("/Home/Page404/");
			}
		}

		public ActionResult ShowPic()
		{
			new BLL.user_info().CheckLogin();
			return View();
		}
		public ActionResult ShowMovie()
		{
			new BLL.user_info().CheckLogin();
			return View();
		}

		public ActionResult Page404()
		{
			return View();
		}
	}
}