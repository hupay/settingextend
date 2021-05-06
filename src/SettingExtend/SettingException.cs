using System;

namespace SettingExtend
{
    /// <summary>
    /// 系统异常
    /// </summary>
    public class SettingException : Exception
    {
        public SettingException() { }
        public SettingException(string message) : base(message) { }
        public SettingException(string message, Exception inner) : base(message, inner) { }
    }
}
