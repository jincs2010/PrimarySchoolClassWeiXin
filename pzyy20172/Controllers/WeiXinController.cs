using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pzyy20172.Controllers
{
	public class WeiXinController : Controller
	{
		// GET: WeiXin
		public ActionResult API2()
		{
			this.HttpContext.Response.ContentType = "text/plain";
			this.HttpContext.Response.ContentEncoding = System.Text.Encoding.UTF8;

			if (Request.HttpMethod.ToLower() == "post")
			{
				string postStr = "";
				System.IO.Stream s = System.Web.HttpContext.Current.Request.InputStream;
				byte[] b = new byte[s.Length];
				s.Read(b, 0, (int)s.Length);
				postStr = System.Text.Encoding.UTF8.GetString(b);
				if (!string.IsNullOrEmpty(postStr))
				{
					//具体处理
					return Content(new Common.WeiXinTools().DealPost(postStr));
				}
				return Content("");
			}
			else
			{
				//提交URL时要校验
				return Content(kin.Utilities.WebHttp.GetRequest("echostr"));
			}
		}

		#region 公众号业务
		public ActionResult ComeIn()
		{
			string strOpenID = Request["openid"].ToString();
			return Content("strOpenID:" + strOpenID);
		}

		#endregion

		#region 公众号接口设置
		// 
		public ActionResult Setting()
		{
			return View();
		}

		//对未认证的个人公众号，没有API创建菜单功能。
		public ActionResult CreatMenu()
		{

			//string accessToken = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(Common.Const.wx_AppID, Common.Const.wx_AppSecret);

			//ButtonGroup bg = new ButtonGroup();
			//#region 第一组菜单
			////二级菜单
			//var subButton = new SubButton() { name = "班级事务" };
			//subButton.sub_button.Add(new SingleViewButton()
			//{
			//	url = "http://www.xuhuicn.com/product_hd05.html",
			//	name = "课程表"
			//});
			//subButton.sub_button.Add(new SingleViewButton()
			//{
			//	url = "http://www.xuhuicn.com/product_hd02.html",
			//	name = "作息表"
			//});
			//bg.button.Add(subButton);
			//#endregion

			//#region 第二组菜单
			//var subButton2 = new SubButton() { name = "成长历程" };
			//subButton2.sub_button.Add(new SingleViewButton()
			//{
			//	url = "http://www.xuhuicn.com",
			//	name = "活动派对"
			//});
			//subButton2.sub_button.Add(new SingleViewButton()
			//{
			//	//url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Common.Const.wx_AppID + "&redirect_uri=http://wx.xuhuicn.com/Reg.aspx&response_type=code&scope=snsapi_base&state=1#wechat_redirect",
			//	url = "http://www.xuhuicn.com",
			//	name = "比赛荣誉"
			//});
			//bg.button.Add(subButton2);
			//#endregion

			////提交微信接口
			//var result = CommonApi.CreateMenu(accessToken, bg);

			//return Json(result, JsonRequestBehavior.AllowGet);
			return View();
		}

		#endregion

		//#region 图片和视频下载

		////从微信服务器下载图片到自己的服务器
		//[HttpGet]
		//public ActionResult DownloadPic()
		//{
		//	try
		//	{
		//		string strmedia = Request["mediaid"].ToString();
		//		string accessToken = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(Common.Const.wx_AppID, Common.Const.wx_AppSecret);
		//		var url = string.Format("https://api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", accessToken, strmedia);
		//		//保存，返回在自己的服务器
		//		var PicPath = Common.WeiXinTools.GetWxPic(url, "").ToString();
		//		return Json(new { result = 1, msg = PicPath }, JsonRequestBehavior.AllowGet);
		//	}
		//	catch (Exception ex)
		//	{
		//		Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());
		//		return Json(new { result = 1, msg = "DownloadPic失败！" }, JsonRequestBehavior.AllowGet);
		//	}
		//}
		//#endregion
	}
}