using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Admin.Models;

namespace Core.Admin.Validator
{
    public class CompanyValidator : AbstractValidator<CompanyModel>
    {
        public CompanyValidator()
        {
            //RuleFor(x => x.CompanyName).NotEmpty().WithMessage("الحقل مطلوب");
        }
    }
}