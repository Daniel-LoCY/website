using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using WebApplication1.Service;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class ChatController : Controller
    {
        private RequestService requestService = new RequestService();
        private readonly string Connstr = "Data Source=daniell.database.windows.net;Initial Catalog=daniel;Persist Security Info=True;User ID=daniel;Password=5627Abcd;";
        public IActionResult Index()
        {
            var viewModel = new ChatViewModel();
            string response = requestService.Get("Chat", "list");
            Console.WriteLine(response);
            var data = JsonConvert.DeserializeObject<Chat_List>(response);
            viewModel.Chat_List = data.list;
            
            return View(viewModel);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New([Bind(Prefix = "chat_New_Request")] Chat_New_Request request)
        {
            var response = requestService.Post("Chat", "New", request);
            var data = JsonConvert.DeserializeObject<Chat_New_Response>(response);
            if(data.result == "success")
                TempData["success_message"] = "新增成功";
            else
                TempData["alert_message"] = "新增失敗";
            return RedirectToAction("Index", "Chat");
        }

        [HttpPost]
        public JsonResult Delete(Chat_Delete_Request request)
        {
            var response = requestService.Post("Chat", "Delete", request);
            Console.WriteLine(response);
            var data = JsonConvert.DeserializeObject<Chat_Delete_Response>(response);
            return Json(new{ result = data.result, message = data.message });
        }

        [HttpPost]
        public JsonResult Modify(Chat_Modify_Request request){
            var  response = requestService.Post("Chat", "Modify", request);
            Console.WriteLine(response);
            var data = JsonConvert.DeserializeObject<Chat_Modify_Response>(response);

            // if(data.result == "success")
            //     TempData["success_message"] = "修改成功";
            // else
            //     TempData["alert_message"] = "修改失敗";
            return Json(new {result = data.result});
        }
    }
}
