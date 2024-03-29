//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models.EFrame
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.DetailCarts = new HashSet<DetailCart>();
        }
    
        public int prId { get; set; }
        public string prName { get; set; }
        public int cateId { get; set; }
        public int prodId { get; set; }
        public string description { get; set; }
        public string images { get; set; }
        public int amount { get; set; }
        public int price { get; set; }
        public Nullable<System.DateTime> dateUpdate { get; set; }
        public Nullable<int> status { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailCart> DetailCarts { get; set; }
        public virtual Producer Producer { get; set; }
    }
}
