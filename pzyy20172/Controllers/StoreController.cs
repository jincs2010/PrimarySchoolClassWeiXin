using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pzyy20172.Controllers
{
	/// <summary>
	/// 学习资料
	/// </summary>
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
			try
			{
			new BLL.user_info().CheckLogin();
			return View();
			}
			catch (Exception ex)
			{
				Common.ErrorHelp.SendError(Common.ErrorHelp.GetMethodInfo(), ex.ToString());
				return Redirect("/Home/Page404/");
			}
		}
    }
}