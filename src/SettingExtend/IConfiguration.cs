namespace SettingExtend
{
    public interface IConfiguration
    {
        /// <summary>
        ///     根据key获取配置节值
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string Get(string path);
    }
}