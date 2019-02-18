using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace pzyy20172.Common
{
	public class VoiceHelper
	{
		/// <summary>
		/// 方法一：简单，快速，完美
		/// </summary>
		/// <param name="amrFileName"></param>
		public void AmrToMP3(string amrFileName)
		{
			string pyFileName = System.Web.HttpContext.Current.Server.MapPath(amrFileName);
			//解码器文件位置
			string ffmpeg = HttpContext.Current.Server.MapPath("/content/ffmpeg.exe");

			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
			startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

			startInfo.Arguments = " -i " + pyFileName + " " + pyFileName + ".mp3";
			try
			{
				System.Diagnostics.Process.Start(startInfo);
				return;
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());
				return;
			}
		}

		#region 本地测试OK，但在服务器上，会卡死

		public string ConvertToMp3(string amrFileName)
		{
			string pyFileName = System.Web.HttpContext.Current.Server.MapPath(amrFileName);
			string c = System.Web.HttpContext.Current.Server.MapPath("/content/") + @"ffmpeg.exe -i " + pyFileName + " " + pyFileName + ".mp3";
			string str = RunCmd(c);
			return str;
		}

		/// <summary>
		/// 执行Cmd命令，此方法每执行一次就会多一个cmd进程
		/// </summary>
		private string RunCmd(string c)
		{
			try
			{
				ProcessStartInfo info = new ProcessStartInfo("cmd.exe");
				info.RedirectStandardOutput = false;
				info.UseShellExecute = false;
				Process p = Process.Start(info);
				p.StartInfo.UseShellExecute = false;
				p.StartInfo.RedirectStandardInput = true;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.RedirectStandardError = true;
				//p.StartInfo.CreateNoWindow = true;	没有用
				p.Start();
				p.StandardInput.WriteLine(c);
				p.StandardInput.AutoFlush = true;
				Thread.Sleep(1000);
				p.StandardInput.WriteLine("exit");
				p.WaitForExit();	//
				string outStr = p.StandardOutput.ReadToEnd();
				p.Close();

				return outStr;
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());
				return "error" + ex.Message;
			}
		}

		#endregion

	}
}
