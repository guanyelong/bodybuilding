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
    
    public partial class tb_User_Buy_Rec
    {
        public int Id { get; set; }
        public Nullable<int> uId { get; set; }
        public Nullable<System.DateTime> RecDate { get; set; }
        public Nullable<int> Buy_Hospid { get; set; }
        public Nullable<int> BuyNum { get; set; }
        public Nullable<decimal> PayMoney { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public Nullable<int> CreatorId { get; set; }
        public string Creator { get; set; }
        public Nullable<System.DateTime> C_time { get; set; }
    }
}
