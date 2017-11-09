using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using CityExperiences.Models;

namespace CityExperiences.Tests
{
    [TestClass]
    public class TagTests : IDisposable
    {
        public TagTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=cityexperiences_test;";
        }

        public void Dispose()
        {
            Tag.DeleteAll();
            // Experience.DeleteAll();
        }

        [TestMethod]
        public void GetAll_TagsEmptyAtFirst_0()
        {
            //Arrange, Act
            int result = Tag.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_OverrideTrueIfTagNamesAreTheSame_Tag()
        {
            // Arrange, Act
            Tag firstTag = new Tag("Diving");
            Tag secondTag = new Tag("Diving");

            // Assert
            Assert.AreEqual(firstTag, secondTag);
        }

        [TestMethod]
        public void Save_SavesTagsToDatabase_TagList()
        {
            //Arrange
            Tag testTag = new Tag("Diving");

            //Act
            testTag.Save();
            List<Tag> result = Tag.GetAll();
            List<Tag> testList = new List<Tag>{testTag};

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_Id()
        {
            //Arrange
            Tag testTag = new Tag("Diving");

            //Act
            testTag.Save();
            Tag savedTag = Tag.GetAll()[0];

            int result = savedTag.GetId();
            int testId = testTag.GetId();

            //Assert
            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsTagInDatabase_Tag()
        {
            //Arrange
            Tag testTag = new Tag("Diving");
            testTag.Save();

            //Act
            Tag foundTag = Tag.Find(testTag.GetId());

            //Assert
            Assert.AreEqual(testTag, foundTag);
        }

        [TestMethod]
        public void Update_UpdatesTagInDatabase_String()
        {
            //Arrange
            Tag testTag = new Tag("Diving");
            testTag.Save();
            string newName = "SkyDiving";

            //Act
            testTag.UpdateTagName(newName);

            string result = Tag.Find(testTag.GetId()).GetTagName();

            //Assert
            Assert.AreEqual(newName, result);
        }

        // [TestMethod]
        // public void Delete_DeletesTagAssociationsFromDatabase_TagList()
        // {
        //     //Arrange
        //     Experience testExperience = new Experience("SkyDiving");
        //     testExperience.Save();
        //
        //     string testName = "Diving";
        //     Tag testTag = new Tag(testName);
        //     testTag.Save();
        //
        //     //Act
        //     testTag.AddExperience(testExperience);
        //     testTag.DeleteTag();
        //
        //     List<Tag> resultExperienceTags = testExperience.GetTags();
        //     List<Tag> testExperienceTags = new List<Tag> {};
        //
        //     //Assert
        //     CollectionAssert.AreEqual(testExperienceTags, resultExperienceTags);
        // }
        //
        // [TestMethod]
        // public void AddExperience_AddsExperienceToTag_ExperienceList()
        // {
        //     //Arrange
        //     Tag testTag = new Tag("Diving");
        //     testTag.Save();
        //
        //     Experience testExperience = new Experience("SkyDiving");
        //     testExperience.Save();
        //
        //     //Act
        //     testTag.AddExperience(testExperience);
        //
        //     List<Experience> result = testTag.GetExperiences();
        //     List<Experience> testList = new List<Experience>{testExperience};
        //
        //     //Assert
        //     CollectionAssert.AreEqual(testList, result);
        // }
        //
        // [TestMethod]
        // public void GetExperiences_ReturnsAllTagExperiences_ExperienceList()
        // {
        //     //Arrange
        //     Tag testTag = new Tag("Diving");
        //     testTag.Save();
        //
        //     Experience testExperience1 = new Experience("SkyDiving");
        //     testExperience1.Save();
        //
        //     Experience testExperience2 = new Experience("Swimming");
        //     testExperience2.Save();
        //
        //     //Act
        //     testTag.AddExperience(testExperience1);
        //     List<Experience> result = testTag.GetExperiences();
        //     List<Experience> testList = new List<Experience> {testExperience1};
        //
        //     //Assert
        //     CollectionAssert.AreEqual(testList, result);
        // }
    }
}
