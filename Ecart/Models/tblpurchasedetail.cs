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
    
    public partial class tblpurchasedetail
    {
        public int purchasedetailsid { get; set; }
        public Nullable<int> purchaseid { get; set; }
        public Nullable<int> productid { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<decimal> amount { get; set; }
    
        public virtual tblproduct tblproduct { get; set; }
        public virtual tblpurchase tblpurchase { get; set; }
    }
}