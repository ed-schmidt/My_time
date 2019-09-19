using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;

namespace Logging
{
    public static class TextLog
    {
        public static string currentError = string.Empty;

        public static void Logit(string str, Boolean sysInfo = false)
        {
            try
            {
                using (TextWriter w = new StreamWriter("MyNotes.txt", true))
                {
                    w.WriteLine(string.Format("{0},{1}", DateTime.Now.ToString(), str));
				    if (sysInfo)
					    w.WriteLine(systemStatus());
                    w.Flush();
                }
            }
            catch (Exception)
            {
                currentError += string.Format("{0}: {1}\n", DateTime.Now.ToString(), str);
            }
        }
        public static void LogErr(string str)
        {
            if (str.Contains("Not a valid password") == false)
            {
                currentError += string.Format("{0}: {1}\n", DateTime.Now.ToString(), str);
                Logit(str);
            }
        }

			private static string systemStatus()
			{
                return string.Format("Memory used: {0}", System.GC.GetTotalMemory(false).ToString());
                //string Stats = "[------ Status ------------]\r\n";
                //Stats += "Memory used: "  + System.GC.GetTotalMemory(false).ToString() + "\r\n";
                //Stats += "User Name:   " + SystemInformation.UserName.ToString() + "\r\n";
                //Stats +=       "[------ END Status --------]" ;
                //return Stats;
			}
    }
}
