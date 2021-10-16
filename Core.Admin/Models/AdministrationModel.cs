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
    [Validator(typeof(AdministrationValidator))]
    public class AdministrationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SectorId { get; set; }
        public string SectorName { get; set; }
        public int? CompanyId { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedAtName { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Note { get; set; }
        public SelectList AdministrationNoList { get;  set; }
        public SelectList SectorList { get;  set; }
        public SelectList AdministrationStatusList { get;  set; }
        public SelectList CreatedByNameList { get;  set; }
    }
}