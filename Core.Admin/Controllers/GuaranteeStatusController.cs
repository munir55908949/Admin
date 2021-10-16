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
    public class GuaranteeStatusController : ParentController
    {
        // GET: GuaranteeStatus
        public ActionResult Index()
        {
            GuaranteeStatusModel model = new GuaranteeStatusModel();
            InitalizeModelLookups.InitModel(model);
            return View(model);
        }
        // Search
        [HttpPost]
        //paramter fixed with all Search functions  int jtStartIndex = 0, int jtPageSize = 10, string jtSorting = null
        public JsonResult Search( int? Id, string Name,string Note, DateTime? CreatedAt, string CreatedBy,
    out int AllListCount, int jtStartIndex = 0,
    int jtPageSize = 10, string jtSorting = null)
        {
            int count = 0;
            AllListCount = 0;
            //pass from controller to service
            var list = _GuaranteeStatusServices.SearchGrid(Id, Name, Note, CreatedAt, CreatedBy, out count, jtStartIndex, jtPageSize, jtSorting);
            return Json(new { Result = "OK", Records = list, TotalRecordCount = count });
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_GuaranteeStatusServices.SearchGrid().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ExportReport(int? Id, string Name, string Note,
            DateTime? CreatedAt,  string CreatedBy)
        {
            var path = ExportPdf(Id, Name, Note, CreatedAt, CreatedBy, context);
            if (!string.IsNullOrEmpty(path))
                return Json(path, JsonRequestBehavior.AllowGet);
            else
                return Json(new { error = "حدث خطأ يرجى إعادة المحاولة" });
        }
        public string ExportPdf( int? Id, string Name, string Note,
            DateTime? CreatedAt,  string CreatedBy, DatabseEntities context)
        {
            #region prepareData
            var list = _GuaranteeStatusServices.SearchGrid().Where(x =>
             (Id > 0 ? x.Id == Id : Id == 0)
            && (CreatedAt.HasValue ? x.CreatedAt >= CreatedAt : CreatedAt == null)
            && (!string.IsNullOrEmpty(CreatedBy) ? x.CreatedByName.Contains(CreatedBy) : string.IsNullOrEmpty(CreatedBy))
            && (!string.IsNullOrEmpty(Note) ? x.Note.Contains(Note) : string.IsNullOrEmpty(Note))
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
                headerTable.AddCell(new PdfPCell(new Phrase($"تقـريــر  القطاعات ", f3))
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
                PdfWriter writer = PdfWriter.GetInstance(ds, new FileStream(OutputPath, FileMode.Create));
                writer.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                ds.Open();
                #endregion
                #region //First block 
                PdfPTable tableRequestDetails = new PdfPTable(4); // a table with 4 cell 
                tableRequestDetails.DefaultCell.Padding = 5;
                tableRequestDetails.WidthPercentage = 100f;
                tableRequestDetails.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tableRequestDetails.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                tableRequestDetails.SpacingBefore = 30;
                tableRequestDetails.SetTotalWidth(new float[] { 7, 15, 15, 4 });
                tableRequestDetails.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // can also be set on the cell
                tableRequestDetails.HorizontalAlignment = PdfWriter.CenterWindow; // can also be set on the cell
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("م", f1)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase(" القطاع", f1)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase(" ملاحظات", f1)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                tableRequestDetails.AddCell(new PdfPCell(new Phrase("تاريخ تسجيل القطاع", f1)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                #endregion
                int index = 1;
                foreach (var model in list)
                {
                    #region Attendence   
                    PdfPCell phCelle = new PdfPCell();
                    phCelle.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    phCelle.Phrase = new Phrase();
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(index.ToString(), f)) { BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.Name, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.Note, f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    tableRequestDetails.AddCell(new PdfPCell(new Phrase(model.CreatedAt?.ToString("yyyy/MM/dd"), f)) { MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                    #endregion
                    index++;
                }
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
                    .AddCell(new PdfPCell(new Phrase($"اجمـــالي عــدد القطاعات /  {list.Count.ToString()} قطاع", f1))
                    { BorderColor = WebColors.GetRGBColor("#006ead"), Border = (int)BorderStyle.Outset, BorderWidth = 1, BorderWidthLeft = 1, BorderWidthBottom = 1, BackgroundColor = WebColors.GetRGBColor("#e8f0fe"), MinimumHeight = 30, HorizontalAlignment = Element.ALIGN_CENTER });
                # endregion
                ds.Add(headerTable);
                ds.Add(tableRequestDetails);
                ds.Add(secondTable);
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
                            new Phrase($"صفحـــة {page} (إدارة المشتـــريـات)", f), document.GetLeft(470), 7, 0, PdfWriter.RUN_DIRECTION_RTL, ColumnText.AR_NOVOWEL);
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
        public ActionResult AddEdit(int? Id)
        {
            GuaranteeStatusModel model = new GuaranteeStatusModel();
            GuaranteeStatus obj = new GuaranteeStatus();
            if (Id > 0)
            {
                obj = _GuaranteeStatusServices.GetGuaranteeStatusById(Id.Value);
            }
            Mapper.FillModelFromObject(model, obj);
            InitalizeModelLookups.InitModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEdit(GuaranteeStatusModel model)
        {
            if (ModelState.IsValid)
            {
                GuaranteeStatus obj = context.GuaranteeStatus
                    .Where(p => p.Id == model.Id).FirstOrDefault();
                if (obj == null)
                {
                    obj = new GuaranteeStatus();
                }
                var userId = User.Identity.GetUserId();
                Mapper.FillObjectFromModel(obj, model);
                if (model.Id == 0)//not exist in DB and create new recored
                {
                    obj.CreatedBy = userId;
                    obj.CreatedAt = DateTime.Now;
                    context.GuaranteeStatus.Add(obj);
                }
                context.SaveChanges();
                TempData["Success"] = "تم الحفظ بنجاح";
                return RedirectToAction("Index", "GuaranteeStatus");
            }
            InitalizeModelLookups.InitModel(model);
            return View(model);
        }
        public JsonResult Complete(int Id)
        {
            var item = context.GuaranteeStatus
                .Where(p => p.Id == Id).FirstOrDefault();
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItem(int Id)
        {
            var item = context.GuaranteeStatus
                .Where(p => p.Id == Id).FirstOrDefault();
            item.IsDeleted = true;
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}