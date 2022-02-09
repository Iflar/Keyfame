using Keyframe.Data;
using Keyframe.Models.UserProfileModels;
using Keyframe.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyframeMVC.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfileCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateUserProfileService();

            if (service.UserOwnsProfile())
            {
                ViewBag.ErrorMessag = "You already own a profile";
                return View(model);
            }

            if (service.CreateProfile(model))
            {
                TempData["SaveResult"] = "UserProfile Created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Profile could not be created.");
            return View(model);
        }

        private UserProfileService CreateUserProfileService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserProfileService(userId);
            return service;
        }
    }
}