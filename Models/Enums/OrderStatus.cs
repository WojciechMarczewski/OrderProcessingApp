namespace OrderProcessingApp.Models.Enums
{
    public enum OrderStatus
    {
        New,
        InStock,
        InShipment,
        ReturnedToClient,
        Error,
        Closed
    }
}
