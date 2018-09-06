namespace NetCoreStack.Localization.Test.Hosting.Exceptions
{
    /// <summary>
    /// Implementing Domain Operation Exceptions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOperationalException<T>
    {
        T Type { get; set; }
        string CustomParameter { get; set; }
    }
}
