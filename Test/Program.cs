using BusinessService.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonContent = JsonConvert.SerializeObject(
              new
              {
                  email = "343688972@qq.com",
                  password = "111111",
                  token = "59ea927e-3a4a-4387-97c4-158071e31fce",
                  userID = 20,
                  id = 1
              }
              );

            //HttpClient clinet = new HttpClient();
            //MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();

            //HttpContent content = new ObjectContent<string>(jsonContent, jsonFormatter);

            //var rsp = clinet.PostAsync(new Uri("http://172.17.10.196:9999/API/Event/PushMessage"), content);
            //var stream = rsp.Result.Content;
            //string jsonre = string.Empty;
            //using (var read = new StreamReader(stream.ReadAsStreamAsync().Result))
            //{
            //    jsonre = read.ReadToEnd();
            //}
            string json = string.Empty;
            using (var client = new HttpClient())
            {
                var res = client.PostAsync(new Uri("http://172.17.10.196:9999/API/User/Login"), new { email = "343688972@qq.com", password = "111111" }, new JsonMediaTypeFormatter());
                var stream = res.Result.Content;
               
                using (var read = new StreamReader(stream.ReadAsStreamAsync().Result))
                {
                    json = read.ReadToEnd();
                }

            }

            //var stream = clinet.GetAsync(new Uri("http://172.17.10.196:9999/API/User/GetUserInfo?userID=1")).Result.Content;
            //string jsonre = string.Empty;

            //using (var streamRead = new StreamReader(stream.ReadAsStreamAsync().Result))
            //{
            //    jsonre = streamRead.ReadToEnd();
            //    var a = JsonConvert.DeserializeObject(jsonre);
            //}


            Console.WriteLine(json);

            //new EventBLL().BirthdayRemind();
            Console.ReadKey();
        }
    }
}
