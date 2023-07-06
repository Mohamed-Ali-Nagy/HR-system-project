using HRSystem.Models;
using HRSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace HRSystem.Controllers
{
    public class GeneralSettingController : Controller
    {
        IGeneralSettingRepository generalSettingRepository;
        public GeneralSettingController(IGeneralSettingRepository _generalSettingRepository)
        {
            generalSettingRepository = _generalSettingRepository;
        }
        
        public IActionResult Index()
        {
            ViewBag.Message = "";
            GeneralSettings generalSettings = new GeneralSettings();
            generalSettings.WeekRest1 = generalSettingRepository.GetWeekRest1();
            generalSettings.WeekRest2= generalSettingRepository.GetWeekRest2();
            generalSettings.AddHourRate=generalSettingRepository.GetAddHourRate();
            generalSettings.DeducateHourRate=generalSettingRepository.GetDeducateHourRate();
            return View(generalSettings);
        }
        
        public IActionResult Edit()
        {
            GeneralSettings generalSettings = new GeneralSettings();
            generalSettings.WeekRest1 = generalSettingRepository.GetWeekRest1();
            generalSettings.WeekRest2 = generalSettingRepository.GetWeekRest2();
            generalSettings.AddHourRate = generalSettingRepository.GetAddHourRate();
            generalSettings.DeducateHourRate = generalSettingRepository.GetDeducateHourRate();
            return View(generalSettings);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(GeneralSettings generalSettings) 
        {
            if(ModelState.IsValid)
            {
                
                generalSettingRepository.UpdateAddHourRate(generalSettings.AddHourRate);
                generalSettingRepository.UpdateDeducateHourRate(generalSettings.DeducateHourRate);
                generalSettingRepository.UpdateWeekRest1(generalSettings.WeekRest1);
                generalSettingRepository.UpdateWeekRest2(generalSettings.WeekRest2);
                generalSettingRepository.Save();
                return RedirectToAction("index", generalSettings);
            }
            return View(generalSettings);
        }
        public IActionResult Validation(string WeekRest2, string WeekRest1)
        {
            if (WeekRest2 != WeekRest1)
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}
