using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace pzyy20172.Common
{
	public class ErrorHelp
	{

		public static string GetMethodInfo()
		{
			string str = "";
			StackTrace ss = new StackTrace(true);
			MethodBase mb = ss.GetFrame(1).GetMethod();
			//取得父方法类全名
			str += mb.DeclaringType.FullName + ".";
			//取得父方法名
			str += mb.Name;
			return str;
		}

		/// <summary>
		/// 将错误发送到邮箱
		/// </summary>
		public static void SendError(string MethodInfo, string ErrorInfo)
		{
			string message = "[\"pyzz\",\"" + string2Json(MethodInfo) + "\",\"" + string2Json(ErrorInfo) + "\",\"" + string2Json(System.Web.HttpContext.Current.Request.Url.AbsoluteUri) + "\",\"" + string2Json(kin.Utilities.WebHttp.GetClientIP()) + "\"]";

			string url = "http://er.wzxq.net/api/V1/";


			//设置访问信息
			byte[] buffer = Encoding.UTF8.GetBytes(message);
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

			request.Method = "POST";
			request.ContentType = "application/json";
			request.ContentLength = buffer.Length;
			request.MaximumAutomaticRedirections = 1;
			request.AllowAutoRedirect = true;

			//发送
			Stream requestStram = request.GetRequestStream();
			requestStram.Write(buffer, 0, buffer.Length);
			requestStram.Close();

			//获取返回数据
			Stream getStream = request.GetResponse().GetResponseStream();
			StreamReader sr = new StreamReader(getStream, Encoding.UTF8);
			string resultStr = sr.ReadToEnd();
			sr.Close();
			request.Abort();
			//Response.Write(resultStr);

			//try
			//{
			//	string strBody = "错误代码位置：" + MethodInfo + "<br>错误信息：" + ErrorInfo;
			//	if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Url.AbsoluteUri))
			//	{
			//		strBody += "<br>当前请求：" + System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
			//	}
			//	if (true)
			//	{
			//		strBody += "<br>客户端IP：" + kin.Utilities.WebHttp.GetClientIP();
			//	}
			//	kin.Utilities.Server.SendMail("jincs@qq.com", "程序错误报警", strBody, "jinchuanshu@126.com", "jinchuanshu@126.com", "1q2w3e", "smtp.126.com");
			//}
			//catch {
			//	kin.Utilities.Server.SendMail("jincs@qq.com", "程序错误报警", "LuCheng.BLL.Custom.SendError出错", "jinchuanshu@126.com", "jinchuanshu@126.com", "1q2w3e", "smtp.126.com");
			//}
		}

		private static string string2Json(string s)
		{
			string newstr = "";
			char[] sArray = s.ToCharArray();
			for (Int32 i = 0; i < sArray.Length; i++)
			{
				switch (sArray[i])
				{
					case '\"':
						{
							newstr += "\\\"";
							break;
						}
					case '\\':
						{
							newstr += "\\\\";
							break;
						}
					case '/':
						{
							newstr += "\\/";
							break;
						}
					case '\b':
						{
							newstr += "\\b";
							break;
						}
					case '\f':
						{
							newstr += "\\f";
							break;
						}
					case '\n':
						{
							newstr += "\\n";
							break;
						}
					case '\r':
						{
							newstr += "\\r";
							break;
						}
					case '\t':
						{
							newstr += "\\t";
							break;
						}
					default:
						{
							newstr += sArray[i];
							break;
						}
				}
			}
			return newstr;
		}
	}
}
