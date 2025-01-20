using GreenZones.DAL;
using GreenZones.Models;
using GreenZones.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Diagnostics;

namespace GreenZones.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitofWork;

        public HomeController(ILogger<HomeController> logger, IUnitofWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            var request = HttpContext.User;
            if (request.Identity.IsAuthenticated)
            {
                var userId = request.GetObjectId();
                var user = _unitofWork.UserRepository.FindAsync(u => u.UserPrincipalName == userId).Result.FirstOrDefault();

                if (user == null)
                {
                    _unitofWork.UserRepository.AddAsync(new User
                    {
                        UserPrincipalName = userId,
                        DisplayName = request.GetDisplayName()
                    ,
                        CreatedBy = "droopy",
                        CreatedDate = DateTime.Now,
                        UpdatedBy = "droopy",
                        UpdatedDate = DateTime.Now
                    });

                    _unitofWork.Save();
                }
            }
            var viewModel = new IndexViewModel();
            viewModel.display_name = request.GetDisplayName();
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
