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
            var viewModel = new IndexViewModel();
            try
            {
                var requestUser = HttpContext.User;
                var user = new User();
                if (requestUser?.Identity?.IsAuthenticated == true)
                {
                    var userId = requestUser.GetObjectId();
                    if (userId != null)
                    {
                        user = _unitofWork.UserRepository.FindAsync(u => u.UserPrincipalName == userId).Result.FirstOrDefault();

                        if (user == null)
                        {
                            user = new User
                            {
                                UserPrincipalName = userId,
                                DisplayName = requestUser.GetDisplayName() ?? string.Empty,
                                CreatedBy = "System",
                                CreatedDate = DateTime.Now,
                                UpdatedBy = "System",
                                UpdatedDate = DateTime.Now
                            };

                            _unitofWork.UserRepository.AddAsync(user);
                            _unitofWork.Save();
                        }
                    }
                }
                viewModel.display_name = user?.DisplayName ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.Index");
                TempData.Add("Error", ex.ToString());
                return View("Error");
            }
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
