namespace AuctionSystem.Tests
{
    using Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Models;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    [TestClass]
    public class TestProduct
    {
        private Mock<AuctionContext> db;
        private ProductControllerMock productController;
        private Mock<DbSet<Product>> mockSet;
        private List<Product> data;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange

            this.db = new Mock<AuctionContext>(); // create virtual database context

            this.productController = new ProductControllerMock(this.db.Object); // creates controller to test

            var availableProduct = GetFakeAvailableProduct(); // get some random product
            var unavailableProduct = GetFakeUnavailableProduct(); // get some random product

            this.data = new List<Product>() { availableProduct, unavailableProduct }; // insert in collection

            this.mockSet = new Mock<DbSet<Product>>().SetupData(this.data); // creates fake table mockSet of Products and insert the data

            this.db.Setup(m => m.Products).Returns(mockSet.Object); // attach the table into the database and returns it as an object ready to be used 
        }

        // Get product

        [TestMethod]
        public void GetProductByNameShouldPass()
        {
            // Act

            var expected = GetExistingProductFromDb().Name;

            var actualProduct = productController.GetProductByName("Available FakeProduct").Name;

            // Assert

            Assert.AreEqual(expected, actualProduct);
        }

        [TestMethod]
        public void GetUnavailableProductShouldReturnFalse()
        {
            // Act

            var currentProduct = this.productController.GetProductByName("Unavailable FakeProduct").IsAvailable;

            // Assert

            Assert.IsFalse(currentProduct);
        }

        [TestMethod]
        public void GetAvailableProductShouldReturnTrue()
        {
            // Act

            var actualProduct = this.productController.GetProductByName("Available FakeProduct").IsAvailable;

            // Assert

            Assert.IsTrue(actualProduct);
        }

        [TestMethod]
        public void GetUnexistingProductShouldReturnNull()
        {
            // Act

            var product = this.productController.GetProductByName("Unexisting");

            // Assert

            Assert.IsNull(product);
        }

