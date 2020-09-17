using System;
using Xunit;

namespace SettingExtend.Test
{
    public class TextTest
    {
        [Fact]
        public void read_text_config()
        {
            var setting = Utility.Parse("Text1.txt");
            Assert.NotNull(setting);
            Assert.NotEmpty(setting.Value);
        }
    }
}
