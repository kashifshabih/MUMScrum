using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace edu.mum.mumscrum.Controllers
{
    [Authorize(Roles = "HRAdministrator")]
    public class HRAdminController : Controller
    {
        // GET: HRAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}