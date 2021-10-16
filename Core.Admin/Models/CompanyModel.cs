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
    //[Validator(typeof(CompanyValidator))]
    public class CompanyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public SelectList CompanyNameList { get;  set; }
        public SelectList CompanyAddressList { get;  set; }
        public SelectList CreatedByNameList { get;  set; }
    }
}