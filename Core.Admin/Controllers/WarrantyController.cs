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
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text;
using static iTextSharp.text.TabStop;
using System.Web.UI.WebControls;

namespace Core.Admin.Controllers
{
    [AuthorizeUsers]
    public class WarrantyController : ParentController
    {
        #region Views

        // GET: Warranty
        public ActionResult Index()
        {
            WarrantyModel model = new WarrantyModel();
            InitalizeModelLookups.InitModel(model);
            return View(model);
        }

        public ActionResult AddEdit(int? Id, int contractDataId)
        {
            WarrantyModel model = new WarrantyModel();
            Warranty obj = new Warranty();
            if (Id > 0)
            {
                obj = _WarrantyServices.GetWarrantyById(Id.Value);
            }
            //model represent view user data
            //obj represnt data from DB
            //fill model that user will show from obj that come from DB
            Mapper.FillModelFromObject(model, obj);
            //we used this mehode when we need DDL in the page
            InitalizeModelLookups.InitModel(model);
            model.ContractDataId = contractDataId;
            return View(model);
        }
        public ActionResult AddEditDetails(int Id)
        {
            var details = _ContractDataServices.GetContractDataDetailsById(Id);

            WarrantyModel model;
            if (details == null)
            {
                model = new WarrantyModel();
            }
            else
            {
                model = new WarrantyModel
                {
                    ContractDataId = Id,
                    ContractNumber = details.ContractNumber,
                    CompanyName = details.CompanyName,
                    BankName = details.BankName,
                    SectorName = details.SectorName,
                    AdministrationName = details.AdministrationName,
                    ContractStatusName = details.ContractStatusName,

                };
            }
            InitalizeModelLookups.InitModel(model);
            return View(model);
        }
        public ActionResult AddEditWarrantyHistory(int Id)
        {
            var history = _WarrantyServices.GetWarrantyHistoryById(Id);

            WarrantyHistoryModel model;
            if (history == null)
            {
                model = new WarrantyHistoryModel();
            }
            else
            {
                model = new WarrantyHistoryModel
                {
                    WarrantyId = Id,
                    WarrantyNumber = history.WarrantyNumber,
                    WarrantyValue = history.WarrantyValue,
                    GuaranteeStatusName = history.GuaranteeStatusName,
                    GuaranteeEndDate = history.GuaranteeEndDate,
                    Path = history.Path,

                };
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEdit(WarrantyModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                Warranty obj = context.Warranty
                        .Where(p => p.Id == model.Id).FirstOrDefault();
                if (obj == null)
                {

                    obj = new Warranty();

                    if (model.File != null)
                    {
                        Guid nme = Guid.NewGuid();
                        string filename = nme + Path.GetExtension(model.File.FileName);
                        string filepath = Path.Combine(Server.MapPath("/FilesUpload/") + filename);
                        model.File.SaveAs(filepath);
                        obj.Path = filename;
                    }
                    Mapper.FillObjectFromModel(obj, model);
                    obj.CreatedBy = userId;
                    obj.ModifiedBy = userId;
                    obj.CreatedAt = DateTime.Now;
                    context.Warranty.Add(obj);
                    context.SaveChanges();
                    WarrantyHistory objHistory = context.WarrantyHistory
                      .Where(p => p.Id == model.WarrantyId).FirstOrDefault();
                    if (objHistory == null)
                    {
                        objHistory = new WarrantyHistory();
                    }
                    if (model.File != null)
                    {
                        Guid nme = Guid.NewGuid();
                        string filename = nme + Path.GetExtension(model.File.FileName);
                        string filepath = Path.Combine(Server.MapPath("/FilesUpload/") + filename);
                        model.File.SaveAs(filepath);
                        objHistory.Path = filename;
                    }
                    objHistory.WarrantyId = obj.Id;
                    objHistory.WarrantyNumber = obj.WarrantyNumber;
                    objHistory.WarrantyValue = obj.WarrantyValue;
                    objHistory.DecreaseOrIncreaseValue = obj.DecreaseOrIncreaseValue;
                    objHistory.GuaranteeIssueDate = obj.GuaranteeIssueDate;
                    objHistory.GuaranteeStartDate = obj.GuaranteeStartDate;
                    objHistory.GuaranteeEndDate = obj.GuaranteeEndDate;
                    objHistory.GuaranteeSortId = obj.GuaranteeSortId;
                    objHistory.GuaranteeStatusId = obj.GuaranteeStatusId;
                    objHistory.WarrantyTypeId = obj.WarrantyTypeId;
                    objHistory.DecreaseOrIncreaseId = obj.DecreaseOrIncreaseId;
                    objHistory.CreatedBy = userId;
                    objHistory.CreatedAt = DateTime.Now;
                    objHistory.ModifiedBy = userId;
                    objHistory.ModifiedAt = DateTime.Now;
                    objHistory.IsDeleted = false;
                    context.WarrantyHistory.Add(objHistory);
                    context.SaveChanges();



                }
                else
                {
                    if (model.File != null)
                    {
                        Guid nme = Guid.NewGuid();
                        string filename = nme + Path.GetExtension(model.File.FileName);
                        string filepath = Path.Combine(Server.MapPath("/FilesUpload/") + filename);
                        model.File.SaveAs(filepath);
                        obj.Path = filename;
                    }
                    Mapper.FillObjectFromModel(obj, model);
                    WarrantyHistory objHistory = context.WarrantyHistory
                  .Where(p => p.Id == model.WarrantyId).FirstOrDefault();
                    if (objHistory == null)
                    {
                        objHistory = new WarrantyHistory();
                    }
                    if (model.File != null)
                    {
                        Guid nme = Guid.NewGuid();
                        string filename = nme + Path.GetExtension(model.File.FileName);
                        string filepath = Path.Combine(Server.MapPath("/FilesUpload/") + filename);
                        model.File.SaveAs(filepath);
                        objHistory.Path = filename;
                    }

                    objHistory.WarrantyId = obj.Id;
                    objHistory.WarrantyNumber = obj.WarrantyNumber;
                    objHistory.WarrantyValue = obj.WarrantyValue;
                    objHistory.DecreaseOrIncreaseValue = obj.DecreaseOrIncreaseValue;
                    objHistory.GuaranteeIssueDate = obj.GuaranteeIssueDate;
                    objHistory.GuaranteeStartDate = obj.GuaranteeStartDate;
                    objHistory.GuaranteeEndDate = obj.GuaranteeEndDate;
                    objHistory.GuaranteeSortId = obj.GuaranteeSortId;
                    objHistory.GuaranteeStatusId = obj.GuaranteeStatusId;
                    objHistory.WarrantyTypeId = obj.WarrantyTypeId;
                    objHistory.DecreaseOrIncreaseId = obj.DecreaseOrIncreaseId;
                    objHistory.CreatedBy = userId;
                    objHistory.CreatedAt = DateTime.Now;
                    objHistory.ModifiedBy = userId;
                    objHistory.ModifiedAt = DateTime.Now;
                    objHistory.IsDeleted = false;
                    objHistory.Path = obj.Path;
                    context.WarrantyHistory.Add(objHistory);
                    context.SaveChanges();
                    InitalizeModelLookups.InitModel(model);
                }
                TempData["Success"] = "تم الحفظ بنجاح";
                return RedirectToAction("AddEditDetails", "Warranty", new { id = model.ContractDataId });
                //return RedirectToAction("AddEditDetails", "Warranty");
            }
            InitalizeModelLookups.InitModel(model);
            return View(model);
        }
        #endregion
        public JsonResult Complete(int Id)
        {
            var item = context.Warranty
                .Where(p => p.Id == Id).FirstOrDefault();
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItem(int Id)
        {
            var item = context.Warranty
                .Where(p => p.Id == Id).FirstOrDefault();
            item.IsDeleted = true;
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteWarrantyHistory(int Id)
        {
            var item = context.WarrantyHistory
                .Where(p => p.Id == Id).FirstOrDefault();
            item.IsDeleted = true;
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult NotificationInbox()
        {
            try
            {
                //Get your list 
                var modelList = _WarrantyServices.GetNotifications().ToList();

                string Message = @"<li>
                                    <span class='dropdown-menu-title mdgCountLabel'> " + modelList.Count + @"</span>
                                </li>";
                foreach (var msg in modelList)
                {
                    Message += @" <li>
                  <a href='javascript:void(0)'>
                    <div class='clearfix'>
                        <div class='thread-image'>
                            <img alt='' src='./assets/images/image03.jpg'>
                        </div>
                        <div class='thread-content'>
                            <span class='author'>" + msg.WarrantyNumber + @"  رقم الكفالة</span>
                            <span class='preview'>" + msg.GuaranteeEndDate + @" تاريخ إنتهاء الكفالة</span>
                            <span class='author'>" + msg.ContractNumber + @" رقم العقد</span>

                        </div>
                    </div>
                </a>
            </li>";

                }
                return Json(new { modelList.Count, Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("");
            }
        }

        // Search
        [HttpPost]
        //paramter fixed with all Search functions  int jtStartIndex = 0, int jtPageSize = 10, string jtSorting = null
        public JsonResult Search(string ContractNumber, string WarrantyNumber, decimal? WarrantyValue, decimal? DecreaseOrIncreaseValue, int? ContractDataId, int? GuaranteeSortId, int? GuaranteeStatusId, int? WarrantyTypeId, int? CurrencyId, int? DecreaseOrIncreaseId, DateTime? GuaranteeIssueDate, DateTime? GuaranteeStartDate, DateTime? GuaranteeEndDate,DateTime? From,DateTime? To, string NoteWarranty, string CreatedBy,
    out int AllListCount, int jtStartIndex = 0,
    int jtPageSize = 10, string jtSorting = null)
        {
            int count = 0;
            AllListCount = 0;
            //pass from controller to service
            var list = _WarrantyServices.SearchGrid(ContractNumber, WarrantyNumber, WarrantyValue, DecreaseOrIncreaseValue, ContractDataId, GuaranteeSortId, GuaranteeStatusId, WarrantyTypeId, CurrencyId, DecreaseOrIncreaseId, GuaranteeIssueDate, GuaranteeStartDate, GuaranteeEndDate,From,To, NoteWarranty, CreatedBy, out count, jtStartIndex, jtPageSize, jtSorting);
            return Json(new { Result = "OK", Records = list, TotalRecordCount = count });
        }
        public ActionResult fileUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult fileUp(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string filepath = Path.Combine(Server.MapPath("/FilesUpload/") + filename);
                    file.SaveAs(filepath);
                }
                ViewBag.Message = ("تم رفع الملف بنجاح");
                return View();
            }
            catch
            {
                ViewBag.Message = ("لم يتم رفع الملف ");
                return View();
            }
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request, int? contractDataId)
        {
            return Json(_WarrantyServices.SearchGrid(contractDataId).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        //GET: GuaranteeDetails
        public ActionResult ReadWarrantyHistory([DataSourceRequest] DataSourceRequest request, int Id)
        {
            return Json(_WarrantyServices.SearchWarrantyHistoryGrid(Id).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ExportReport(string ContractNumber, string WarrantyNumber, decimal? WarrantyValue, decimal? DecreaseOrIncreaseValue, int? ContractDataId, int? GuaranteeSortId, int? GuaranteeStatusId, int? WarrantyTypeId, int? CurrencyId, int? DecreaseOrIncreaseId, DateTime? GuaranteeIssueDate, DateTime? GuaranteeStartDate, DateTime? GuaranteeEndDate, DateTime? From, DateTime? To, string NoteWarranty, string CreatedBy)

        {
            var path = ExportPdf(ContractNumber, WarrantyNumber, WarrantyValue, DecreaseOrIncreaseValue, ContractDataId, GuaranteeSortId, GuaranteeStatusId, WarrantyTypeId, CurrencyId, DecreaseOrIncreaseId, GuaranteeIssueDate, GuaranteeStartDate, GuaranteeEndDate, NoteWarranty, CreatedBy, From, To, context);
            if (!string.IsNullOrEmpty(path))
                return Json(path, JsonRequestBehavior.AllowGet);
            else
                return Json(new { error = "حدث خطأ يرجى إعادة المحاولة" });
        }
        public string ExportPdf(string ContractNumber, string WarrantyNumber, decimal? WarrantyValue, decimal? DecreaseOrIncreaseValue, int? ContractDataId, int? GuaranteeSortId, int? GuaranteeStatusId, int? WarrantyTypeId, int? CurrencyId, int? DecreaseOrIncreaseId, DateTime? GuaranteeIssueDate, DateTime? GuaranteeStartDate, DateTime? GuaranteeEndDate, string NoteWarranty, string CreatedBy, DateTime? From, DateTime? To, DatabseEntities context)
        {
            #region prepareData
            var list = _WarrantyServices.SearchGrid(ContractDataId).Where(x =>
            (WarrantyValue.HasValue ? x.WarrantyValue == WarrantyValue : WarrantyValue == null)
            && (DecreaseOrIncreaseValue.HasValue ? x.DecreaseOrIncreaseValue == DecreaseOrIncreaseValue : DecreaseOrIncreaseValue == null)
            && (GuaranteeSortId > 0 ? x.GuaranteeSortId == GuaranteeSortId : GuaranteeSortId == 0)
            && (WarrantyTypeId > 0 ? x.WarrantyTypeId == WarrantyTypeId : WarrantyTypeId == 0)
            && (CurrencyId > 0 ? x.CurrencyId == CurrencyId : CurrencyId == 0)
            && (DecreaseOrIncreaseId > 0 ? x.DecreaseOrIncreaseId == DecreaseOrIncreaseId : DecreaseOrIncreaseId == 0)
            && (!string.IsNullOrEmpty(ContractNumber) ? x.ContractNumber.Contains(ContractNumber) : string.IsNullOrEmpty(ContractNumber))
            && (!string.IsNullOrEmpty(WarrantyNumber) ? x.WarrantyNumber.Contains(WarrantyNumber) : string.IsNullOrEmpty(WarrantyNumber))
            && (!string.IsNullOrEmpty(NoteWarranty) ? x.NoteWarranty.Contains(NoteWarranty) : string.IsNullOrEmpty(NoteWarranty))
            && (!string.IsNullOrEmpty(CreatedBy) ? x.CreatedByName.Contains(CreatedBy) : string.IsNullOrEmpty(CreatedBy))
            && (GuaranteeIssueDate.HasValue ? x.GuaranteeIssueDate == GuaranteeIssueDate : GuaranteeIssueDate == null)
            && (GuaranteeStartDate.HasValue ? x.GuaranteeStartDate == GuaranteeStartDate : GuaranteeStartDate == null)
            && (GuaranteeEndDate.HasValue ? x.GuaranteeEndDate == GuaranteeEndDate : GuaranteeEndDate == null)
            && (From.HasValue ? x.GuaranteeEndDate >= From : true)
            && (To.HasValue ? x.GuaranteeEndDate <= To : true)
            ).ToList();
            #endregion
            string fileName = Guid.NewGuid().ToString();
            var filePath = string.Format("/FilesUpload/{0}", fileName + ".pdf");
            var OutputPath = Server.MapPath(filePath);
            using (MemoryStream stream = new System.IO.MemoryStream())
            {

                #region //Define the page with the header
                string fontpath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\times.ttf";
                BaseFont bf = BaseFont.CreateFont(fontpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 12);
                iTextSharp.text.Font f1 = new iTextSharp.text.Font(bf, 16, Font.BOLD, BaseColor.BLUE);
                iTextSharp.text.Font f3 = new iTextSharp.text.Font(bf, 18, Font.BOLD, BaseColor.BLUE);
                iTextSharp.text.Font fBold = new iTextSharp.text.Font(bf, 18, Font.BOLD | Font.UNDERLINE);
                iTextSharp.text.Document ds = new iTextSharp.text.Document(PageSize.A4_LANDSCAPE, 50f, 50f, 5f, 15f);
                ds.SetPageSize(iTextSharp.text.PageSize.A4_LANDSCAPE.Rotate());

                PdfPTable headerTable = new PdfPTable(1);
                headerTable.DefaultCell.Padding = 3;
                headerTable.DefaultCell.Border = (int)BorderStyle.Outset;
                headerTable.DefaultCell.BorderColor = BaseColor.BLUE;
                headerTable.WidthPercentage = 100f;
                headerTable.SpacingBefore = 10;
                headerTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                headerTable.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                headerTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // can also be set on the cell
                headerTable.AddCell(new PdfPCell(new Phrase($"تقـريــر الكفالات الخاصة بالعقــــد ", f3))
                {
                    BorderColor = WebColors.GetRGBColor("#ffffff"),
                    Border = (int)BorderStyle.Outset,
                    BorderWidth = 1,
                    BorderWidthLeft = 1,
                    MinimumHeight = 30,
                    BackgroundColor = WebColors.GetRGBColor("#e8f0fe"),
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                headerTable.HorizontalAlignment = (int)Alignment.CENTER;

                //Save file 

                PdfWriter writer = PdfWriter.GetInstance(ds, new FileStream(OutputPath, FileMode.Create));
                writer.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

                ds.Open();

                #endregion

                #region //First block 
                PdfPTable tableRequestDetails = new PdfPTable(15); // a table with 4 cell 
                tableRequestDetails.DefaultCell.Padding = 5;
                tableRequestDetails.WidthPercentage = 100f;
                tableRequestDetails.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tableRequestDetails.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                tableRequestDetails.SpacingBefore = 30;

                tableRequestDetails.SetTotalWidth(new float[] { 5, 5, 5, 7, 7, 7, 7, 7, 5, 5, 6, 5, 6, 6, 3 });
                tableRequestDetails.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // can also be set on the cell

                tableRequestDetails.HorizontalAlignment = PdfWriter.CenterWindow; // can also be set on the cell
                ////
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("م", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });

                tableRequestDetails.AddCell(new PdfPCell(new Phrase("رقم العقد", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("رقم الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("قيمة الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("العملة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("حالة مبلغ الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("قيمة المبلغ", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("حالة الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("تصنيف الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("نوع الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                
                tableRequestDetails.AddCell(new PdfPCell(new Phrase(" إصدار الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase(" بداية الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase(" إنتهاء الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("ملاحظات", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("منفذ المعاملة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                ///

                #endregion

                // f.Size = 18;
                //Add contnet to tablecontent
                int index = 1;
                foreach (var model in list)
                {
                    #region Attendence   
                    PdfPCell phCelle = new PdfPCell();
                    phCelle.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    phCelle.Phrase = new Phrase();
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(index.ToString(), f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.ContractNumber, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.WarrantyNumber, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.WarrantyValue.ToString(), f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.CurrencyName, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.DecreaseOrIncreaseName, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.DecreaseOrIncreaseValue.ToString(), f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.GuaranteeStatusName, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.GuaranteeSortName.ToString(), f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.WarrantyTypeName, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.GuaranteeIssueDate?.ToString("yyyy/MM/dd"), f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.GuaranteeStartDate?.ToString("yyyy/MM/dd"), f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.GuaranteeEndDate?.ToString("yyyy/MM/dd"), f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.NoteWarranty, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.CreatedByName, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });


                    index++;

                }
                #endregion
                #region //Second table
                //Add Content
                PdfPTable secondTable = new PdfPTable(1); // a table with 1 cell
                secondTable.DefaultCell.Padding = 3;
                secondTable.DefaultCell.Border = (int)BorderStyle.Outset;
                secondTable.DefaultCell.BorderColor = BaseColor.BLUE;
                secondTable.WidthPercentage = 100f;
                secondTable.SpacingBefore = 10;
                secondTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                secondTable.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                secondTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // can also be set on the cell
                secondTable
                    .AddCell(new PdfPCell(new Phrase($"اجمـــالي قيمــة الكفالات الخاصة بالعقد / {list.Sum(x => x.WarrantyValue).Value.ToString("0,000.000")} دينـار كويتـــي", f1))
                    { BorderColor = WebColors.GetRGBColor("#006ead"), Border = (int)BorderStyle.Outset, BorderWidth = 1, BorderWidthLeft = 1, MinimumHeight = 30, BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), HorizontalAlignment = Element.ALIGN_CENTER });
                secondTable
                   .AddCell(new PdfPCell(new Phrase($"اجمـــالي قيمــة التخفيض أو الزيادة بالعقد / {list.Sum(x => x.DecreaseOrIncreaseValue).Value.ToString("0,000.000")} دينـار كويتـــي", f1))
                   { BorderColor = WebColors.GetRGBColor("#006ead"), Border = (int)BorderStyle.Outset, BorderWidth = 1, BorderWidthLeft = 1, MinimumHeight = 30, BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), HorizontalAlignment = Element.ALIGN_CENTER });
                secondTable
                    .AddCell(new PdfPCell(new Phrase($"اجمـــالي عــدد الكفالات الخاصة بالعقد /  {list.Count.ToString()} كفالة", f1))
                    { BorderColor = WebColors.GetRGBColor("#006ead"), Border = (int)BorderStyle.Outset, BorderWidth = 1, BorderWidthLeft = 1, BorderWidthBottom = 1, BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });


                #endregion
                //Add Content
                PdfPTable tableContent = new PdfPTable(2); // a table with 1 cell
                tableContent.DefaultCell.Padding = 5;
                tableContent.DefaultCell.Border = (int)BorderStyle.None;
                tableContent.DefaultCell.BorderColor = BaseColor.WHITE;
                tableContent.WidthPercentage = 100f;
                tableContent.SpacingBefore = 8;
                tableContent.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tableContent.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                tableContent.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // can also be set on the cell

                //Append To page
                ds.Add(headerTable);
                ds.Add(tableRequestDetails);
                ds.Add(tableContent);
                ds.Add(secondTable);
                // step 5
                ds.Close();

                #region // رقم الصفحة والخلفية
                try
                {
                    Document document = new Document();
                    PdfReader pdfReader = new PdfReader(OutputPath);
                    PdfStamper stamp = new PdfStamper(pdfReader, new FileStream(OutputPath.Replace(".pdf", "[temp][file].pdf"), FileMode.Create));
                    //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Server.MapPath(@"/Content/img/logo.png"));
                    //img.SetAbsolutePosition(125, 300); // set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                    PdfContentByte waterMark;
                    for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                    {
                        waterMark = stamp.GetUnderContent(page);
                        //waterMark.AddImage(img);
                        ColumnText.ShowTextAligned(stamp.GetOverContent(page), Element.ALIGN_RIGHT,
                            new Phrase($"صفحــة {page} (إدارة العقود والمناقصات)", f), document.GetLeft(470), 10, 0, PdfWriter.RUN_DIRECTION_RTL, ColumnText.AR_NOVOWEL);

                    }
                    stamp.FormFlattening = true;
                    stamp.Close();
                    pdfReader.Close();
                    document.Close();

                    // now delete the original file and rename the temp file to the original file
                    System.IO.File.Delete(OutputPath);
                    System.IO.File.Move(OutputPath.Replace(".pdf", "[temp][file].pdf"), OutputPath);
                }
                catch (Exception ex)
                {
                }
                #endregion

            }
            return fileName + ".pdf";
        }


        [HttpPost]
        public JsonResult ExportWarrantyHistoryReport(string ContractNumber, int Id, string WarrantyNumber, decimal? WarrantyValue, int? GuaranteeSortId, int? GuaranteeStatusId, int? WarrantyTypeId, int? DecreaseOrIncreaseId, DateTime? GuaranteeIssueDate, DateTime? GuaranteeStartDate, DateTime? GuaranteeEndDate, string CreatedBy)

        {
            var path = ExportWarrantyHistoryPdf(ContractNumber, Id, WarrantyNumber, WarrantyValue, GuaranteeSortId, GuaranteeStatusId, WarrantyTypeId, DecreaseOrIncreaseId, GuaranteeIssueDate, GuaranteeStartDate, GuaranteeEndDate, CreatedBy, context);
            if (!string.IsNullOrEmpty(path))
                return Json(path, JsonRequestBehavior.AllowGet);
            else
                return Json(new { error = "حدث خطأ يرجى إعادة المحاولة" });
        }
        public string ExportWarrantyHistoryPdf(string ContractNumber, int Id, string WarrantyNumber, decimal? WarrantyValue, int? GuaranteeSortId, int? GuaranteeStatusId, int? WarrantyTypeId, int? DecreaseOrIncreaseId, DateTime? GuaranteeIssueDate, DateTime? GuaranteeStartDate, DateTime? GuaranteeEndDate, string CreatedBy, DatabseEntities context)
        {
            #region prepareData
            var list = _WarrantyServices.SearchWarrantyHistoryGrid(Id).Where(x =>
            (WarrantyValue.HasValue ? x.WarrantyValue == WarrantyValue : WarrantyValue == null)
            && (GuaranteeSortId > 0 ? x.GuaranteeSortId == GuaranteeSortId : GuaranteeSortId == 0)
            && (WarrantyTypeId > 0 ? x.WarrantyTypeId == WarrantyTypeId : WarrantyTypeId == 0)
            && (DecreaseOrIncreaseId > 0 ? x.DecreaseOrIncreaseId == DecreaseOrIncreaseId : DecreaseOrIncreaseId == 0)
            && (!string.IsNullOrEmpty(ContractNumber) ? x.ContractNumber.Contains(ContractNumber) : string.IsNullOrEmpty(ContractNumber))
            && (!string.IsNullOrEmpty(WarrantyNumber) ? x.WarrantyNumber.Contains(WarrantyNumber) : string.IsNullOrEmpty(WarrantyNumber))
            && (!string.IsNullOrEmpty(CreatedBy) ? x.CreatedByName.Contains(CreatedBy) : string.IsNullOrEmpty(CreatedBy))
            ).ToList();
            #endregion
            string fileName = Guid.NewGuid().ToString();
            var filePath = string.Format("/FilesUpload/{0}", fileName + ".pdf");
            var OutputPath = Server.MapPath(filePath);
            using (MemoryStream stream = new System.IO.MemoryStream())
            {

                #region //Define the page with the header
                string fontpath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\times.ttf";
                BaseFont bf = BaseFont.CreateFont(fontpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 12);
                iTextSharp.text.Font f1 = new iTextSharp.text.Font(bf, 16, Font.BOLD, BaseColor.BLUE);
                iTextSharp.text.Font f3 = new iTextSharp.text.Font(bf, 18, Font.BOLD, BaseColor.BLUE);
                iTextSharp.text.Font fBold = new iTextSharp.text.Font(bf, 18, Font.BOLD | Font.UNDERLINE);
                iTextSharp.text.Document ds = new iTextSharp.text.Document(PageSize.A4_LANDSCAPE, 50f, 50f, 5f, 15f);
                ds.SetPageSize(iTextSharp.text.PageSize.A4_LANDSCAPE.Rotate());

                PdfPTable headerTable = new PdfPTable(1);
                headerTable.DefaultCell.Padding = 3;
                headerTable.DefaultCell.Border = (int)BorderStyle.Outset;
                headerTable.DefaultCell.BorderColor = BaseColor.BLUE;
                headerTable.WidthPercentage = 100f;
                headerTable.SpacingBefore = 10;
                headerTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                headerTable.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                headerTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // can also be set on the cell
                headerTable.AddCell(new PdfPCell(new Phrase($"تقـريــر أرشيف الكفالة ", f3))
                {
                    BorderColor = WebColors.GetRGBColor("#ffffff"),
                    Border = (int)BorderStyle.Outset,
                    BorderWidth = 1,
                    BorderWidthLeft = 1,
                    MinimumHeight = 30,
                    BackgroundColor = WebColors.GetRGBColor("#e8f0fe"),
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                headerTable.HorizontalAlignment = (int)Alignment.CENTER;

                //Save file 

                PdfWriter writer = PdfWriter.GetInstance(ds, new FileStream(OutputPath, FileMode.Create));
                writer.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

                ds.Open();

                #endregion

                #region //First block 
                PdfPTable tableRequestDetails = new PdfPTable(13); // a table with 4 cell 
                tableRequestDetails.DefaultCell.Padding = 5;
                tableRequestDetails.WidthPercentage = 100f;
                tableRequestDetails.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tableRequestDetails.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                tableRequestDetails.SpacingBefore = 30;

                tableRequestDetails.SetTotalWidth(new float[] { 7, 7, 7, 7, 5, 5, 5, 6, 5, 6, 6, 6, 3 });
                tableRequestDetails.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // can also be set on the cell

                tableRequestDetails.HorizontalAlignment = PdfWriter.CenterWindow; // can also be set on the cell
                ////
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("م", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });

                tableRequestDetails.AddCell(new PdfPCell(new Phrase("رقم العقد", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("رقم الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("قيمة الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("حالة مبلغ الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("قيمة مبلغ الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("حالة الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("تصنيف الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("نوع الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                //tableRequestDetails.AddCell(new PdfPCell(new Phrase("العملة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase(" إصدار الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase(" بداية الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase(" إنتهاء الكفالة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });

                //tableRequestDetails.AddCell(new PdfPCell(new Phrase("ملاحظات", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("منفذ المعاملة", f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                ///

                #endregion

                // f.Size = 18;
                //Add contnet to tablecontent
                int index = 1;
                foreach (var model in list)
                {
                    #region Attendence   
                    PdfPCell phCelle = new PdfPCell();
                    phCelle.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    phCelle.Phrase = new Phrase();
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(index.ToString(), f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.ContractNumber, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.WarrantyNumber, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.WarrantyValue.ToString(), f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.DecreaseOrIncreaseName, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.DecreaseOrIncreaseValue.ToString(), f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.GuaranteeStatusName, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.GuaranteeSortName.ToString(), f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.WarrantyTypeName, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    //tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.CurrencyName, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.GuaranteeIssueDate?.ToString("yyyy/MM/dd"), f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.GuaranteeStartDate?.ToString("yyyy/MM/dd"), f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.GuaranteeEndDate?.ToString("yyyy/MM/dd"), f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });

                    //tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.NoteWarranty, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.CreatedByName, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });


                    index++;

                }
                #endregion
                #region //Second table
                //Add Content
                PdfPTable secondTable = new PdfPTable(1); // a table with 1 cell
                secondTable.DefaultCell.Padding = 3;
                secondTable.DefaultCell.Border = (int)BorderStyle.Outset;
                secondTable.DefaultCell.BorderColor = BaseColor.BLUE;
                secondTable.WidthPercentage = 100f;
                secondTable.SpacingBefore = 10;
                secondTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                secondTable.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                secondTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // can also be set on the cell
                //secondTable
                //    .AddCell(new PdfPCell(new Phrase($"اجمـــالي قيمــة الكفالات الخاصة بالعقد / {list.Sum(x => x.WarrantyValue).Value.ToString("0,000.000")} دينـار كويتـــي", f1))
                //    { BorderColor = WebColors.GetRGBColor("#006ead"), Border = (int)BorderStyle.Outset, BorderWidth = 1, BorderWidthLeft = 1, MinimumHeight = 30, BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), HorizontalAlignment = Element.ALIGN_CENTER });
                //secondTable
                //   .AddCell(new PdfPCell(new Phrase($"اجمـــالي قيمــة التخفيض أو الزيادة بالعقد / {list.Sum(x => x.DecreaseOrIncreaseValue).Value.ToString("0,000.000")} دينـار كويتـــي", f1))
                //   { BorderColor = WebColors.GetRGBColor("#006ead"), Border = (int)BorderStyle.Outset, BorderWidth = 1, BorderWidthLeft = 1, MinimumHeight = 30, BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), HorizontalAlignment = Element.ALIGN_CENTER });
                //secondTable
                //    .AddCell(new PdfPCell(new Phrase($"اجمـــالي عــدد الكفالات الخاصة بالعقد /  {list.Count.ToString()} كفالة", f1))
                //    { BorderColor = WebColors.GetRGBColor("#006ead"), Border = (int)BorderStyle.Outset, BorderWidth = 1, BorderWidthLeft = 1, BorderWidthBottom = 1, BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });


                #endregion
                //Add Content
                PdfPTable tableContent = new PdfPTable(2); // a table with 1 cell
                tableContent.DefaultCell.Padding = 5;
                tableContent.DefaultCell.Border = (int)BorderStyle.None;
                tableContent.DefaultCell.BorderColor = BaseColor.WHITE;
                tableContent.WidthPercentage = 100f;
                tableContent.SpacingBefore = 8;
                tableContent.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tableContent.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                tableContent.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // can also be set on the cell

                //Append To page
                ds.Add(headerTable);
                ds.Add(tableRequestDetails);
                ds.Add(tableContent);
                ds.Add(secondTable);
                // step 5
                ds.Close();

                #region // رقم الصفحة والخلفية
                try
                {
                    Document document = new Document();
                    PdfReader pdfReader = new PdfReader(OutputPath);
                    PdfStamper stamp = new PdfStamper(pdfReader, new FileStream(OutputPath.Replace(".pdf", "[temp][file].pdf"), FileMode.Create));
                    //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Server.MapPath(@"/Content/img/logo.png"));
                    //img.SetAbsolutePosition(125, 300); // set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                    PdfContentByte waterMark;
                    for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                    {
                        waterMark = stamp.GetUnderContent(page);
                        //waterMark.AddImage(img);
                        ColumnText.ShowTextAligned(stamp.GetOverContent(page), Element.ALIGN_RIGHT,
                            new Phrase($"صفحــة {page} (إدارة العقود والمناقصات)", f), document.GetLeft(470), 10, 0, PdfWriter.RUN_DIRECTION_RTL, ColumnText.AR_NOVOWEL);

                    }
                    stamp.FormFlattening = true;
                    stamp.Close();
                    pdfReader.Close();
                    document.Close();

                    // now delete the original file and rename the temp file to the original file
                    System.IO.File.Delete(OutputPath);
                    System.IO.File.Move(OutputPath.Replace(".pdf", "[temp][file].pdf"), OutputPath);
                }
                catch (Exception ex)
                {
                }
                #endregion

            }
            return fileName + ".pdf";
        }

    }

}