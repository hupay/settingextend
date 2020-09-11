using System;
using System.Collections.Generic;
using System.Linq;

namespace SettingExtend
{
    /// <summary>
    /// 配置节点实体
    /// </summary>
    public class Setting : ISetting
    {
        ///// <summary>
        ///// 配置节点的路径
        ///// </summary>
        //public string Key { get; private set; }
        /// <summary>
        /// 配置节点的值
        /// </summary>
        public string Value { get; private set; }
        private string _value { get; set; }

        public Setting(string value)
        {
            //this.Key = key;
            _value = value;
            Parse();
        }

        public string Parse()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return default;
            var array = _value.Split("\r\n");
            var first = array.First();
            if (first.StartsWith(Constant.Type))
            {
                return ParseType(array);
            }
            else if (first.StartsWith(Constant.Import))
            {

            }
            else if (first.StartsWith(Constant.Variable))
            {

            }
            else
            {
                return ParseText(array);
            }
        }

        public string ParseType(string[] array)
        {
            var first = array.First();
            var arr = first.Split(Constant.Space);
            if (arr.Length != 2)
                throw new SettingException("[头部]-[类型]的语法错误！");
            switch (arr[1])
            {
                case Constant.Array:
                    ParseTypeArray(array);
                    return default;
                case Constant.Dictionary:
                    ParseTypeDictionary(array);
                    return default;
                case Constant.Code:
                    ParseTypeCode(array);
                    break;
                default:
                    throw new SettingException("[头部]-[类型]出现不支持的类型：" + arr[1]);
            }
        }

        public virtual string[] ParseTypeArray(string[] array)
        {
            throw new NotImplementedException();
        }

        public virtual Dictionary<string,string> ParseTypeDictionary(string[] array)
        {
            throw new NotImplementedException();
        }

        public virtual List<string> ParseTypeCode(string[] array)
        {
            throw new NotImplementedException();
        }

        private string ParseText(string[] array)
        {
            var arr = array
                .Where(x => !x.StartsWith(Constant.Notes) && !string.IsNullOrEmpty(x));
            return string.Join("\r\n", arr);
        }

        public string Get(string path)
        {
            throw new NotImplementedException();
        }
    }
}
