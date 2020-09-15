using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettingExtend
{
    public class SettingArray :Setting
    {
        public string[] Array { get; set; }
        public string this[int index]
        {
            get { return Array[index]; }
        }
        public SettingArray(string key, string value) : base(key, value)
        {
            Type = SettingHeadTypeEnum.array;
        }
    }
}
