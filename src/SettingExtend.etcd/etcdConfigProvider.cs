using dotnet_etcd;

namespace SettingExtend.etcd
{
    /// <summary>
    /// etcd供应者
    /// </summary>
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
