using Familia.WebApp.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FML.WebApp.MVC.Controllers
{
    public class GaleriaController : MainController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
