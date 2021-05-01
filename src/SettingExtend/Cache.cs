using System;
using System.Collections.Concurrent;

namespace SettingExtend
{
    /// <summary>
    /// 内置缓存
    /// </summary>
    public static class Cache
    {
        /// <summary>
        /// 跨线程调用
        /// </summary>
        private static ConcurrentDictionary<string, string> dictionary = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 得到缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            string value;
            if (dictionary.TryGetValue(key, out value))
                return value;
            return null;
        }

        /// <summary>
        /// 把新值加入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Add(string key,string value)
        {
            if(!dictionary.TryAdd(key, value))
            {
                if (dictionary.TryUpdate(key, value, key))
                    return;
            }
            throw new SettingException("加入缓存失败！");
        }

        /// <summary>
        /// 获取缓存中值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static string GetWithCache(string key, Func<string, string> func)
        {
            var result = Get(key);
            if (result != null)
                return result;
            result = func(key);
            if (result != null)
            {
                Add(key, result);
                return result;
            }
            throw new SettingException("配置节不存在！");
        }
    }
}
