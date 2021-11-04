using System;
using dotnet_etcd;

namespace SettingExtend.Provider.etcd
{
    /// <summary>
    ///     etcd供应者
    /// </summary>
    public class etcdConfig : IConfiguration
    {
        private static readonly string URL;
        public EtcdClient etcdClient;

        static etcdConfig()
        {
            URL = Utility.GetConfig()["etcdConfigURL"];
            if (string.IsNullOrWhiteSpace(URL))
                throw new SettingException("配置文件地址未设置。");
        }

        public etcdConfig()
        {
            etcdClient = new EtcdClient(URL);
        }

        /// <summary>
        ///     实现方法
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string Get(string path)
        {
            var result = Cache.GetWithCache(path, x => { return etcdClient.GetVal(x); });
            if (result != null)
                return result;
            throw new SettingException("配置节不存在！");
        }
    }
}