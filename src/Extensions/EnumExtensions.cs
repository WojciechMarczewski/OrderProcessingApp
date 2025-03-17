using OrderProcessingApp.Models.Enums;

namespace OrderProcessingApp.Extensions
{
    public static class EnumExtensions
    {
        public static string ToPLString(this PaymentMethod paymentMethod)
        {
            switch (paymentMethod)
            {
                case PaymentMethod.Card:
                    return "Karta";
                case PaymentMethod.CashOnDelivery:
                    return "Gotówka przy odbiorze";
                default:
                    return string.Empty;
            }
        }
        public static string ToPLString(this ClientType clientType)
        {
            switch (clientType)
            {
                case ClientType.Person:
                    return "Osoba fizyczna";
                case ClientType.Company:
                    return "Firma";
                default:
                    return string.Empty;
            }
        }
        public static string ToPLString(this OrderStatus orderStatus)
        {
            switch (orderStatus)
            {
                case OrderStatus.New:
                    return "Nowe";
                case OrderStatus.InStock:
                    return "W magazynie";
                case OrderStatus.InShipment:
                    return "W wysyłce";
                case OrderStatus.ReturnedToClient:
                    return "Zwrócono do klienta";
                case OrderStatus.Error:
                    return "Błąd";
                case OrderStatus.Closed:
                    return "Zamknięte";
                default:
                    return string.Empty;
            }
        }
    }
}
