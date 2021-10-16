using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.AccessLayer;
using Microsoft.AspNet.Identity;
using Core.Admin.Models;
using iTextSharp.text.pdf.qrcode;
using Core.Services;

namespace Core.Admin.Helpers
{
    public class Mapper
    {
        //This function to fill data from page to database
        public static void FillObjectFromModel(Administration obj, AdministrationModel model)
        {
            if (model == null)
                model = new AdministrationModel();
            obj.Name = model.Name;
            obj.Note = model.Note;
            obj.SectorId = model.SectorId;
            obj.CreatedAt = model.CreatedAt;
        }
        public static void FillModelFromObject(AdministrationModel model, Administration obj)
        {
            if (model == null)
                model = new AdministrationModel();
            model.Id = obj.Id;
            model.Name = obj.Name;
            model.Note = obj.Note;
            model.SectorId = obj.SectorId;
            model.ModifiedBy = obj.ModifiedBy;
        }
        //This function to fill data from page to database
        public static void FillObjectFromModel(ContractData obj, ContractDataModel model)
        {
            if (model == null)
                model = new ContractDataModel();
            obj.ContractNumber = model.ContractNumber;
            obj.ContractStatusId = model.ContractStatusId;
            obj.CompanyId = model.CompanyId;
            obj.BankId = model.BankId;
            obj.SectorId = model.SectorId;
            obj.AdministrationId = model.AdministrationId;
            obj.Note = model.Note;
            obj.CreatedAt = model.CreatedAt;
        }
        public static void FillModelFromObject(ContractDataModel model, ContractData obj)
        {
            if (model == null)
                model = new ContractDataModel();
            model.Id = obj.Id;
            model.ContractNumber = obj.ContractNumber;
            model.ContractStatusId = obj.ContractStatusId;
            model.CompanyId = obj.CompanyId;
            model.BankId = obj.BankId;
            model.SectorId = obj.SectorId;
            model.AdministrationId = obj.AdministrationId;
            model.Note = obj.Note;
            model.CreatedBy = obj.CreatedBy;
            model.Path = obj.Path;
        }
        //This function to fill data from page to database
        public static void FillObjectFromModel(Warranty obj, WarrantyModel model)
        {
            if (model == null)
                model = new WarrantyModel();
            obj.ContractDataId = model.ContractDataId;
            obj.WarrantyNumber = model.WarrantyNumber;
            obj.WarrantyValue = model.WarrantyValue;
            obj.DecreaseOrIncreaseValue = model.DecreaseOrIncreaseValue;
            obj.DecreaseOrIncreaseId = model.DecreaseOrIncreaseId;
            obj.CurrencyId = model.CurrencyId;
            obj.WarrantyTypeId = model.WarrantyTypeId;
            obj.GuaranteeStatusId = model.GuaranteeStatusId;
            obj.GuaranteeSortId = model.GuaranteeSortId;
            obj.GuaranteeIssueDate = model.GuaranteeIssueDate;
            obj.GuaranteeStartDate = model.GuaranteeStartDate;
            obj.GuaranteeEndDate = model.GuaranteeEndDate;
            obj.NoteWarranty = model.NoteWarranty;
            obj.CreatedAt = model.CreatedAt;
        }
        public static void FillModelFromObject(WarrantyModel model, Warranty obj)
        {
            if (model == null)
                model = new WarrantyModel();
            model.Id = obj.Id;
            model.ContractDataId = obj.ContractDataId;
            model.WarrantyNumber = obj.WarrantyNumber;
            model.WarrantyValue = obj.WarrantyValue;
            model.DecreaseOrIncreaseValue = obj.DecreaseOrIncreaseValue;
            model.DecreaseOrIncreaseId = obj.DecreaseOrIncreaseId;
            model.CurrencyId = obj.CurrencyId;
            model.WarrantyTypeId = obj.WarrantyTypeId;
            model.GuaranteeStatusId = obj.GuaranteeStatusId;
            model.GuaranteeSortId = obj.GuaranteeSortId;
            model.GuaranteeIssueDate = obj.GuaranteeIssueDate;
            model.GuaranteeStartDate = obj.GuaranteeStartDate;
            model.GuaranteeEndDate = obj.GuaranteeEndDate;
            model.NoteWarranty = obj.NoteWarranty;
            model.CreatedBy = obj.CreatedBy;
            model.Path = obj.Path;
        }
        //This function to fill data from page to database
        public static void FillObjectFromModel(Company obj, CompanyModel model)
        {
            if (model == null)
                model = new CompanyModel();
            obj.Name = model.Name;
            obj.Address = model.Address;
            obj.CreatedAt = model.CreatedAt;
        }
        public static void FillModelFromObject(CompanyModel model, Company obj)
        {
            if (model == null)
                model = new CompanyModel();
            model.Id = obj.Id;
            model.Name = obj.Name;
            model.Address = obj.Address;
            model.CreatedBy = obj.CreatedBy;
            model.CreatedAt = obj.CreatedAt;
        }
        public static void FillObjectFromModel(Sector obj, SectorModel model)
        {
            if (model == null)
                model = new SectorModel();
            obj.Name = model.Name;
            obj.Note = model.Note;
            obj.CreatedAt = model.CreatedAt;
        }
        public static void FillModelFromObject(SectorModel model, Sector obj)
        {
            if (model == null)
                model = new SectorModel();
            model.Id = obj.Id;
            model.Name = obj.Name;
            model.Note = obj.Note;
            model.CreatedBy = obj.CreatedBy;
            model.CreatedAt = obj.CreatedAt;
        }
        public static void FillObjectFromModel(GuaranteeStatus obj, GuaranteeStatusModel model)
        {
            if (model == null)
                model = new GuaranteeStatusModel();
            obj.Name = model.Name;
            obj.Note = model.Note;
            obj.CreatedAt = model.CreatedAt;
        }
        public static void FillModelFromObject(GuaranteeStatusModel model, GuaranteeStatus obj)
        {
            if (model == null)
                model = new GuaranteeStatusModel();
            model.Id = obj.Id;
            model.Name = obj.Name;
            model.Note = obj.Note;
            model.CreatedBy = obj.CreatedBy;
            model.CreatedAt = obj.CreatedAt;
        }
        public static void FillObjectFromModel(GuaranteeSort obj, GuaranteeSortModel model)
        {
            if (model == null)
                model = new GuaranteeSortModel();
            obj.Name = model.Name;
            obj.Note = model.Note;
            obj.CreatedAt = model.CreatedAt;
        }
        public static void FillModelFromObject(GuaranteeSortModel model, GuaranteeSort obj)
        {
            if (model == null)
                model = new GuaranteeSortModel();
            model.Id = obj.Id;
            model.Name = obj.Name;
            model.Note = obj.Note;
            model.CreatedBy = obj.CreatedBy;
            model.CreatedAt = obj.CreatedAt;
        }
        //This function to fill data from page to database
        public static void FillObjectFromModel(Bank obj, BankModel model)
        {
            if (model == null)
                model = new BankModel();
            obj.Name = model.Name;
            obj.CreatedAt = model.CreatedAt;
        }
        public static void FillModelFromObject(BankModel model, Bank obj)
        {
            if (model == null)
                model = new BankModel();
            model.Id = obj.Id;
            model.Name = obj.Name;
            model.CreatedBy = obj.CreatedBy;
            model.CreatedAt = obj.CreatedAt;
        }
        public static void FillModelFromObject(RegisterViewModel model, AspNetUsers obj)
        {
            if (model == null)
                model = new RegisterViewModel();

            model.Id = obj.Id;
            //model.PhoneNumber = obj.PhoneNumber;
            model.UserName = obj.UserName;
            model.FullName = obj.FullName;
        }
        public static void FillModelFromObject(PagesModel model, Pages obj)
        {
            if (model == null)
                model = new PagesModel();

            model.Id = obj.Id;
            model.Name = obj.Name;
            model.Icon = obj.Icon;
            model.Url = obj.Url;
            model.Order = obj.Order;
        }
        public static void FillObjectFromModel(Pages obj, PagesModel model)
        {
            if (model == null)
                model = new PagesModel();

            obj.Name = model.Name;
            obj.Url = model.Url;
            obj.Icon = model.Icon;
            obj.Order = model.Order;
        }
    }
}
