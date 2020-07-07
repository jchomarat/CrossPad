using System;
using System.Diagnostics;
using System.IO;

namespace CrossPad.Core.Helpers
{
    public class FilesHelper
    {
        /// <summary>
        /// Returns the string content of a given file path
        /// </summary>
        /// <param name="FileName">Path to the file</param>
        /// <returns>TThe content as string. Empty if an exception occurs</returns>
        public static string ReadAll(string FileName)
        {
            try
            {
                return File.ReadAllText(FileName);
            }
            catch(Exception)
            {
                Debug.WriteLine($"Could not open file ${FileName}");
                return string.Empty;
            }
        }

        public static bool WriteAll(string FileName, string FileContent)
        {
            try
            {
                File.WriteAllText(FileName, FileContent, System.Text.Encoding.UTF8);
                return true;
            }
            catch (Exception)
            {
                Debug.WriteLine($"Could not write file ${FileName}");
                return false;
            }
        }
    }
}
