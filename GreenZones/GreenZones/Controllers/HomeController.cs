using GreenZones.DAL;
using GreenZones.Models;
using GreenZones.Models.ViewModels;
using GreenZones.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

                var sessions = await _unitofWork.SessionRepository.GetAllAsync();
                var shotType = await _unitofWork.ShotTypeRepository.GetAllAsync();

                viewModel.Display_name = user?.DisplayName ?? string.Empty;
                viewModel.Sessions = sessions.Where(m => m.UserId == user.Id).ToList();
                viewModel.Sessions.ForEach(m => m.ShotType.Name.Any(x => x.Equals(shotType.Select(m => m.Name))));

                if (viewModel.Sessions.Sum(m => m.TotalShots) > 0)
                {
                    viewModel.Total_Shot_Percentage = Convert.ToInt32(Convert.ToDecimal(viewModel.Sessions.Sum(m => m.Makes)) / viewModel.Sessions.Sum(m => m.TotalShots) * 100);
                }

                viewModel.Total_Shot_Made = viewModel.Sessions.Sum(m => m.Makes);
                viewModel.longest_streak = viewModel.Sessions.Max(m => m.Streak);

                if (viewModel.longest_streak == null)
                {
                    viewModel.longest_streak = 0;
                }

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
            var user = await _userService.GetUserByIdAsync(_httpContextAccessor);
            viewModel.UserId = user.Id;
            viewModel.DisplayName = user.DisplayName;

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
                        UserId = vm.UserId,
                        Name = vm.ShotType,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        CreatedBy = vm.DisplayName,
                        UpdatedBy = vm.DisplayName
                    };

                    await _unitofWork.ShotTypeRepository.AddAsync(shotType);
                    _unitofWork.Save();
                    return RedirectToAction("ShotTypes", "Home");

                }
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.AddShotType");
                TempData.Add("Error", ex.ToString());
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditShotType(int id)
        {

            var viewModel = new EditShotTypeViewModel();
            var user = await _userService.GetUserByIdAsync(_httpContextAccessor);
            var shotType = await _unitofWork.ShotTypeRepository.GetByIdAsync(id);

            viewModel.UserId = user.Id;
            viewModel.ShotTypeId = id;
            viewModel.DisplayName = user.DisplayName;
            viewModel.ShotType = shotType.Name;

            try
            {
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.EditShotType");
                TempData.Add("Error", ex.ToString());
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditShotType(EditShotTypeViewModel viewModel)
        {
            var vm = viewModel;

            try
            {
                if (ModelState.IsValid)
                {
                    var shotType = await _unitofWork.ShotTypeRepository.GetByIdAsync(vm.ShotTypeId);
                    shotType.Name = vm.ShotType;
                    shotType.UpdatedDate = DateTime.Now;
                    shotType.UpdatedBy = vm.DisplayName;

                    _unitofWork.ShotTypeRepository.Update(shotType);
                    _unitofWork.Save();
                    return RedirectToAction("ShotTypes", "Home");

                }
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.EditShotType");
                TempData.Add("Error", ex.ToString());
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddSession()
        {

            var viewModel = new AddSessionViewModel();
            var User = await _userService.GetUserByIdAsync(_httpContextAccessor);
            var shotTypes = await _unitofWork.ShotTypeRepository.GetAllAsync();

            viewModel.ShotTypes = shotTypes.Where(m => m.UserId == User.Id).Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name });
            viewModel.UserId = User.Id;
            viewModel.DisplayName = User.DisplayName;

            try
            {
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.AddSession");
                TempData.Add("Error", ex.ToString());
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSession(AddSessionViewModel viewModel)
        {
            var vm = viewModel;

            try
            {
                if (viewModel.Makes > viewModel.TotalShot)
                {
                    ModelState.AddModelError("Makes", "Makes cannot be greater than Total Shots");
                }

                if (ModelState.IsValid)
                {
                    var session = new Session
                    {
                        UserId = vm.UserId,
                        Makes = vm.Makes,
                        ShotTypeId = Convert.ToInt32(vm.ShotTypeId),
                        Streak = vm.Streak,
                        TotalShots = vm.TotalShot,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        CreatedBy = vm.DisplayName,
                        UpdatedBy = vm.DisplayName
                    };

                    await _unitofWork.SessionRepository.AddAsync(session);
                    _unitofWork.Save();
                    return RedirectToAction("Index", "Home");

                }

                var shotTypes = await _unitofWork.ShotTypeRepository.GetAllAsync();
                vm.ShotTypes = shotTypes.Where(m => m.UserId == vm.UserId).Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name });
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.AddSession");
                TempData.Add("Error", ex.ToString());
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditSession(int id)
        {

            var viewModel = new EditSessionViewModel();
            var session = await _unitofWork.SessionRepository.GetByIdAsync(id);
            var User = await _userService.GetUserByIdAsync(_httpContextAccessor);
            var shotTypes = await _unitofWork.ShotTypeRepository.GetAllAsync();

            viewModel.SessionId = session.Id;
            viewModel.ShotTypes = shotTypes.Where(m => m.UserId == User.Id).Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name });
            viewModel.UserId = User.Id;
            viewModel.DisplayName = User.DisplayName;
            viewModel.Makes = session.Makes;
            viewModel.ShotTypeId = session.ShotTypeId.ToString();
            viewModel.Streak = session.Streak;
            viewModel.TotalShot = session.TotalShots;

            try
            {
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.EditSession");
                TempData.Add("Error", ex.ToString());
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSession(EditSessionViewModel viewModel)
        {
            var vm = viewModel;

            try
            {
                if (vm.Makes > vm.TotalShot)
                {
                    ModelState.AddModelError("Makes", "Makes cannot be greater than Total Shots");
                }

                if (ModelState.IsValid)
                {
                    var session = await _unitofWork.SessionRepository.GetByIdAsync(vm.SessionId);

                    session.Makes = vm.Makes;
                    session.ShotTypeId = Convert.ToInt32(vm.ShotTypeId);
                    session.Streak = vm.Streak;
                    session.TotalShots = vm.TotalShot;
                    session.UpdatedDate = DateTime.Now;
                    session.UpdatedBy = vm.DisplayName;


                    _unitofWork.SessionRepository.Update(session);
                    _unitofWork.Save();
                    return RedirectToAction("Index", "Home");

                }

                var shotTypes = await _unitofWork.ShotTypeRepository.GetAllAsync();
                vm.ShotTypes = shotTypes.Where(m => m.UserId == vm.UserId).Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name });
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.EditSession");
                TempData.Add("Error", ex.ToString());
                return View("Error");
            }
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
