using dotnet_etcd;

namespace SettingExtend.Provider.etcd
{
    /// <summary>
    /// etcd供应者
    /// </summary>
    public class etcdConfig : IConfiguration
    {
        private static string URL = null;
        static etcdConfig()
        {
            URL = Utility.GetConfig()["etcdConfigURL"];
            if (string.IsNullOrWhiteSpace(URL))
                throw new SettingException("配置文件地址未设置。");
        }
        public EtcdClient etcdClient = null;
        public etcdConfig()
        {
            etcdClient = new EtcdClient(URL);
        }

        /// <summary>
        /// 实现方法
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string Get(string path)
        {
            var response = etcdClient.GetVal(path);
            return response;
        }
    }
}
