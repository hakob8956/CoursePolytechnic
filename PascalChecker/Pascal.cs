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
        static string GeneralTests = @"D:\Programming\CoursePol\CoursePol\wwwroot\pascalFile\GeneralTests\";
        private readonly static object lockForStream = new object();
        public static string GetSolutionContent(int numberSolution, string content)//numberner@ @st xndragrqi,content-@ useri-cod@(submit)
        {
        string output =
$@"unit Solutions;
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
                    output = GetFileContent(GeneralTests + $"test2.pas");
                    break;
                case 2:
                    output = GetFileContent(GeneralTests + $"test2.pas");
                    break;
                default:
                    break;
            }
            return output;
        }
        public static void DeleteTempFile(string path)
        {
            FileInfo file = new FileInfo(path + "\\output.txt");
            if (file.Exists)
            {
                file.Delete();
            }
            file = new FileInfo(GeneralSolution + "ProjectEuler.exe");
            if (file.Exists)
            {
                file.Delete();
            }
        }
        public static bool BuildOutput(int numberSolution, string userPath, string filename)
        {
            DeleteTempFile(userPath);//Delete output.txt ProjectEuler.exe 

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

            //Get Test and write to test.pas
            using (StreamWriter writer = new StreamWriter(test))
            {
                writer.Write(GetTestContent(numberSolution));
                writer.Close();
            }
            //Link solution.pas to test.pas in ProjectEuler
            string[] LinkComands = { $"cd {GeneralSolution}", "fpc ProjectEuler.pas" };
            string[] OutputComands = { $"cd {GeneralSolution}", $"start /B ProjectEuler.exe > {userPath}\\output.txt" };

            //I divided into two parts 1.Link-->Create ProjectEuler.exe or Not 2.Check ProjectEuler.exe Exist and create output.txt 

            File.WriteAllLines("coms.bat", LinkComands);

            //#1 Build Project -->Create ProjectEuler.exe
            Process LinkProcess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "coms.bat";
            LinkProcess.StartInfo = startInfo;
            LinkProcess.Start();

            LinkProcess.WaitForExit();
            LinkProcess.Close();

            //#2 Read ProjectEuler.exe  into output.txt 
            FileInfo file = new FileInfo($"{GeneralSolution}ProjectEuler.exe");
            if (!file.Exists)
            {
                return false;
            }
            File.WriteAllLines("coms.bat", OutputComands);

            Process OutputProcess = new Process();
            OutputProcess.StartInfo = startInfo;
            OutputProcess.Start();
            OutputProcess.WaitForExit();
            OutputProcess.Close();

            //#3 Read output File and delete temp file
            bool output = ReturnOutput(userPath + "\\output.txt");
            return output;


        }

        public static bool ReturnOutput(string pathOutput)
        {
            Thread.Sleep(1000);
            FileInfo output = new FileInfo(pathOutput);
            if (!output.Exists) { return false; }
            FileStream file = new FileStream(pathOutput, FileMode.Open, FileAccess.Read, FileShare.Read);

            bool answer = false;
            using (StreamReader reader = new StreamReader(file))
            {
                answer = reader.ReadToEnd().Contains("OK: 1 tests");
            }

            return answer;
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
