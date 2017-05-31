using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWeather.Controllers
{
    public class WeatherController : Controller
    {

        public ActionResult SearchWeather()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchWeather(string search)
        {
            search = search.ToLower();

            if (search.Equals(""))
            {
                return RedirectToAction("SearchWeather");
            }
            AccessWebAPI access = new AccessWebAPI();

            List<Weather> weathers = access.getWeatherByCity(search);


            if (weathers.Count == 0)
            {
                return RedirectToAction("SearchWeather");
            }
            GlobalController.searchCity = weathers.ElementAt(0).city.name;
            return RedirectToAction("WeatherCity", new { cityName = search });
        }

        public ActionResult NewWeather()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewWeather(int degreeMorning, int degreeAfternoon, int precipitation, int humidity, int wind, DateTime date)
        {

            if (date <= DateTime.Now)
            {
                ModelState.AddModelError("date", "Date must be after today");
                return RedirectToAction("NewWeather");
            }
            AccessWebAPI access = new AccessWebAPI();
            Weather w = new Weather { degreeMorning = degreeMorning, degreeAfternoon = degreeAfternoon, precipitation = precipitation, humidity = humidity, wind = wind, date = date };
            w.city = access.getCityByName(GlobalController.searchCity);

            access.PostWeather(w);


            return RedirectToAction("WeatherCity", new { cityName = GlobalController.searchCity });
        }

        public ActionResult AllWeathers()
        {
            AccessWebAPI access = new AccessWebAPI();
            return View(access.getAllWeather());
        }

        [HttpGet]
        [Route("Weather/WeatherCity/{cityName}")]
        public ActionResult WeatherCity(string cityName)
        {
            AccessWebAPI access = new AccessWebAPI();
            return View(access.getWeatherByCity(cityName));
        }

        public ActionResult DeleteWeather(int id)
        {
            AccessWebAPI access = new AccessWebAPI();
            access.DeleteWeather(id);
            return RedirectToAction("WeatherCity", new { cityName = GlobalController.searchCity });
        }

        public ActionResult EditWeather(int id)
        {
            AccessWebAPI access = new AccessWebAPI();
            Weather w = access.getWeatherByCity(GlobalController.searchCity).Where(we => we.id == id).First();
            return View(w);
        }

        [HttpPost]
        public ActionResult EditWeather(int id, int degreeMorning, int degreeAfternoon, int precipitation, int humidity, int wind, DateTime date)
        {
            AccessWebAPI access = new AccessWebAPI();
            Weather w = new Weather { id = id, degreeMorning = degreeMorning, degreeAfternoon = degreeAfternoon, precipitation = precipitation, humidity = humidity, wind = wind, date = date };
            w.city = access.getCityByName(GlobalController.searchCity);
            access.PutWeather(w);


            return RedirectToAction("WeatherCity", new { cityName = GlobalController.searchCity });
        }
    }
}
