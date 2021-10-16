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
    //[Validator(typeof(RequestPoValidator))]
    public class UsersModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string DepartmentName { get; set; }
        public string ManufactoryName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}