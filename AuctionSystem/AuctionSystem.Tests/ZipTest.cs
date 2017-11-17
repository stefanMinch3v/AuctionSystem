using AuctionSystem.Data;
using AuctionSystem.Models;
using AuctionSystem.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



[TestClass]
public class ZipTest
{
    private Mock<AuctionContext> db;
    private ZipControllerMock zipController;
    private Mock<DbSet<Zip>> mockSet;

    [TestInitialize]

    public void createData()
    {
        this.db = new Mock<AuctionContext>(); // creates the virtual database

        this.zipController = new ZipControllerMock(this.db.Object);

        var zip = new Zip
        {
            ZipId = 1,
            ZipCode = "9",
            Country = "Den",
            City = "Aal"
        };


        var data = new List<Zip> { zip };

        this.mockSet = new Mock<DbSet<Zip>>().SetupData(data); // prepare the virtual zip 

        this.db.Setup(z => z.Zips).Returns(this.mockSet.Object);

    }

    //CREATE ZIP

    [TestMethod]
    public void CreateZipShouldReturnTrue()
    {

        //Act
        this.zipController.AddZip("9000", "Denmark", "Aalborg");
        var currentZip = this.zipController.GetZipByZipCode("9000");

        //Assert

        Assert.AreEqual("9000", currentZip.ZipCode);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithEmptyZipCodeParametersShouldThrowException()
    {

        this.zipController.AddZip("", "Denmark", "Aalborg");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithEmptyCountryParametersShouldThrowException()
    {

        this.zipController.AddZip("9000", "", "Aalborg");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithEmptyCityParametersShouldThrowException()
    {

        this.zipController.AddZip("9000", "Denmark", "");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithNullZipCodeParametersShouldThrowException()
    {

        this.zipController.AddZip(null, "Denmark", "Aalborg");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithNullCountryParametersShouldThrowException()
    {

        this.zipController.AddZip("9000", null, "Aalborg");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithNullCityParametersShouldThrowException()
    {

        this.zipController.AddZip("9000", "Denmark", null);

    }


    //GET ZIP

    [TestMethod]
    public void GetZipByZipCodeShouldPass()
    {
        //Act
        var actual = this.zipController.GetZipByZipCode("9");
        var expected = this.db.Object.Zips.FirstOrDefault(z => z.ZipCode == "9");
        //Assert
        Assert.AreEqual(expected, actual);
    }


    [TestMethod]

    public void GetZipByZipCodeWithNonExistingZipCodeShouldReturnNull()
    {

        var zipCode = this.zipController.GetZipByZipCode("9999999999999");

        Assert.IsNull(zipCode);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetZipWithEmptyZipCodeParametersShouldThrowException()
    {

        this.zipController.GetZipByZipCode("");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetZipWithNullZipCodeParametersShouldThrowException()
    {

        this.zipController.GetZipByZipCode(null);

    }


    //IS ZIP EXISTING

    [TestMethod]
    public void IsZipExistingShouldPass()
    {
        //Act
        var actual = this.zipController.IsZipExisting(1);

        //Assert
        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void IsZipExistingShouldReturnFalse()
    {
        //Act
        var actual = this.zipController.IsZipExisting(2);

        //Assert
        Assert.IsFalse(actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IsZipExistingWithEmptyShouldThrowException()
    {

        this.zipController.GetZipByZipCode("");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IsZipExistingWithNullShouldThrowException()
    {

        this.zipController.GetZipByZipCode(null);

    }
    

    //UPDATE ZIP

        //UPDATING THE ZIPCODE PROPERTY
    [TestMethod]
    public void UpdateZipCodeShouldReturnTrue()
    {
        var zip = this.db.Object.Zips.First(z => z.ZipCode == "9");
        var successUpdate = this.zipController.UpdateZip("9", "ZipCode", "9000");

        Assert.IsTrue(successUpdate);
    }

    [TestMethod]
    public void UpdateZipCodeShouldPassIfZipCodeIsChanged()
    {
        var zip = this.db.Object.Zips.First(z => z.ZipCode == "9");
        var successUpdate = this.zipController.UpdateZip("9", "ZipCode", "9000");
        var updatedZipZipcode = this.db.Object.Zips.First(z => z.ZipCode == "9000").ZipCode;
        Assert.AreEqual("9000",updatedZipZipcode);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateZipWithEmptyZipCodeShouldThrowException()
    {

        this.zipController.UpdateZip("9","ZipCode","");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateZipWithNullZipCodeShouldThrowException()
    {

        this.zipController.UpdateZip(null, "Den", "Aal");

    }

    //UPDATE THE COUNTRY PROPERTY

    [TestMethod]
   
    public void UpdateZipCountryShouldReturnTrue()
    {
        var zip = this.db.Object.Zips.First(z => z.ZipCode == "9");
        var successUpdate = this.zipController.UpdateZip("9", "Country", "Sweden");

        Assert.IsTrue(successUpdate);
    }

    [TestMethod]
    public void UpdateZipCodeShouldPassIfCountryIsChanged()
    {
        var zip = this.db.Object.Zips.First(z => z.ZipCode == "9");
        var successUpdate = this.zipController.UpdateZip("9", "Country", "Sweden");
        var updatedZipCountry = this.db.Object.Zips.First(z => z.ZipCode == "9").Country;
        Assert.AreEqual("Sweden", updatedZipCountry);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateZipWithEmptyCountryShouldThrowException()
    {

        this.zipController.UpdateZip("9", "Country", "");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateZipWithNullCountryShouldThrowException()
    {

        this.zipController.UpdateZip("9", "Country", null);

    }

    //UPDATE THE CITY PROPERTY
    [TestMethod]
   
    public void UpdateZipCityShouldReturnTrue()
    {
        var zip = this.db.Object.Zips.First(z => z.ZipCode == "9");
        var successUpdate = this.zipController.UpdateZip("9", "City", "Aarhus");

        Assert.IsTrue(successUpdate);
    }

    [TestMethod]
    public void UpdateZipCodeShouldPassIfCityIsChanged()
    {
        var zip = this.db.Object.Zips.First(z => z.ZipCode == "9");
        var successUpdate = this.zipController.UpdateZip("9", "City", "Aarhus");
        var updatedZipCity = this.db.Object.Zips.First(z => z.ZipCode == "9").City;
        Assert.AreEqual("Aarhus", updatedZipCity);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateZipWithEmptyCityShouldThrowException()
    {
        
        this.zipController.UpdateZip("9", "City", "");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateZipWithNullCityShouldThrowException()
    {

        this.zipController.UpdateZip("9", "City", null);

    }



}

