﻿using System.Collections.Generic;

namespace SettingExtend
{
    /// <summary>
    ///     字典类型
    /// </summary>
    public class SettingDictionary : Setting
    {
        public SettingDictionary(string key, string value) : base(key, value)
        {
            Type = SettingHeadTypeEnum.dictionary;
        }

        public Dictionary<string, string> Dictionary { get; set; }

        /// <summary>
        ///     变量名
        /// </summary>
        public string Name { get; set; }

        public string this[string key] => Dictionary[key];
    }
}