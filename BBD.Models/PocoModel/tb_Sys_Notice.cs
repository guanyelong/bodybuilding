
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
	public partial class tb_Sys_Notice
	{

		public tb_Sys_Notice MiniItem(bool isMini = true){
			return new tb_Sys_Notice(){

				Id = this.Id,

				Title = this.Title,

				Contents = this.Contents,

				Hot = this.Hot,

				Seq = this.Seq,

				ClickNum = this.ClickNum,

				IsDel = this.IsDel,

				Creator = this.Creator,

				C_Time = this.C_Time,

				ShowImgUrl = this.ShowImgUrl,

				FilePath = this.FilePath,

				FileName = this.FileName,

				IsPush = this.IsPush,

			};
		}
	}

}
