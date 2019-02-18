using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace pzyy20172.Controllers
{
	public class DataController : Controller
	{
		// GET: Data
		public ActionResult Index()
		{
			return View();
		}

		//上传控件后台处理程序,20150708
		//2015.11.03：为何要带参数？会传参数过来吗？是的，自己会传过来。
		public ActionResult UpLoadProcess(string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
		{
			try
			{
				//文件名，不带目录
				string fileName = string.Empty;

				//保存目录
				string strDatePath = DateTime.Now.ToString("yyyyMM") + "/";
				string strPhysicalPath = Server.MapPath("/Upload/" + strDatePath);
				if (Request.Files.Count == 0)
				{
					return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" });
				}
				//HttpPostedFileBase file = Request.Files[0];
				string ex = Path.GetExtension(file.FileName);
				if (string.IsNullOrEmpty(ex)) ex = ".jpg";		//PNG图片上传获取不到后缀名
				fileName = kin.Utilities.String.RndDateStr() + ex;
				if (!System.IO.Directory.Exists(strPhysicalPath))
				{
					System.IO.Directory.CreateDirectory(strPhysicalPath);
				}
				file.SaveAs(Path.Combine(strPhysicalPath, fileName));

				string strVirtualPath = "/Upload/" + strDatePath + fileName;

				Common.ImageHelper clsImg = new Common.ImageHelper();
				//根据图片exif调整方向，旋转后图片大小会改变
				clsImg.RotateImage(strVirtualPath);
				//超过1M，压缩图片
				clsImg.PicCompress(strVirtualPath);

				return Json(new
				{
					result = 1,
					id = id,
					filePath = strVirtualPath
				}, JsonRequestBehavior.AllowGet);

			}
			catch (Exception ex)
			{
				//上传失败
				Common.ErrorHelp.SendError(pzyy20172.Common.ErrorHelp.GetMethodInfo(), ex.ToString());
				return Json(new
				{
					result = -1,
					id = id,
					filePath = ""
				}, JsonRequestBehavior.AllowGet);
			}
		}

		//删除已上传的文件
		public ActionResult DeleteProcess(string filepath)
		{
			try
			{
				System.IO.File.Delete(filepath);
				return Content("1");
			}
			catch
			{
				return Content("-1");
			}
		}
	}
}