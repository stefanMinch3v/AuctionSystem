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

            this.data = new List<Product>() {availableProduct, unavailableProduct}; // insert in collection

            this.mockSet =
                new Mock<DbSet<Product>>()
                    .SetupData(this.data); // creates fake table mockSet of Products and insert the data

            this.db.Setup(m => m.Products)
                .Returns(mockSet
                    .Object); // attach the table into the database and returns it as an object ready to be used 
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUnexistingProductShouldThrowException()
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetProductByIdShouldThrowException()
        {
            // Act
            var product = this.productController.GetProductById(99999);
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
        [ExpectedException(typeof(ArgumentException))]
        public void CreateProductWithEmptyNameShouldThrowException()
        {
            // Act
            var product = GetProductNotFromDb();
            product.Name = "";

            this.productController.CreateProduct(product);
        }

        [TestMethod]
        public void CreateProductShouldPass()
        {
            // Act
            var product = GetProductNotFromDb();

            this.productController.CreateProduct(product);

            var actual = this.db.Object.Products.FirstOrDefault(p => p.Name == "Random name");

            // Assert

            Assert.AreEqual("Random name", actual.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateProductWithEmptyDescriptionShouldThrowException()
        {
            var product = GetProductNotFromDb();
            product.Description = "";

            this.productController.CreateProduct(product);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateProductWithNullNameShouldThrowException()
        {
            var product = GetProductNotFromDb();
            product.Name = null;

            this.productController.CreateProduct(product);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateProductWithNUllDescriptionShouldThrowException()
        {
            var product = GetProductNotFromDb();
            product.Description = null;

            this.productController.CreateProduct(product);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateProductWithNegativePriceShouldThrowException()
        {
            var product = GetProductNotFromDb();
            product.Price = -1;

            this.productController.CreateProduct(product);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateProductWithZeroPriceShouldThrowException()
        {
            var product = GetProductNotFromDb();
            product.Price = 0;

            this.productController.CreateProduct(product);
        }

        // Delete product

        [TestMethod]
        public void DeleteProductShouldPass()
        {
            // Act

            var existingProduct = GetExistingProductFromDb();

            var deletedProduct = this.productController.DeleteProduct(existingProduct);

            // Assert

            Assert.IsTrue(deletedProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteProductShouldThrowException()
        {
            // Act
            var product = GetProductNotFromDb();

            var deletedProduct = this.productController.DeleteProduct(product);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteProductWithNegativeIdShouldThrowException()
        {
            var product = GetProductNotFromDb();
            product.Id = -1;

            var deletedProduct = this.productController.DeleteProduct(product);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteProductWithZeroIdShouldThrowException()
        {
            var product = GetProductNotFromDb();
            product.Id = 0;

            var deletedProduct = this.productController.DeleteProduct(product);
        }

        // Existing product

        [TestMethod]
        public void IsProductExistingShouldPass()
        {
            // Act
            var product = GetFakeAvailableProduct();

            var isProductExists = this.productController.IsProductExisting(product.Name);

            // Assert

            Assert.IsTrue(isProductExists);
        }

        [TestMethod]
        public void IsProductExistingShouldReturnFalse()
        {
            // Act
            var product = GetProductNotFromDb();

            var isProductExists = this.productController.IsProductExisting(product.Name);

            // Assert

            Assert.IsFalse(isProductExists);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsProductExistingWithNullNameShouldThrowException()
        {
            var product = GetProductNotFromDb();
            product.Name = null;

            var isProductExists = this.productController.IsProductExisting(product.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsProductExistingWithEmptyNameShouldThrowException()
        {
            var product = GetProductNotFromDb();
            product.Name = "";

            var isProductExists = this.productController.IsProductExisting(product.Name);
        }

        // Update product

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductWithNegativeIdShouldThrowException()
        {
            var product = GetFakeAvailableProduct();
            product.Id = -1;

            var successUpdate = this.productController.UpdateProduct(product);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductWithZeroIdShouldThrowException()
        {
            var product = GetFakeAvailableProduct();
            product.Id = 0;

            var successUpdate = this.productController.UpdateProduct(product);
        }

        [TestMethod]
        public void UpdateProductNameShouldReturnTrue()
        {
            // Act

            var currentProduct = GetExistingProductFromDb();
            currentProduct.Name = "Random name";

            var successUpdating = this.productController.UpdateProduct(currentProduct);

            // Assert

            Assert.IsTrue(successUpdating);
        }

        [TestMethod]
        public void UpdateProductNameShouldPassIfNameIsChanged()
        {
            // Act

            var currentProduct = GetExistingProductFromDb();
            currentProduct.Name = "Random name";

            var successUpdating = this.productController.UpdateProduct(currentProduct);

            var changedProductName = this.db.Object.Products.FirstOrDefault(p => p.Id == currentProduct.Id).Name;

            // Assert

            Assert.AreEqual("Random name", changedProductName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductNameShouldThrowExceptionIfNameIsNull()
        {
            var currentProduct = GetExistingProductFromDb();
            currentProduct.Name = null;

            var successUpdating = this.productController.UpdateProduct(currentProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductNameShouldThrowExceptionIfNameEmpty()
        {
            var currentProduct = GetExistingProductFromDb();
            currentProduct.Name = "";
            var successUpdating = this.productController.UpdateProduct(currentProduct);
        }

        //
        [TestMethod]
        public void UpdateProductPriceShouldReturnTrue()
        {
            // Act

            var currentProduct = GetExistingProductFromDb();
            currentProduct.Price = 5000;

            var successUpdating = this.productController.UpdateProduct(currentProduct);

            // Assert

            Assert.IsTrue(successUpdating);
        }

        [TestMethod]
        public void UpdateProductPriceShouldPassIfPriceIsPositive()
        {
            // Act

            var currentProduct = GetExistingProductFromDb();
            currentProduct.Price = 10;
            var successUpdating = this.productController.UpdateProduct(currentProduct);

            var changedProductName = this.db.Object.Products.FirstOrDefault(p => p.Id == currentProduct.Id).Price;

            // Assert

            Assert.AreEqual(10, changedProductName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductPriceShouldThrowExceptionIfPriceIsZero()
        {
            var currentProduct = GetExistingProductFromDb();
            currentProduct.Price = 0;
            var successUpdating = this.productController.UpdateProduct(currentProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductPriceShouldThrowExceptionIfPriceIsNegative()
        {
            var currentProduct = GetExistingProductFromDb();
            currentProduct.Price = -1;
            var successUpdating = this.productController.UpdateProduct(currentProduct);
        }

        [TestMethod]
        public void UpdateProductDescriptionShouldPass()
        {
            // Act

            var currentProduct = GetExistingProductFromDb();
            currentProduct.Description = "new description";
            var successUpdating = this.productController.UpdateProduct(currentProduct);

            // Assert

            Assert.IsTrue(successUpdating);
        }

        [TestMethod]
        public void UpdateProductDescriptionSouldPassIfDescriptionIsChanged()
        {
            // Act

            var currentProduct = GetExistingProductFromDb();
            currentProduct.Description = "new description";
            var successUpdating = this.productController.UpdateProduct(currentProduct);

            var changedDescription = this.db.Object.Products.FirstOrDefault(p => p.Id == currentProduct.Id).Description;

            // Assert

            Assert.AreEqual("new description", changedDescription);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductPriceShouldThrowExceptionIfDescriptionIsNull()
        {
            var currentProduct = GetExistingProductFromDb();
            currentProduct.Description = null;

            var successUpdating = this.productController.UpdateProduct(currentProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductPriceShouldThrowExceptionIfDescriptionIsEmpty()
        {
            var currentProduct = GetExistingProductFromDb();
            currentProduct.Description = "";
            var successUpdating = this.productController.UpdateProduct(currentProduct);
        }

        [TestMethod]
        public void UpdateStartDateShouldReturnTrue()
        {
            // Act

            var currentProduct = GetExistingProductFromDb();
            currentProduct.StartDate = DateTime.Now.AddDays(1);
            var successUpdating = this.productController.UpdateProduct(currentProduct);

            // Assert

            Assert.IsTrue(successUpdating);
        }


        [TestMethod]
        public void UpdateStartDateShouldPassIfDateIsUpdated()
        {
            // Act

            var currentProduct = GetExistingProductFromDb();
            currentProduct.StartDate = DateTime.Now.AddDays(1);
            var successUpdating = this.productController.UpdateProduct(currentProduct);

            // Assert

            Assert.AreEqual(currentProduct.StartDate, GetExistingProductFromDb().StartDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductShouldThrowExceptionIfStartDateIsLessThanTodaysDate()
        {
            var currentProduct = GetExistingProductFromDb();
            var product = new Product
            {
                Name = currentProduct.Name,
                StartDate = DateTime.Parse("08-08-2016"),
                Description = currentProduct.Description,
                EndDate = currentProduct.EndDate,
                Id = currentProduct.Id,
                Price = currentProduct.Price
            };
            var successUpdating = this.productController.UpdateProduct(product);
        }

        [TestMethod]
        public void UpdateEndDateShouldReturnTrue()
        {
            // Act

            var currentProduct = GetExistingProductFromDb();
            currentProduct.EndDate = DateTime.Now.AddDays(5);
            var successUpdating = this.productController.UpdateProduct(currentProduct);

            // Assert

            Assert.IsTrue(successUpdating);
        }


        [TestMethod]
        public void UpdateEndDateShouldPassIfDateIsUpdated()
        {
            // Act

            var currentProduct = GetExistingProductFromDb();
            currentProduct.EndDate = DateTime.Now.AddDays(10);
            var successUpdating = this.productController.UpdateProduct(currentProduct);

            // Assert

            Assert.AreEqual(currentProduct.EndDate, GetExistingProductFromDb().EndDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateProductShouldThrowExceptionIfEndDateIsLessThanTodaysDate()
        {
            var currentProduct = GetExistingProductFromDb();
            currentProduct.EndDate = DateTime.Parse("08-08-2016");
            var successUpdating = this.productController.UpdateProduct(currentProduct);
        }

        // TODO
        // Test List<User>GetProductUsers when user controller is ready!

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
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now.AddDays(2)
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

        private Product GetProductNotFromDb()
        {
            return new Product
            {
                Id = 3,
                Name = "Random name",
                Description = "Unique product",
                Price = 1000m,
                IsAvailable = true,
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now.AddDays(2)
            };
        }

        #endregion
    }
}