using CSScriptLib;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Threading.Tasks;
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
        [Fact]
        public void read_text_config_with_note_emptyline()
        {
            var setting = Utility.Parse("Text2.txt");
            Assert.Equal("����һ�������ļ���", setting.Value);
        }

        [Fact]
        public void read_noexists_config()
        {
            Assert.ThrowsAsync<SettingException>(() => Task.FromResult(Utility.Parse("notexists.txt")));
        }

        [Fact]
        public void read_array_config()
        {
            var setting = Utility.Parse<SettingArray>("array.txt");
            Assert.Equal("33", setting[0]);
            Assert.Equal("����", setting[1]);
            Assert.Equal("abc", setting[2]);
        }

        [Fact]
        public void read_dictionary_config()
        {
            var setting = Utility.Parse<SettingDictionary>("dictionary.txt");
            Assert.Equal("�û�", setting["user"]);
            Assert.Equal("����Ա", setting["admin"]);
            Assert.Equal("�༭", setting["editor"]);
        }

        [Fact]
        public void read_simplecode_config()
        {
            var setting = Utility.Parse<SettingCode>("simplecode.txt");
            Assert.Contains("���1", setting.Result);
            Assert.Contains(DateTime.Now.ToString("yyyy-MM-dd"), setting.Result);
        }

        [Fact]
        public void read_errorcode_config()
        {
            Assert.ThrowsAsync<CompilerException>(() => Task.FromResult(Utility.Parse<SettingCode>("errorcode.txt")));
        }


        [Fact]
        public void read_complexText1_config()
        {
            var setting = Utility.Parse("complexText1.txt");
            Assert.Contains("33", setting.Value);
        }

        [Fact]
        public void read_complexText2_config()
        {
            var setting = Utility.Parse("complexText2.txt");
            Assert.Contains("33", setting.Value);
            Assert.Contains("Ŀǰӵ�е�Ȩ��Ϊ���û�", setting.Value);
        }

        [Fact]
        public void read_complexText3_config()
        {
            var setting = Utility.Parse("complexText3.txt");
            Assert.Contains("33", setting.Value);
            Assert.Contains("Ŀǰӵ�е�Ȩ��Ϊ���û�", setting.Value);
            Assert.Contains("���1", setting.Value);
            Assert.Contains(DateTime.Now.ToString("yyyy-MM-dd"), setting.Value);
        }
    }
}