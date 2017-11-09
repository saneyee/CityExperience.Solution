using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using CityExperiences.Models;

namespace CityExperiences.Tests
{
  [TestClass]
  public class ExperienceTests : IDisposable
  {
    public ExperienceTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=cityexperiences_test;";
    }
    public void Dispose()
    {
      Experience.DeleteAll();
    }

    [TestMethod]
    public void GetAll_ExperiencesEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Experience.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_OverrideTrueForSameTitle_Experience()
    {
      //Arrange, Act
      Experience firstExperience = new Experience(1, 2, "Sky Diving", "Blah Blah..", "/hjfsddjf.com", 150);
      Experience secondExperience = new Experience(1, 2, "Sky Diving", "Blah Blah..", "/hjfsddjf.com", 150);

      //Assert
      Assert.AreEqual(firstExperience, secondExperience);
    }

    [TestMethod]
    public void Save_SavesToDatabase_Experience()
    {
      //Arrange
      Experience testExperience = new Experience(1, 2, "Sky Diving", "Blah Blah..", "/hjfsddjf.com", 150);

      //Act
      testExperience.Save();
      List<Experience> result = Experience.GetAll();
      List<Experience> testList = new List<Experience>{testExperience};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_id()
    {
      //Arrange
      Experience testExperience = new Experience(1, 2, "Sky Diving", "Blah Blah..", "/hjfsddjf.com", 150);
      testExperience.Save();

      //Act
      Experience savedExperience = Experience.GetAll()[0];

      int result = savedExperience.GetId();
      int testId = testExperience.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsExperienceInDatabase_Experience()
    {
      //Arrange
      Experience testExperience = new Experience(1, 2, "Sky Diving", "Blah Blah..", "/hjfsddjf.com", 150);
      testExperience.Save();

      //Act
      Experience result = Experience.Find(testExperience.GetId());

      //Assert
      Assert.AreEqual(testExperience, result);
    }

    [TestMethod]
    public void Update_UpdatesDescriptionInDatabase_String()
    {
      // Arrange
      Experience testExperience = new Experience(1, 2, "Sky Diving", "Blah Blah..", "/hjfsddjf.com", 150);
      testExperience.Save();
      string newDescription = "Bungee jumping";

      // Act
      testExperience.UpdateDescription(newDescription);

      string result = Experience.Find(testExperience.GetId()).GetDescription();

      // Assert
      Assert.AreEqual(newDescription, result);
    }

    [TestMethod]
    public void Update_UpdatesPriceInDatabase_Int()
    {
      // Arrange
      Experience testExperience = new Experience(1, 2, "Sky Diving", "Blah Blah..", "/hjfsddjf.com", 150);
      testExperience.Save();
      int newPrice = 180;

      // Act
      testExperience.UpdatePrice(newPrice);

      int result = Experience.Find(testExperience.GetId()).GetPrice();

      // Assert
      Assert.AreEqual(newPrice, result);
    }
  }
}
