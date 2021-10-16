using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Admin.Models;

namespace Core.Admin.Validator
{
    public class PagesValidator : AbstractValidator<PagesModel>
    {
        public PagesValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("الحقل مطلوب");
            RuleFor(x => x.Url).NotEmpty().WithMessage("الحقل مطلوب");
            RuleFor(x => x.Order).NotEmpty().WithMessage("الحقل مطلوب");
            RuleFor(x => x.Icon).NotEmpty().WithMessage("الحقل مطلوب");
        }
    }
}