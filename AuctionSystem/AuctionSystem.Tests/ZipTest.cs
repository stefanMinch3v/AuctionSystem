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

    public void CreateData()
    {
        this.db = new Mock<AuctionContext>(); // creates the virtual database

        this.zipController = new ZipControllerMock(this.db.Object);

        var zip = GetZipFromDb();

        var data = new List<Zip> { zip };

        this.mockSet = new Mock<DbSet<Zip>>().SetupData(data); // prepare the virtual zip 

        this.db.Setup(z => z.Zips).Returns(this.mockSet.Object);

    }

    private Zip GetZipFromDb()
    {
        return new Zip
        {
            Id = 1,
            ZipCode = "9",
            Country = "Den",
            City = "Aal"
        };
    }

    private Zip GetZipNotFromDb()
    {
        return new Zip
        {
            Id = 2,
            ZipCode = "9000",
            Country = "Denmark",
            City = "Aalborg"
        };
    }

    //CREATE ZIP

    [TestMethod]
    public void CreateZipShouldReturnTrue()
    {

        //Act
        var zip = GetZipNotFromDb();
        this.zipController.AddZip(zip);
        var currentZip = this.zipController.GetZipByZipCode("9000");

        //Assert

        Assert.AreEqual("9000", currentZip.ZipCode);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithEmptyZipCodeParametersShouldThrowException()
    {
        var zip = GetZipNotFromDb();
        zip.ZipCode = "";

        this.zipController.AddZip(zip);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithEmptyCountryParametersShouldThrowException()
    {
        var zip = GetZipNotFromDb();
        zip.Country = "";

        this.zipController.AddZip(zip);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithEmptyCityParametersShouldThrowException()
    {
        var zip = GetZipNotFromDb();
        zip.City = "";

        this.zipController.AddZip(zip);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithNullZipCodeParametersShouldThrowException()
    {
        var zip = GetZipNotFromDb();
        zip.ZipCode = null;

        this.zipController.AddZip(zip);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithNullCountryParametersShouldThrowException()
    {
        var zip = GetZipNotFromDb();
        zip.Country = null;

        this.zipController.AddZip(zip);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithNullCityParametersShouldThrowException()
    {
        var zip = GetZipNotFromDb();
        zip.City = null;

        this.zipController.AddZip(zip);
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
        var zip = GetZipFromDb().Id;
        var actual = this.zipController.IsZipExisting(zip);

        //Assert
        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void IsZipExistingShouldReturnFalse()
    {
        //Act
        var zip = GetZipNotFromDb().Id;
        var actual = this.zipController.IsZipExisting(zip);

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
        var successUpdate = this.zipController.UpdateZip(zip, "ZipCode", "9000");

        Assert.IsTrue(successUpdate);
    }

    [TestMethod]
    public void UpdateZipCodeShouldPassIfZipCodeIsChanged()
    {
        var zip = this.db.Object.Zips.First(z => z.ZipCode == "9");
        var successUpdate = this.zipController.UpdateZip(zip, "ZipCode", "9000");
        var updatedZipZipcode = this.db.Object.Zips.First(z => z.ZipCode == "9000").ZipCode;

        Assert.AreEqual("9000",updatedZipZipcode);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateZipWithEmptyZipCodeShouldThrowException()
    {
        var zip = GetZipFromDb();

        this.zipController.UpdateZip(zip, "ZipCode","");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void UpdateZipWithNullZipCodeShouldThrowException()
    {

        this.zipController.UpdateZip(null, "Den", "Aal");

    }

    //UPDATE THE COUNTRY PROPERTY

    [TestMethod]
   
    public void UpdateZipCountryShouldReturnTrue()
    {
        var zip = this.db.Object.Zips.First(z => z.ZipCode == "9");
        var successUpdate = this.zipController.UpdateZip(zip, "Country", "Sweden");

        Assert.IsTrue(successUpdate);
    }

    [TestMethod]
    public void UpdateZipCodeShouldPassIfCountryIsChanged()
    {
        var zip = this.db.Object.Zips.First(z => z.ZipCode == "9");
        var successUpdate = this.zipController.UpdateZip(zip, "Country", "Sweden");
        var updatedZipCountry = this.db.Object.Zips.First(z => z.ZipCode == "9").Country;

        Assert.AreEqual("Sweden", updatedZipCountry);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateZipWithEmptyCountryShouldThrowException()
    {
        var zip = GetZipFromDb();

        this.zipController.UpdateZip(zip, "Country", "");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateZipWithNullCountryShouldThrowException()
    {
        var zip = GetZipFromDb();

        this.zipController.UpdateZip(zip, "Country", null);

    }

    //UPDATE THE CITY PROPERTY
    [TestMethod]
   
    public void UpdateZipCityShouldReturnTrue()
    {
        var zip = this.db.Object.Zips.First(z => z.ZipCode == "9");
        var successUpdate = this.zipController.UpdateZip(zip, "City", "Aarhus");

        Assert.IsTrue(successUpdate);
    }

    [TestMethod]
    public void UpdateZipCodeShouldPassIfCityIsChanged()
    {
        var zip = this.db.Object.Zips.First(z => z.ZipCode == "9");
        var successUpdate = this.zipController.UpdateZip(zip, "City", "Aarhus");
        var updatedZipCity = this.db.Object.Zips.First(z => z.ZipCode == "9").City;

        Assert.AreEqual("Aarhus", updatedZipCity);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateZipWithEmptyCityShouldThrowException()
    {
        var zip = GetZipFromDb();

        this.zipController.UpdateZip(zip, "City", "");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateZipWithNullCityShouldThrowException()
    {
        var zip = GetZipFromDb();

        this.zipController.UpdateZip(zip, "City", null);

    }



}

