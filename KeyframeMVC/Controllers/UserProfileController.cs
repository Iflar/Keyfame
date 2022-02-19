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
    [Authorize]
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserProfileService(userId);
            var model = service.GetUsers();

            var roleName = service.GetRoleNameByUserId(userId);

            TempData["RoleName"] = $"{roleName}";
            return View(model);
        }

        public ActionResult Create()
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
                TempData["ExceededProfileCount"] = "You already have a profile.";
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

        public ActionResult Edit(int id)
        {
            var service = CreateUserProfileService();
            var detail = service.GetUserById(id);
            var model =
                new UserProfileEdit
                {
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    Biography = detail.Biography,
                    ProfilePictureURL = detail.ProfilePictureURL
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserProfileEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateUserProfileService();

            if (service.UpdateUser(model))
            {
                TempData["SaveResult"] = "Your profile was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your profile could not be updated.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateUserProfileService();
            var model = service.GetUserById(id);

            var userId = Guid.Parse(User.Identity.GetUserId());

            var roleName = service.GetRoleNameByUserId(userId);

            ViewBag.RoleName = roleName;

            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateUserProfileService();
            var model = svc.GetUserById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProfile(int id)
        {
            var service = CreateUserProfileService();

            service.DeleteUser(id);

            TempData["SaveResult"] = "Your profile was deleted";

            return RedirectToAction("Index");
        }

        public ActionResult AcceptRequest(int requestId)
        {
            var service = CreateUserProfileService();

            if(service.AcceptRequest(requestId))
            {
                TempData["AcceptResult"] = "Request Accepted";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Something went wrong.");
            return View();
        }


    }
}