using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Admin.Models;

namespace Core.Admin.Validator
{
    public class BankValidator : AbstractValidator<BankModel>
    {
        public BankValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("الحقل مطلوب");
        }
    }
}