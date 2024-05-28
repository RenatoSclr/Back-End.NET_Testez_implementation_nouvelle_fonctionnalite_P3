using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models;
using System.Linq;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests.UnitTests
{
    public class CartTests
    {
        [Fact]
        public void AddItemInCart()
        {
            Cart cart = new Cart();
            Product product1 = new Product { Id = 1, Name = "Product 1", Price = 9.99, Quantity = 10 };
            Product product2 = new Product { Id = 1, Name = "Product 1", Price = 9.99, Quantity = 10 };
            Product product3 = new Product { Id = 2, Name = "Product 2", Price = 20, Quantity = 500 };

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cart.AddItem(product3, 1);

            Assert.NotEmpty(cart.Lines);
            Assert.Equal(2, cart.Lines.Count());
            Assert.Equal(2, cart.Lines.FirstOrDefault(p => p.Product.Id == 1).Quantity);
        }

        [Fact]
        public void DoNotAddItemInCart_QuantityGreaterThanStock()
        {

            // Arrange
            Product product1 = new Product { Id = 1, Name = "Product 1", Quantity = 10, Price = 20.0 };

            // Act
            Cart cart = new Cart();
            cart.AddItem(product1, 10);
            cart.AddItem(product1, 1);

            //Assert
            Assert.True(cart.Lines.FirstOrDefault(p => p.Product.Id == 1).Quantity == 10);
        }
        [Fact]
        public void IncrementQuantitiesInCart()
        {

            // Arrange
            Product product1 = new Product { Id = 1, Name = "Product 1", Quantity = 10, Price = 20.0 };

            // Act
            Cart cart = new Cart();
            cart.AddItem(product1, 3);
            cart.AddItem(product1, 5);

            //Assert
            Assert.True(cart.Lines.FirstOrDefault(p => p.Product.Id == 1).Quantity == 8);
        }

        [Fact]
        public void GetAverageValue()
        {
            ICart cart = new Cart();

            var product1 = new Product { Id = 2, Name = "Product 1", Quantity = 10, Price = 9.99 };
            var product2 = new Product { Id = 5, Name = "Product 2", Quantity = 5, Price = 895.00 };

            // Act
            cart.AddItem(product1, 2);
            cart.AddItem(product2, 1);
            double averageValue = cart.GetAverageValue();
            double expectedValue = (9.99 * 2 + 895.00) / 3;

            Assert.Equal(expectedValue, averageValue);
        }

        [Fact]
        public void GetTotalValue()
        {
            ICart cart = new Cart();

            var product1 = new Product { Id = 1, Name = "Product 1", Quantity = 10, Price = 92.50 };
            var product2 = new Product { Id = 4, Name = "Product 2", Quantity = 5, Price = 32.50 };
            var product3 = new Product { Id = 5, Name = "Product 3", Quantity = 5, Price = 895.00 };

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 3);
            cart.AddItem(product3, 1);
            double totalValue = cart.GetTotalValue();
            double expectedValue = 92.50 + 32.50 * 3 + 895.00;

            Assert.Equal(expectedValue, totalValue);
        }


        [Fact]
        public void RemoveLineInCart()
        {
            Cart cart = new Cart();

            var product1 = new Product { Id = 1, Name = "Product 1", Quantity = 10, Price = 92.50 };
            var product2 = new Product { Id = 4, Name = "Product 2", Quantity = 5, Price = 32.50 };
            var product3 = new Product { Id = 5, Name = "Product 3", Quantity = 5, Price = 895.00 };

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 3);
            cart.AddItem(product3, 1);

            cart.RemoveLine(product2);

            Assert.Equal(2, cart.Lines.Count());
            Assert.Null(cart.Lines.FirstOrDefault(l => l.Product.Id == 4));

        }


        [Fact]
        public void ClearProductInCart()
        {
            Cart cart = new Cart();

            var product1 = new Product { Id = 1, Name = "Product 1", Quantity = 10, Price = 92.50 };
            var product2 = new Product { Id = 4, Name = "Product 2", Quantity = 5, Price = 32.50 };
            var product3 = new Product { Id = 5, Name = "Product 3", Quantity = 5, Price = 895.00 };

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 3);
            cart.AddItem(product3, 1);

            cart.Clear();

            Assert.Empty(cart.Lines);
        }
    }
}
