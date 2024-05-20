using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests.UnitTests
{
    public class ProductViewModelTest
    {
        [Fact]
        public void NameIsNotValidWhenEmpty()
        {
            // Arrange
            var product = new ProductViewModel();
            product.Name = "";
            product.Price = "9.99";
            product.Stock = "10";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Name"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "ErrorMissingName");
        }

        [Fact]
        public void NameIsValidWhenStringIsNotEmpty()
        {
            // Arrange
            var product = new ProductViewModel();
            product.Name = "test";
            product.Price = "9.99";
            product.Stock = "10";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            //Assert
            Assert.True(isValid);
            Assert.DoesNotContain(validationResults, vr => vr.MemberNames.Contains("Name"));
        }

        [Fact]
        public void PriceIsNotValidWhenEmpty()
        {
            // Arrange
            var product = new ProductViewModel();
            product.Name = "Test";
            product.Price = "";
            product.Stock = "10";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Price"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "ErrorMissingPrice");
        }

        [Fact]
        public void PriceIsNotValidWhenIsEqualToZero()
        {
            // Arrange
            var product = new ProductViewModel();
            product.Name = "Test";
            product.Price = "0";
            product.Stock = "10";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Price"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "ErrorPriceValue");
        }


        [Fact]
        public void PriceIsNotValidWhenIsLowerThanZero()
        {
            // Arrange
            var product = new ProductViewModel();
            product.Name = "Test";
            product.Price = "-20";
            product.Stock = "10";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Price"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "ErrorPriceValue");
        }

        [Fact]
        public void PriceIsValidWhenStringIsAnIntegerAndUpperThanZero()
        {
            // Arrange
            var product = new ProductViewModel();
            product.Price = "10";
            product.Stock = "50";
            product.Name = "Test";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            //Assert
            Assert.True(isValid);
            Assert.DoesNotContain(validationResults, vr => vr.MemberNames.Contains("Price"));
        }

        [Fact]
        public void PriceIsValidWhenStringIsDoubleAndUpperThanZero()
        {
            // Arrange
            var product = new ProductViewModel();
            product.Price = "9.99";
            product.Stock = "50";
            product.Name = "Test";
            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            //Assert
            Assert.True(isValid);
            Assert.DoesNotContain(validationResults, vr => vr.MemberNames.Contains("Price"));
        }

        [Fact]
        public void StockIsNotValidWhenEmpty()
        {
            // Arrange
            var product = new ProductViewModel();
            product.Name = "Test";
            product.Price = "5.99";
            product.Stock = "";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Stock"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "ErrorMissingStock");
        }

        [Fact]
        public void StockIsNotValidWhenIsEqualToZero()
        {
            // Arrange
            var product = new ProductViewModel();
            product.Name = "Test";
            product.Price = "25";
            product.Stock = "0";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Stock"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "ErrorStockValue");
        }


        [Fact]
        public void StockIsNotValidWhenIsLowerThanZero()
        {
            // Arrange
            var product = new ProductViewModel();
            product.Name = "Test";
            product.Price = "25";
            product.Stock = "-2";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Stock"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "ErrorStockValue");
        }

        [Fact]
        public void StockIsValidWhenStringIsAnInteger()
        {
            // Arrange
            var product = new ProductViewModel();
            product.Name = "Test";
            product.Price = "25";
            product.Stock = "10";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            //Assert
            Assert.True(isValid);
            Assert.DoesNotContain(validationResults, vr => vr.MemberNames.Contains("Stock"));
        }

        [Fact]
        public void StockIsNotValidWhenStringIsDouble()
        {
            // Arrange
            var product = new ProductViewModel();
            product.Name = "Test";
            product.Price = "25";
            product.Stock = "9.99";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            //Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Stock"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "ErrorStockValue");
        }
    }
}
