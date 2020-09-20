//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace SettingExtend.Test
//{
//    public class etcdTest : TextTest
//    {
//        public etcdTest()
//        {
//            // 如果有etcd服务器，可以取消注释。并将settingextend.provider配置调整为etcd供应者
//            // init
//            var filepath = Utility.GetConfig()["FileConfigPath"];
//            var files = Directory.GetFiles(filepath, "*.*", SearchOption.AllDirectories);
//            var basepath = Path.GetFullPath(filepath);
//            if (files == null || !files.Any())
//            {
//                throw new Exception("未配置根目录。");
//            }
//            var provider = new etcdConfigProvider();
//            foreach (var item in files)
//            {
//                var p = Path.GetFullPath(item);
//                var key = p.Replace(basepath, string.Empty);
//                key = key.TrimStart('\\').Replace("\\", "/");
//                var response = provider.etcdClient.PutAsync(key, File.ReadAllText(p)).Result;
//                Assert.NotNull(response.Header);
//            }
//        }
//    }
//}
