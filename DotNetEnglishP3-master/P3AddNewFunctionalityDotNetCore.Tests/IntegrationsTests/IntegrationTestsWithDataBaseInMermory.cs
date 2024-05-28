using Microsoft.EntityFrameworkCore;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using System.Linq;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests.IntegrationTests
{
    public class IntegrationTestsWithDataBaseInMermory
    {
        [Fact]
        public void AddProductInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<P3Referential>()
                .UseInMemoryDatabase(databaseName: "TestProductDatabaseInMemory")
                .Options;

            var newProduct = new Product {Id = 1, Name = "Test Product", Quantity = 10, Price = 9.99 };
            var context = new P3Referential(options, null);
            var repository = new ProductRepository(context);
            
            
            // Act
            repository.SaveProduct(newProduct);
            
            
            // Assert
            Assert.Equal(1, context.Product.Count());
            Assert.Equal("Test Product", context.Product.Single().Name);
            Assert.Equal(10, context.Product.Single().Quantity);
            

            // Clean up
            context.Database.EnsureDeleted();
            
        }

        [Fact]
        public void DeleteProductInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<P3Referential>()
                .UseInMemoryDatabase(databaseName: "TestProductDatabaseInMemory")
                .Options;

            var newProduct1 = new Product { Id = 1, Name = "Test Product1", Quantity = 10, Price = 9.99 };
            var newProduct2 = new Product { Id = 2, Name = "Test Product2", Quantity = 5, Price = 25 };
            var newProduct3 = new Product { Id = 3, Name = "Test Product3", Quantity = 200, Price = 14.99 };

            var context = new P3Referential(options, null);
            var repository = new ProductRepository(context);

            // Act
            repository.SaveProduct(newProduct1);
            repository.SaveProduct(newProduct2);
            repository.SaveProduct(newProduct3);

            repository.DeleteProduct(context.Product.FirstOrDefault(x => x.Id == 2).Id);
            
            // Assert
            Assert.Equal(2, context.Product.Count());
            Assert.Null(context.Product.FirstOrDefault(p => p.Id == 2));
            Assert.Equal(200, context.Product.Last().Quantity);
            
            // Clean up
            context.Database.EnsureDeleted();
            

        }

        [Fact]
        public void UpdateProductInDatabaseWhenStockGreaterThanQuantity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<P3Referential>()
                .UseInMemoryDatabase(databaseName: "TestProductDatabaseInMemory")
                .Options;

            var newProduct1 = new Product { Id = 1, Name = "Test Product1", Quantity = 10, Price = 9.99 };
            var newProduct2 = new Product { Id = 2, Name = "Test Product2", Quantity = 5, Price = 25 };
            var newProduct3 = new Product { Id = 3, Name = "Test Product3", Quantity = 200, Price = 14.99 };
            int quantityToRemove = 2;

            var context = new P3Referential(options, null);
            var repository = new ProductRepository(context);

            // Act
            repository.SaveProduct(newProduct1);
            repository.SaveProduct(newProduct2);
            repository.SaveProduct(newProduct3);

            repository.UpdateProductStocks(newProduct1.Id, quantityToRemove);

            // Assert
            Assert.Equal(8, context.Product.FirstOrDefault(p => p.Id == 1).Quantity);
            Assert.Equal(3, context.Product.Count());

            // Clean up
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void UpdateProductInDatabaseWhenStockEqualQuantity_RemoveProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<P3Referential>()
                .UseInMemoryDatabase(databaseName: "TestProductDatabaseInMemory")
                .Options;

            var newProduct1 = new Product { Id = 1, Name = "Test Product1", Quantity = 10, Price = 9.99 };
            var newProduct2 = new Product { Id = 2, Name = "Test Product2", Quantity = 5, Price = 25 };
            var newProduct3 = new Product { Id = 3, Name = "Test Product3", Quantity = 200, Price = 14.99 };
            int quantityToRemove = 10;

            var context = new P3Referential(options, null);
            var repository = new ProductRepository(context);

            // Act
            repository.SaveProduct(newProduct1);
            repository.SaveProduct(newProduct2);
            repository.SaveProduct(newProduct3);

            repository.UpdateProductStocks(newProduct1.Id, quantityToRemove);

            // Assert
            Assert.Null(context.Product.FirstOrDefault(p => p.Id == 1));
            Assert.Equal(2, context.Product.Count());

            // Clean up
            context.Database.EnsureDeleted();
        }
    }
}
