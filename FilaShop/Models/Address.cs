//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FilaShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Address()
        {
            this.Orders = new HashSet<Orders>();
        }
    
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public string ReceiverName { get; set; }
        public string Phone { get; set; }
        public Nullable<int> AreaId { get; set; }
        public string DetailAddress { get; set; }
        public bool Isdefault { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual Userinfo Userinfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}