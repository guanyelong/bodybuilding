
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
	public partial class tb_Consume_Log
	{

		public tb_Consume_Log MiniItem(bool isMini = true){
			return new tb_Consume_Log(){

				Id = this.Id,

				uId = this.uId,

				rec_date = this.rec_date,

				num = this.num,

				flag = this.flag,

				creatorid = this.creatorid,

				creator = this.creator,

				c_time = this.c_time,

				finish_time = this.finish_time,

			};
		}
	}

}
