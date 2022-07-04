using System.Net;
using System.Text;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class RequestService
    {
        #if false
            private readonly string url = "https://localhost:7164/";
        #else
            private readonly string url = "https://daniellapi.azurewebsites.net/";
        #endif
        // public string Get(string api, string method){
        //     // Create a request for the URL.
        //     WebRequest request = WebRequest.Create(url + api + "/" + method);
        //     request.Credentials = CredentialCache.DefaultCredentials;
        //     WebResponse response = request.GetResponse();
        //     string ResponseString = "";
        //     using (Stream dataStream = response.GetResponseStream())
        //     {
        //         StreamReader reader = new StreamReader(dataStream);
        //         ResponseString = reader.ReadToEnd();
        //     }
        //     response.Close();
        //     Console.WriteLine($"{url}{api}/{method} {ResponseString}");
        //     return ResponseString;
        // }

        // public string Post(string api, string method, object param){
        //     WebRequest request = WebRequest.Create(url + api + "/" + method);
        //     request.Method = "POST";
        //     request.Credentials = CredentialCache.DefaultCredentials;
        //     string postData = JsonConvert.SerializeObject(param, new JsonSerializerSettings(){NullValueHandling = NullValueHandling.Ignore});
        //     Console.WriteLine($"{url}{api}/{method}");
        //     byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        //     request.ContentType = "application/json";
        //     request.ContentLength = byteArray.Length;
        //     using(Stream dataStream = request.GetRequestStream()){
        //         dataStream.Write(byteArray, 0, byteArray.Length);
        //     }
        //     WebResponse response = request.GetResponse();
        //     string s = "";
        //     using (Stream dataStream = response.GetResponseStream())
        //     {
        //         StreamReader reader = new StreamReader(dataStream);
        //         s = reader.ReadToEnd();
        //     }
        //     response.Close();
        //     return s;
        // }

        public async Task<string> Get(string api, string method)
        {
            using(HttpClient client = new HttpClient())
            {
                string route = url + api + "/" + method;
                Console.WriteLine($"(Get) {route}");
                HttpResponseMessage responseMessage = await client.GetAsync(route);
                responseMessage.EnsureSuccessStatusCode();
                string responseBody = await responseMessage.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        public async Task<string> Post(string api, string method, object param)
        {
            using(HttpClient client = new HttpClient())
            {
                string route = url + api + "/" + method;
                Console.WriteLine($"(Post) {route}");
                var jsonString = JsonConvert.SerializeObject(param);
                var data = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(route, data);
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
    }

    public class ResponseService
    {
        public T GetData<T>(string ResponseString)
        {
            Console.WriteLine(JsonConvert.DeserializeObject(ResponseString)); // 印出反序列化結果
            var obj = JsonConvert.DeserializeObject<T>(ResponseString, new JsonSerializerSettings{
                NullValueHandling = NullValueHandling.Ignore
            });
            return obj;
        }
    }
    
}    