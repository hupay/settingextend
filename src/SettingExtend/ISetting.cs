using System;
using System.Collections.Generic;
using System.Text;

namespace SettingExtend
{
    interface ISetting
    {
        /// <summary>
        /// 根据key获取配置节值
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string Get(string path);
    }
}
