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
    
    public partial class BookList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BookList()
        {
            this.BookSection = new HashSet<BookSection>();
        }
    
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Author { get; set; }
        public string CoverIMG { get; set; }
        public System.DateTime AddTime { get; set; }
        public Nullable<int> BookType { get; set; }
        public byte State { get; set; }
        public Nullable<System.DateTime> TopTime { get; set; }
        public Nullable<bool> Recommend { get; set; }
        public Nullable<int> ClickNum { get; set; }
        public string Remark { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookSection> BookSection { get; set; }
        public virtual BookType BookType1 { get; set; }
    }
}
