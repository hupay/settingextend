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

        public void Parse()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return;
            var array = _value.Split(Constant.LineRreak);
            array = Filter(array);
            if (array.Length == 0)
                return;
            var first = array.First();
            if (first.StartsWith(Constant.Type))
            {
                ParseType(array);
            }
            else if (first.StartsWith(Constant.Import)||first.StartsWith(Constant.Variable))
            {

            }
            else
            {
                ParseText(array);
            }
        }

        /// <summary>
        /// 类型格式化
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public void ParseType(string[] array)
        {
            var first = array.First();
            var arr = first.Split(Constant.Space);
            if (arr.Length != 2)
                throw new SettingException("[头部]-[类型]的语法错误！");
            if (TypeDic.ContainsKey(arr[1]))
                TypeDic[arr[1]](array);
            else
            {
                TypeDic.Values.Last()(array);
            }
        }

        Dictionary<string, Action<string[]>> TypeDic => new Dictionary<string, Action<string[]>>
        {
            {
                Constant.Array, x=>
                {
                    ParseTypeArray(x);
                    Type = SettingHeadEnum.type;
                }
            },
            {
                Constant.Dictionary, x=>
                {
                    ParseTypeDictionary(x);
                    Type = SettingHeadEnum.type;
                }
            },{
                Constant.Code, x=>
                {
                    ParseTypeCode(x);
                }
            },{
                "last", x=>
                {
                    throw new SettingException("[头部]-[类型]出现不支持的类型：" + x);
                }
            },
        };

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
            List<string> list = new List<string>(),
                dlllist = new List<string>(),
                nslist = new List<string>(),
                codelist = new List<string>();
            List<Setting> settings = new List<Setting>();
            bool code = false;
            foreach (var item in array)
            {
                if (item.StartsWith(Constant.Import))
                {
                    var coll = Constant.ImportReg.Match(item);
                    if (coll.Success)
                    {
                        string type = coll.Groups[1].Value,
                            name=coll.Groups[2].Value;
                        if (string.IsNullOrWhiteSpace(name))
                            throw new SettingException("语法错误。当前为：" + item);
                        switch (type)
                        {
                            case Constant.Path:
                                var value = Get(name);
                                settings.Add(new Setting(value));
                                break;
                            case Constant.Dll:
                                dlllist.Add(name);
                                break;
                            case Constant.NameSpace:
                                nslist.Add(name);
                                break;
                            default:
                                throw new SettingException("无效语法。[import]参数后面应为path、dll、namespace中的一种。");
                        }
                    }
                }
                else if (item.StartsWith(Constant.Variable))
                {
                    // TODO 变量类型的处理
                }
                else if (item.StartsWith(Constant.CodeStart))
                {
                    code = true;
                }
                else if (code)
                {
                    codelist.Add(item);
                }
                else if (item.StartsWith(Constant.CodeEnd))
                {
                    code = false;
                    var result = CodeRun.Run(string.Join(Constant.LineRreak, codelist), dlllist, nslist);
                    list.Add(result);
                    codelist.Clear();
                }
                else
                {
                    list.Add(item);
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
