//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RLRMWF.Tests
{
    using System;
    using System.Collections.Generic;
    
    public partial class qualityControl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public qualityControl()
        {
            this.products = new HashSet<product>();
            this.rawMaterials = new HashSet<rawMaterial>();
        }
    
        public string sampleSubmitNumber { get; set; }
        public Nullable<decimal> inspectionLotNumber { get; set; }
        public Nullable<int> vendorBatchId { get; set; }
        public bool rejected { get; set; }
        public Nullable<System.DateTime> rejectedDate { get; set; }
        public Nullable<System.DateTime> approvalDate { get; set; }
        public Nullable<System.DateTime> experiationDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
        public virtual vendorBatchInformation vendorBatchInformation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rawMaterial> rawMaterials { get; set; }
    }
}
