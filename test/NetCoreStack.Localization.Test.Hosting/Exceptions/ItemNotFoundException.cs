namespace NetCoreStack.Localization.Test.Hosting.Exceptions
{
    public class ItemNotFoundException : OperationalException
    {
        public ItemNotFoundException()
        {
            Type = OperationalExceptionType.ItemNotFound;
        }
    }
}
