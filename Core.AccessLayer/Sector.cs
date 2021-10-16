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
    using System.Collections.Generic;
    
    public partial class Sector
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sector()
        {
            this.Administration = new HashSet<Administration>();
            this.ContractData = new HashSet<ContractData>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public string Note { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Administration> Administration { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContractData> ContractData { get; set; }
    }
}
