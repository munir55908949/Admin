using Core.Admin.Helpers;
using Core.ViewModels;
using iTextSharp.text.pdf.qrcode;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Admin.Controllers
{
    [Authorize]
    public class HomeController : ParentController
    {
        public ActionResult Index(string returnUrl)
        {
            return View();
        }
    }
}