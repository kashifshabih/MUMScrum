﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace edu.mum.mumscrum.Controllers
{
    [Authorize(Roles = "ProductOwner")]
    public class ProductOwnerController : Controller
    {
        // GET: ProductOwner
        public ActionResult Index()
        {
            return View();
        }
    }
}