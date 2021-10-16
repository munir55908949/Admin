using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Admin.Models;

namespace Core.Admin.Validator
{
    public class GuaranteeStatusValidator : AbstractValidator<GuaranteeStatusModel>
    {
        public GuaranteeStatusValidator()
        {
            //RuleFor(x => x.GuaranteeStatusName).NotEmpty().WithMessage("الحقل مطلوب");
        }
    }
}