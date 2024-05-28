using System;
using System.Collections.Generic;
using System.Linq;

namespace AnonCrypter.Crypter
{
    internal class Randomiser
    {
        private static Random _rng;
        private const string _chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly HashSet<string> _usedStrings = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public static Random RNG
        {
            get
            {
                if (Randomiser._rng == null)
                    Randomiser._rng = new Random();
                return Randomiser._rng;
            }
        }

        public static string GetString(int length)
        {
            string str;
            string stringUnsafe;
            do
            {
                stringUnsafe = Randomiser.GetStringUnsafe(length);
            } while (!_usedStrings.Add(stringUnsafe));

            return stringUnsafe;
        }

        public static string GetStringUnsafe(int length) => new string(Enumerable.Repeat<string>("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", length).Select<string, char>((Func<string, char>)(s => s[RNG.Next(s.Length)])).ToArray<char>());

        public static string[] GetStrings(int length, int amount)
        {
            List<string> stringList = new List<string>();
            for (int index = 0; index < amount; ++index)
                stringList.Add(Randomiser.GetString(length));
            return stringList.ToArray();
        }

        public static void ClearStrings() => _usedStrings.Clear();
    }
}