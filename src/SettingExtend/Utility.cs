using Microsoft.Extensions.Configuration;
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
                var config = GetConfig();
                var type = config["settingextend.provider"];
                if (string.IsNullOrWhiteSpace(type))
                    throw new SettingException("未配置配置供应者");
                // TODO 改为从配置中获取
                var obj = Activator.CreateInstance(Type.GetType(type));
                Configuration = obj as IConfiguration;
                if (Configuration == null)
                    throw new SettingException("配置供应者生成失败");
            }
        }

        /// <summary>
        /// 获得配置文件
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot GetConfig()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
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
            array = array.Skip(1).ToArray();
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
                throw new SettingException("[头部]-[类型]出现不支持的类型：" + arr[1]);
            }
        }

        /// <summary>
        /// 数组类型
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static SettingArray ParseTypeArray(string key, string value, string[] array)
        {
            var arr = array
                .ToArray();
            return new SettingArray(key, value) { Array = arr };
        }

        /// <summary>
        /// 字典类型
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static SettingDictionary ParseTypeDictionary(string key, string value, string[] array)
        {
            var dic = new Dictionary<string, string>();
            for (int i = 0; i < array.Length; i++)
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

        /// <summary>
        /// 文本、代码混合类型
        /// </summary>
        /// <param name="key"></param>
        /// <param name="array"></param>
        /// <returns></returns>
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
                    var match = Constant.ImportReg.Match(item);
                    if (match.Success)
                    {
                        string type = match.Groups[1].Value,
                            name = match.Groups[2].Value,
                            var = match.Groups.Count == 4 ? match.Groups[3].Value : null;
                        if (string.IsNullOrWhiteSpace(name))
                            throw new SettingException("语法错误。当前为：" + item);
                        switch (type)
                        {
                            case Constant.Path:
                                var model = Parse(name);
                                if (model.GetType() == typeof(SettingArray))
                                {
                                    var b = model as SettingArray;
                                    b.Name = var;
                                }
                                if (model.GetType() == typeof(SettingDictionary))
                                {
                                    var b = model as SettingDictionary;
                                    b.Name = var;
                                }
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
                //else if (item.StartsWith(Constant.Variable))
                //{
                //    // TODO 变量类型的处理
                //}
                else if (item.StartsWith(Constant.CodeStart))
                {
                    code = true;
                }
                else if (item.StartsWith(Constant.CodeEnd))
                {
                    code = false;
                    var str = string.Join(Constant.LineRreak, codelist);
                    var result = CodeRun.Run(str, dlllist, nslist);
                    list.Add(result);
                    codelist.Clear();
                }
                else if (code)
                {
                    codelist.Add(item);
                }
                else
                {
                    var temp = Replace(settings, item);
                    list.Add(temp);
                }
            }
            var val = string.Join(Constant.LineRreak, list);
            return new Setting(key, val);
        }

        /// <summary>
        /// 文本类型
        /// </summary>
        /// <param name="key"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        private static Setting ParseText(string key, string[] array)
        {
            var arr = array
                .Where(x => !x.StartsWith(Constant.Notes) && !string.IsNullOrEmpty(x));
            var result = string.Join(Constant.LineRreak, arr);
            return new Setting(key, result);
        }

        /// <summary>
        /// 替换字符串中约定的数据类型。
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Replace(List<Setting> settings,string str)
        {
            if (settings == null || !settings.Any()) return str;
            foreach (var item in settings)
            {
                if (item.GetType() == typeof(SettingArray))
                {
                    var obj = item as SettingArray;
                    for (int i = 0; i < obj.Array.Length; i++)
                    {
                        str = str.Replace($"${obj.Name}[{i}]", obj[i]);
                    }
                }
                if (item.GetType() == typeof(SettingDictionary))
                {
                    var obj = item as SettingDictionary;
                    foreach (var i in obj.Dictionary.Keys)
                    {
                        str = str.Replace($"${obj.Name}[\"{i}\"]", obj[i]);
                    }
                }
            }
            return str;
        }
    }
}