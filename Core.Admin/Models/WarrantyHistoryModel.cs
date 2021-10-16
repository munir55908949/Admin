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
    //[Validator(typeof(GuaranteeValidator))]
    public class WarrantyHistoryModel
    {
        public int Id { get; set; }
        public int WarrantyId { get; set; }
        public string ContractNumber { get; set; }
        public string PracticeNumber { get; set; }
        public string WarrantyNumber { get; set; }
        public decimal? WarrantyValue { get; set; }
        public decimal? TotalGuaranteeValue { get; set; }
        public DateTime? GuaranteeStartDate { get; set; }
        public DateTime? GuaranteeEndDate { get; set; }
        public DateTime? NewGuaranteeEndDate { get; set; }
        public DateTime? GuaranteeIssueDate { get; set; }
        public int? GuaranteeSortId { get; set; }
        public string GuaranteeSortName { get; set; }
        public int? GuaranteeStatusId { get; set; }
        public string GuaranteeStatusName { get; set; }
        public int? SectorId { get; set; }
        public string SectorName { get; set; }
        public int? AdministrationId { get; set; }
        public string AdministrationName { get; set; }
        public int? BankId { get; set; }
        public string BankName { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
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
        public HttpPostedFileBase File { get; set; }
        public WarrantyViewModel WarrantyHistory { get; set; }
        public SelectList GuaranteeNoList { get;  set; }
        public SelectList GuaranteeStatusList { get;  set; }
        public SelectList SectorList { get; set; }
        public SelectList AdministratorList { get; set; }
        public SelectList BankList { get;  set; }
        public SelectList CompanyList { get;  set; }
        public SelectList CreatedByNameList { get;  set; }
    }
}