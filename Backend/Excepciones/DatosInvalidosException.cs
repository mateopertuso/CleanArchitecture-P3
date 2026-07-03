using System;
using System.Collections.Generic;
using System.Text;

namespace Excepciones
{
    public class DatosInvalidosException : Exception
    {
        public DatosInvalidosException() { }

        public DatosInvalidosException(string message) : base(message) { }

        public DatosInvalidosException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
