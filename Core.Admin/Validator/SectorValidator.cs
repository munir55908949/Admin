using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Admin.Models;

namespace Core.Admin.Validator
{
    public class SectorValidator : AbstractValidator<SectorModel>
    {
        public SectorValidator()
        {
            //RuleFor(x => x.SectorName).NotEmpty().WithMessage("الحقل مطلوب");
        }
    }
}