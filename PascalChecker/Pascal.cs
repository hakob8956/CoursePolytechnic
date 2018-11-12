using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PascalChecker
{
    public static class Pascal
    {
        public static bool BuildOutput(string userPath, string filename)
        {
            //TODO with inputs 
            string commands = $"cd {userPath} \n fpc {filename}.pas \n start /B {filename}.exe > output.txt";

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false

                }
            };
            process.Start();
            try
            {
                using (StreamWriter pWriter = process.StandardInput)
                {
                    if (pWriter.BaseStream.CanWrite)
                    {
                        foreach (var line in commands.Split('\n'))
                        {
                            pWriter.WriteLine(line);

                        }

                    }
                }
                


                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static string GetOutput(string userPath)
        {
            string path = userPath + "\\output.txt";
            string output;
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    output = reader.ReadToEnd();
                }
            }
            catch (Exception) { output = "Error"; }
            return output;
        }
    }
}
