using System;

namespace NetCoreStack.Localization.Test.Hosting.Exceptions
{
    public class OperationalException : Exception, IOperationalException<OperationalExceptionType>
    {
        public OperationalExceptionType Type { get; set; }
        public string CustomParameter { get; set; }
    }
}
