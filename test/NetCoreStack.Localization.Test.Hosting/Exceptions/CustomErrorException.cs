namespace NetCoreStack.Localization.Test.Hosting.Exceptions
{
    public class CustomErrorException : OperationalException
    {
        public CustomErrorException()
        {
            Type = OperationalExceptionType.CustomException;
        }
    }
}
