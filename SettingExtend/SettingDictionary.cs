using System;
using System.Collections.Generic;
using System.Text;

namespace SettingExtend
{
    public class SettingDictionary : Setting
    {
        public Dictionary<string, string> Dictionary { get; private set; }
        public SettingDictionary(string value) : base(value) { }
        public string this[string key]
        {
            get { return Dictionary[key]; }
        }

        public override Dictionary<string, string> ParseTypeDictionary(string[] array)
        {
            var dic = new Dictionary<string, string>();
            for (int i = 1; i < array.Length; i++)
            {
                var arr = array[i].Split(Constant.Equal);
                if (arr.Length != 2)
                    throw new SettingException("[头部]-[类型]-[字典]的语法错误！示例：name=china");
                if (dic.Keys.Count == 0)
                {
                    dic.Add(arr[0], arr[1]);
                    continue;
                }
                if (dic.ContainsKey(arr[0]))
                    throw new SettingException("[头部]-[类型]-[字典]的键重复！");
                dic.Add(arr[0], arr[1]);
            }
            Dictionary = dic;
            return dic;
        }
    }
}
