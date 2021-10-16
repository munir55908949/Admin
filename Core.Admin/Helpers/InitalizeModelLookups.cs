using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Admin.Models;
using Core.Services;
using Core.ViewModels;
using Microsoft.AspNet.Identity;

namespace Core.Admin.Helpers
{
    public class InitalizeModelLookups
    {
        #region Private Feilds
        private static PagesServices _PagesServices = new PagesServices();
        private static UsersServices _UsersServices = new UsersServices();
        private static CompanyServices _CompanyServices = new CompanyServices();
        private static BankServices _BankServices = new BankServices();
        private static SectorServices _SectorServices = new SectorServices();
        private static AdministrationServices _AdministrationServices = new AdministrationServices();
        private static GuaranteeSortServices _GuaranteeSortServices = new GuaranteeSortServices();
        private static GuaranteeStatusServices _GuaranteeStatusServices = new GuaranteeStatusServices();
        private static ContractDataServices _ContractDataServices = new ContractDataServices();
        private static WarrantyServices _WarrantyServices = new WarrantyServices();
        #endregion
       
        
        public static void InitModel(ContractDataModel model)
        {
            string actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            if (actionName == "Index")
            {
                model.CompanyList = new SelectList(_CompanyServices.GetCompanyName("البحث بالشركة"), "Id", "Name", model.CompanyList);
                model.BankList = new SelectList(_BankServices.GetBankName("البحث بالبنك"), "Id", "Name", model.BankList);
                model.SectorList = new SelectList(_SectorServices.GetSectorName("البحث بالقطاع"), "Id", "Name", model.SectorList);
                model.AdministrationList = new SelectList(_AdministrationServices.GetAdministrationName("البحث بالإدارة"), "Id", "Name", model.AdministrationList);
                model.ContractStatusList = new SelectList(_ContractDataServices.GetContractStatusName("البحث بحالة العقـــد"), "Id", "Name", model.ContractStatusList);
            }
            else
            {
                model.CompanyList = new SelectList(_CompanyServices.GetCompanyName("إختر الشركة"), "Id", "Name", model.CompanyList);
                model.BankList = new SelectList(_BankServices.GetBankName("إختر البنك"), "Id", "Name", model.BankList);
                model.SectorList = new SelectList(_SectorServices.GetSectorName("إختر القطاع"), "Id", "Name", model.SectorList);
                model.AdministrationList = new SelectList(_AdministrationServices.GetAdministrationName("إختر الإدارة"), "Id", "Name", model.AdministrationList);
                model.ContractStatusList = new SelectList(_ContractDataServices.GetContractStatusName("إختر حالة العقـــد"), "Id", "Name", model.ContractStatusList);
            }
        }
        public static void InitModel(WarrantyModel model)
        {
            string actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            if (actionName == "Index")
            {
                model.GuaranteeSortList = new SelectList(_WarrantyServices.GetGuaranteeSortName("البحث بتصنيف الكفالة"), "Id", "Name", model.GuaranteeSortList);
                model.GuaranteeStatusList = new SelectList(_WarrantyServices.GetGuaranteeStatusName("البحث بحالة الكفالة"), "Id", "Name", model.GuaranteeStatusList);
                model.WarrantyTypeList = new SelectList(_WarrantyServices.GetWarrantyTypeName("البحث بنوع الكفالة"), "Id", "Name", model.WarrantyTypeList);
                model.CurrencyList = new SelectList(_WarrantyServices.GetCurrencyName("البحث بتخفيض أو زيادة"), "Id", "Name", model.CurrencyList);
                model.DecreaseOrIncreaseList = new SelectList(_WarrantyServices.GetDecreaseOrIncreaseName("البحث بالعملة"), "Id", "Name", model.DecreaseOrIncreaseList);
            }
            else
            {
                model.GuaranteeSortList = new SelectList(_WarrantyServices.GetGuaranteeSortName("إختر تصنيف الكفالة"), "Id", "Name", model.GuaranteeSortList);
                model.GuaranteeStatusList = new SelectList(_WarrantyServices.GetGuaranteeStatusName("إختر حالة الكفالة"), "Id", "Name", model.GuaranteeStatusList);
                model.WarrantyTypeList = new SelectList(_WarrantyServices.GetWarrantyTypeName("إختر نوع الكفالة"), "Id", "Name", model.WarrantyTypeList);
                model.CurrencyList = new SelectList(_WarrantyServices.GetCurrencyName("إختر العملة"), "Id", "Name", model.CurrencyList);
                model.DecreaseOrIncreaseList = new SelectList(_WarrantyServices.GetDecreaseOrIncreaseName("إختر تخفيض أو زيادة"), "Id", "Name", model.DecreaseOrIncreaseList);
            }
        }
        
