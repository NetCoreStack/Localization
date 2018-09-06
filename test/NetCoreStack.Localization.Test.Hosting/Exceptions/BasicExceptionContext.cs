namespace NetCoreStack.Localization.Test.Hosting.Exceptions
{
    public class BasicExceptionContext
    {
        public string LogGuid { get; set; }
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public string ExceptionDetail { get; set; }
        public bool IsOperationalException { get; set; }
    }
}
