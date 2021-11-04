using System.IO;

namespace SettingExtend.Provider.File
{
    public class FileConfig : IConfiguration
    {
        private static readonly string RootPath;

        static FileConfig()
        {
            RootPath = Utility.GetConfig()["FileConfigPath"];
            if (string.IsNullOrWhiteSpace(RootPath))
                throw new SettingException("配置文件目录未设置。");
            var fileSystemWatcher = new FileSystemWatcher(RootPath);
            fileSystemWatcher.IncludeSubdirectories = true;
            fileSystemWatcher.NotifyFilter = NotifyFilters.Size | NotifyFilters.FileName;
            // TODO 未完成
            fileSystemWatcher.Changed += (obj, arg) => { };
            fileSystemWatcher.Renamed += (obj, arg) => { };
            fileSystemWatcher.Created += (obj, arg) => { };
            fileSystemWatcher.Deleted += (obj, arg) => { };
        }

        /// <summary>
        ///     根据key获取配置节值
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string Get(string path)
        {
            var result = Cache.GetWithCache(path, x =>
            {
                var filepath = Path.Combine(RootPath, x);
                if (System.IO.File.Exists(filepath)) return System.IO.File.ReadAllText(filepath);
                return null;
            });
            if (result != null)
                return result;
            throw new SettingException("配置节不存在！");
        }
    }
}