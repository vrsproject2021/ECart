//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecart.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblcategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblcategory()
        {
            this.tblproducts = new HashSet<tblproduct>();
        }
    
        public int categoryid { get; set; }
        public string category { get; set; }
        public Nullable<int> userid { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public string status { get; set; }
    
        public virtual tbllogin tbllogin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblproduct> tblproducts { get; set; }
    }
}