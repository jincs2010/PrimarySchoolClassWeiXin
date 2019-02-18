using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace pzyy20172.Common
{
	public class ImageHelper
	{
		/// <summary>
		/// 图片压缩，标准化图片尺寸，头像200X200，普通照片1000X1000；原文件生成.bak备份，新图替换原图文件名
		/// </summary>
		/// <param name="filePath">如“/upload/201705/201705310837075585.jpg”</param>
		public void PicCompress(string filePath)
		{
			//1000X1000的JPG，约200K，200X200的JPG约15K

			int maxPicSize = 2000;
			string strPhysicsFilePath = HttpContext.Current.Server.MapPath(filePath);

			//文件不存在，直接返回
			if (File.Exists(strPhysicsFilePath) == false) return;
			//若已存在.bak文件，表示文件已经被压缩过，不处理
			if (File.Exists(strPhysicsFilePath + ".bak")) return;

			System.Drawing.Image imgYuanTu = System.Drawing.Image.FromFile(strPhysicsFilePath);

			//尺寸宽高比规定的都要小，不处理
			if (imgYuanTu.Width <= maxPicSize && imgYuanTu.Height <= maxPicSize) return;

			//计算缩略图的宽高
			int ww = imgYuanTu.Width;
			int hh = imgYuanTu.Height;
			if (ww > hh)
			{
				ww = maxPicSize;
				hh = Convert.ToInt32(imgYuanTu.Height * (Convert.ToDouble(ww) / Convert.ToDouble(imgYuanTu.Width)));
			}
			else
			{
				hh = maxPicSize;
				ww = Convert.ToInt32(imgYuanTu.Width * (Convert.ToDouble(hh) / Convert.ToDouble(imgYuanTu.Height)));
			}

			//新建一个bmp图片
			System.Drawing.Image bitmap = new System.Drawing.Bitmap(ww, hh);
			//新建一个画板
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
			//设置高质量插值法
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
			//设置高质量,低速度呈现平滑程度
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
			//清空一下画布，白色为底
			g.Clear(System.Drawing.Color.White);
			//在指定位置画图
			g.DrawImage(imgYuanTu, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
			new System.Drawing.Rectangle(0, 0, ww, hh),
			System.Drawing.GraphicsUnit.Pixel);
			//保存高清晰度的缩略图
			bitmap.Save(strPhysicsFilePath + ".tmp", System.Drawing.Imaging.ImageFormat.Jpeg);

			////生成缩略图，此方法模糊
			//System.Drawing.Image img = img1.GetThumbnailImage(ww, hh, null, IntPtr.Zero);
			//img.Save(strPhysicsFilePath + ".tmp", System.Drawing.Imaging.ImageFormat.Jpeg);

			//释放内存
			bitmap.Dispose();
			imgYuanTu.Dispose();

			//文件名调换：原文件->.bak，缩略图.tmp->原文件名
			System.IO.FileInfo fi = new System.IO.FileInfo(strPhysicsFilePath);
			System.IO.FileInfo fi2 = new System.IO.FileInfo(strPhysicsFilePath + ".tmp");
			fi.MoveTo(strPhysicsFilePath + ".bak");
			fi2.MoveTo(strPhysicsFilePath);
		}

		/// <summary>
		/// 根据图片exif调整方向，旋转后图片大小会改变
		/// </summary>
		/// <param name="sm"></param>
		/// <returns></returns>
		public void RotateImage(string filePath)
		{
			string strPhysicsFilePath = HttpContext.Current.Server.MapPath(filePath);

			//文件不存在，直接返回
			if (File.Exists(strPhysicsFilePath) == false) return;
			System.Drawing.Image img = System.Drawing.Image.FromFile(strPhysicsFilePath);

			var exif = img.PropertyItems;
			byte orien = 0;
			var item = exif.Where(m => m.Id == 274).ToArray();
			if (item.Length > 0)
				orien = item[0].Value[0];
			if (orien > 8 && orien < 2)
			{
				//没有exif方向信息
				return;
			}

			switch (orien)
			{
				case 2:
					img.RotateFlip(RotateFlipType.RotateNoneFlipX);     //horizontal flip  
					break;
				case 3:
					img.RotateFlip(RotateFlipType.Rotate180FlipNone);   //right-top 
					break;
				case 4:
					img.RotateFlip(RotateFlipType.RotateNoneFlipY);     //vertical flip  
					break;
				case 5:
					img.RotateFlip(RotateFlipType.Rotate90FlipX);
					break;
				case 6:
					img.RotateFlip(RotateFlipType.Rotate90FlipNone);    //right-top  
					break;
				case 7:
					img.RotateFlip(RotateFlipType.Rotate270FlipX);
					break;
				case 8:
					img.RotateFlip(RotateFlipType.Rotate270FlipNone);   //left-bottom  
					break;
				default:
					break;
			}
			img.Save(strPhysicsFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
			img.Dispose();
		}
	}
}
