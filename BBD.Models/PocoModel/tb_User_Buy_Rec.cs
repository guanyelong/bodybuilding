
//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
 
namespace BBD.Models
{
	[Serializable] 
	public partial class tb_User_Buy_Rec
	{

		public tb_User_Buy_Rec MiniItem(bool isMini = true){
			return new tb_User_Buy_Rec(){

				Id = this.Id,

				uId = this.uId,

				RecDate = this.RecDate,

				Buy_Hospid = this.Buy_Hospid,

				BuyNum = this.BuyNum,

				PayMoney = this.PayMoney,

				ProductId = this.ProductId,

				ProductPrice = this.ProductPrice,

				CreatorId = this.CreatorId,

				Creator = this.Creator,

				C_time = this.C_time,

			};
		}
	}

}
