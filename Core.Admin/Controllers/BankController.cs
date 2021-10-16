using Core.AccessLayer;
using Core.Admin.Helpers;
using Core.Admin.Models;
using Microsoft.AspNet.Identity;
using Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Core.ViewModels;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Core.Admin.Controllers
{
    [AuthorizeUsers]
    public class BankController : ParentController
    {
        // GET: Bank
        public ActionResult Index()
        {
            BankModel model = new BankModel();
            InitalizeModelLookups.InitModel(model);
            return View(model);
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_BankServices.SearchGrid().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddEdit(int? Id)
        {
            BankModel model = new BankModel();
            Bank obj = new Bank();
            if (Id > 0)
            {
                obj = _BankServices.GetBankById(Id.Value);
            }
            Mapper.FillModelFromObject(model, obj);
            InitalizeModelLookups.InitModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEdit(BankModel model)
        {
            if (ModelState.IsValid)
            {
                Bank obj = context.Bank
                    .Where(p => p.Id == model.Id).FirstOrDefault();
                if (obj == null)
                {
                    obj = new Bank();
                }
                var userId= User.Identity.GetUserId();
                Mapper.FillObjectFromModel(obj, model);
                if (model.Id == 0)//not exist in DB and create new recored
                {
                    obj.CreatedBy = userId;
                    obj.CreatedAt = DateTime.Now;
                    context.Bank.Add(obj);
                }
                context.SaveChanges();
                TempData["Success"] = "تم الحفظ بنجاح";
                return RedirectToAction("Index", "Bank");
            }
            InitalizeModelLookups.InitModel(model);
            return View(model);
        }
        public JsonResult Complete(int Id)
        {
            var item = context.Company
                .Where(p => p.Id == Id).FirstOrDefault();
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItem(int Id)
        {
            var item = context.Bank
                .Where(p => p.Id == Id).FirstOrDefault();
            item.IsDeleted = true;
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}