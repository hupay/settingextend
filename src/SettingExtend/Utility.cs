using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SettingExtend
{
    public class Utility
    {
        public static IConfiguration Configuration { get; private set; }

        static Utility()
        {
            if (Configuration == null)
            {
                // TODO 改为从配置中获取
                var obj = Activator.CreateInstance(Type.GetType("etcd"));
                Configuration = obj as IConfiguration;
                if (Configuration == null)
                    throw new SettingException("配置");
            }
        }
        
        public static Setting Parse(string key)
        {
            return Parse<Setting>(key);
        }

        public static T Parse<T>(string key) where T : Setting
        {
            if (string.IsNullOrWhiteSpace(key))
                return default;
            var value = Configuration.Get(key);
            if (string.IsNullOrWhiteSpace(value))
                return default;
            var array = value.Split(Constant.LineRreak);
            array = Filter(array);
            if (array.Length == 0)
                return default;
            var first = array.First();
            if (first.StartsWith(Constant.Type))
            {
                return ParseType(key,value,array) as T;
            }
            else if (first.StartsWith(Constant.Import) || first.StartsWith(Constant.Variable))
            {
                return ParseTextCode(key, array) as T;
            }
            else
            {
                return ParseText(key,array) as T;
            }
        }


        /// <summary>
        /// 类型格式化
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Setting ParseType(string key,string value, string[] array)
        {
            var first = array.First();
            var arr = first.Split(Constant.Space);
            if (arr.Length != 2)
                throw new SettingException("[头部]-[类型]的语法错误！");
            if (arr[1] == Constant.Array)
            {
                return ParseTypeArray(key, value, array);
            }
            else if (arr[1] == Constant.Dictionary)
            {
                return ParseTypeDictionary(key, value, array);
            }
            else if (arr[1] == Constant.Code)
            {
                return ParseTypeCode(key, value, array);
            }
            else
            {
                throw new SettingException("[头部]-[类型]出现不支持的类型：" + x);
            }
        }

        public static SettingArray ParseTypeArray(string key, string value, string[] array)
        {
            var arr = array
                .ToArray();
            return new SettingArray(key, value) { Array = arr };
        }

        public static SettingDictionary ParseTypeDictionary(string key, string value, string[] array)
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
            return new SettingDictionary(key, value) { Dictionary = dic };
        }

        public static SettingCode ParseTypeCode(string key, string value, string[] array)
        {
            var result = CodeRun.Run(string.Join(Constant.LineRreak, array));
            return new SettingCode(key, value) { Result = result };
        }

        /// <summary>
        /// 统一过滤
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static string[] Filter(string[] array)
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

        private static Setting ParseTextCode(string key,string[] array)
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
                            name = coll.Groups[2].Value;
                        if (string.IsNullOrWhiteSpace(name))
                            throw new SettingException("语法错误。当前为：" + item);
                        switch (type)
                        {
                            case Constant.Path:
                                var value = Configuration.Get(name);
                                var model = Parse(value);
                                settings.Add(model);
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
                    var str = string.Join(Constant.LineRreak, codelist);
                    var result = CodeRun.Run(str, dlllist, nslist);
                    list.Add(result);
                    codelist.Clear();
                }
                else
                {
                    list.Add(item);
                }
            }
            var val = string.Join(Constant.LineRreak, list);
            return new Setting(key, val);
        }

        private static Setting ParseText(string key, string[] array)
        {
            var arr = array
                .Where(x => !x.StartsWith(Constant.Notes) && !string.IsNullOrEmpty(x));
            var result = string.Join(Constant.LineRreak, arr);
            return new Setting(key, result);
        }
    }
}