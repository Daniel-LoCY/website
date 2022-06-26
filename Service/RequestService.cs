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
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            // Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            string s = "";
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                s = reader.ReadToEnd();
                // Display the content.
                // Console.WriteLine(responseFromServer);
            }
            // Close the response.
            response.Close();
            Console.WriteLine($"{api}/{method}");
            return s;
        }

        public string Post(string api, string method, object param){
            WebRequest request = WebRequest.Create(url + api + "/" + method);
            request.Method = "POST";
            request.Credentials = CredentialCache.DefaultCredentials;
            string postData = JsonConvert.SerializeObject(param, new JsonSerializerSettings(){NullValueHandling = NullValueHandling.Ignore});
            Console.WriteLine($"{api}/{method} {postData}");
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
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                s = reader.ReadToEnd();
                // Display the content.
                // Console.WriteLine(responseFromServer);
            }
            // Close the response.
            response.Close();
            return s;
        }
    }

}    