namespace WebApplication1.Models
{
    public class SqlModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class Chat_List
    {
        public List<SqlModel> list { get; set; }
    }

    public class Chat_New_Request
    {
        public string name { get; set; }
    }

    public class Chat_New_Response
    {
        public string result { get; set; }
        public string message { get; set; }
    }

    public class Chat_Delete_Request
    {
        public int id { get; set; }
    }

    public class Chat_Delete_Response
    {
        public string result { get; set; }
        public string message { get; set; }
    }

    public class Chat_Modify_Response
    {
        public string result { get; set; }
        public string message { get; set; }
    }

    public class Chat_Modify_Request
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}