using OrderProcessingApp.Models;

namespace OrderProcessingApp.BusinessRules
{
    public interface IOrderBusinessRule
    {
        bool IsViolated(Order order);
        string Explain();
    }
}
