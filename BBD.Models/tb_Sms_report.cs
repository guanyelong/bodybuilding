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
    
    public partial class tb_Sms_report
    {
        public long id { get; set; }
        public string mobile { get; set; }
        public Nullable<System.DateTime> C_time { get; set; }
        public Nullable<System.DateTime> R_time { get; set; }
        public Nullable<int> pipeId { get; set; }
        public string errMsg { get; set; }
        public string M_no { get; set; }
        public Nullable<System.DateTime> S_time { get; set; }
    }
}
