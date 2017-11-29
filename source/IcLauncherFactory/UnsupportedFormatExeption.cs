using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IcLauncherFactory
{
    /// <summary>
    /// Исключение при неверном формате файла
    /// </summary>
    class UnsupportedFormatExeption:ApplicationException
    {
        public UnsupportedFormatExeption() { }

        public UnsupportedFormatExeption(string message) : base(message) { }

        public UnsupportedFormatExeption(string message, Exception inner) : base(message, inner) { }

        protected UnsupportedFormatExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
