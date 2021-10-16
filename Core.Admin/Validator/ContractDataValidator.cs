using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Admin.Models;

namespace Core.Admin.Validator
{
    public class ContractDataValidator : AbstractValidator<ContractDataModel>
    {
        public ContractDataValidator()
        {
            RuleFor(x => x.ContractNumber).NotEmpty().WithMessage("الحقل مطلوب");
            RuleFor(x => x.CompanyId).InclusiveBetween(1, 1000000000).WithMessage("الحقل مطلوب");
            RuleFor(x => x.BankId).InclusiveBetween(1, 1000000000).WithMessage("الحقل مطلوب");
            RuleFor(x => x.SectorId).InclusiveBetween(1, 1000000000).WithMessage("الحقل مطلوب");
            RuleFor(x => x.AdministrationId).InclusiveBetween(1, 1000000000).WithMessage("الحقل مطلوب");
            RuleFor(x => x.ContractStatusId).InclusiveBetween(1, 1000000000).WithMessage("الحقل مطلوب");
            
        }
    }
}