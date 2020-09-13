using CSScriptLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SettingExtend
{
    public partial class CodeRun
    {
        /// <summary>
        /// 代码执行
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dlllist"></param>
        /// <param name="nslist"></param>
        /// <returns></returns>
        public static string Run(string code, List<string> dlllist = null, List<string> nslist = null)
        {
            string ns = string.Empty;
            if (nslist != null && nslist.Any())
            {
                foreach (var item in nslist)
                {
                    ns += item + Constant.LineRreak;
                }
            }
            string strExpre = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
$ns
public class CodeRun
{
    public List<string> Result { get; private set; }
    public CodeRun()
    {
        Result = new List<string>();
    }

    public void Exec()
    {
$code
    }

    public void print(string value)
    {
        Result.Add(value);
    }
}
";
            strExpre = strExpre.Replace("$ns", ns).Replace("$code", code);
            dynamic script = CSScript.Evaluator.LoadCode(strExpre);
            script.Exec();
            return script.Result == null ? string.Empty : string.Join(Constant.LineRreak, script.Result);
        }
    }
}
