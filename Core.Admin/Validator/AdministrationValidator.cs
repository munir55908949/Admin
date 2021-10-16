using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Admin.Models;

namespace Core.Admin.Validator
{
    public class AdministrationValidator : AbstractValidator<AdministrationModel>
    {
        public AdministrationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("الحقل مطلوب");
            RuleFor(x => x.SectorId).NotEmpty().WithMessage("الحقل مطلوب");
            
        }
    }
}