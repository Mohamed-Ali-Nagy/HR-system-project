using HRSystem.Models;
using Microsoft.AspNetCore.Mvc;
using HRSystem.Repository;

namespace HRSystem.Controllers
{
    public class HolidaysController : Controller
    {
        private readonly IHolidaysRepository holidayRepository;
        public HolidaysController(IHolidaysRepository holidayRepository)
        {
            this.holidayRepository = holidayRepository;
        }
        public IActionResult Index()
        {

            return View(holidayRepository.GetAll());
        }

        public IActionResult New()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            var hModel = holidayRepository.GetById(id);
            return View(hModel);
        }
        [HttpPost]
        public IActionResult Edit(int id, Holidays h)
        {
            if (ModelState != null)
            {
                holidayRepository.Update(id, h);
                holidayRepository.Save();
                return RedirectToAction("Index");
            }
            return View(h);
        }
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

        public IActionResult Remove(int id)
        {
            holidayRepository.Delete(id);
            holidayRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
