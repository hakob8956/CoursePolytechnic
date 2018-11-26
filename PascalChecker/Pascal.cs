using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PascalChecker
{
    public static class Pascal
    {
        static string GeneralSolution = @"D:\Programming\CoursePol\CoursePol\wwwroot\pascalFile\GeneralSolution\";
        static string GeneralTests = @"D:\Programming\CoursePol\CoursePol\wwwroot\pascalFile\Tests\";

        public static string GetSolutionContent(int numberSolution, string content)//numberner@ @st xndragrqi,content-@ useri-cod@(submit)
        {
            switch (numberSolution)
            {
                case 1://For Test
                    content = $@"function Solution{numberSolution}: Int64;
                              implementation
                                 function Solution{numberSolution}: Int64;
	                         begin
		                        {content}                      
		                        Solution1:=a;
	                         end;";
                    break;
                case 2://orinak gtnel max
                    content = $@"function Solution{numberSolution}(a,b,c: Int64): Int64;
                              implementation
                                 function Solution{numberSolution}(a,b,c: Int64): Int64;
                             var max:Int64;
	                         begin
		                        {content}
		                        Solution2:=max;
	                         end;";
                    break;
                case 3:
                    content = @"";
                    break;
                default:
                    content = @"";
                    break;
            }
            //TODO Fix Output always Int64
            string output = $@"unit Solutions;
                            {{$mode tp}}
                            {{$H+}}
                            interface
                            uses
                              SysUtils;
                               {content}                    
                            end.";
            return output;
        }

        public static string GetTestContent(int numberSolution)
        {
            string output = String.Empty;
            switch (numberSolution)
            {
                case 1:
                    output = GetFileContent(GeneralTests + $"test{numberSolution}.pas");
                    break;
                case 2:
                    output = GetFileContent(GeneralTests + $"test{numberSolution}.pas");
                    break;
                default:
                    break;
            }
            return output;
        }

        public static bool BuildOutput(int numberSolution, string userPath, string filename)
        {
            //Code Paste in solution.ps in GeneralSolution
            FileStream solution = new FileStream(GeneralSolution + "solution.pas", FileMode.Create, FileAccess.ReadWrite);
            FileStream test = new FileStream(GeneralSolution + "tests.pas", FileMode.Create, FileAccess.ReadWrite);

            string content = GetFileContent(userPath + $"\\{filename}.pas");

            //Get Solution and write to solution.pas
            using (StreamWriter writer = new StreamWriter(solution))
            {
                writer.Write(GetSolutionContent(numberSolution, content));
                writer.Close();
            }
            Thread.Sleep(100);
            //Get Test and write to test.pas
            using (StreamWriter writer = new StreamWriter(test))
            {
                writer.Write(GetTestContent(numberSolution));
                writer.Close();
            }
            
            Thread.Sleep(100);
            //Link solution.pas to test.pas in ProjectEuler
            string commands = $"cd {GeneralSolution} \n fpc ProjectEuler.pas \n start /B ProjectEuler.exe > {userPath}\\output.txt";

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
        public static string GetFileContent(string Path)
        {

            string output;
            try
            {
                using (StreamReader reader = new StreamReader(Path))
                {
                    output = reader.ReadToEnd();
                }
            }

            catch (Exception) { output = "Error"; }
            return output;
        }
    }
}
