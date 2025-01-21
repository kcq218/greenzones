using GreenZones.DAL;
using GreenZones.Models;
using GreenZones.Models.ViewModels;
using GreenZones.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GreenZones.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitofWork;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IUnitofWork unitofWork,
            IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _unitofWork = unitofWork;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();

            try
            {
                var user = await _userService.GetUserByIdAsync(_httpContextAccessor);

                if (user == null)
                {
                    user = await _userService.AddUser(_httpContextAccessor);
                }

                viewModel.Display_name = user?.DisplayName ?? string.Empty;
                return View(viewModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.Index");
                TempData.Add("Error", ex.ToString());
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ShotTypes()
        {
            var viewModel = new ShotTypeViewModel();
            try
            {
                viewModel.User = await _userService.GetUserByIdAsync(_httpContextAccessor);

                var shotTypes = await _unitofWork.ShotTypeRepository.GetAllAsync();

                viewModel.ShotTypes = shotTypes.Where(m => m.UserId == viewModel.User.Id).ToList();

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.ShotTypes");
                TempData.Add("Error", ex.ToString());
                return View("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddShotType()
        {

            var viewModel = new AddShotTypeViewModel();
            var userId = await _userService.GetUserByIdAsync(_httpContextAccessor);
            viewModel.UserId = userId.Id;
            viewModel.DisplayName = userId.DisplayName;

            try
            {
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.AddShotType");
                TempData.Add("Error", ex.ToString());
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddShotType(AddShotTypeViewModel viewModel)
        {
            var vm = viewModel;

            try
            {
                if (ModelState.IsValid)
                {
                    var shotType = new ShotType
                    {
                        UserId = viewModel.UserId,
                        Name = viewModel.ShotType,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        CreatedBy = viewModel.DisplayName,
                        UpdatedBy = viewModel.DisplayName
                    };

                    await _unitofWork.ShotTypeRepository.AddAsync(shotType);
                    _unitofWork.Save();
                    return RedirectToAction("ShotTypes", "Home");

                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.AddShotType");
                TempData.Add("Error", ex.ToString());
                return View("Error");
            }
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
