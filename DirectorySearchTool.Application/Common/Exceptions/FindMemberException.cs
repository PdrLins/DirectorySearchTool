using System;

namespace DirectorySearchTool.Application.Common.Exceptions
{
    public class FindMemberException : Exception
    {
        public FindMemberException()
            : base()
        {

        }
        public FindMemberException(string msg)
          : base(msg)
        {

        }

        public FindMemberException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
