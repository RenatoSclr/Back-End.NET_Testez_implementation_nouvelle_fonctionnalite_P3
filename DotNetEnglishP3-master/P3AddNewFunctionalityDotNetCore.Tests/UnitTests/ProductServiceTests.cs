using Moq;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using P3AddNewFunctionalityDotNetCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Globalization;
using System;

namespace P3AddNewFunctionalityDotNetCore.Tests.UnitTests
{
    public class ProductServiceTests
    {

        /// <summary>
        /// Method who return a list of product for all the test where it's needed
        /// </summary>
        private List<Product> GetTestProducts()
        {
            var products = new List<Product>
                {
                    new Product { Id = 1, Name = "Product1", Price = 10, Quantity = 5 },
                    new Product { Id = 2, Name = "Product2", Price = 20, Quantity = 10 }
                };

            return products;
        }

        [Fact]
        public void GetAllProductsViewModel()
        {
            // Arrange
            var mockProductRepository = Mock.Of<IProductRepository>();
            Mock.Get(mockProductRepository).Setup(repo => repo.GetAllProducts())
                .Returns(GetTestProducts());

            var productService = new ProductService(null, mockProductRepository, null, null);
            // Act

            var result = productService.GetAllProductsViewModel();
            // Assert
            Assert.True(result is List<ProductViewModel>);
            Assert.Equal(2, result.Count);
            Assert.Equal("Product1", result[0].Name);
            Assert.Equal("Product2", result[1].Name);

        }

        [Fact]
        public void GetAllProducts_ReturnListProduct_WhenRepositoryReturnListProduct()
        {
            // Arrange

            var mockProductRepository = Mock.Of<IProductRepository>();
            Mock.Get(mockProductRepository).Setup(repo => repo.GetAllProducts())
                .Returns(GetTestProducts());

            var productService = new ProductService(null, mockProductRepository, null, null);
            // Act

            var result = productService.GetAllProducts();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Product1", result[0].Name);
            Assert.Equal(10, result[1].Quantity);
            Assert.IsType<List<Product>>(result);
        }

        [Fact]
        public void GetAllProducts_ReturnNull_WhenRepositoryReturnNull()
        {
            // Arrange

            var mockProductRepository = Mock.Of<IProductRepository>();
            Mock.Get(mockProductRepository).Setup(repo => repo.GetAllProducts()).Returns((List<Product>)null);

            var productService = new ProductService(null, mockProductRepository, null, null);
            // Act

            var result = productService.GetAllProducts();
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetProductByIdViewModel()
        {
            // Arrange

            var mockProductRepository = Mock.Of<IProductRepository>();
            Mock.Get(mockProductRepository).Setup(repo => repo.GetAllProducts())
                .Returns(GetTestProducts());

            var productService = new ProductService(null, mockProductRepository, null, null);
            // Act

            var result = productService.GetProductByIdViewModel(1);
            // Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Product1", result.Name);
            Assert.True(result is ProductViewModel);
        }

        [Fact]
        public void GetProductById()
        {
            // Arrange

            var mockProductRepository = Mock.Of<IProductRepository>();
            Mock.Get(mockProductRepository).Setup(repo => repo.GetAllProducts())
                .Returns(GetTestProducts());

            var productService = new ProductService(null, mockProductRepository, null, null);
            // Act

            var result = productService.GetProductById(1);
            // Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Product1", result.Name);
            Assert.True(result is Product);
        }

        [Fact]
        public async Task GetProduct_ReturnProductByThisId_WhenGeProductInProductRepositoryReturnProduct()
        {
            // Arrange

            var mockProductRepository = Mock.Of<IProductRepository>();
            Mock.Get(mockProductRepository).Setup(repo => repo.GetProduct(1))
                .ReturnsAsync(new Product { Id = 1, Name = "TestProduct", Price = 10, Quantity = 5 });

            var productService = new ProductService(null, mockProductRepository, null, null);
            // Act

            var result = await productService.GetProduct(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("TestProduct", result.Name);
        }

        [Fact]
        public async Task GetProducts_ReturnProductList_WhenGetProductsInProductRepositoryReturnList()
        {
            // Arrange

            var mockProductRepository = Mock.Of<IProductRepository>();
            Mock.Get(mockProductRepository).Setup(repo => repo.GetProducts())
                .ReturnsAsync(GetTestProducts());
            var productService = new ProductService(null, mockProductRepository, null, null);
            // Act

            var result = await productService.GetProducts();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("Product2", result[1].Name);
        }

        [Fact]
        public void SaveProduct()
        {
            // Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            var productService = new ProductService(null, mockProductRepository.Object, null, null);

            var productViewModel = new ProductViewModel
            {
                Id = 1,
                Name = "Test Product",
                Price = "9.99",
                Description = "Description",
                Stock = "10"
            };

            // Act
            productService.SaveProduct(productViewModel);


            // Assert
            mockProductRepository.Verify(repo => repo.SaveProduct(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void DeleteProduct()
        {
            // Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            var mockCart = new Mock<ICart>();

            var productService = new ProductService(mockCart.Object, mockProductRepository.Object, null, null);

            var productId = 1; // ID du produit à supprimer

            // Act
            productService.DeleteProduct(productId);

            // Assert
            mockCart.Verify(cart => cart.RemoveLine(It.IsAny<Product>()), Times.Once);
            mockProductRepository.Verify(repo => repo.DeleteProduct(productId), Times.Once);

        }          
    }
}