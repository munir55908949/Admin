using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ContractDataViewModel
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? BankId { get; set; }
        public string BankName { get; set; }
        public int? SectorId { get; set; }
        public string SectorName { get; set; }
        public int? AdministrationId { get; set; }
        public string AdministrationName { get; set; }
        public int? ContractStatusId { get; set; }
        public string ContractStatusName { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedAtName { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string Path { get; set; }
        public string Note { get; set; }
        public bool IsDeleted { get; set; }


    }
}
