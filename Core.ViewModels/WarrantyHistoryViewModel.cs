using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class WarrantyHistoryViewModel
    {
        public int Id { get; set; }
        public string WarrantyNumber { get; set; }
        public string ContractNumber { get; set; }
        public decimal? WarrantyValue { get; set; }
        public decimal? DecreaseOrIncreaseValue { get; set; }
        public int? ContractDataId { get; set; }
        public string ContractDataName { get; set; }
        public int? GuaranteeSortId { get; set; }
        public string GuaranteeSortName { get; set; }
        public int? GuaranteeStatusId { get; set; }
        public string GuaranteeStatusName { get; set; }
        public int? WarrantyTypeId { get; set; }
        public string WarrantyTypeName { get; set; }
        public int? DecreaseOrIncreaseId { get; set; }
        public string DecreaseOrIncreaseName { get; set; }
        public int? CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public DateTime? GuaranteeIssueDate { get; set; }
        public DateTime? GuaranteeStartDate { get; set; }
        public DateTime? GuaranteeEndDate { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedAtName { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string Path { get; set; }
        public string NoteWarranty { get; set; }
        public bool IsDeleted { get; set; }


    }
}
