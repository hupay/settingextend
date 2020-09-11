using System;
using System.Collections.Generic;
using System.Text;

namespace SettingExtend
{
    public class SettingCode : Setting
    {
        public string Code { get; private set; }
        public SettingCode(string value) : base(value) { }
        public override List<string> ParseTypeCode(string[] array)
        {
            var list = new List<string>();
            for (int i = 1; i < array.Length; i++)
            {
                if (string.IsNullOrEmpty(array[i])) continue;
                if (array[i].StartsWith(Constant.Notes)) continue;
                list.Add(array[i]);
            }
            Code = string.Join("\r\n", list);
            return list;
        }

    }
}
