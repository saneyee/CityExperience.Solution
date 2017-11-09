using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using CityExperiences.Models;
using CityExperiences;

namespace CityExperiences.Controllers
{
  public class HomeController : Controller
  {
    //GUEST LANDING PAGE
    [HttpGet("/")]
    public ActionResult Index()
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();
      List<Experience> newestExperiences = Experience.GetAll();
      List<City> allCities = City.GetAll();

      model.Add("newest-experiences", newestExperiences);
      model.Add("all-cities", allCities);

      return View("Index", model);
    }

    //USER LANDING PAGE
    [HttpGet("/user/{userId}/home")]
    public ActionResult IndexUser(int userId)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();
      Person thisPerson = Person.Find(userId);
      List<Experience> newestExperiences = Experience.GetAll();
      List<City> allCities = City.GetAll();

      model.Add("user", thisPerson);
      model.Add("newest-experiences", newestExperiences);
      model.Add("all-cities", allCities);

      return View("IndexUser", model);
    }

    //SEARCH EXPERIENCES BY TAG
    [HttpPost("/experiences/tag/search")]
    public ActionResult ViewTagExperiences()
    {
      string thistag = Request.Form["tag-name"];
      Tag tagSearch = Tag.FindId(thistag.ToLower());
      Console.WriteLine(tagSearch);
      List<Experience> allTagExperiences = tagSearch.GetTagExperiences();

      return View("TagExperiences", allTagExperiences);
    }

    //SEARCH EXPERIENCES BY TAG WITH USER LOGIN
    [HttpPost("/user/{userId}/experiences/tag/search")]
    public ActionResult ViewTagExperiences(int userId)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();

      Person thisPerson = Person.Find(userId);
      string thistag = Request.Form["tag-name"];
      Tag tagSearch = Tag.FindId(thistag.ToLower());
      List<Experience> allTagExperiences = tagSearch.GetTagExperiences();

      model.Add("user", thisPerson);
      model.Add("experiences", allTagExperiences);

      return View("TagExperiencesUser", model);
    }

    //SEARCH EXPERIENCES BY CITY
    [HttpPost("/experiences/city/search")]
    public ActionResult ViewCityExperiences()
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();

      string thiscity = Request.Form["city-name"];
      City citySearch = City.FindId(thiscity);
      List<Experience> allCityExperiences = citySearch.GetCityExperiences();

      model.Add("city", citySearch);
      model.Add("experiences", allCityExperiences);

      return View("CityExperiences", model);
    }

    //User Books an experiences
    [HttpGet("/user/{userId}/experience/{experienceId}/book")]
    public ActionResult BookExperience(int userId, int experienceId)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();

      Person thisPerson = Person.Find(userId);
      Experience thisExperience = Experience.Find(experienceId);
      Booking newBooking = new Booking(userId, experienceId);
      newBooking.Save();

      model.Add("user", thisPerson);
      model.Add("experience", thisExperience);
      model.Add("booking", newBooking);

      return View("BookingConfirm", model);
    }

    //VIEW EXPERIENCES BY CITY WITHOUT USER LOGIN
    [HttpGet("/city/{cityId}/view")]
    public ActionResult ViewCityExperiences(int cityId)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();

      City citySearch = City.Find(cityId);
      List<Experience> allCityExperiences = citySearch.GetCityExperiences();

      model.Add("city", citySearch);
      model.Add("experiences", allCityExperiences);

      return View("CityExperiences", model);
    }

    //VIEW EXPERIENCES BY CITY WITH USER LOGIN THROUGH SEARCH FUNCTION
    [HttpPost("/user/{userId}/experiences/city/search")]
    public ActionResult SearchCityExperiences(int userId)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();

      Person thisPerson = Person.Find(userId);
      City citySearch = City.FindId(Request.Form["city-name"]);
      List<Experience> allCityExperiences = citySearch.GetCityExperiences();

      model.Add("user", thisPerson);
      model.Add("city", citySearch);
      model.Add("experiences", allCityExperiences);

      return View("CityExperiencesUser", model);
    }

    //VIEW EXPERIENCES BY CITY WITH USER LOGIN
    [HttpGet("/user/{userId}/city/{cityId}/view")]
    public ActionResult ViewCityExperiences(int userId, int cityId)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();

      Person thisPerson = Person.Find(userId);
      City citySearch = City.Find(cityId);
      List<Experience> allCityExperiences = citySearch.GetCityExperiences();

      model.Add("user", thisPerson);
      model.Add("city", citySearch);
      model.Add("experiences", allCityExperiences);

      return View("CityExperiencesUser", model);
    }

    [HttpGet("/city/{cityId}/experience/{experienceId}/view")]
    public ActionResult ViewExperienceFromCity(int experienceId)
    {

      Experience thisExperience = Experience.Find(experienceId);

      return View("ViewExperience", thisExperience);
    }

    //VIEW AN EXPERIENCE FROM CITY WITH USER LOGIN
    [HttpGet("/user/{userId}/city/{cityId}/experience/{experienceId}/view")]
    public ActionResult ViewCityExperience(int userId, int cityId, int experienceId)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();

      Person thisPerson = Person.Find(userId);
      City citySearch = City.Find(cityId);
      Experience thisExperience = Experience.Find(experienceId);
      model.Add("user", thisPerson);
      model.Add("city", citySearch);
      model.Add("experience", thisExperience);

      return View("ViewExperienceUser", model);
    }

    [HttpGet("/user/{userId}/experience/{experienceId}/view")]
    public ActionResult ViewExperienceUser(int userId, int experienceId)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();

      Person thisPerson = Person.Find(userId);
      Experience thisExperience = Experience.Find(experienceId);

      model.Add("user", thisPerson);
      model.Add("experience", thisExperience);

      return View("ViewExperienceUser", model);
    }


    [HttpGet("/experience/{experienceId}/view")]
    public ActionResult ViewExperience(int experienceId)
    {

      Experience thisExperience = Experience.Find(experienceId);

      return View("ViewExperience", thisExperience);
    }

    [HttpGet("/experiences/tag/experience/{experienceId}/view")]
    public ActionResult ViewExperienceViaTag(int experienceId)
    {

      Experience thisExperience = Experience.Find(experienceId);

      return View("ViewExperience", thisExperience);
    }

    [HttpGet("/experiences/city/experience/{experienceId}/view")]
    public ActionResult ViewExperienceViaCity(int experienceId)
    {

      Experience thisExperience = Experience.Find(experienceId);

      return View("ViewExperience", thisExperience);
    }

    //USER PROFILE
    [HttpGet("/user/{userId}/profile")]
    public ActionResult UserProfile(int userId)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();

      Person thisPerson = Person.Find(userId);
      List<Experience> userListings = thisPerson.GetPersonListings();
      List<Experience> userBookings = thisPerson.GetPersonBookings();

      model.Add("user", thisPerson);
      model.Add("listings", userListings);
      model.Add("bookings", userBookings);

      return View(model);
    }

    //User Login PAGE
    [HttpGet("/login")]
    public ActionResult LoginPage()
    {
      return View("Login");
    }

    //User Login Logic
    [HttpPost("/user/home/login")]
    public ActionResult Login()
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();

      string username  = Request.Form["username"];
      string password = Request.Form["password"];

      List<Person> allUsers = Person.GetAll();


      foreach (var person in allUsers)
      {
        if(person.GetName() == username && person.GetPassword() == password)
        {
          List<Experience> newestExperiences = Experience.GetAll();
          List<City> allCities = City.GetAll();

          model.Add("user", person);
          model.Add("newest-experiences", newestExperiences);
          model.Add("all-cities", allCities);

          return View("IndexUser", model);
        }

      }
      return View("Login");
    }

    // User Signup Page
    [HttpGet("/signup")]
    public ActionResult SignUp()
    {
      return View();
    }

    //User Object Created bringing Users into User Enabled Index Page.
    [HttpPost("/user/home/signup")]
    public ActionResult SignUpPost()
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();

      Person newPerson = new Person(Request.Form["name"], Request.Form["dob"], Request.Form["country"], Request.Form["email"], Request.Form["phone"], Request.Form["password"]);
      List<Experience> allExperiences = Experience.GetAll();
      List<City> allCities = City.GetAll();

      newPerson.Save();
      model.Add("user", newPerson);
      model.Add("newest-experiences", allExperiences);
      model.Add("all-cities", allCities);

      return View("IndexUser", model);
    }



    [HttpGet("/user/{userId}/experience/new")]
    public ActionResult CreateExperience(int userId)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();
      Person thisPerson = Person.Find(userId);
      List<City> allCities = City.GetAll();

      model.Add("user", thisPerson);
      model.Add("cities", allCities);

      return View(model);
    }

    [HttpPost("/user/{userId}/experience/add")]
    public ActionResult AddViewExperience(int userId)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();

      Person thisPerson = Person.Find(userId);
      int UserId = userId;

      Experience newExperience = new Experience(Int32.Parse(Request.Form["experience-location"]),
      UserId, Request.Form["experience-title"], Request.Form["experience-description"], Request.Form["experience-photo"], Int32.Parse(Request.Form["experience-price"]));
      newExperience.Save();
      int TagValue = Int32.Parse(Request.Form["number-loop"]);
      for(var i=1;i<=TagValue;i++)
      {
        Console.WriteLine("tag"+Request.Form["tag-name"+i]);
        Tag newTag = new Tag(Request.Form["tag-name"+i]);
        if(newTag.IsNewTag() == true)
        {

          newTag.Save();
          newExperience.AddTag(newTag);
        }
        else
        {
          Tag repeatTag = newTag.FindTag();
          newExperience.AddTag(repeatTag);
        }
      }
      List<Experience> newestExperiences = Experience.GetAll();
      List<City> allCities = City.GetAll();

      model.Add("user", thisPerson);
      model.Add("newest-experiences", newestExperiences);
      model.Add("all-cities", allCities);


      return View("IndexUser", model);
    }

    [HttpGet("/user/{userId}/experience/{experienceId}/edit")]
    public ActionResult EditExperiences(int userId, int experienceId)
    {
      Person thisPerson = Person.Find(userId);
      Experience thisExperience = Experience.Find(experienceId);
      Dictionary<string, object> model = new Dictionary<string, object> ();

      model.Add("user", thisPerson);
      model.Add("experience", thisExperience);

      return View("EditExperience", model);
    }

    [HttpPost("/user/{userId}/experience/{experienceId}/edit")]
    public ActionResult EditExperience(int userId, int experienceId)
    {
      Person thisPerson = Person.Find(userId);
      Dictionary<string, object> model = new Dictionary<string, object> ();
      Experience thisExperience = Experience.Find(experienceId);

      thisExperience.UpdateDescription(Request.Form["experience-description"]);
      thisExperience.UpdatePhoto(Request.Form["experience-photo"]);
      thisExperience.UpdatePrice(Int32.Parse(Request.Form["experience-price"]));

      model.Add("user", thisPerson);
      model.Add("experience", thisExperience);

      return View("ViewExperienceUser", model);
    }

  }
}