        [TestMethod]
        public void GetProductByIdShouldPass()
        {
            // Act

            var actual = this.productController.GetProductById(1);
            var expected = this.db.Object.Products.FirstOrDefault(p => p.Id == 1);

            // Assert

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetProductByIdShouldReturnNull()
        {
            // Act
            var product = this.productController.GetProductById(99999);

            // Assert

            Assert.IsNull(product);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetProductByIdWithNegativeNumberShouldThrowException()
        {
            // Act

            var product = this.productController.GetProductById(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetProductByIdWithZeroNumberShouldThrowException()
        {
            // Act

            var product = this.productController.GetProductById(0);
        }

        // Create product

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateProductWithEmptyNameShouldThrowException()
        {
            // Act

            this.productController.CreateProduct("", "some unique", 55, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        public void CreateProductShouldPass()
        {
            // Act

            this.productController.CreateProduct("Some name", "description", 202m, DateTime.Now, DateTime.Now);

            var actual = this.db.Object.Products.FirstOrDefault(p => p.Name == "Some name");

            // Assert

            Assert.AreEqual("Some name", actual.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateProductWithEmptyDescriptionShouldThrowException()
        {
            this.productController.CreateProduct("Product", "", 55, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateProductWithNullNameShouldThrowException()
        {
            this.productController.CreateProduct(null, "unique", 55, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateProductWithNUllDescriptionShouldThrowException()
        {
            this.productController.CreateProduct("Product", null, 55, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateProductWithNegativePriceShouldThrowException()
        {
            this.productController.CreateProduct("Product", "description", -1, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateProductWithZeroPriceShouldThrowException()
        {
            this.productController.CreateProduct("Product", "description", 0, DateTime.Now, DateTime.Now);
        }

        // Delete product

        [TestMethod]
        public void DeleteProductShouldPass()
        {
            // Act

            var existingProductId = GetExistingProductFromDb().Id;

            var deletedProduct = this.productController.DeleteProduct(existingProductId);

            // Assert

            Assert.IsTrue(deletedProduct);
        }

        [TestMethod]
        public void DeleteProductShouldReturnFalse()
        {
            // Act

            var deletedProduct = this.productController.DeleteProduct(99999);

            // Assert

            Assert.IsFalse(deletedProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteProductWithNegativeIdShouldThrowException()
        {
            var deletedProduct = this.productController.DeleteProduct(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteProductWithZeroIdShouldThrowException()
        {
            var deletedProduct = this.productController.DeleteProduct(0);
        }

        // Existing product

        [TestMethod]
        public void IsProductExistingShouldPass()
        {
            // Act

            var isProductExists = this.productController.IsProductExisting("Available FakeProduct");

            // Assert

            Assert.IsTrue(isProductExists);
        }

        [TestMethod]
        public void IsProductExistingShouldReturnFalse()
        {
            // Act

            var isProductExists = this.productController.IsProductExisting("Unexisting product");

            // Assert

            Assert.IsFalse(isProductExists);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsProductExistingWithNullValueShouldThrowException()
        {
            var isProductExists = this.productController.IsProductExisting(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsProductExistingWithEmptyValueShouldThrowException()
        {
            var isProductExists = this.productController.IsProductExisting("");
        }

        // Update product

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductWithNegativeIdShouldThrowException()
        {
            var successUpdate = this.productController.UpdateProduct(-1, "Description", "test description");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductWithZeroIdShouldThrowException()
        {
            var successUpdate = this.productController.UpdateProduct(0, "Description", "test description");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductWithUnexistingPropertyShouldThrowException()
        {
            var successUpdate = this.productController.UpdateProduct(1, "unexisting", "test description");
        }

        [TestMethod]
        public void UpdateProductNameShouldReturnTrue()
        {
            // Act

            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Name", "Random name");

            // Assert

            Assert.IsTrue(successUpdating);
        }

        [TestMethod]
        public void UpdateProductNameShouldPassIfNameIsChanged()
        {
            // Act

            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Name", "Random name");

            var changedProductName = this.db.Object.Products.FirstOrDefault(p => p.Id == currentProductId).Name;

            // Assert

            Assert.AreEqual("Random name", changedProductName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductNameShouldThrowExceptionIfNameIsNull()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Name", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductNameShouldThrowExceptionIfNameEmpty()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Name", "");
        }

        //
        [TestMethod]
        public void UpdateProductPriceShouldReturnTrue()
        {
            // Act

            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Price", "5000");

            // Assert

            Assert.IsTrue(successUpdating);
        }

        [TestMethod]
        public void UpdateProductPriceShouldPassIfPriceIsPositive()
        {
            // Act

            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Price", "10");

            var changedProductName = this.db.Object.Products.FirstOrDefault(p => p.Id == currentProductId).Price;

            // Assert

            Assert.AreEqual(10, changedProductName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductPriceShouldThrowExceptionIfPriceIsZero()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Price", "0");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductPriceShouldThrowExceptionIfPriceIsNegative()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Price", "-1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductPriceShouldThrowExceptionIfPriceIsNonDigit()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Price", "price");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductPriceShouldThrowExceptionIfPriceIsNull()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Price", null);
        }

        [TestMethod]
        public void UpdateProductDescriptionShouldPass()
        {
            // Act

            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Description", "new description");

            // Assert

            Assert.IsTrue(successUpdating);
        }

        [TestMethod]
        public void UpdateProductDescriptionSouldPassIfDescriptionIsChanged()
        {
            // Act

            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Description", "new description");

            var changedDescription = this.db.Object.Products.FirstOrDefault(p => p.Id == currentProductId).Description;

            // Assert

            Assert.AreEqual("new description", changedDescription);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductPriceShouldThrowExceptionIfDescriptionIsNull()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Description", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductPriceShouldThrowExceptionIfDescriptionIsEmpty()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "Description", "");
        }

        [TestMethod]
        public void UpdateStartDateShouldReturnTrue()
        {
            // Act

            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "StartDate", "12-12-2017");

            // Assert

            Assert.IsTrue(successUpdating);
        }


        [TestMethod]
        public void UpdateStartDateShouldPassIfDateIsUpdated()
        {
            // Act

            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "StartDate", "12-12-2017");

            // Assert

            Assert.AreEqual(DateTime.Parse("12-12-2017"), GetExistingProductFromDb().StartDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductShouldThrowExceptionIfStartDateIsLessThanTodaysDate()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "StartDate", "08-08-2016");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductShouldThrowExceptionIfStartDateIsEmpty()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "StartDate", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductShouldThrowExceptionIfStartDateIsNull()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "StartDate", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductShouldThrowExceptionIfStartDateIsLetters()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "StartDate", "give me the date");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductShouldThrowExceptionIfStartDateIsRandomNumbers()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "StartDate", "11248576456453453");
        }

        //////////

        [TestMethod]
        public void UpdateEndDateShouldReturnTrue()
        {
            // Act

            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "EndDate", "12-12-2017");

            // Assert

            Assert.IsTrue(successUpdating);
        }


        [TestMethod]
        public void UpdateEndDateShouldPassIfDateIsUpdated()
        {
            // Act

            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "EndDate", "12-12-2017");

            // Assert

            Assert.AreEqual(DateTime.Parse("12-12-2017"), GetExistingProductFromDb().EndDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductShouldThrowExceptionIfEndDateIsLessThanTodaysDate()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "EndDate", "08-08-2016");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePriceShouldThrowExceptionIfEndDateIsEmpty()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "EndDate", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductShouldThrowExceptionIfEndDateIsNull()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "EndDate", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductShouldThrowExceptionIfEndDateIsLetters()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "EndDate", "give me the date");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductShouldThrowExceptionIfEndDateIsRandomNumbers()
        {
            var currentProductId = GetExistingProductFromDb().Id;

            var successUpdating = this.productController.UpdateProduct(currentProductId, "EndDate", "11248576456453453");
        }


        #region Test purpose methods
        private Product GetExistingProductFromDb()
        {
            return this.db.Object.Products.First(p => p.Name == "Available FakeProduct");
        }

        private Product GetFakeAvailableProduct()
        {
            return new Product
            {
                Id = 1,
                Name = "Available FakeProduct",
                Description = "Unique fake product",
                Price = 2000m,
                IsAvailable = true,
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(1)
            };
        }

        private Product GetFakeUnavailableProduct()
        {
            return new Product
            {
                Id = 2,
                Name = "Unavailable FakeProduct",
                Description = "Unique fake product",
                Price = 1000m,
                IsAvailable = false,
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now.AddDays(-1)
            };
        }

        #endregion
    }
}
