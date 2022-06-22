using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace WebApplication1.Service{
    
    public class RequestService{
        public string Get(string url = "http://20.80.46.248:5000/static/chat.txt"){
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(url);
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

            return s;
        }

        public string Post(object param, string url){
            WebRequest request = WebRequest.Create("https://daniellapi.azurewebsites.net/" + url);
            request.Method = "POST";
            request.Credentials = CredentialCache.DefaultCredentials;
            string postData = JsonConvert.SerializeObject(param, new JsonSerializerSettings(){NullValueHandling = NullValueHandling.Ignore});
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