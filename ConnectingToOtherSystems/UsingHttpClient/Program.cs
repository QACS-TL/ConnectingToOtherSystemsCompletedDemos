using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Calling_APIs_Demo
{

    internal class Program
    {
        static void Main(string[] args)
        {
            //Outdoor cinema weather check
            // Create an HTTP client (should only have one of these for whole application)
            var http = new HttpClient();

            //// Send a GET request and process the response as JSON, using C# dynamic objects
            string city = "Leeds";
            string url = $"https://weather-api.qaalabs.com/api/weather/{city}";
            string json = http.GetStringAsync(url).Result;
            dynamic obj = JsonConvert.DeserializeObject(json);
            string temp = obj["TemperatureInCelsius"] >= 15 ? "warm" : "cold";
            Console.WriteLine($"The weather for the outdoor cinema event " +
                $"in {obj["City"]} is {obj["WeatherDescription"]} and it will be {temp}.");

            //// Send a POST request with movie blog data to an API, and collect the response
            //var data = new
            //{
            //    title = "my movie blog post",
            //    body = "Apollo 10.5 is a great movie. I'd rate it at 10 out of 10",
            //    userId = 101
            //};
            //string dataJson = JsonConvert.SerializeObject(data);
            //url = "https://jsonplaceholder.typicode.com/posts";
            //HttpResponseMessage response = http.PostAsync(url, 
            //        new StringContent(dataJson, Encoding.UTF8, "application/json")).Result;
            //string responseJson = response.Content.ReadAsStringAsync().Result;
            //dynamic responseData = JsonConvert.DeserializeObject(responseJson);
            //Console.WriteLine($"New post has ID {responseData["id"]}");
            //Console.WriteLine($"New post has title text of {responseData["title"]}");
            //Console.WriteLine($"New post has body text of {responseData["body"]}");

            // Sending headers with request, and extracting headers from response
            var data = new
            {
                title = "My movie blog post - Up",
                body = "Up is an uplifting film",
                userId = 101
            };
            string dataJson = JsonConvert.SerializeObject(data);
            url = "https://jsonplaceholder.typicode.com/posts";
            HttpContent content = new StringContent(dataJson, Encoding.UTF8, "application/json");
            content.Headers.Add("movie_title", "Up");
            content.Headers.Add("rating", "10");
            HttpResponseMessage response = http.PostAsync(url, content).Result;
            string responseJson = response.Content.ReadAsStringAsync().Result;
            dynamic responseData = JsonConvert.DeserializeObject(responseJson);
            Console.WriteLine($"New post has ID {responseData["id"]}");
            Console.WriteLine($"New post has title text of {responseData["title"]}");
            Console.WriteLine($"New post has body text of {responseData["body"]}");
            // NOTE: the typicode site does not do anything with the additional header content and so
            // it does not get returned in the repsonse
            foreach (var header in response.Headers)
            {
                Console.WriteLine($"{header.Key} = {header.Value.First()}");
            }

            CreateAndPostMovieBlog();
        }


        public class Post
        {
            public int Id { get; set; }
            public int userId { get; set; }
            public string title { get; set; }
            public string body { get; set; }
        }

        public static void CreateAndPostMovieBlog()
        {
            string movieBlogPost = "title=Jurassic Park";
            movieBlogPost = movieBlogPost + "&body=The Wonkymotion version of Jurassic Park is a great movie. I'd rate it at 11 out of 10";
            movieBlogPost = movieBlogPost + "&userId=101";
            var encodedPost = Encoding.UTF8.GetBytes(movieBlogPost);
            var webRequest = WebRequest.CreateHttp("http://jsonplaceholder.typicode.com/posts");
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = encodedPost.Length;
            webRequest.UserAgent = "WebRequestDemo";
            //Write the Post data for the stream
            using (var stream = webRequest.GetRequestStream())
            {
                stream.Write(encodedPost, 0, encodedPost.Length);
                stream.Close();
            }
            //Read and display the response
            using (var response = webRequest.GetResponse())
            {
                var responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                object objResponse = reader.ReadToEnd();
                var post = JsonConvert.DeserializeObject<Post>(objResponse.ToString());
                Console.WriteLine(post.Id + " " + post.title + ": " + post.body);
                responseStream.Close();
                response.Close();
            }
            Console.ReadLine();
        }
    }
}

