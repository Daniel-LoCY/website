using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.ViewModels
{
    public class ChatViewModel
    {
        public ChatViewModel()
        {
            Chat_List = new List<SqlModel>();
            AddSqlModel = new SqlModel();
            chat_New_Request = new Chat_New_Request();
        }
        public List<SqlModel> Chat_List { get; set; }
        public SqlModel AddSqlModel { get; set; }
        public Chat_New_Request chat_New_Request { get; set; }
    }
    

}