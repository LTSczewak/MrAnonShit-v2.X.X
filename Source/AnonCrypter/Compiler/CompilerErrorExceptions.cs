using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonCrypter.Crypter
{
    internal class CompilerErrorException : Exception
    {
        public CompilerErrorException()
        {

        }

        public CompilerErrorException(string message) : base(message)
        {

        }

        public CompilerErrorException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}