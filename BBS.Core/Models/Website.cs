//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BBS.Core.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Website
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Website()
        {
            this.WebsiteItems = new HashSet<WebsiteItem>();
        }
    
        public int WebsiteId { get; set; }
        public int CustomerId { get; set; }
        public string DomainName { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WebsiteItem> WebsiteItems { get; set; }
    }
}