using System.IO;

namespace SettingExtend.File
{
    public class FileConfigProvider : IConfiguration
    {
        private static string RootPath = null;
        static FileConfigProvider()
        {
            RootPath = Utility.GetConfig()["FileConfigPath"];
            if (string.IsNullOrWhiteSpace(RootPath))
                throw new SettingException("配置文件目录未设置。");
        }

        /// <summary>
        /// 根据key获取配置节值
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string Get(string path)
        {
            var filepath = Path.Combine(RootPath, path);
            if (System.IO.File.Exists(filepath))
            {
                return System.IO.File.ReadAllText(filepath);
            }
            throw new SettingException("配置节不存在！");
        }
    }
}
