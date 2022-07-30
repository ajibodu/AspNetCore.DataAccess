using System;

namespace DataAccess.Exceptions
{
    public class EmptyConnectionStringException : Exception
    {
        public EmptyConnectionStringException(string message):base(message)
        { }
        
        public EmptyConnectionStringException(string message, Exception inner):base(message, inner)
        { }
    }
}