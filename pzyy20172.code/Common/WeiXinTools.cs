using kin.BLL.WeiXin;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace pzyy20172.Common
{
	//摘自OA.xuhuicn，只摘了部分
	public class WeiXinTools
	{
		#region API接口处理

		private string strTeacherYuLu = "已存入语录！回复“班级”二字进入官微。";
		private string strParentYuLu = "回复“班级”二字进入官微。";

		// 主函数：处理消息
		public string DealPost(string postStr)
		{
			try
			{
				kin.BLL.WeiXin.DealXml dx = new kin.BLL.WeiXin.DealXml(postStr);
				pzyy20172.BLL.user_info bllUser = new pzyy20172.BLL.user_info();

				#region 消息类型：文本
				if (dx.MsgType.Equals("text"))
				{
					kin.BLL.WeiXin.TextType tt = dx.NewTextType(dx.Xn);
					SaveLog(tt.FromUserName, kin.BLL.WeiXin.UnixTime.FromUnixTime(tt.CreateTime), postStr);
					//是老师发的文字、图片、视频，就纳入到老师语录，并回复给老师

					if (tt.Content.Trim() == "班级")
					{
						//回复“班级”二字进入官微
						return SendTuWen(tt.FromUserName, tt.ToUserName);
					}

					pzyy20172.Model.user_info mUser = bllUser.GetSingle(t => t.OpenID == tt.FromUserName);
					if (mUser != null)
					{
						//已关注：老师发的，存入语录，返回提示成功；家长发的，不理睬，返回文字
						if (mUser.Type == 1)
						{
							pzyy20172.Model.teacher_yulu mYuLu = new pzyy20172.Model.teacher_yulu();
							mYuLu.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
							mYuLu.Content = tt.Content;
							mYuLu.OpenID = tt.FromUserName;
							mYuLu.State = 0;
							mYuLu.ToUserID = mUser.ID;
							mYuLu.Type = mUser.Type;
							mYuLu.MsgType = 1;
							new pzyy20172.BLL.teacher_yulu().Add(mYuLu);

							return ResponseWeixin.ResponseText("文字" + strTeacherYuLu, tt.FromUserName, tt.ToUserName);
						}
						else
						{
							return ResponseWeixin.ResponseText(strParentYuLu, tt.FromUserName, tt.ToUserName);
						}
					}
					else
						//没有关注的人，返回图文
						return SendTuWen(tt.FromUserName, tt.ToUserName);
				}
				#endregion
				#region 消息类型：图片
				else if (dx.MsgType.Equals("image"))
				{
					kin.BLL.WeiXin.PicType pt = dx.NewPicType(dx.Xn);
					SaveLog(pt.FromUserName, kin.BLL.WeiXin.UnixTime.FromUnixTime(pt.CreateTime), postStr);

					pzyy20172.Model.user_info mUser = bllUser.GetSingle(t => t.OpenID == pt.FromUserName);
					if (mUser != null)
					{
						if (mUser.Type == 1)
						{
							//下载图片
							string strPath = new pzyy20172.Common.WeiXinTools().DownloadMedia(pt.MediaId, 2);
							pzyy20172.Model.teacher_yulu mYuLu = new pzyy20172.Model.teacher_yulu();
							mYuLu.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
							mYuLu.Content = strPath;    //pt.PicUrl;
							mYuLu.OpenID = pt.FromUserName;
							mYuLu.State = 0;
							mYuLu.ToUserID = mUser.ID;
							mYuLu.Type = mUser.Type;
							mYuLu.MsgType = 2;
							new pzyy20172.BLL.teacher_yulu().Add(mYuLu);

							return ResponseWeixin.ResponseText("图片" + strTeacherYuLu, pt.FromUserName, pt.ToUserName);
						}
						else
						{
							return ResponseWeixin.ResponseText(strParentYuLu, pt.FromUserName, pt.ToUserName);
						}
					}
					else
						return SendTuWen(pt.FromUserName, pt.ToUserName);
				}
				#endregion
				#region 消息类型：语音
				else if (dx.MsgType.Equals("voice"))
				{
					kin.BLL.WeiXin.VoiceType pt = dx.NewVoiceType(dx.Xn);
					SaveLog(pt.FromUserName, kin.BLL.WeiXin.UnixTime.FromUnixTime(pt.CreateTime), postStr);

					pzyy20172.Model.user_info mUser = bllUser.GetSingle(t => t.OpenID == pt.FromUserName);
					if (mUser != null)
					{
						if (mUser.Type == 1)
						{
							//下载
							string strPath = new pzyy20172.Common.WeiXinTools().DownloadMedia(pt.MediaId, 3);
							//Amr转MP3
							new Common.VoiceHelper().ConvertToMp3(strPath);
							pzyy20172.Model.teacher_yulu mYuLu = new pzyy20172.Model.teacher_yulu();
							mYuLu.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
							mYuLu.Content = strPath + ".mp3";
							mYuLu.OpenID = pt.FromUserName;
							mYuLu.State = 0;
							mYuLu.ToUserID = mUser.ID;
							mYuLu.Type = mUser.Type;
							mYuLu.MsgType = 3;
							new pzyy20172.BLL.teacher_yulu().Add(mYuLu);

							return ResponseWeixin.ResponseText("语音" + strTeacherYuLu, pt.FromUserName, pt.ToUserName);
						}
						else
						{
							return ResponseWeixin.ResponseText(strParentYuLu, pt.FromUserName, pt.ToUserName);
						}
					}
					else
						return SendTuWen(pt.FromUserName, pt.ToUserName);
				}
				#endregion
				#region 消息类型：视频
				else if (dx.MsgType.Equals("video"))
				{
					kin.BLL.WeiXin.VideoType pt = dx.NewVideoType(dx.Xn);
					SaveLog(pt.FromUserName, kin.BLL.WeiXin.UnixTime.FromUnixTime(pt.CreateTime), postStr);

					pzyy20172.Model.user_info mUser = bllUser.GetSingle(t => t.OpenID == pt.FromUserName);
					if (mUser != null)
					{
						if (mUser.Type == 1)
						{
							//下载
							string strPath = new pzyy20172.Common.WeiXinTools().DownloadMedia(pt.MediaId, 4);
							string strPathThumb = new pzyy20172.Common.WeiXinTools().DownloadMedia(pt.ThumbMediaId, 2);
							pzyy20172.Model.teacher_yulu mYuLu = new pzyy20172.Model.teacher_yulu();
							mYuLu.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
							mYuLu.Content = strPath + "|" + strPathThumb;
							mYuLu.OpenID = pt.FromUserName;
							mYuLu.State = 0;
							mYuLu.ToUserID = mUser.ID;
							mYuLu.Type = mUser.Type;
							mYuLu.MsgType = 4;
							new pzyy20172.BLL.teacher_yulu().Add(mYuLu);

							return ResponseWeixin.ResponseText("视频" + strTeacherYuLu, pt.FromUserName, pt.ToUserName);
						}
						else
						{
							return ResponseWeixin.ResponseText(strParentYuLu, pt.FromUserName, pt.ToUserName);
						}
					}
					else
						return SendTuWen(pt.FromUserName, pt.ToUserName);
				}
				#endregion

				#region 消息类型：事件，包括点击－菜单
				else if (dx.MsgType.Equals("event"))
				{
					kin.BLL.WeiXin.EventType et = dx.NewEnentType(dx.Xn);
					SaveLog(et.FromUserName, kin.BLL.WeiXin.UnixTime.FromUnixTime(et.CreateTime), postStr);

					#region 关注、订阅
					if (et.Event.Equals("subscribe"))
					{
						return SendTuWen(et.FromUserName, et.ToUserName);
					}
					#endregion
				}
				#endregion
				else
				{
					SaveLog("未解析", DateTime.Now, postStr);
				}
				return "";
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());
				return "";
			}
		}

		//回复主图文
		private string SendTuWen(string FromUserName, string ToUserName)
		{
			List<news> _List = new List<news>();
			news ne = new news();
			ne.Description = "欢迎关注温州市蒲州育英学校2017级(2)班官方微信公众号！";
			ne.PicUrl = pzyy20172.Common.Const.SITE_URL + "/upload/images/guanzhu_banner.jpg";
			ne.Title = "点击此图文进入四叶草班级官微。";
			ne.Url = pzyy20172.Common.Const.SITE_URL + "/Home/Bind/?openid=" + FromUserName;
			_List.Add(ne);
			return kin.BLL.WeiXin.ResponseWeixin.ResponsePic(_List, FromUserName, ToUserName);
		}
		//所有请求写入日志
		private void SaveLog(string fromUserName, DateTime createTime, string xMLContent)
		{
			pzyy20172.Model.post_log mLog = new pzyy20172.Model.post_log();
			mLog.FromUserName = fromUserName;
			mLog.CreateTime = createTime.ToString("yyyy-MM-dd HH:mm:ss");
			mLog.XMLContent = xMLContent;
			new pzyy20172.BLL.post_log().Add(mLog);
		}
		#endregion

		#region 下载媒体

		/// <summary>
		/// 下载保存多媒体文件,返回多媒体保存路径 
		/// </summary>
		/// <param name="mediaId"></param>
		/// <param name="msgType"></param>
		/// <returns></returns>
		public string DownloadMedia(string mediaId, int msgType)
		{
			try
			{
				string accessToken = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(Common.Const.wx_AppID, Common.Const.wx_AppSecret);
				//return GetMultimedia(accessToken, mediaId, msgType);
				string content = string.Empty;
				string strpath = string.Empty;
				string savepath = string.Empty;
				string stUrl = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token=" + accessToken + "&media_id=" + mediaId;

				string strExName = "";
				if (msgType == 2) strExName = ".jpg";
				else if (msgType == 3) strExName = ".amr";
				else if (msgType == 4) strExName = ".mp4";

				HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(stUrl);

				req.Method = "GET";
				using (WebResponse wr = req.GetResponse())
				{
					HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

					strpath = myResponse.ResponseUri.ToString();
					WebClient mywebclient = new WebClient();
					savepath = "/Upload/" + DateTime.Now.ToString("yyyyMM") + "/" + kin.Utilities.String.RndDateStr() + strExName;
					try
					{
						mywebclient.DownloadFile(strpath, HttpContext.Current.Server.MapPath(savepath));
					}
					catch (Exception ex)
					{
						savepath = ex.ToString();
					}

				}
				return savepath;
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());
				return "";
			}
		}


		#endregion
	}
}
