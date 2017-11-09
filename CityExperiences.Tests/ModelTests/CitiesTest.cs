using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using CityExperiences.Models;

namespace CityExperiences.Tests
{
  [TestClass]
  public class CityTests : IDisposable
  {
    public CityTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=cityexperiences_test;";
    }
    public void Dispose()
    {
      City.DeleteAll();
    }

    [TestMethod]
    public void Equals_OverrideTrueForSameName_City()
    {
      //Arrange, Act
      City firstCity = new City("Denver");
      City secondCity = new City("Denver");

      //Assert
      Assert.AreEqual(firstCity, secondCity);
    }
  }
}
