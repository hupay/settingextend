using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
        public const string LineRreak = "\r\n";
        public const string Last = "last";
        #region 语法头

        /// <summary>
        /// 类型
        /// </summary>
        public static string Type = SettingHeadEnum.type.ToString() + Space;
        /// <summary>
        /// 引用
        /// </summary>
        public static string Import = SettingHeadEnum.import.ToString() + Space;
        /// <summary>
        /// 变量
        /// </summary>
        public static string Variable = SettingHeadEnum.var.ToString() + Space;

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
        public static string Array = SettingHeadTypeEnum.array.ToString();
        /// <summary>
        /// 字典
        /// </summary>
        public static string Dictionary = SettingHeadTypeEnum.dictionary.ToString();
        /// <summary>
        /// 代码
        /// </summary>
        public static string Code = SettingHeadTypeEnum.code.ToString();
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


        #region 常用正则

        public static Regex ImportReg = new Regex($"^{Import} ({Path}|{Dll}|{NameSpace}) (.+)$");
        #endregion
    }
}
