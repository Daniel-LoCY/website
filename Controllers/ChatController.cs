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
        public IActionResult Index()
        {
            var viewModel = new ChatViewModel();
            string response = requestService.Get("Chat", "list");
            var data = JsonConvert.DeserializeObject<Chat_List>(response);
            viewModel.Chat_List = data.list;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult New([Bind(Prefix = "chat_New_Request")] Chat_New_Request request)
        {
            var response = requestService.Post("Chat", "new", request);
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
            var response = requestService.Post("Chat", "delete", request);
            var data = JsonConvert.DeserializeObject<Chat_Delete_Response>(response);
            return Json(new{ result = data.result, message = data.message });
        }

        [HttpPost]
        public JsonResult Modify(Chat_Modify_Request request)
        {
            Console.WriteLine(request.content);
            var  response = requestService.Post("Chat", "modify", request);
            var data = JsonConvert.DeserializeObject<Chat_Modify_Response>(response);
            return Json(new {result = data.result});
        }
    }
}
