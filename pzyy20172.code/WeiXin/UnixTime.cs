using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kin.BLL.WeiXin
{
	/// <summary>
	/// Unix时间戳转换
	/// </summary>
	public class UnixTime
	{
		private static DateTime BaseTime = new DateTime(1970, 1, 1);

		/// <summary>   
		/// 将unixtime转换为.NET的DateTime   
		/// </summary>   
		/// <param name="timeStamp">秒数</param>   
		/// <returns>转换后的时间</returns>   
		public static DateTime FromUnixTime(long timeStamp)
		{
			return new DateTime((timeStamp + 8 * 60 * 60) * 10000000 + BaseTime.Ticks);
		}
		public static DateTime FromUnixTime(string timeStamp)
		{
			if (string.IsNullOrEmpty(timeStamp)) return Convert.ToDateTime("1900-1-1");
			if(kin.Utilities.PageValidate.IsNumber(timeStamp) ==false) return Convert.ToDateTime("1900-1-1");

			return FromUnixTime(Convert.ToInt64(timeStamp));
		}

		/// <summary>   
		/// 将.NET的DateTime转换为unix time   
		/// </summary>   
		/// <param name="dateTime">待转换的时间</param>   
		/// <returns>转换后的unix time</returns>   
		public static long FromDateTime(DateTime dateTime)
		{
			return (dateTime.Ticks - BaseTime.Ticks) / 10000000 - 8 * 60 * 60;
		}
		public static long FromDateTime(string dateTime)
		{
			DateTime dt = Convert.ToDateTime("1900-1-1");
			if (!string.IsNullOrEmpty(dateTime))
				dt = Convert.ToDateTime(dateTime);

			return FromDateTime(dt);
		}
	}   
}
