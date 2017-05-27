using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Net.Http;
using Newtonsoft.Json;

namespace BLL
{
    public class AccessWebAPI
    {
        public List<Weather> getAllWeather()
        {
            string uri = "http://localhost:56463/api/weather";

            using (HttpClient client = new HttpClient())
            {
                Task<string> response = client.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<List<Weather>>(response.Result);
            }
        }

        public List<Weather> getWeatherByCity(string cityName)
        {
            string uri = "http://localhost:56463/api/weather/" + cityName;

            using (HttpClient client = new HttpClient())
            {
                Task<string> response = client.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<List<Weather>>(response.Result);
            }
        }

        public City getCityByName(string cityName)
        {
            string uri = "http://localhost:56463/api/city/"+cityName;

            using (HttpClient client = new HttpClient())
            {
                Task<string> response = client.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<City>(response.Result);
            }

        }


        public bool PostWeather(Weather w)
        {
            string uri = "http://localhost:56463/api/weather/";

            using (HttpClient httpClient = new HttpClient())
            {
                string pro = JsonConvert.SerializeObject(w);
                StringContent frame = new StringContent(pro, Encoding.UTF8, "Application/json");
                Task<HttpResponseMessage> response = httpClient.PostAsync(uri, frame);
                return response.Result.IsSuccessStatusCode;
            }
        }

        public bool DeleteWeather(int id)
        {
            string uri = "http://localhost:56463/api/weather/"+id;
            using (HttpClient httpClient = new HttpClient())
            {
                Task<HttpResponseMessage> response = httpClient.DeleteAsync(uri);
                return response.Result.IsSuccessStatusCode;
            }
        }

        public bool PutWeather(Weather w)
        {
            string uri = "http://localhost:56463/api/weather/";

            using (HttpClient httpClient = new HttpClient())
            {
                string pro = JsonConvert.SerializeObject(w);
                StringContent frame = new StringContent(pro, Encoding.UTF8, "Application/json");
                Task<HttpResponseMessage> response = httpClient.PutAsync(uri, frame);
                return response.Result.IsSuccessStatusCode;
            }
        }

    }
}