        public static void InitModel(CompanyModel model)
        {

            string actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            if (actionName == "Index")
            {
                model.CompanyNameList = new SelectList(_CompanyServices.GetCompanyName("البحث بالشركة"), "Id", "Name", model.CompanyNameList);
            }
            else
            {
                model.CompanyNameList = new SelectList(_CompanyServices.GetCompanyName("إختر الشركة"), "Id", "Name", model.CompanyNameList);
            }
        }
        public static void InitModel(SectorModel model)
        {

            string actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            if (actionName == "Index")
            {
                model.SectorNameList = new SelectList(_SectorServices.GetSectorName("البحث بالقطاع"), "Id", "Name", model.SectorNameList);
            }
            else
            {
                model.SectorNameList = new SelectList(_SectorServices.GetSectorName("إختر القطاع"), "Id", "Name", model.SectorNameList);
            }
        }
        public static void InitModel(GuaranteeSortModel model)
        {

            string actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            if (actionName == "Index")
            {
                model.GuaranteeSortNameList = new SelectList(_GuaranteeSortServices.GetGuaranteeSortName("البحث بتصنيف الكفالة"), "Id", "Name", model.GuaranteeSortNameList);
            }
            else
            {
                model.GuaranteeSortNameList = new SelectList(_GuaranteeSortServices.GetGuaranteeSortName("إختر تصنيف الكفالة"), "Id", "Name", model.GuaranteeSortNameList);
            }
        }
        public static void InitModel(GuaranteeStatusModel model)
        {

            string actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            if (actionName == "Index")
            {
                model.GuaranteeStatusNameList = new SelectList(_GuaranteeStatusServices.GetGuaranteeStatusName("البحث بحالة الكفالة"), "Id", "Name", model.GuaranteeStatusNameList);
            }
            else
            {
                model.GuaranteeStatusNameList = new SelectList(_GuaranteeStatusServices.GetGuaranteeStatusName("إختر حالة الكفالة"), "Id", "Name", model.GuaranteeStatusNameList);
            }
        }
        public static void InitModel(AdministrationModel model)
        {
            string actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            if (actionName == "Index")
            {
               
                model.SectorList = new SelectList(_SectorServices.GetSectorName("البحث بالقطاع"), "Id", "Name", model.SectorList);
                
            }
            else
            {
               
                model.SectorList = new SelectList(_SectorServices.GetSectorName("إختر القطاع"), "Id", "Name", model.SectorList);
                
            }
        }
        public static void InitModel(BankModel model)
        {

            string actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            if (actionName == "Index")
            {
                model.BankList = new SelectList(_BankServices.GetBankName("البحث بالبنك"), "Id", "Name", model.BankList);
            }
            else
            {
                model.BankList = new SelectList(_BankServices.GetBankName("إختر البنك"), "Id", "Name", model.BankList);
            }
        }
        public static void InitModel(RegisterViewModel model)
        {
            string actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            if (actionName == "Index")
            {
            }
            else
            {
            }
        }
    }
}