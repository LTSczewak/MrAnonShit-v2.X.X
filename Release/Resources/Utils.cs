using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Security.Cryptography;

namespace AnonCrypter.Crypter
{
    internal class Utils
    {
        private static Dictionary<string, byte[]> m_Resources;

        public static Dictionary<string, byte[]> Resources
        {
            get
            {
                if (Utils.m_Resources == null)
                {
                    Utils.m_Resources = new Dictionary<string, byte[]>();
                    LoadResources();
                }
                return Utils.m_Resources;
            }
        }

        private static void LoadResources()
        {

        }

        public static byte[] Encrypt(byte[] input, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform encryptor = aes.CreateEncryptor(key, iv))
                    return encryptor.TransformFinalBlock(input, 0, input.Length);
            }
        }

        public static byte[] Compress(byte[] bytes)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                    gzipStream.Write(bytes, 0, bytes.Length);
                return memoryStream.ToArray();
            }
        }

        public static void PumpFile(string input, int fileSizeInKB)
        {
            int bufferSize = 1024;
            int numBytesToWrite = fileSizeInKB * 1024;

            using (FileStream fileStream = new FileStream(input, FileMode.Append, FileAccess.Write))
            {
                byte[] buffer = new byte[bufferSize];
                Random random = new Random();

                while (numBytesToWrite > 0)
                {
                    random.NextBytes(buffer);

                    int bytesToWrite = Math.Min(buffer.Length, numBytesToWrite);
                    fileStream.Write(buffer, 0, bytesToWrite);

                    numBytesToWrite -= bytesToWrite;
                }
            }
        }
    }
}