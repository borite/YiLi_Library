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
    
    public partial class UserReadHistory
    {
        public int HisID { get; set; }
        public string OpenID { get; set; }
        public Nullable<int> BookListID { get; set; }
        public Nullable<System.DateTime> ReadTime { get; set; }
        public string ReadSection { get; set; }
    
        public virtual BookList BookList { get; set; }
    }
}
