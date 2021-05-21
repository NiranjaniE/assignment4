using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonProfile.Models;
using PersonProfile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonProfile.Controllers
{
    public class ProfileController : Controller
    {
        private IRepo<Profile> _repo;
        private ILogger<ProfileController> _logger;

        public ProfileController(IRepo<Profile> repo, ILogger<ProfileController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Profile> profiles = _repo.GetAll().ToList(); ;
            return View(profiles);

        }

        public IActionResult Details(int id)
        {
            Profile profile = _repo.Get(id);
            return View(profile);
        }
        [HttpPost]
        public IActionResult Details(Profile profile)
        {
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(Profile profile)
        {
            _repo.Add(profile);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Profile profile = _repo.Get(id);
            return View(profile);
        }
        [HttpPost]
        public IActionResult Edit(int id, Profile profile)
        {
            _repo.Update(id, profile);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Profile profile = _repo.Get(id);
            return View(profile);

        }
        [HttpPost]
        public IActionResult Delete(Profile profile)
        {
            _repo.Delete(profile);
            return RedirectToAction("Index");
        }

    }
}