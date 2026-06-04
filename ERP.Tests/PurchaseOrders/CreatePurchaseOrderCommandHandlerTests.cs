using ERP.Application.DTOs;
using ERP.Application.Interfaces;
using ERP.Application.PurchaseOrders.Commands.CreatePurchaseOrder;
using ERP.Domain.Entities;
using Moq;
using Xunit;

namespace ERP.Tests.PurchaseOrders
{
    public class CreatePurchaseOrderCommandHandlerTests
    {
        [Fact]
        public async Task Should_Create_Purchase_Order()
        {
            // Arrange

            var repository =
                new Mock<IPurchaseOrderRepository>();

            repository
                .Setup(x =>
                    x.CreateAsync(
                        It.IsAny<PurchaseOrder>()))
                .Returns(Task.CompletedTask);

            var handler =
                new CreatePurchaseOrderHandler(
                    repository.Object);

            var command = new CreatePurchaseOrderCommand(
    "Dell",
    "IT",
    new List<LineItemDto>
    {
       new LineItemDto
                    {
                        ProductName = "Laptop",
                        Quantity = 1,
                        UnitPrice = 1000
                    }
    }
);

            // Act

            var result =
                await handler.Handle(
                    command,
                    CancellationToken.None);

            // Assert

            Assert.NotNull(result);

            repository.Verify(
                x => x.CreateAsync(
                    It.IsAny<PurchaseOrder>()),
                Times.Once);
        }
    }
}