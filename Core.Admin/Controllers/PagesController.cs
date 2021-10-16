using Core.AccessLayer;
using Core.Admin.Helpers;
using Core.Admin.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Admin.Controllers
{
    [AuthorizeUsers]
    public class PagesController : ParentController
    {
        // GET: Pages
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Search(string Name,int jtStartIndex = 0, int jtPageSize = 10, string jtSorting = null)
        {
            int count = 0;
            var list = _PagesServices.SearchGrid(Name, out count, jtStartIndex, jtPageSize, jtSorting);
            return Json(new { Result = "OK", Records = list, TotalRecordCount = count });
        }
        public ActionResult AddEdit(int? Id)
        {
            PagesModel model = new PagesModel();
            Pages obj = new Pages();
            if (Id.HasValue)
            {
                obj = _PagesServices.GetItemById(Id.Value);
            }
            Mapper.FillModelFromObject(model, obj);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEdit(PagesModel model)
        {
            if (ModelState.IsValid)
            {
                Pages obj = context.Pages
                    .Where(p => p.Id == model.Id).FirstOrDefault();
                if (obj == null)
                {
                    obj = new Pages();
                }
                Mapper.FillObjectFromModel(obj, model);
                if (model.Id == 0)
                {
                    context.Pages.Add(obj);
                }
                context.SaveChanges();
                TempData["Success"] = "تم الحفظ بنجاح";
                return RedirectToAction("Index", "Pages");
            }
            return View(model);
        }
    }
}