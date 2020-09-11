using System;
using System.Collections.Generic;
using System.Text;

namespace SettingExtend
{
    /// <summary>
    /// 常量
    /// </summary>
    public class Constant
    {
        public const string Space = " ";
        public const string Notes = "#";
        public const string Equal = "=";
        #region 语法头

        /// <summary>
        /// 类型
        /// </summary>
        public const string Type = "type" + Space;
        /// <summary>
        /// 引用
        /// </summary>
        public const string Import = "import" + Space;
        /// <summary>
        /// 变量
        /// </summary>
        public const string Variable = "var" + Space;

        #endregion

        #region 值

        #endregion

        #region 类型常量
        //array|dictionary|code
        ///// <summary>
        ///// 字符串
        ///// </summary>
        //public const string String = "string";
        /// <summary>
        /// 数组
        /// </summary>
        public const string Array = "array";
        /// <summary>
        /// 字典
        /// </summary>
        public const string Dictionary = "dictionary";
        /// <summary>
        /// 代码
        /// </summary>
        public const string Code = "code";
        #endregion

        #region 引用常量
        /// <summary>
        /// 配置路径
        /// </summary>
        public const string Path = "path";
        /// <summary>
        /// dll类库。当前程序根目录需要有此dll
        /// </summary>
        public const string Dll = "dll";
        /// <summary>
        /// 命名空间
        /// </summary>
        public const string NameSpace = "namespace";

        #endregion

        #region 代码常量
        public const string CodeStart = "[code]";
        public const string CodeEnd = "[/code]";
        #endregion

    }
}
