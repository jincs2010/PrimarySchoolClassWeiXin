using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pzyy20172.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
			//生成类库
			new pzyy20172.Dal.DbBase().CreateModelClassFile();

			//***************
			//string strVirtualPath = "/upload/test/1.jpg";
			//Common.ImageHelper clsImg = new Common.ImageHelper();
			////根据图片exif调整方向，旋转后图片大小会改变
			//clsImg.RotateImage(strVirtualPath);
			////超过1M，压缩图片
			//clsImg.PicCompress(strVirtualPath);


			//***************
			//new Common.VoiceHelper().AmrToMP3("/upload/201803/2018032323540268718.amr");

			return Content("1");
        }
	}
}