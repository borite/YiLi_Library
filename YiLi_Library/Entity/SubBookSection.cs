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
    
    public partial class SubBookSection
    {
        public int SubSectionID { get; set; }
        public int SectionID { get; set; }
        public int SubSectionOrder { get; set; }
        public string SubSectionContent { get; set; }
        public System.DateTime AddTime { get; set; }
        public string Article { get; set; }
    
        public virtual BookSection BookSection { get; set; }
    }
}
