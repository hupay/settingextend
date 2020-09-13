using System;
using System.Collections.Generic;
using System.Text;

namespace SettingExtend
{
    public class SettingCode : Setting
    {
        public string Code { get; private set; }
        public SettingCode(string value) : base(value) { }
        public override string[] ParseTypeCode(string[] array)
        {
            Code = string.Join(Constant.LineRreak, array);
            return array;
        }

    }
}
