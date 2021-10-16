﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DatabseEntities : DbContext
    {
        public DatabseEntities()
            : base("name=DatabseEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Administration> Administration { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<ContractData> ContractData { get; set; }
        public virtual DbSet<ContractStatus> ContractStatus { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<DecreaseOrIncrease> DecreaseOrIncrease { get; set; }
        public virtual DbSet<GuaranteeSort> GuaranteeSort { get; set; }
        public virtual DbSet<GuaranteeStatus> GuaranteeStatus { get; set; }
        public virtual DbSet<Pages> Pages { get; set; }
        public virtual DbSet<Sector> Sector { get; set; }
        public virtual DbSet<Users_Pages> Users_Pages { get; set; }
        public virtual DbSet<Warranty> Warranty { get; set; }
        public virtual DbSet<WarrantyHistory> WarrantyHistory { get; set; }
        public virtual DbSet<WarrantyType> WarrantyType { get; set; }
    
        public virtual ObjectResult<Usp_Guaranteesearch_Result> Usp_Guaranteesearch(Nullable<System.DateTime> fromdate, Nullable<System.DateTime> todate)
        {
            var fromdateParameter = fromdate.HasValue ?
                new ObjectParameter("Fromdate", fromdate) :
                new ObjectParameter("Fromdate", typeof(System.DateTime));
    
            var todateParameter = todate.HasValue ?
                new ObjectParameter("Todate", todate) :
                new ObjectParameter("Todate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Usp_Guaranteesearch_Result>("Usp_Guaranteesearch", fromdateParameter, todateParameter);
        }
    }
}
