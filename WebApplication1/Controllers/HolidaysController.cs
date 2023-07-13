using HRSystem.Models;
using Microsoft.AspNetCore.Mvc;
using HRSystem.Repository;
using HRSystem.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HRSystem.Controllers
{
    public class HolidaysController : Controller
    {
        private readonly IHolidaysRepository holidayRepository;
        public HolidaysController(IHolidaysRepository holidayRepository)
        {
            this.holidayRepository = holidayRepository;
        }
        [Authorize(Permission.Holidays.View)]
        public IActionResult Index()
        {

            return View(holidayRepository.GetAll());
        }
        [Authorize(Permission.Holidays.Create)]

        public IActionResult New()
        {
            return View();
        }

        [Authorize(Permission.Holidays.Edit)]

        public IActionResult Edit(int id)
        {
            var hModel = holidayRepository.GetById(id);
            return View(hModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Holidays h)
        {
            if (ModelState.IsValid)
            {
                holidayRepository.Update(id, h);
                holidayRepository.Save();
                return RedirectToAction("Index");
            }
            return View(h);
        }
        [Authorize(Permission.Holidays.Create)]

        public IActionResult AddNewHoliday(Holidays h)
        {
            if (ModelState.IsValid)
            {
                holidayRepository.Insert(h);
                holidayRepository.Save();
                return RedirectToAction("Index");
            }
            return View("New", h);
        }
        [Authorize(Permission.Holidays.Delete)]

        public IActionResult Remove(int id)
        {
            holidayRepository.Delete(id);
            holidayRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
