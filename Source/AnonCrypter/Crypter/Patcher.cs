using dnlib.DotNet;
using dnlib.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace AnonCrypter.Crypter
{
    internal class Patcher
    {
        public static void AddResources(ref byte[] input, IEnumerable<EmbeddedResource> resources)
        {
            using (ModuleDefMD moduleDefMd = ModuleDefMD.Load(input))
            {
                foreach (EmbeddedResource resource in resources)
                    moduleDefMd.Resources.Add(resource);
                using (MemoryStream dest = new MemoryStream())
                {
                    moduleDefMd.Write(dest);
                    input = dest.ToArray();
                }
            }
        }

        public static void ChangeName(ref byte[] input, string name)
        {
            using (ModuleDefMD moduleDefMd = ModuleDefMD.Load(input))
            {
                moduleDefMd.Name = (UTF8String)name;
                moduleDefMd.Assembly.Name = (UTF8String)name;
                using (MemoryStream dest = new MemoryStream())
                {
                    moduleDefMd.Write(dest);
                    input = dest.ToArray();
                }
            }
        }

        public static bool IsMTAThread(byte[] input)
        {
            using (ModuleDefMD moduleDefMd = ModuleDefMD.Load(input))
            {
                foreach (CustomAttribute customAttribute in moduleDefMd.EntryPoint.CustomAttributes)
                {
                    if (customAttribute.TypeFullName == "System.MTAThreadAttribute")
                        return true;
                }
            }
            return false;
        }

        private static MethodDef GetSystemMethod(Type itype, string name, int idx = 0)
        {
            IEnumerable<TypeDef> types = ModuleDefMD.Load(itype.Module.FullyQualifiedName).GetTypes();
            List<MethodDef> methodDefList = new List<MethodDef>();
            foreach (TypeDef typeDef in types)
            {
                if (typeDef.Name == itype.Name)
                {
                    foreach (MethodDef method in typeDef.Methods)
                    {
                        if (method.Name == name)
                            methodDefList.Add(method);
                    }
                }
            }
            return methodDefList.Count > 0 ? methodDefList[idx] : null;
        }
    }
}