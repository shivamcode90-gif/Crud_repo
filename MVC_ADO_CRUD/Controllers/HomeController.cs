using Microsoft.AspNetCore.Mvc;
using MVC_ADO_CRUD.Models;
using MVC_ADO_CRUD.Services;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MVC_ADO_CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeServices homeServices;
        public HomeController(ILogger<HomeController> logger,HomeServices _homeServices)
        {
            _logger = logger;
            homeServices =  _homeServices;
        }
       
        public IActionResult Index()
        {
            homeServices.GetAllData();
            return View();

        }
        public IActionResult InsterAllCustomer(Customer customer)
        {
            homeServices.InsertCustomer(customer);
            return View();
        }
        public IActionResult Privacy1()
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Privacy2()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
