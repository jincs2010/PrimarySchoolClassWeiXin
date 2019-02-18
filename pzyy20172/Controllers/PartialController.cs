using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pzyy20172.Controllers
{
    public class PartialController : Controller
    {
        // GET: Partial
        public ActionResult Footer()
        {
            return View();
        }
    }
}