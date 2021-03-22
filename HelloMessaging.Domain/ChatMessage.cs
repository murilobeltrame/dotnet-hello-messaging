namespace HelloMessaging.Domain
{
    public interface IChatMessage
    {
        string Text { get; set; }
#pragma warning disable IDE1006 // Naming Style for message Header expected by MassTransit
        string __Version { get; set; }
#pragma warning restore IDE1006 // Naming Style for message Header expected by MassTransit
    }
}
