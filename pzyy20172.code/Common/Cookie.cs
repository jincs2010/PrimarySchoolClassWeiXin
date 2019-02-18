using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace pzyy20172.Common
{
	//本应用中所有Cookie的相关类
	public class Cookie
	{
		/// <summary>
		/// 主键ID，用于判断登录，存在Session，存在Cookie是不安全的。会被篡改
		/// </summary>
		public static int UserID
		{
			get
			{
				if (System.Web.HttpContext.Current.Request.Cookies["pzyy"] != null)
					if (System.Web.HttpContext.Current.Request.Cookies["pzyy"]["UserID"] != null)
					{
						string strUser = new Common.Encrypt().Decode(System.Web.HttpContext.Current.Request.Cookies["pzyy"]["UserID"]);
						//string strUser = System.Web.HttpContext.Current.Request.Cookies["pzyy"]["UserID"];
						if (kin.Utilities.PageValidate.IsNumber(strUser))
						{
							return Convert.ToInt32(strUser);
						}
					}

				return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TestUserID"]);
			}
			set
			{
				System.Web.HttpContext.Current.Response.Cookies["pzyy"]["UserID"] = new Common.Encrypt().Encode(value.ToString());
				//System.Web.HttpContext.Current.Response.Cookies["pzyy"]["UserID"] = value.ToString();
			}
		}
	}
}
