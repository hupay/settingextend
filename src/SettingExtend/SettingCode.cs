namespace SettingExtend
{
    /// <summary>
    ///     代码类型
    /// </summary>
    public class SettingCode : Setting
    {
        public SettingCode(string key, string value) : base(key, value)
        {
            Type = SettingHeadTypeEnum.code;
        }

        public string Result { get; set; }
    }
}