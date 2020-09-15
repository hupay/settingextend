using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SettingExtend
{
    /// <summary>
    /// 配置节点实体
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// 配置节点的路径
        /// </summary>
        public string Key { get; protected set; }
        /// <summary>
        /// 配置节点的值
        /// </summary>
        public string Value { get; protected set; }
        public SettingHeadTypeEnum Type { get; set; }
        public Setting(string key,string value)
        {
            Key = key;
            Value = value;
        }
    }
}
