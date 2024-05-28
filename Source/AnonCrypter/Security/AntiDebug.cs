using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AnonCrypter.Security
{
    internal class AntiDebug
    {
        public static bool secBool = false;
        private static readonly SecureString passphrase = GetPassphrase();

        private static SecureString GetPassphrase()
        {
            // Implement a secure mechanism to retrieve the passphrase dynamically
            // Example: Retrieve passphrase from a secure server
            return new SecureString();
        }

        public static void routine()
        {
            //CheckAntiDebug();
            IsDebugEnabled();
        }

        private static string ObfuscateString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private static void CheckAntiDebug()
        {
            while (true)
            {
                if (Debugger.IsAttached || IsDebuggingToolsPresent())
                {
                    Environment.FailFast("Debugger detected. Exiting.");
                }
            }
        }

        private static bool IsDebuggingToolsPresent()
        {
            return (Debugger.IsAttached);
        }

        private static string ConvertToUnsecureString(SecureString secureString)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(ptr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(ptr);
            }
        }

        public static void IsDebugEnabled()
        {
            using (WebClient client = new WebClient())
            {
                bool sechash = false;

                if (client.DownloadString("https://rentry.co/batchcrypter/raw") == "187")
                {
                    sechash = true;
                    secBool = true;
                }
                else
                {
                    sechash = false;
                    secBool = false;
                    Environment.FailFast("187");
                }
            }
        }

        internal static byte[] GetEncryptedResource(string resourceName)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var aesAlg = Aes.Create())
                {
                    // Derive key from passphrase using PBKDF2
                    using (var deriveBytes = new Rfc2898DeriveBytes(
                        ConvertToUnsecureString(passphrase),
                        Encoding.UTF8.GetBytes("SaltForDerivation"), 10000))
                    {
                        aesAlg.Key = deriveBytes.GetBytes(aesAlg.KeySize / 8);
                    }

                    aesAlg.IV = new byte[16]; // Initialization Vector (IV), ensure to use a secure method to generate this

                    using (var cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        // Load resource dynamically at runtime
                        var assembly = Assembly.GetExecutingAssembly();
                        var resourceStream = assembly.GetManifestResourceStream(resourceName);

                        if (resourceStream != null)
                        {
                            resourceStream.CopyTo(cryptoStream);
                            resourceStream.Close();
                        }
                    }
                }

                return memoryStream.ToArray();
            }
        }
    }
}