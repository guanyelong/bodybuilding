
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
	public partial class tb_Hei_Wei
	{

		public tb_Hei_Wei MiniItem(bool isMini = true){
			return new tb_Hei_Wei(){

				Id = this.Id,

				Female = this.Female,

				Height = this.Height,

				St_weight = this.St_weight,

				Pt_weight = this.Pt_weight,

				WC = this.WC,

				Hipline = this.Hipline,

				Thigh_Cir = this.Thigh_Cir,

				C_time = this.C_time,

			};
		}
	}

}
