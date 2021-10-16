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
    [Validator(typeof(ContractDataValidator))]
    public class ContractDataModel
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? BankId { get; set; }
        public string BankName { get; set; }
        public int? SectorId { get; set; }
        public string SectorName { get; set; }
        public int? AdministrationId { get; set; }
        public string AdministrationName { get; set; }
        public int? ContractStatusId { get; set; }
        public string ContractStatusName { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedAtName { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string Path { get; set; }
        public string Note { get; set; }
        public bool IsDeleted { get; set; }
        public HttpPostedFileBase File { get; set; }
        public ContractDataViewModel ContractDataDetails { get; set; }
        public SelectList ContractNumberList { get;  set; }
        public SelectList ContractStatusList { get;  set; }
        public SelectList BankList { get;  set; }
        public SelectList CompanyList { get;  set; }
        public SelectList SectorList { get;  set; }
        public SelectList AdministrationList { get;  set; }
        public SelectList CreatedByNameList { get;  set; }
    }
}