using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace kin.BLL.WeiXin
{

	#region 五种微信xml消息的类封装

	public class TextType
	{

		private string ToUserName1;
		/// <summary>
		/// 开发者微信号 
		/// </summary>
		public string ToUserName
		{
			get { return ToUserName1; }
			set { ToUserName1 = value; }
		}

		private string FromUserName1;
		/// <summary>
		/// 发送方帐号（一个OpenID） 
		/// </summary>
		public string FromUserName
		{
			get { return FromUserName1; }
			set { FromUserName1 = value; }
		}

		private string CreateTime1;
		/// <summary>
		/// 消息创建时间 （整型）
		/// </summary>
		public string CreateTime
		{
			get { return CreateTime1; }
			set { CreateTime1 = value; }
		}

		private string MsgType1 = "text";
		/// <summary>
		///消息类型：text
		/// </summary>
		public string MsgType
		{
			get { return MsgType1; }
			set { MsgType1 = value; }
		}

		private string Content1;
		/// <summary>
		/// 文本消息内容 
		/// </summary>
		public string Content
		{
			get { return Content1; }
			set { Content1 = value; }
		}

		private string MsgId1;
		/// <summary>
		/// 消息id，64位整型 
		/// </summary>
		public string MsgId
		{
			get { return MsgId1; }
			set { MsgId1 = value; }
		}
	}
	public class PicType
	{

		private string ToUserName1;
		/// <summary>
		/// 开发者微信号 
		/// </summary>
		public string ToUserName
		{
			get { return ToUserName1; }
			set { ToUserName1 = value; }
		}

		private string FromUserName1;
		/// <summary>
		/// 发送方帐号（一个OpenID） 
		/// </summary>
		public string FromUserName
		{
			get { return FromUserName1; }
			set { FromUserName1 = value; }
		}

		private string CreateTime1;
		/// <summary>
		/// 消息创建时间 （整型）
		/// </summary>
		public string CreateTime
		{
			get { return CreateTime1; }
			set { CreateTime1 = value; }
		}

		private string MsgType1 = "image";
		/// <summary>
		/// 消息类型：image
		/// </summary>
		public string MsgType
		{
			get { return MsgType1; }
			set { MsgType1 = value; }
		}

		private string PicUrl1;
		/// <summary>
		/// 图片链接 
		/// </summary>
		public string PicUrl
		{
			get { return PicUrl1; }
			set { PicUrl1 = value; }
		}

		private string MsgId1;
		/// <summary>
		/// 消息id，64位整型 
		/// </summary>
		public string MsgId
		{
			get { return MsgId1; }
			set { MsgId1 = value; }
		}

		private string MediaId1;

		public string MediaId
		{
			get { return MediaId1; }
			set { MediaId1 = value; }
		}

	}
	public class VoiceType
	{

		private string ToUserName1;
		/// <summary>
		/// 开发者微信号 
		/// </summary>
		public string ToUserName
		{
			get { return ToUserName1; }
			set { ToUserName1 = value; }
		}

		private string FromUserName1;
		/// <summary>
		/// 发送方帐号（一个OpenID） 
		/// </summary>
		public string FromUserName
		{
			get { return FromUserName1; }
			set { FromUserName1 = value; }
		}

		private string CreateTime1;
		/// <summary>
		/// 消息创建时间 （整型）
		/// </summary>
		public string CreateTime
		{
			get { return CreateTime1; }
			set { CreateTime1 = value; }
		}

		private string MsgType1 = "video";
		/// <summary>
		/// 消息类型：video
		/// </summary>
		public string MsgType
		{
			get { return MsgType1; }
			set { MsgType1 = value; }
		}

		private string MediaId1;

		public string MediaId
		{
			get { return MediaId1; }
			set { MediaId1 = value; }
		}

		private string Format1;
		/// <summary>
		/// 图片链接 
		/// </summary>
		public string Format
		{
			get { return Format1; }
			set { Format1 = value; }
		}

		private string MsgId1;
		/// <summary>
		/// 消息id，64位整型 
		/// </summary>
		public string MsgId
		{
			get { return MsgId1; }
			set { MsgId1 = value; }
		}

		private string _Recognition;
		/// <summary>
		/// 消息id，64位整型 
		/// </summary>
		public string Recognition
		{
			get { return _Recognition; }
			set { _Recognition = value; }
		}
	}

	public class VideoType
	{

		private string ToUserName1;
		/// <summary>
		/// 开发者微信号 
		/// </summary>
		public string ToUserName
		{
			get { return ToUserName1; }
			set { ToUserName1 = value; }
		}

		private string FromUserName1;
		/// <summary>
		/// 发送方帐号（一个OpenID） 
		/// </summary>
		public string FromUserName
		{
			get { return FromUserName1; }
			set { FromUserName1 = value; }
		}

		private string CreateTime1;
		/// <summary>
		/// 消息创建时间 （整型）
		/// </summary>
		public string CreateTime
		{
			get { return CreateTime1; }
			set { CreateTime1 = value; }
		}

		private string MsgType1 = "video";
		/// <summary>
		/// 消息类型：video
		/// </summary>
		public string MsgType
		{
			get { return MsgType1; }
			set { MsgType1 = value; }
		}

		private string ThumbMediaId1;
		/// <summary>
		/// 图片链接 
		/// </summary>
		public string ThumbMediaId
		{
			get { return ThumbMediaId1; }
			set { ThumbMediaId1 = value; }
		}

		private string MsgId1;
		/// <summary>
		/// 消息id，64位整型 
		/// </summary>
		public string MsgId
		{
			get { return MsgId1; }
			set { MsgId1 = value; }
		}

		private string MediaId1;

		public string MediaId
		{
			get { return MediaId1; }
			set { MediaId1 = value; }
		}

	}
	public class LocationType
	{

		private string ToUserName1;
		/// <summary>
		/// 开发者微信号 
		/// </summary>
		public string ToUserName
		{
			get { return ToUserName1; }
			set { ToUserName1 = value; }
		}

		private string FromUserName1;
		/// <summary>
		/// 发送方帐号（一个OpenID） 
		/// </summary>
		public string FromUserName
		{
			get { return FromUserName1; }
			set { FromUserName1 = value; }
		}

		private string CreateTime1;
		/// <summary>
		/// 消息创建时间 （整型）
		/// </summary>
		public string CreateTime
		{
			get { return CreateTime1; }
			set { CreateTime1 = value; }
		}

		private string MsgType1;
		/// <summary>
		/// location
		/// </summary>
		public string MsgType
		{
			get { return MsgType1; }
			set { MsgType1 = value; }
		}

		private string Location_X1;
		/// <summary>
		/// 地理位置纬度 
		/// </summary>
		public string Location_X
		{
			get { return Location_X1; }
			set { Location_X1 = value; }
		}

		private string Location_Y1;
		/// <summary>
		/// 地理位置经度
		/// </summary>
		public string Location_Y
		{
			get { return Location_Y1; }
			set { Location_Y1 = value; }
		}

		private string Scale1;
		/// <summary>
		/// 地图缩放大小
		/// </summary>
		public string Scale
		{
			get { return Scale1; }
			set { Scale1 = value; }
		}

		private string Label1;
		/// <summary>
		/// 地理位置信息 
		/// </summary>
		public string Label
		{
			get { return Label1; }
			set { Label1 = value; }
		}

		private string MsgId1;
		/// <summary>
		/// 消息id，64位整型 
		/// </summary>
		public string MsgId
		{
			get { return MsgId1; }
			set { MsgId1 = value; }
		}
	}
	public class LinkType
	{
		private string ToUserName1;
		/// <summary>
		/// 开发者微信号 
		/// </summary>
		public string ToUserName
		{
			get { return ToUserName1; }
			set { ToUserName1 = value; }
		}

		private string FromUserName1;
		/// <summary>
		/// 发送方帐号（一个OpenID） 
		/// </summary>
		public string FromUserName
		{
			get { return FromUserName1; }
			set { FromUserName1 = value; }
		}

		private string CreateTime1;
		/// <summary>
		/// 消息创建时间 （整型）
		/// </summary>
		public string CreateTime
		{
			get { return CreateTime1; }
			set { CreateTime1 = value; }
		}

		private string MsgType1 = "text";
		/// <summary>
		/// link
		/// </summary>
		public string MsgType
		{
			get { return MsgType1; }
			set { MsgType = value; }
		}

		private string Title1;
		/// <summary>
		/// 消息标题 
		/// </summary>
		public string Title
		{
			get { return Title1; }
			set { Title1 = value; }
		}

		private string Description1;
		/// <summary>
		/// 消息描述 
		/// </summary>
		public string Description
		{
			get { return Description1; }
			set { Description1 = value; }
		}

		private string Url1;
		/// <summary>
		/// 消息链接 
		/// </summary>
		public string Url
		{
			get { return Url1; }
			set { Url1 = value; }
		}

		private string MsgId1;
		/// <summary>
		/// 消息id，64位整型 
		/// </summary>
		public string MsgId
		{
			get { return MsgId1; }
			set { MsgId1 = value; }
		}
	}
	public class EventType
	{
		private string Event1;
		/// <summary>
		/// 事件类型，subscribe(订阅)、unsubscribe(取消订阅)、CLICK(自定义菜单点击事件) 
		/// </summary>
		public string Event
		{
			get { return Event1; }
			set { Event1 = value; }
		}

		//King代码
		private string EventKey1 = "";
		/// <summary>
		/// CLICK(自定义菜单点击事件的标识
		/// </summary>
		public string EventKey
		{
			get { return EventKey1; }
			set { EventKey1 = value; }
		}

		private string ToUserName1;
		/// <summary>
		/// 开发者微信号 
		/// </summary>
		public string ToUserName
		{
			get { return ToUserName1; }
			set { ToUserName1 = value; }
		}

		private string FromUserName1;
		/// <summary>
		/// 发送方帐号（一个OpenID） 
		/// </summary>
		public string FromUserName
		{
			get { return FromUserName1; }
			set { FromUserName1 = value; }
		}

		private string CreateTime1;
		/// <summary>
		/// 消息创建时间 （整型）
		/// </summary>
		public string CreateTime
		{
			get { return CreateTime1; }
			set { CreateTime1 = value; }
		}
	}

	#endregion

	/*
 * 调用构造函数DealXml(weixinXML)，得到MsgType和XML集合xn
 * 根据MsgType调用相应的方法将xn转化成对应的类
 * 使用类进行操作
 */
	public class DealXml
	{
		private string _msgtype;
		/// <summary>
		/// 消息类型
		/// </summary>
		public string MsgType
		{
			get { return _msgtype; }
			set { _msgtype = value; }
		}

		private string _fromusername;
		/// <summary>
		/// 访问者
		/// </summary>
		public string FromUserName
		{
			get { return _fromusername; }
			set { _fromusername = value; }
		}

		private XmlNode xn;

		public XmlNode Xn
		{
			get { return xn; }
			set { xn = value; }
		}

		public DealXml(string weixinXML)
		{
			System.Xml.XmlDocument doc = new XmlDocument();
			doc.LoadXml(weixinXML);
			XmlNodeList list = doc.GetElementsByTagName("xml");
			xn = list[0];
			MsgType = xn.SelectSingleNode("//MsgType").InnerText;
			FromUserName = xn.SelectSingleNode("//FromUserName").InnerText;
		}

		public TextType NewTextType(XmlNode xn)
		{
			TextType tt = new TextType();
			tt.ToUserName = xn.SelectSingleNode("//ToUserName").InnerText;
			tt.FromUserName = xn.SelectSingleNode("//FromUserName").InnerText;
			tt.CreateTime = xn.SelectSingleNode("//CreateTime").InnerText;
			tt.MsgType = xn.SelectSingleNode("//MsgType").InnerText;
			tt.Content = xn.SelectSingleNode("//Content").InnerText;
			tt.MsgId = xn.SelectSingleNode("//MsgId").InnerText;
			return tt;
		}
		public PicType NewPicType(XmlNode xn)
		{
			PicType tt = new PicType();
			tt.ToUserName = xn.SelectSingleNode("//ToUserName").InnerText;
			tt.FromUserName = xn.SelectSingleNode("//FromUserName").InnerText;
			tt.CreateTime = xn.SelectSingleNode("//CreateTime").InnerText;
			tt.MsgType = xn.SelectSingleNode("//MsgType").InnerText;
			tt.PicUrl = xn.SelectSingleNode("//PicUrl").InnerText;
			tt.MsgId = xn.SelectSingleNode("//MsgId").InnerText;
			tt.MediaId = xn.SelectSingleNode("//MediaId").InnerText;
			return tt;
		}
		public VideoType NewVideoType(XmlNode xn)
		{
			VideoType vt = new VideoType();
			vt.ToUserName = xn.SelectSingleNode("//ToUserName").InnerText;
			vt.FromUserName = xn.SelectSingleNode("//FromUserName").InnerText;
			vt.CreateTime = xn.SelectSingleNode("//CreateTime").InnerText;
			vt.MsgType = xn.SelectSingleNode("//MsgType").InnerText;
			vt.ThumbMediaId = xn.SelectSingleNode("//ThumbMediaId").InnerText;
			vt.MsgId = xn.SelectSingleNode("//MsgId").InnerText;
			vt.MediaId = xn.SelectSingleNode("//MediaId").InnerText;
			return vt;
		}
		public VoiceType NewVoiceType(XmlNode xn)
		{
			VoiceType vt = new VoiceType();
			vt.ToUserName = xn.SelectSingleNode("//ToUserName").InnerText;
			vt.FromUserName = xn.SelectSingleNode("//FromUserName").InnerText;
			vt.CreateTime = xn.SelectSingleNode("//CreateTime").InnerText;
			vt.MsgType = xn.SelectSingleNode("//MsgType").InnerText;
			vt.Format = xn.SelectSingleNode("//Format").InnerText;
			vt.MsgId = xn.SelectSingleNode("//MsgId").InnerText;
			vt.MediaId = xn.SelectSingleNode("//MediaId").InnerText;
			vt.Recognition = xn.SelectSingleNode("//Recognition").InnerText;
			return vt;
		}
		public LocationType NewLocationType(XmlNode xn)
		{
			LocationType tt = new LocationType();
			tt.ToUserName = xn.SelectSingleNode("//ToUserName").InnerText;
			tt.FromUserName = xn.SelectSingleNode("//FromUserName").InnerText;
			tt.CreateTime = xn.SelectSingleNode("//CreateTime").InnerText;
			tt.MsgType = xn.SelectSingleNode("//MsgType").InnerText;
			tt.Location_X = xn.SelectSingleNode("//Location_X").InnerText;
			tt.Location_Y = xn.SelectSingleNode("//Location_Y").InnerText;
			tt.Scale = xn.SelectSingleNode("//Scale").InnerText;
			tt.Label = xn.SelectSingleNode("//Label").InnerText;
			tt.MsgId = xn.SelectSingleNode("//MsgId").InnerText;
			return tt;
		}
		public LinkType NewLinkType(XmlNode xn)
		{
			LinkType tt = new LinkType();
			tt.ToUserName = xn.SelectSingleNode("//ToUserName").InnerText;
			tt.FromUserName = xn.SelectSingleNode("//FromUserName").InnerText;
			tt.CreateTime = xn.SelectSingleNode("//CreateTime").InnerText;
			tt.MsgType = xn.SelectSingleNode("//MsgType").InnerText;
			tt.Title = xn.SelectSingleNode("//Title").InnerText;
			tt.Description = xn.SelectSingleNode("//Description").InnerText;
			tt.Url = xn.SelectSingleNode("//Url").InnerText;
			tt.MsgId = xn.SelectSingleNode("//MsgId").InnerText;
			return tt;

		}
		public EventType NewEnentType(XmlNode xn)
		{
			EventType tt = new EventType();
			try
			{
				tt.ToUserName = xn.SelectSingleNode("//ToUserName").InnerText;
				tt.FromUserName = xn.SelectSingleNode("//FromUserName").InnerText;
				tt.Event = xn.SelectSingleNode("//Event").InnerText;
				tt.EventKey = xn.SelectSingleNode("//EventKey").InnerText;
				tt.CreateTime = xn.SelectSingleNode("//CreateTime").InnerText;
			}
			catch { }
			return tt;
		}

	}


	public class news
	{
		private string Title1;
		/// <summary>
		/// 图文消息标题 
		/// </summary>
		public string Title
		{
			get { return Title1; }
			set { Title1 = value; }
		}
		private string Description1;
		/// <summary>
		/// 图文消息描述 
		/// </summary>
		public string Description
		{
			get { return Description1; }
			set { Description1 = value; }
		}
		private string PicUrl1;
		/// <summary>
		/// 图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80，限制图片链接的域名需要与开发者填写的基本资料中的Url一致
		/// </summary>
		public string PicUrl
		{
			get { return PicUrl1; }
			set { PicUrl1 = value; }
		}
		private string Url1;
		/// <summary>
		///  	点击图文消息跳转链接 
		/// </summary>
		public string Url
		{
			get { return Url1; }
			set { Url1 = value; }
		}
	}
}