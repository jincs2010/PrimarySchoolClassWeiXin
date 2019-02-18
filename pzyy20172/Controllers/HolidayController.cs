using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pzyy20172.Controllers
{
	public class HolidayController : Controller
	{
		// GET: Holiday
		public ActionResult ParentDaFan1()
		{
			DateTime dtStart = Convert.ToDateTime("2018-09-03");
			DateTime dtEnd = Convert.ToDateTime("2019-01-27");
			//用holiday类型来存储最终结果
			List<Model.holiday> lstDay = new List<Model.holiday>();
			//假期信息
			List<Model.holiday> lstHos = new BLL.holiday().GetList();
			//学生信息
			List<Model.student_teacher> lstStudent = new BLL.student_teacher().GetList(t => t.Type == 0, t => t.ID, false, 0);
			//每天家长人数
			int intNumPerDay = 2;
			//家长索引
			int intNumIndex = 0;

			//第一步：根据节假日信息，将日期列表排好
			while (dtStart < dtEnd)
			{
				//1、检查当天是否上课，如果是周末，是否在调休Type=2的记录中，如果是非周末，是否在假期Type=1的记录中
				if (dtStart.DayOfWeek == DayOfWeek.Saturday || dtStart.DayOfWeek == DayOfWeek.Sunday)
				{
					//如果是周末，是否在调休Type=2的记录中，则为上课
					Model.holiday mdTemp = lstHos.Where(t => t.ToDate == dtStart.ToString("yyyy-MM-dd") && t.Type == 2).FirstOrDefault();
					if (mdTemp != null)
						lstDay.Add(new Model.holiday { ToDate = dtStart.ToString("yyyy-MM-dd") + kin.Utilities.Date.GetWeekCHN(dtStart) });
				}
				else
				{
					//如果是非周末，且不在假期Type=1的记录中，为上课
					Model.holiday mdTemp = lstHos.Where(t => t.ToDate == dtStart.ToString("yyyy-MM-dd") && t.Type == 1).FirstOrDefault();
					if (mdTemp == null)
						lstDay.Add(new Model.holiday { ToDate = dtStart.ToString("yyyy-MM-dd") + kin.Utilities.Date.GetWeekCHN(dtStart) });
				}
				//日期+1
				dtStart = dtStart.AddDays(1);
			}
			//第二步：根据日期列表，将学生按学号写入
			for (int i = 0; i < lstDay.Count; i++)
			{
				//intNumIndex==，即索引超出了，从0开始
				if (intNumIndex == lstStudent.Count) intNumIndex = 0;
				lstDay[i].Remark = lstStudent[intNumIndex].Name + ",";
				intNumIndex++;

				//第二位
				if (intNumIndex == lstStudent.Count) intNumIndex = 0;
				lstDay[i].Remark += lstStudent[intNumIndex].Name;
				intNumIndex++;
			}


			return View(lstDay);
		}

		/// <summary>
		/// 单个家长打饭，2018.11.13起
		/// </summary>
		/// <returns></returns>
		public ActionResult ParentDaFan()
		{
			DateTime dtStart = Convert.ToDateTime("2018-11-13");
			DateTime dtEnd = Convert.ToDateTime("2019-01-27");
			//用holiday类型来存储最终结果
			List<Model.holiday> lstDay = new List<Model.holiday>();
			//假期信息
			List<Model.holiday> lstHos = new BLL.holiday().GetList();
			//学生信息
			List<Model.student_teacher> lstStudent = new BLL.student_teacher().GetList(t => t.Type == 0, t => t.ID, false, 0);
			//家长索引，从1开始
			int intNumIndex = 0;

			//第一步：根据节假日信息，将日期列表排好
			while (dtStart < dtEnd)
			{
				//1、检查当天是否上课，如果是周末，是否在调休Type=2的记录中，如果是非周末，是否在假期Type=1的记录中
				if (dtStart.DayOfWeek == DayOfWeek.Saturday || dtStart.DayOfWeek == DayOfWeek.Sunday)
				{
					//如果是周末，是否在调休Type=2的记录中，则为上课
					Model.holiday mdTemp = lstHos.Where(t => t.ToDate == dtStart.ToString("yyyy-MM-dd") && t.Type == 2).FirstOrDefault();
					if (mdTemp != null)
						lstDay.Add(new Model.holiday { ToDate = dtStart.ToString("yyyy-MM-dd") + kin.Utilities.Date.GetWeekCHN(dtStart) });
				}
				else
				{
					//如果是非周末，且不在假期Type=1的记录中，为上课
					Model.holiday mdTemp = lstHos.Where(t => t.ToDate == dtStart.ToString("yyyy-MM-dd") && t.Type == 1).FirstOrDefault();
					if (mdTemp == null)
						lstDay.Add(new Model.holiday { ToDate = dtStart.ToString("yyyy-MM-dd") + kin.Utilities.Date.GetWeekCHN(dtStart) });
				}
				//日期+1
				dtStart = dtStart.AddDays(1);
			}
			//第二步：根据日期列表，将学生按学号写入
			for (int i = 0; i < lstDay.Count; i++)
			{
				//intNumIndex==，即索引超出了，从0开始
				if (intNumIndex == lstStudent.Count) intNumIndex = 0;
				lstDay[i].Remark = lstStudent[intNumIndex].Name;
				intNumIndex++;

				////第二位
				//if (intNumIndex == lstStudent.Count) intNumIndex = 0;
				//lstDay[i].Remark += lstStudent[intNumIndex].Name;
				//intNumIndex++;
			}


			return View(lstDay);
		}

	}
}