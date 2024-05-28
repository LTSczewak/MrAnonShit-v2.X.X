using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnonCrypter.Crypter
{
    internal class Obfuscator
    {
        public static string Powershell(string content, string[] strings)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string str1 = Randomiser.GetString(4);
            strings = strings.OrderBy(x => Randomiser.RNG.Next()).ToArray();
            stringBuilder.Append("$" + str1 + "=");
            for (int index = 0; index < strings.Length; ++index)
            {
                string str2 = Randomiser.GetString(4);
                stringBuilder.Append("'" + Obfuscator.InsertChar(strings[index], str2) + "'.Replace('" + str2 + "', '')");
                if (index != strings.Length - 1)
                    stringBuilder.Append(',');
                content = content.Replace(strings[index], $"(${str1}[{index}])");
            }
            stringBuilder.Append(";");
            stringBuilder.AppendLine();
            stringBuilder.Append(content);
            return stringBuilder.ToString();
        }

        public static string InsertChar(string input, params string[] d)
        {
            List<string> stringList = new List<string>();
            string empty = string.Empty;
            foreach (char ch in input)
            {
                if (empty.Length >= Randomiser.RNG.Next(1, 4))
                {
                    stringList.Add(empty);
                    empty = string.Empty;
                }
                empty += ch.ToString();
            }
            stringList.Add(empty);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string str in stringList)
            {
                if (d.Length > 1)
                    stringBuilder.Append(str + d[Randomiser.RNG.Next(d.Length)]);
                else
                    stringBuilder.Append(str + d[0]);
            }
            return stringBuilder.ToString();
        }

        public static string Obfuscate(string content, string[] strings)
        {
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder();
            string str1 = Randomiser.GetString(4);
            strings = strings.OrderBy(x => Randomiser.RNG.Next()).ToArray();
            stringBuilder.Append("$" + str1 + "=");
            for (int index = 0; index < strings.Length; ++index)
            {
                string str2 = Randomiser.GetString(4);
                stringBuilder.Append("'" + Obfuscator.InsertChar(strings[index], str2) + "'.Replace('" + str2 + "', '')");
                if (index != strings.Length - 1)
                    stringBuilder.Append(',');
                content = content.Replace(strings[index], $"(${str1}[{index}])");
            }
            stringBuilder.Append(";");
            stringBuilder.AppendLine();
            stringBuilder.Append(content);

            // Additional obfuscation step
            stringBuilder.Replace("Obfuscator", Randomiser.GetString(10));
            stringBuilder.Replace("Powershell", Randomiser.GetString(10));
            stringBuilder.Replace("InsertChar", Randomiser.GetString(10));
            stringBuilder.Replace("stringList", Randomiser.GetString(10));
            stringBuilder.Replace("empty", Randomiser.GetString(10));
            stringBuilder.Replace("stringBuilder", Randomiser.GetString(10));
            stringBuilder.Replace("Obfuscate", Randomiser.GetString(10));
            stringBuilder.Replace("strings", Randomiser.GetString(10));
            stringBuilder.Replace("index", Randomiser.GetString(10));
            stringBuilder.Replace("str1", Randomiser.GetString(10));
            stringBuilder.Replace("str2", Randomiser.GetString(10));

            // Advanced obfuscation step
            stringBuilder.Replace("public", Randomiser.GetString(10));
            stringBuilder.Replace("internal", Randomiser.GetString(10));
            stringBuilder.Replace("static", Randomiser.GetString(10));
            stringBuilder.Replace("StringBuilder", Randomiser.GetString(10));
            stringBuilder.Replace("return", Randomiser.GetString(10));
            stringBuilder.Replace("foreach", Randomiser.GetString(10));
            stringBuilder.Replace("char", Randomiser.GetString(10));
            stringBuilder.Replace("string", Randomiser.GetString(10));
            stringBuilder.Replace("List", Randomiser.GetString(10));
            stringBuilder.Replace("Array", Randomiser.GetString(10));
            stringBuilder.Replace("OrderBy", Randomiser.GetString(10));

            return stringBuilder.ToString();
        }
    }
}