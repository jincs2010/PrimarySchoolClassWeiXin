using System;
using System.Collections.Generic;
using System.Web;

namespace kin.BLL.WeiXin
{

	/// <summary>
	///ResponseWeixin 的摘要说明
	/// </summary>
	public static class ResponseWeixin
	{
		/// <summary>
		/// 回复文本消息
		/// </summary>
		/// <param name="content">回复内容</param>
		/// <param name="UserOpenId">用户的openid</param>
		/// <param name="DevOpenId">开发者的微信号</param>
		/// <returns></returns>
		public static string ResponseText(string content, string UserOpenId, string DevOpenId)
		{
			string Txt = "<xml>";
			Txt = Txt + "<ToUserName><![CDATA[" + UserOpenId + "]]></ToUserName>";
			Txt = Txt + "<FromUserName><![CDATA[" + DevOpenId + "]]></FromUserName>";
			Txt = Txt + "<CreateTime>" + kin.BLL.WeiXin.UnixTime.FromDateTime(DateTime.Now).ToString() + "</CreateTime>";
			Txt = Txt + "<MsgType><![CDATA[text]]></MsgType>";
			Txt = Txt + "<Content><![CDATA[" + content + "]]></Content>";
			Txt = Txt + "<MsgId>00000</MsgId>";
			Txt = Txt + "</xml>";
			return Txt;
		}

		/// <summary>
		/// 回复图文消息
		/// </summary>
		/// <param name="_List">List类型的news消息，具体参照news类</param>
		/// <param name="UserOpenId">要发送用户的openid</param>
		/// <param name="DevOpenId">开发者的微信号</param>
		/// <returns></returns>
		public static string ResponsePic(List<news> _List, string UserOpenId, string DevOpenId)
		{
			string pic = "<xml>";
			pic = pic + "<ToUserName><![CDATA[" + UserOpenId + "]]></ToUserName>";
			pic = pic + "<FromUserName><![CDATA[" + DevOpenId + "]]></FromUserName>";
			pic = pic + "<CreateTime>" + kin.BLL.WeiXin.UnixTime.FromDateTime(DateTime.Now).ToString() + "</CreateTime>";
			pic = pic + "<MsgType><![CDATA[news]]></MsgType>";
			pic = pic + "<ArticleCount>" + _List.Count + "</ArticleCount>";
			pic = pic + "<Articles>";
			for (int i = 0; i < _List.Count; i++)
			{
				pic = pic + "<item>";
				pic = pic + "<Title><![CDATA[" + _List[i].Title + "]]></Title>";
				pic = pic + "<Description><![CDATA[" + _List[i].Description + "]]></Description>";
				pic = pic + "<PicUrl><![CDATA[" + _List[i].PicUrl + "]]></PicUrl>";
				pic = pic + "<Url><![CDATA[" + _List[i].Url + "]]></Url>";
				pic = pic + "</item>";
			}
			pic = pic + "</Articles>";
			pic = pic + "<FuncFlag>1</FuncFlag>";
			pic = pic + "</xml>";
			return pic;
		}

        /// <summary>
        /// 转发给多客服
        /// </summary>
        /// <param name="UserOpenId">用户的openid</param>
        /// <param name="DevOpenId">开发者的微信号</param>
        public static string ResponseTransKF(string UserOpenId, string DevOpenId, string _CreateTime)
        {
            string Txt = "<xml>";
            Txt = Txt + "<ToUserName><![CDATA[" + UserOpenId + "]]></ToUserName>";
            Txt = Txt + "<FromUserName><![CDATA[" + DevOpenId + "]]></FromUserName>";
            Txt = Txt + "<CreateTime>" + _CreateTime + "</CreateTime>";
            Txt = Txt + "<MsgType><![CDATA[transfer_customer_service]]></MsgType>";
            Txt = Txt + "</xml>";
            return Txt;
        }
	}
}