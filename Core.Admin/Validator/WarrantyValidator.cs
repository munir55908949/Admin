using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Admin.Models;

namespace Core.Admin.Validator
{
    public class WarrantyValidator : AbstractValidator<WarrantyModel>
    {
        public WarrantyValidator()
        {
            RuleFor(x => x.WarrantyNumber).NotEmpty().WithMessage("الحقل مطلوب");
            RuleFor(x => x.WarrantyValue).NotEmpty().WithMessage("الحقل مطلوب");
            RuleFor(x => x.DecreaseOrIncreaseValue).NotEmpty().WithMessage("الحقل مطلوب");
            RuleFor(x => x.CurrencyId).InclusiveBetween(1, 1000000000).WithMessage("الحقل مطلوب");
            RuleFor(x => x.WarrantyTypeId).InclusiveBetween(1, 1000000000).WithMessage("الحقل مطلوب");
            RuleFor(x => x.DecreaseOrIncreaseId).InclusiveBetween(1, 1000000000).WithMessage("الحقل مطلوب");
            RuleFor(x => x.GuaranteeSortId).InclusiveBetween(1, 1000000000).WithMessage("الحقل مطلوب");
            RuleFor(x => x.GuaranteeStatusId).InclusiveBetween(1, 1000000000).WithMessage("الحقل مطلوب");
            RuleFor(x => x.GuaranteeIssueDate).NotEmpty().WithMessage("الحقل مطلوب");
            RuleFor(x => x.GuaranteeStartDate).NotEmpty().WithMessage("الحقل مطلوب");
            RuleFor(x => x.GuaranteeEndDate).NotEmpty().WithMessage("الحقل مطلوب");

        }
    }
}