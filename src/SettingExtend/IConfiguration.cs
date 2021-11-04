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

        /// <summary>
        ///     内容变动监听
        ///     公开此方法，用于第三方配置中心针对变动配置推送时自动修改配置值
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        void Change(string path, string value);
    }
}