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
    
    public partial class GoodsType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoodsType()
        {
            this.Goods = new HashSet<Goods>();
            this.GoodsType1 = new HashSet<GoodsType>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> PId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Goods> Goods { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsType> GoodsType1 { get; set; }
        public virtual GoodsType GoodsType2 { get; set; }
    }
}