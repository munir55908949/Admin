using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Core.Admin.Validator;
using Core.ViewModels;
using System.Web.Mvc;

namespace Core.Admin.Models
{
    [Validator(typeof(WarrantyValidator))]
    public class WarrantyModel
    {
        public WarrantyModel()
        {
            ContractDataDetails = new ContractDataViewModel();
        }
        public int Id { get; set; }
        public string WarrantyNumber { get; set; }
        public string ContractNumber { get; set; }
        public int? WarrantyId { get; set; }
        public decimal? WarrantyValue { get; set; }
        public decimal? DecreaseOrIncreaseValue { get; set; }
        public int? ContractDataId { get; set; }
        public string ContractDataName { get; set; }
        public int? GuaranteeSortId { get; set; }
        public string GuaranteeSortName { get; set; }
        public int? GuaranteeStatusId { get; set; }
        public string GuaranteeStatusName { get; set; }
        public int? WarrantyTypeId { get; set; }
        public string WarrantyTypeName { get; set; }
        public int? DecreaseOrIncreaseId { get; set; }
        public string DecreaseOrIncreaseName { get; set; }
        public int? CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public DateTime? GuaranteeIssueDate { get; set; }
        public DateTime? GuaranteeStartDate { get; set; }
        public DateTime? GuaranteeEndDate { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedAtName { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string Path { get; set; }
        public string NoteWarranty { get; set; }
        public bool IsDeleted { get; set; }

        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? BankId { get; set; }
        public string BankName { get; set; }
        public int? ContractStatusId { get; set; }
        public string ContractStatusName { get; set; }
        public int? SectorId { get; set; }
        public string SectorName { get; set; }
        public int? AdministrationId { get; set; }
        public string AdministrationName { get; set; }
        public ContractDataViewModel ContractDataDetails { get; set; }
        public HttpPostedFileBase File { get; set; }
        public WarrantyViewModel WarrantyDetails { get; set; }
        public SelectList WarrantyNumberList { get;  set; }
        public SelectList GuaranteeSortList { get;  set; }
        public SelectList GuaranteeStatusList { get;  set; }
        public SelectList WarrantyTypeList { get;  set; }
        public SelectList DecreaseOrIncreaseList { get;  set; }
        public SelectList CurrencyList { get;  set; }
        public SelectList ContractList { get;  set; }
        public SelectList CreatedByNameList { get;  set; }
    }
}