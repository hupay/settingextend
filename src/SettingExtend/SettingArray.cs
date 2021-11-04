namespace SettingExtend
{
    /// <summary>
    ///     数组类型
    /// </summary>
    public class SettingArray : Setting
    {
        public SettingArray(string key, string value) : base(key, value)
        {
            Type = SettingHeadTypeEnum.array;
        }

        public string[] Array { get; set; }

        /// <summary>
        ///     变量名
        /// </summary>
        public string Name { get; set; }

        public string this[int index] => Array[index];
    }
}