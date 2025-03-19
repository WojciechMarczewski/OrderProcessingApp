using OrderProcessingApp.BusinessRules;
using OrderProcessingApp.Models.Enums;
using OrderProcessingApp.Tests.Factories;

namespace OrderProcessingApp.Tests
{
    public class OrderBusinessRulesTests
    {
        [Fact]
        public void CashOnDeliveryThresholdRule_ReturnsTrue_WhenThresholdExceeded_AndCashOnDelivery()
        {
            //Arrange
            var order = new TestOrderFactory().CreateOrderWithAmountAndPaymentMethod((decimal)2500.01, PaymentMethod.CashOnDelivery);
            var rule = new CashOnDeliveryThresholdRule();
            //Act
            var result = rule.IsViolated(order);
            //Assert
            Assert.True(result, "The rule should be violated for order amount exceeding the threshold with Cash On Delivery.");
        }
        [Fact]
        public void CashOnDeliveryThresholdRule_ReturnsFalse_WhenThresholdExceeded_AndCard()
        {
            //Arrange
            var order = new TestOrderFactory().CreateOrderWithAmountAndPaymentMethod((decimal)2500.01, PaymentMethod.Card);
            var rule = new CashOnDeliveryThresholdRule();
            //Act
            var result = rule.IsViolated(order);
            //Assert
            Assert.False(result);
        }
        [Fact]
        public void CashOnDeliveryThresholdRule_ReturnsFalse_WhenThresholdExceeded_AndCashOnDelivery()
        {
            //Arrange
            var order = new TestOrderFactory().CreateOrderWithAmountAndPaymentMethod((decimal)2499.99, PaymentMethod.CashOnDelivery);
            var rule = new CashOnDeliveryThresholdRule();
            //Act
            var result = rule.IsViolated(order);
            //Assert
            Assert.False(result);
        }
        [Fact]
        public void OrderCanChangeStatusRule_ReturnsFalse_WhenOrderChangesFromNewToInStock()
        {
            //Arrange
            var order = new TestOrderFactory().CreateOrderWithStatus(OrderStatus.New);
            var rule = new OrderCanChangeStatusRule(OrderStatus.InStock);
            //Act
            var result = rule.IsViolated(order);
            //Assert
            Assert.False(result, "The rule shouldn't be violated when transitioning from New to InStock.");
        }
        [Fact]
        public void OrderCanChangeStatusRule_ReturnFalse_WhenOrderChangesFromInStockToInShipment()
        {
            //Arrange
            var order = new TestOrderFactory().CreateOrderWithStatus(OrderStatus.InStock);
            var rule = new OrderCanChangeStatusRule(OrderStatus.InShipment);
            //Act
            var result = rule.IsViolated(order);
            //Assert
            Assert.False(result, "The rule shouldn't be violated when transitioning from InStock to InShipment.");
        }
        [Theory]
        [InlineData(OrderStatus.InStock)]
        [InlineData(OrderStatus.ReturnedToClient)]
        [InlineData(OrderStatus.Error)]
        [InlineData(OrderStatus.Unknown)]
        [InlineData(OrderStatus.InShipment)]
        [InlineData(OrderStatus.Closed)]
        public void OrderCanChangeStatusRule_ReturnsTrue_WhenOrderStatusChange_From_IsInvalid_To_InStock(OrderStatus initialStatus)
        {
            //Arrange
            var order = new TestOrderFactory().CreateOrderWithStatus(initialStatus);
            var rule = new OrderCanChangeStatusRule(OrderStatus.InStock);
            //Act
            var result = rule.IsViolated(order);
            //Assert
            Assert.True(result);
        }
        [Theory]
        [InlineData(OrderStatus.New)]
        [InlineData(OrderStatus.ReturnedToClient)]
        [InlineData(OrderStatus.Error)]
        [InlineData(OrderStatus.Unknown)]
        [InlineData(OrderStatus.InShipment)]
        [InlineData(OrderStatus.Closed)]
        public void OrderCanChangeStatusRule_ReturnsTrue_WhenOrderStatusChange_From_IsInvalid_To_InShipment(OrderStatus initialStatus)
        {
            //Arrange
            var order = new TestOrderFactory().CreateOrderWithStatus(initialStatus);
            var rule = new OrderCanChangeStatusRule(OrderStatus.InShipment);
            //Act
            var result = rule.IsViolated(order);
            //Assert
            Assert.True(result);
        }
        [Theory]
        [InlineData("x", "x", "x", "")]
        [InlineData("x", "x", " ", "x")]
        [InlineData("x", "", "x", "x")]
        [InlineData("", "x", "x", "x")]
        public void ShippingAddressRequiredRule_IsViolated_WhenAddressIsMissingField(string addressStreet, string addressCity, string addressZipCode, string addressCountry)
        {
            //Arrange
            var order = new TestOrderFactory().CreateOrderWithAddress(addressStreet, addressCity, addressZipCode, addressCountry);
            var rule = new ShippingAddressRequiredRule();
            //Act
            var result = rule.IsViolated(order);
            //Assert
            Assert.True(result);
        }
    }
}
