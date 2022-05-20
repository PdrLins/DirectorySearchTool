using System;

namespace DirectorySearchTool.Application.Common.Exceptions
{
    public class UnknowUrlException : Exception
    {
        public UnknowUrlException()
            : base()
        {

        }
        public UnknowUrlException(string msg)
          : base(msg)
        {

        }

        public UnknowUrlException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
