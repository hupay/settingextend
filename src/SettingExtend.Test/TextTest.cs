using CSScriptLib;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SettingExtend.Test
{
    public class TextTest
    {
        /// <summary>
        /// 读取文件1
        /// </summary>
        [Fact]
        public void read_text_config()
        {
            var setting = Utility.Parse("Text1.txt");
            Assert.NotNull(setting);
            Assert.NotEmpty(setting.Value);
        }

        /// <summary>
        /// 读取文件2
        /// </summary>
        [Fact]
        public void read_text_config_with_note_emptyline()
        {
            var setting = Utility.Parse("Text2.txt");
            Assert.Equal("这是一个测试文件！", setting.Value);
        }

        /// <summary>
        /// 读取不存在节点
        /// </summary>
        [Fact]
        public void read_noexists_config()
        {
            Assert.ThrowsAsync<SettingException>(() => Task.FromResult(Utility.Parse("notexists.txt")));
        }

        /// <summary>
        /// 读取数组
        /// </summary>
        [Fact]
        public void read_array_config()
        {
            var setting = Utility.Parse<SettingArray>("array.txt");
            Assert.Equal("33", setting[0]);
            Assert.Equal("年龄", setting[1]);
            Assert.Equal("abc", setting[2]);
        }

        [Fact]
        public void read_dictionary_repeat_config()
        {
            Assert.ThrowsAsync<SettingException>(() => Task.FromResult(Utility.Parse("dictionary2.txt")));
        }

        /// <summary>
        /// 读取字典配置
        /// </summary>
        [Fact]
        public void read_dictionary_config()
        {
            var setting = Utility.Parse<SettingDictionary>("dictionary.txt");
            Assert.Equal("用户", setting["user"]);
            Assert.Equal("管理员", setting["admin"]);
            Assert.Equal("编辑", setting["editor"]);
        }

        /// <summary>
        /// 读取含代码配置
        /// </summary>
        [Fact]
        public void read_simplecode_config()
        {
            var setting = Utility.Parse<SettingCode>("simplecode.txt");
            Assert.Contains("输出1", setting.Result);
            Assert.Contains(DateTime.Now.ToString("yyyy-MM-dd"), setting.Result);
        }

        /// <summary>
        /// 读取代码出错
        /// </summary>
        [Fact]
        public void read_errorcode_config()
        {
            Assert.ThrowsAsync<CompilerException>(() => Task.FromResult(Utility.Parse<SettingCode>("errorcode.txt")));
        }

        /// <summary>
        /// 读取文本1
        /// </summary>
        [Fact]
        public void read_complexText1_config()
        {
            var setting = Utility.Parse("complexText1.txt");
            Assert.Contains("33", setting.Value);
        }

        /// <summary>
        /// 读取文本2
        /// </summary>
        [Fact]
        public void read_complexText2_config()
        {
            var setting = Utility.Parse("complexText2.txt");
            Assert.Contains("33", setting.Value);
            Assert.Contains("目前拥有的权限为：用户", setting.Value);
        }

        /// <summary>
        /// 读取文本3
        /// </summary>
        [Fact]
        public void read_complexText3_config()
        {
            var setting = Utility.Parse("complexText3.txt");
            Assert.Contains("33", setting.Value);
            Assert.Contains("目前拥有的权限为：用户", setting.Value);
            Assert.Contains("输出1", setting.Value);
            Assert.Contains(DateTime.Now.ToString("yyyy-MM-dd"), setting.Value);
        }

        /// <summary>
        /// 读取文本4、5
        /// </summary>
        /// <param name="key"></param>
        [Theory]
        [InlineData("complexText4.txt")]
        [InlineData("complexText5.txt")]
        public void read_complexText4_config(string key)
        {
            var setting = Utility.Parse(key);
            Assert.Contains("33", setting.Value);
            Assert.Contains("目前拥有的权限为：用户", setting.Value);
            Assert.Contains("输出1", setting.Value);
            Assert.Contains(DateTime.Now.ToString("yyyy-MM-dd"), setting.Value);
        }
    }
}