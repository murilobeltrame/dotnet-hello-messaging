namespace HelloMessaging.Lib
{
    public interface IBusMessage<T>
    {
        T body {get; set;}
        string lockToken {get; set;}
    }
}