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
        private ResponseService responseService = new ResponseService();
        public async Task<IActionResult> Index()
        {
            var viewModel = new ChatViewModel();
            string response = await requestService.Get("Chat", "list");
            var data = responseService.GetData<Chat_List>(response);
            viewModel.Chat_List = data.list;
            return View(viewModel);
        }

        // public async Task<IActionResult> Test()
        // {
        //     var s = await requestService.Test("chat", "list");
        //     Console.WriteLine(s);
        //     TempData["s"] = s;
        //     return View();
        // }

        [HttpPost]
        public async Task<IActionResult> New([Bind(Prefix = "chat_New_Request")] Chat_New_Request request)
        {
            var response = await requestService.Post("Chat", "new", request);
            var data = responseService.GetData<Chat_New_Response>(response);
            if(data.result == "success")
                TempData["success_message"] = "新增成功";
            else
                TempData["alert_message"] = "新增失敗";
            return RedirectToAction("Index", "Chat");
        }

        [HttpPost]
        public async Task<JsonResult> Delete(Chat_Delete_Request request)
        {
            var response = await requestService.Post("Chat", "delete", request);
            var data = responseService.GetData<Chat_Delete_Response>(response);
            return Json(new{ result = data.result, message = data.message });
        }

        [HttpPost]
        public async Task<JsonResult> Modify(Chat_Modify_Request request)
        {
            Console.WriteLine(request.content);
            var  response = await requestService.Post("Chat", "modify", request);
            var data = responseService.GetData<Chat_Modify_Response>(response);
            return Json(new {result = data.result});
        }
    }
}
