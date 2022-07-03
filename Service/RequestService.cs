using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace WebApplication1.Service
{
    public class RequestService
    {
        #if true
            private readonly string url = "https://localhost:7164/";
        #else
            private readonly string url = "https://daniellapi.azurewebsites.net/";
        #endif
        public string Get(string api, string method){
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(url + api + "/" + method);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            string ResponseString = "";
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                ResponseString = reader.ReadToEnd();
            }
            response.Close();
            Console.WriteLine($"{url}{api}/{method}/ {ResponseString}");
            return ResponseString;
        }

        public string Post(string api, string method, object param){
            WebRequest request = WebRequest.Create(url + api + "/" + method);
            request.Method = "POST";
            request.Credentials = CredentialCache.DefaultCredentials;
            string postData = JsonConvert.SerializeObject(param, new JsonSerializerSettings(){NullValueHandling = NullValueHandling.Ignore});
            Console.WriteLine($"{url}{api}/{method}");
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            using(Stream dataStream = request.GetRequestStream()){
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            WebResponse response = request.GetResponse();
            string s = "";
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                s = reader.ReadToEnd();
            }
            response.Close();
            return s;
        }
    }

}    