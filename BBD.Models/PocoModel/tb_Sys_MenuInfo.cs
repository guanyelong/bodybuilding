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
	public partial class tb_Sys_MenuInfo
	{
		public tb_Sys_MenuInfo MiniItem(bool isMini = true){
			return new tb_Sys_MenuInfo(){
				mId = this.mId,
				mNum = this.mNum,
				mPId = this.mPId,
				mText = this.mText,
				mUrl = this.mUrl,
				mIcon = this.mIcon,
				mAddtime = this.mAddtime,
				mState = this.mState,
				mChecked = this.mChecked,
				mSeq = this.mSeq,
				mPerNum = this.mPerNum,
				mOrderindex = this.mOrderindex,
			};
		}
	}
}
