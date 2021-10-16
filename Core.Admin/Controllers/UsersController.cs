using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Core.Services;
using System.Collections.Generic;
using Core.AccessLayer;
using System.Data.Entity;
using Core.Admin.Models;
using Core.Admin.Helpers;
using System.IO;

namespace Core.Admin.Controllers
{
    [AuthorizeUsers]
    public class UsersController : ParentController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Users
        public ActionResult Index()
        {
            RegisterViewModel model = new RegisterViewModel();
            InitalizeModelLookups.InitModel(model);
            return View(model);
        }
        [HttpPost]
        public JsonResult Search(string UserName,string FullName, string PhoneNumber
            , int jtStartIndex = 0, int jtPageSize = 10, string jtSorting = null)
        {
            int count = 0;
            var list = _UsersServices.SearchGrid(UserName, FullName, PhoneNumber,
                out count, jtStartIndex, jtPageSize, jtSorting);
            return Json(new { Result = "OK", Records = list, TotalRecordCount = count });
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddEdit(string Id)
        {
            RegisterViewModel model = new RegisterViewModel();
            InitalizeModelLookups.InitModel(model);
            AspNetUsers obj = new AspNetUsers();
            if (Id != null)
            {
                obj = context.AspNetUsers.Where(f => f.Id == Id)
                   .FirstOrDefault();
            }
            Mapper.FillModelFromObject(model, obj);
            model.ListPages = _UsersServices.GetAllListPages(Id);
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult EditProfile(string Id)
        {
            RegisterViewModel model = new RegisterViewModel();
            InitalizeModelLookups.InitModel(model);
            if (Id != null)
            {
                var obj = context.AspNetUsers
                    .Where(f => f.Id == Id)
                    .FirstOrDefault();
                Mapper.FillModelFromObject(model, obj);
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(RegisterViewModel model)
        {
            InitalizeModelLookups.InitModel(model);
            if (string.IsNullOrEmpty(model.Id))
            {
                var user = new ApplicationUser
                {
                    Email =model.UserName +"@a.com",
                    UserName = model.UserName,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                if (_UsersServices.CheckUserName(model.Id, model.UserName) != null)
                {
                    TempData["Error"] = "أسم المستخدم مستخدم من قبل";
                    return View(model);
                }
                //if (_UsersServices.CheckPhoneNumber(model.Id, model.PhoneNumber) != null)
                //{
                //    TempData["Error"] = "رقم الجوال مستخدم من قبل";
                //    return View(model);
                //}
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    var obj = context.AspNetUsers
                        .Where(f => f.Id == user.Id).FirstOrDefault();
                    obj.IsActive = true;
                    obj.FullName = model.FullName;
                    context.SaveChanges();
                    if (model.ListPages != null)
                    {
                        List<Users_Pages> listPages = (from c in model.ListPages
                                                       where c.IsChecked == true
                                                           select new Users_Pages
                                                           {
                                                               UserId = obj.Id,
                                                               PageId = c.Id
                                                           }).ToList();
                        context.Users_Pages.AddRange(listPages);
                        context.SaveChanges();
                    }
                    
                    return RedirectToAction("Index", "Users");
                }
            }
            else
            {
                var obj = context.AspNetUsers.Include(f=>f.Users_Pages) 
                    .Where(f => f.Id == model.Id).FirstOrDefault();

                if (model.UserName != obj.UserName)
                {
                    var checkEmail = _UsersServices.CheckUserName(obj.Id, model.UserName);
                    if (checkEmail != null)
                    {
                        TempData["Error"] = "أسم المستخدم مستخدم من قبل";
                        return View(model);
                    }
                }
                //if (model.PhoneNumber != obj.PhoneNumber)
                //{
                //    var checkPhoneNumber = _UsersServices.CheckPhoneNumber(obj.Id, model.PhoneNumber);
                //    if (checkPhoneNumber != null)
                //    {
                //        TempData["Error"] = "رقم الجوال مستخدم من قبل";
                //        return View(model);
                //    }
                //}
                obj.PhoneNumber = model.PhoneNumber;
                obj.UserName = model.UserName;
                obj.FullName = model.FullName;
                obj.Email = model.Email;
                context.SaveChanges();
                if (model.ListPages != null)
                {
                    context.Users_Pages.RemoveRange(obj.Users_Pages);
                    context.SaveChanges();
                    List<Users_Pages> listPages = (from c in model.ListPages
                                                   where c.IsChecked == true
                                                   select new Users_Pages
                                                   {
                                                       UserId = obj.Id,
                                                       PageId = c.Id
                                                   }).ToList();
                    context.Users_Pages.AddRange(listPages);
                    context.SaveChanges();
                }
                 
                TempData["Success"] = "تم حفظ البيانات بنجاح";
                return RedirectToAction("Index", "Users");
            }
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(RegisterViewModel model)
        {
            InitalizeModelLookups.InitModel(model);
            var obj = context.AspNetUsers.Where(f => f.Id == model.Id)
                .FirstOrDefault();
            if (model.UserName != obj.UserName)
            {
                var checkEmail = _UsersServices.CheckUserName(obj.Id, model.UserName);
                if (checkEmail != null)
                {
                    TempData["Error"] = "أسم المستخدم مستخدم من قبل";
                    return View(model);
                }
            }
            //if (model.PhoneNumber != obj.PhoneNumber)
            //{
            //    var checkPhoneNumber = _UsersServices.CheckPhoneNumber(obj.Id, model.PhoneNumber);
            //    if (checkPhoneNumber != null)
            //    {
            //        TempData["Error"] = "رقم الجوال مستخدم من قبل";
            //        return View(model);
            //    }
            //}
            obj.Email = model.Email;
            obj.UserName = model.UserName;
            obj.FullName = model.FullName;
            obj.PhoneNumber = model.PhoneNumber;
            TempData["Success"] = "تم حفظ البيانات بنجاح";
            context.SaveChanges();

            return RedirectToAction("EditProfile", "Users", new { Id = User.Identity.GetUserId() });
        }
        public JsonResult DeleteItem(string UserName)
        {
            var item = context.AspNetUsers.Where(f => f.UserName == UserName)
              .FirstOrDefault();
            item.IsDelete = true;
            item.Email = item.Id;
            item.UserName = item.Id;
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult DesActive(string UserName)
        {
            var item = context.AspNetUsers
                .Where(f => f.UserName == UserName)
              .FirstOrDefault();
            item.IsActive = false;
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult Active(string UserName)
        {
            var item = context.AspNetUsers
                .Where(f => f.UserName == UserName)
              .FirstOrDefault();
            item.IsActive = true;
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}