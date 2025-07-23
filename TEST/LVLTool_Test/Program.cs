using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Text;

namespace LVLTool_Test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestForm());
        }

        public static List<String> GetTestNames()
        {
            List<String> retVal = new List<string>();

            Tests tests = new Tests();
            // Get the type of the class
            Type type = typeof(Tests);
            // Get all methods in the class
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            // Iterate over methods and invoke those ending with "_Test"
            foreach (MethodInfo method in methods)
            {
                if (method.Name.EndsWith("_Test"))
                {
                    retVal.Add(method.Name);
                }
            }
            return retVal;
        }

        public static String RunTest(string testName)
        {
            Tests tests = new Tests();
            // Get the type of the class
            Type type = typeof(Tests);
            // Get all methods in the class
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            StringBuilder result = new StringBuilder();
            object res = null;
            // Iterate over methods and invoke those ending with "_Test"
            foreach (MethodInfo method in methods)
            {
                if (method.Name == testName)
                {
                    try
                    {
                        // Invoke the method on the instance (null for parameters since we assume no parameters)
                        res = method.Invoke(tests, null);
                        if (res != null)
                        {
                            result.Append(String.Format("{0}: {1}\n", method.Name, res.ToString()));
                        }

                        DeleteMungeLogs();
                        if (Directory.Exists("Temp"))
                        {
                            Directory.Delete("Temp", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Append(String.Format("{0}: Error : {1}", method.Name, ex.Message));
                    }
                }
            }
            return result.ToString();
        }

        public static String RunTests()
        {
            Tests tests = new Tests();
            // Get the type of the class
            Type type = typeof(Tests);
            // Get all methods in the class
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            StringBuilder result = new StringBuilder();
            object res = null;
            // Iterate over methods and invoke those ending with "_Test"
            foreach (MethodInfo method in methods)
            {
                if (method.Name.EndsWith("_Test"))
                {
                    try
                    {
                        // Invoke the method on the instance (null for parameters since we assume no parameters)
                        res = method.Invoke(tests, null);
                        if (res != null)
                        {
                            result.Append(String.Format("{0}: {1}\n",method.Name, res.ToString()));
                        }
                        // Clean up tests
                        DeleteMungeLogs();
                        if (Directory.Exists("Temp"))
                        {
                            Directory.Delete("Temp", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Append(String.Format("{0}: Error : {1}", method.Name, ex.Message));
                    }
                }
            }
            return result.ToString();
        }


        public static string RunCommand(string programName, string args, bool includeStdErr)
        {
            Console.WriteLine("info: Running command: " + programName + " " + args);
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = programName,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            FileInfo progToRun = new FileInfo(programName);
            if (!progToRun.Exists)
            {
                Console.WriteLine("Could not find '{0}'", programName);
            }

            if (progToRun.Exists) // Make PATH varialbe awesome
            {
                string newPath = progToRun.DirectoryName + ";" + processStartInfo.EnvironmentVariables["path"];
                processStartInfo.EnvironmentVariables["path"] = newPath;
            }
            var process = Process.Start(processStartInfo);
            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();
            process.WaitForExit();
            if (includeStdErr)
                output = output + "\r\n" + err;
            return output;
        }

        public static void DeleteMungeLogs()
        {
            DirectoryInfo di = new DirectoryInfo(".");
            foreach (FileInfo fi in di.GetFiles("*Munge.log"))
            {
                fi.Delete();
            }
        }

    }
}
