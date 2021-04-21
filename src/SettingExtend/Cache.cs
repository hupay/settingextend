using System.Collections.Concurrent;

namespace SettingExtend
{
    /// <summary>
    /// 内置缓存
    /// </summary>
    public static class Cache
    {
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
                dictionary.TryUpdate(key, value, key);
            }
        }
    }
}
