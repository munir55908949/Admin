using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Admin.Models;

namespace Core.Admin.Validator
{
    public class GuaranteeSortValidator : AbstractValidator<GuaranteeSortModel>
    {
        public GuaranteeSortValidator()
        {
            //RuleFor(x => x.GuaranteeSortName).NotEmpty().WithMessage("الحقل مطلوب");
        }
    }
}