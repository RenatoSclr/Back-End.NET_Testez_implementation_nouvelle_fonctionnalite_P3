using P3AddNewFunctionalityDotNetCore.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests.UnitTests
{
    public class LanguageServiceTests
    {
        [Fact]
        public void SetCulture_Fr()
        {
            // Arrange
            ILanguageService languageService = new LanguageService();
            string language = "French";

            // Act
            string culture = languageService.SetCulture(language);

            // Assert
            Assert.Same("fr-FR", culture);
        }

        [Fact]
        public void SetCulture_En()
        {
            // Arrange
            ILanguageService languageService = new LanguageService();
            string language = "English";

            // Act
            string culture = languageService.SetCulture(language);

            // Assert
            Assert.Same("en-US", culture);
        }

        [Fact]
        public void SetCulture_Es()
        {
            // Arrange
            ILanguageService languageService = new LanguageService();
            string language = "Spanish";

            // Act
            string culture = languageService.SetCulture(language);

            // Assert
            Assert.Same("es-ES", culture);
        }
    }
}
