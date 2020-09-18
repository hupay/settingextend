using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SettingExtend.Test
{
    public class FileConfigProvider:IConfiguration
    {
        private static string RootPath = null;
        static FileConfigProvider()
        {
            RootPath = Utility.GetConfig()["FileConfigPath"];
            if (string.IsNullOrWhiteSpace(RootPath))
                throw new SettingException("配置文件目录未设置。");
        }

        public string Get(string path)
        {
            var filepath = Path.Combine(RootPath, path);
            if (File.Exists(filepath))
            {
                return File.ReadAllText(filepath);
            }
            throw new SettingException("配置节不存在！");
        }
    }
}
