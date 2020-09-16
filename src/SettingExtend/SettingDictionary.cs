using System;
using System.Collections.Generic;
using System.Text;

namespace SettingExtend
{
    public class SettingDictionary : Setting
    {
        public Dictionary<string, string> Dictionary { get; set; }
        /// <summary>
        /// 变量名
        /// </summary>
        public string Name { get; set; }
        public string this[string key]
        {
            get { return Dictionary[key]; }
        }
        public SettingDictionary(string key, string value) : base(key, value)
        {
            Type = SettingHeadTypeEnum.dictionary;
        }
    }
}
