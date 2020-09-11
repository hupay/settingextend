using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettingExtend
{
    public class SettingArray :Setting
    {
        public string[] Array { get; private set; }
        public string this[int index]
        {
            get { return Array[index]; }
        }
        public SettingArray(string value) : base(value) { }

        public override string[] ParseTypeArray(string[] array)
        {
            var arr = array
                .Skip(1)
                .Take(array.Length - 1)
                .Where(x => !x.StartsWith(Constant.Notes) && !string.IsNullOrEmpty(x))
                .ToArray();
            Array = arr;
            return arr;
        }
    }
}
