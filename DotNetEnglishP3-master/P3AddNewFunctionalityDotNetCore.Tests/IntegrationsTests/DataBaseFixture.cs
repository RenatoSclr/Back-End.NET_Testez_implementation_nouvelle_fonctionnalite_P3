using Microsoft.EntityFrameworkCore;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using System;
using System.Linq;

namespace P3AddNewFunctionalityDotNetCore.Tests.IntegrationTests
{
    public class DataBaseFixture: IDisposable
    {
        private readonly string _connectionString = "Server=.;Database=TestDatabase;Trusted_Connection=True;";
        private readonly P3Referential _dbContext;
        
        public DataBaseFixture()
        {
            var options = new DbContextOptionsBuilder<P3Referential>()
                .UseSqlServer(_connectionString)
            .Options;

            _dbContext = new P3Referential(options, null);
            _dbContext.Database.Migrate();
           
        }

        public void AddProduct(Product product)
        {
            var repository = new ProductRepository(_dbContext);
            repository.SaveProduct(product);
        }

        public void DeleteProduct(int productId)
        {
            var repository = new ProductRepository(_dbContext);
           repository.DeleteProduct(productId);
        }

        public void UpdateProductQuantities(int id, int quantityToRemove)
        {
            var repository = new ProductRepository(_dbContext);
            repository.UpdateProductStocks(id, quantityToRemove);
        }

        public Product GetProduct(int productId)
        {
            return _dbContext.Product.FirstOrDefault(p => p.Id == productId);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
