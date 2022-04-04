using SweetAlert.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SweetController : SweetBaseController
    {
        // GET: Sweet
        public ActionResult Index()
        {
            Alert("This is a success message", NotificationType.success);
            return View();
        }
    }
}