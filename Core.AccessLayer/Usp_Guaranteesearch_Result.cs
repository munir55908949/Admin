//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.AccessLayer
{
    using System;
    
    public partial class Usp_Guaranteesearch_Result
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public string GuaranteeNumber { get; set; }
        public Nullable<decimal> GuaranteeValue { get; set; }
        public Nullable<System.DateTime> GuaranteeEndDate { get; set; }
        public Nullable<int> GuaranteeStatusId { get; set; }
        public Nullable<int> BankId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string Note { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string Path { get; set; }
        public Nullable<int> SectorId { get; set; }
        public Nullable<System.DateTime> GuaranteeIssueDate { get; set; }
        public Nullable<int> AdministrationId { get; set; }
        public Nullable<int> GuaranteeSortId { get; set; }
        public Nullable<System.DateTime> GuaranteeStartDate { get; set; }
    }
}