using Microsoft.AspNetCore.Mvc;


namespace LMS.Web.Areas.Userss.Controllers;

[Area("Userss")]
public class HomeController : Controller
{


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
}
