//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BBD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_Sys_LoginLog
    {
        public int id { get; set; }
        public string loginName { get; set; }
        public string loginCity { get; set; }
        public Nullable<System.DateTime> loginTime { get; set; }
        public string LoginIp { get; set; }
    }
}