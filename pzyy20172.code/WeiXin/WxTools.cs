using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text;

namespace kin.BLL.WeiXin
{

	/// <summary>
	/// King自定义的微信工具类
	/// </summary>
	public class WxTools
	{

		//private string _CodeCheckUrl = "";

		#region AccessToken的处理

		/// <summary>
		/// 主调函数：返回订阅号或服务号的access_token，2个小时内不变化
		/// </summary>
		/// <param name="type">1、温州相亲网，2、温州征婚网</param>
		/// <returns></returns>
		public string GetAccessToken()
		{
			if (HttpContext.Current.Application["access_token"] == null)
			{
				return GetAccessTokenFromServer();
			}
			else
			{
				//过时了
				if (Convert.ToDateTime(HttpContext.Current.Application["access_token_time"]) < DateTime.Now)
					return GetAccessTokenFromServer();
				else
					return HttpContext.Current.Application["access_token"].ToString();
			}
		}

		//外部写入
		public void SetAccessToken(Int32 type, string access_token)
		{
			HttpContext.Current.Application["access_token_" + type] = access_token;
			HttpContext.Current.Application["access_token_" + type + "_time"] = DateTime.Now.AddMinutes(100);	//预留20分钟
		}

		//通过微信接口获取新的access_token给Application，并返回
		private string GetAccessTokenFromServer()
		{
			string _json = GetPage("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + pzyy20172.Common.Const.wx_AppID + "&secret=" + pzyy20172.Common.Const.wx_AppSecret, "");

			BLL.Comm.JsonUtils.JsonObject _jsonValue = BLL.Comm.JsonUtils.JsonObject.Parse(_json);

			HttpContext.Current.Application["access_token"] = _jsonValue["access_token"].Text();
			HttpContext.Current.Application["access_token_time"] = DateTime.Now.AddMinutes(100);   //预留20分钟
			return _jsonValue["access_token"].Text();
		}

		#endregion

		#region 网页授权相关处理

		/*说明：
		 * 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
		 * 1、网页授权中的access_token，时间很短，最好是每次都重新获取
		 * 2、网页授权中的access_token，不能复用
		 * 3、网页授权中的access_token，调用次数不限
		 */

		/// <summary>
		/// 通过网页授权的Code，请求微信接口获取用户OpenID
		/// </summary>
		/// <returns></returns>
		public string GetOpenIDByCode(string code)
		{
			//向公众号接口请求
			string strJson = GetPage("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + pzyy20172.Common.Const.wx_AppID + "&secret=" + pzyy20172.Common.Const.wx_AppSecret + "&code=" + code + "&grant_type=authorization_code", "");
			BLL.Comm.JsonUtils.JsonObject _jsonValue = BLL.Comm.JsonUtils.JsonObject.Parse(strJson);

			return _jsonValue["openid"].Text();
		}


		#endregion

		#region 全局通用

		//数据提交至微信服务器
		public string GetPage(string posturl, string postData)
		{
			Stream outstream = null;
			Stream instream = null;
			StreamReader sr = null;
			HttpWebResponse response = null;
			HttpWebRequest request = null;
			Encoding encoding = Encoding.UTF8;
			byte[] data = encoding.GetBytes(postData);

			// 准备请求...
			try
			{
				// 设置参数
				request = WebRequest.Create(posturl) as HttpWebRequest;
				CookieContainer cookieContainer = new CookieContainer();
				request.CookieContainer = cookieContainer;
				request.AllowAutoRedirect = true;
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = data.Length;
				outstream = request.GetRequestStream();
				outstream.Write(data, 0, data.Length);
				outstream.Close();
				//发送请求并获取相应回应数据
				response = request.GetResponse() as HttpWebResponse;
				//直到request.GetResponse()程序才开始向目标网页发送Post请求
				instream = response.GetResponseStream();
				sr = new StreamReader(instream, encoding);
				//返回结果网页（html）代码
				string content = sr.ReadToEnd();
				string err = string.Empty;
				return content;
			}
			catch (Exception ex)
			{
				string err = ex.Message;
				HttpContext.Current.Response.Write(err);
				return string.Empty;
			}
		}

		#endregion

	}
}