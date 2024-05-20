using Moq;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using P3AddNewFunctionalityDotNetCore.Models;
using Xunit;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace P3AddNewFunctionalityDotNetCore.Tests.UnitTests
{
    public class OrderServiceTests
    {

        [Fact]
        public async Task GetOrder_ReturnOrderByThisId()
        {
            //Arrange

            var mockOrderRepository = new Mock<IOrderRepository>();

            mockOrderRepository.Setup(repo => repo.GetOrder(1)).ReturnsAsync(new Order { Id = 1, Name = "Person1" });

            var orderService = new OrderService(null, mockOrderRepository.Object, null);
            // Act

            var result = await orderService.GetOrder(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Person1", result.Name);
        }

        [Fact]
        public async Task GetOrders_ReturnListOrders()
        {
            //Arrange

            var mockOrderRepository = new Mock<IOrderRepository>();

            mockOrderRepository.Setup(repo => repo.GetOrders()).ReturnsAsync(new List<Order> {
                new Order { Id = 1, Name = "Person1" },
                new Order { Id = 2, Name = "Person2" } });

            var orderService = new OrderService(null, mockOrderRepository.Object, null);
            // Act

            var result = await orderService.GetOrders();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("Person2", result[1].Name);
        }

        [Fact]
        public void SaveOrder()
        {
            // Arrange
            var cartlineList = new List<CartLine>
            {
                new CartLine { Quantity = 1, Product = new Product {  Id = 1, Name = "product1", Quantity = 10, Price = 25} },
                new CartLine { Quantity = 3, Product = new Product {  Id = 1, Name = "product2", Quantity = 50, Price = 5} },
            };
            var orderViewModel = new OrderViewModel
            {
                OrderId = 1,
                Name = "test",
                Address = "Adresse",
                City = "City",
                Zip = "Zip",
                Country = "Country",
                Date = DateTime.Now,
                Lines = cartlineList
            };

            var productServiceMock = new Mock<IProductService>();
            var orderRepoMock = new Mock<IOrderRepository>();
            var cart = new Mock<ICart>();

            var orderService = new OrderService(cart.Object, orderRepoMock.Object, productServiceMock.Object);


            // Act
            orderService.SaveOrder(orderViewModel);

            // Assert
            orderRepoMock.Verify(x => x.Save(It.IsAny<Order>()), Times.Once());
            productServiceMock.Verify(x => x.UpdateProductQuantities(), Times.Once());

        }
    }
}
