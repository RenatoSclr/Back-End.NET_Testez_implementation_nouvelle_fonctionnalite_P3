using P3AddNewFunctionalityDotNetCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests.IntegrationTests
{
    public class IntegrationsTestsOnTestDatabase: IClassFixture<DataBaseFixture>
    {
        private readonly DataBaseFixture _fixture;

        public IntegrationsTestsOnTestDatabase(DataBaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void AddProduct()
        {
            // Arrange 
            Product product = new Product
            {
                Name = "TestNom",
                Price = 10.99,
                Description = "TestAddProduct",
                Quantity = 100,
                Details = "AddProduct"
            };

            // Act
            _fixture.AddProduct(product);

            // Assert 
            Product productAdd= _fixture.GetProduct(product.Id);
            Assert.NotNull(productAdd);
            Assert.Equal(product.Name, productAdd.Name);
            Assert.Equal(product.Description, productAdd.Description);
            Assert.Equal(product.Quantity, productAdd.Quantity);
            Assert.Equal(product.Details, productAdd.Details);
        }

        [Fact]
        public void DeleteProduct()
        {
            // Arrange 
            Product product = new Product
            {
                Name = "TestNom",
                Price = 20.99,
                Description = "TestDeleteProduct",
                Quantity = 50,
                Details = "TestDetails"
            };
            _fixture.AddProduct(product);

            // Act 
            _fixture.DeleteProduct(product.Id);

            // Assert 
            Product deletedProduct = _fixture.GetProduct(product.Id);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public void UpdateProductStockWhenStockGreaterThanQuantityToRemove_UpdateProduct()
        {
            
            // Arrange : Créer un produit à mettre à jour
            Product product = new Product
            {
                Name = "TestNom",
                Price = 20.99,
                Description = "TestUpdateProduct",
                Quantity = 50,
                Details = "StockGreaterThanQuantityToRemove"
            };
            int quantityToRemove = 10;

            _fixture.AddProduct(product);

            // Act : Mise a jour du stock du produit 
            _fixture.UpdateProductQuantities(product.Id, quantityToRemove);

            // Assert : Vérifier si le stock a correctement été mis à jour
            Product updateProduct = _fixture.GetProduct(product.Id);
            Assert.True(updateProduct.Quantity == 40);
        }

        [Fact]
        public void UpdateProductStockWhenQuantityToRemoveEqualThanStock_RemoveProduct()
        {

            // Arrange : Créer un produit à mettre à jour
            Product product = new Product
            {
                Name = "TestNom",
                Price = 20.99,
                Description = "TestUpdateProduct_Remove",
                Quantity = 10,
                Details = "QuantityEqualToStock"
            };
            int quantityToRemove = 10;

            _fixture.AddProduct(product);

            // Act : Supprimer le produit de la base de données car il n'y a plus de stock
            _fixture.UpdateProductQuantities(product.Id, quantityToRemove);

            // Assert : Vérifier si le produit a été correctement supprimé
            Product updateProduct = _fixture.GetProduct(product.Id);
            Assert.Null(updateProduct);
        }
    }
}
