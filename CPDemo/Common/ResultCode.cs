using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CPDemo.Common
{
    public enum ResultCode
    {

        /// <summary>
        ///   操作成功
        /// </summary>
        [Description("操作成功。")]
        Success,
        [Description("Toekn过期或不存在")]
        TokenNull,
        /// <summary>
        ///   输入信息验证失败
        /// </summary>
        [Description("输入信息验证失败。")]
        ValidError,

        /// <summary>
        ///   指定参数的数据不存在
        /// </summary>
        [Description("指定参数的数据不存在。")]
        QueryNull,

        /// <summary>
        ///   操作取消或操作没引发任何变化
        /// </summary>
        [Description("操作没有引发任何变化，提交取消。")]
        NoChanged,

        [Description("邮箱格式错误。")]
        EmailError,
        [Description("邮箱已存在。")]
        EmailExist,
        ///   操作引发错误
        /// </summary>
        [Description("操作引发错误。")]
        Error
       
    }
}