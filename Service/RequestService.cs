using System.Net;

namespace WebApplication1.Service{
    
    public class RequestService{
        public string Get(string url = "default"){
            Console.WriteLine(url);
            // Create a request for the URL.
            WebRequest request = WebRequest.Create("http://20.80.46.248:5000/static/chat.txt");
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
    }

}    