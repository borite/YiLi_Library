//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace YiLi_Library.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookSection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BookSection()
        {
            this.SubBookSection = new HashSet<SubBookSection>();
            this.UserReadHistory = new HashSet<UserReadHistory>();
        }
    
        public int SectionlD { get; set; }
        public int BookID { get; set; }
        public Nullable<int> SectionOrder { get; set; }
        public string SectionContent { get; set; }
        public System.DateTime AddTime { get; set; }
        public string Remark { get; set; }
    
        public virtual BookList BookList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubBookSection> SubBookSection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserReadHistory> UserReadHistory { get; set; }
    }
}
