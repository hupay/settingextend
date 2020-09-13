using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SettingExtend
{
    /// <summary>
    /// 配置节点实体
    /// </summary>
    public class Setting : ISetting
    {
        static Regex ImportReg = new Regex("^import (path|dll|namespace) (.+)$");
        ///// <summary>
        ///// 配置节点的路径
        ///// </summary>
        //public string Key { get; private set; }
        /// <summary>
        /// 配置节点的值
        /// </summary>
        public string Value { get; private set; }
        /// <summary>
        /// 配置类型，如果是数组、字典、代码，有值
        /// </summary>
        public SettingHeadEnum? Type { get; private set; }
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
            var array = _value.Split(Constant.LineRreak);
            array = Filter(array);
            if (array.Length == 0)
                return default;
            var first = array.First();
            if (first.StartsWith(Constant.Type))
            {
                return ParseType(array);
            }
            else if (first.StartsWith(Constant.Import)||first.StartsWith(Constant.Variable))
            {

            }
            else
            {
                return ParseText(array);
            }
        }

        /// <summary>
        /// 类型格式化
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
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
                    return default;
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

        public virtual string[] ParseTypeCode(string[] array)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 统一过滤
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private string[] Filter(string[] array)
        {
            var list = new List<string>();
            foreach (var item in array)
            {
                if (string.IsNullOrEmpty(item)) continue;
                if (item.StartsWith(Constant.Notes)) continue;
                list.Add(item);
            }
            return list.ToArray();
        }

        private void ParseHearders(string[] array)
        {
            foreach (var item in array)
            {
                if (item.StartsWith(Constant.Import))
                {
                        var coll = ImportReg.Match(item);
                    if (coll.Success)
                    {
                        string type = coll.Groups[1].Value,
                            name=coll.Groups[2].Value;
                        switch (type)
                        {
                            case Constant.Path:
                                var value = Get(name);

                                break;
                            case Constant.Dll:
                                break;
                            case Constant.NameSpace:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private string ParseText(string[] array)
        {
            var arr = array
                .Where(x => !x.StartsWith(Constant.Notes) && !string.IsNullOrEmpty(x));
            return string.Join(Constant.LineRreak, arr);
        }

        public string Get(string path)
        {
            throw new NotImplementedException();
        }
    }
}
