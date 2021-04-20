using System.Collections.Concurrent;

namespace SettingExtend
{
    public static class Cache
    {
        private static ConcurrentDictionary<string, string> dictionary = new ConcurrentDictionary<string, string>();

        public static string Get(string key)
        {
            string value;
            if (dictionary.TryGetValue(key, out value))
                return value;
            return null;
        }

        public static void Add(string key,string value)
        {
            if(!dictionary.TryAdd(key, value))
            {
                dictionary.TryUpdate(key, value, key);
            }
        }
    }
}
