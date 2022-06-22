using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Information = new Information();
        }
        public Information Information { get; set; }
    }
    

}