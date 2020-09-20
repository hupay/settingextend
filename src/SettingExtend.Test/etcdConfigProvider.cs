using dotnet_etcd;
using System;
using System.Collections.Generic;
using System.Text;

namespace SettingExtend.Test
{
    public class etcdConfigProvider : IConfiguration
    {
        private static string URL = null;
        static etcdConfigProvider()
        {
            URL = Utility.GetConfig()["etcdConfigURL"];
            if (string.IsNullOrWhiteSpace(URL))
                throw new SettingException("配置文件地址未设置。");
        }
        public EtcdClient etcdClient = null;
        public etcdConfigProvider()
        {
            etcdClient = new EtcdClient(URL);
        }
        public string Get(string path)
        {
            var response = etcdClient.GetVal(path);
            return response;
        }
    }
}
