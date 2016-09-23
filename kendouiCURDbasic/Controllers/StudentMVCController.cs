using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kendouiCURDbasic.Controllers
{
    public class StudentMVCController : Controller
    {
        // GET: StudentMVC
        public ActionResult Index()
        {
            return View();
        }
    }
}