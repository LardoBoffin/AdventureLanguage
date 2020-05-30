using System;
using System.Collections.Generic;
using System.Text;
using AdventureLanguage.Data;
using System.IO;
using AdventureLanguage.Helpers;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AdventureLanguage
{
    public static class Extensions
    {

        public static string Right(this string s, int start, int length)
        {
            //s=s.Substring(start, length).Trim();
            return s.Substring(start, length).Trim();
        }

        public static char MidChar(this string a, int start, int length)
        {
            char[] chars = a.ToCharArray();
            return chars[start - 1];
        }

        public static string Mid(this string s, int start, int length)
        {
            string temp = s.Substring(start - 1, length);
            return temp;
        }

        public static string Left(this string s, int leftLength)
        {
            if (s.Length < leftLength)
            {
                leftLength = s.Length;
            }
            string temp = s.Substring(0, leftLength);
            return temp;
        }


    }
}

public static class ShellHelper
{
    public static string Bash(this string cmd)
    {
        var escapedArgs = cmd.Replace("\"", "\\\"");

        var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{escapedArgs}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        process.Start();
        string result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return result;
    }
}

namespace System.IO
{
    public static class ExtendedMethod
    {
        public static void Rename(this FileInfo fileInfo, string newName)
        {
            try
            {
                fileInfo.MoveTo(fileInfo.Directory.FullName + "\\" + newName);
            }
            catch
            {

            }

        }
    }
}