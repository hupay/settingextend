using System;
using System.Collections.Generic;
using System.Text;

namespace SettingExtend
{
    public class SettingCode : Setting
    {
        public string Result { get; set; }
        public SettingCode(string key, string value) : base(key, value)
        {
            Type = SettingHeadTypeEnum.code;
        }
    }
}
