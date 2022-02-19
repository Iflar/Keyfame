using Keyframe.Models.AnimRequestModels;
using Keyframe.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyframeMVC.Controllers
{
    public class AnimRequestController : Controller
    {
        // GET: AnimRequest
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AnimRequestService(userId);
            var model = service.GetMyRequests();

            return View(model);
        }

        public ActionResult OtherRequestIndex()
        {
            
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AnimRequestService(userId);
            var model = service.GetRequests();

            return View(model);
        }

        public ActionResult AcceptedRequests()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserProfileService(userId);
            var model = service.GetAcceptedRequests();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnimRequestCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateAnimRequestService();


            if (service.CreateRequest(model))
            {
                TempData["SaveResult"] = "Request Created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Request could not be created.");
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateAnimRequestService();
            var detail = service.GetRequestById(id);
            var model =
                new AnimRequestEdit
                {
                    Progress = detail.Progress,
                    DateAccepted = detail.DateAccepted,
                    DateCompleted = detail.DateCompleted
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AnimRequestEdit model)
        {
            if (!ModelState.IsValid) return View(model);


            var service = CreateAnimRequestService();

            if (service.UpdateRequest(model))
            {
                TempData["SaveResult"] = "Your Request was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Request could not be updated.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateAnimRequestService();
            var model = svc.GetRequestById(id);

            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateAnimRequestService();
            var model = svc.GetRequestById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAnimRequest(int id)
        {
            var service = CreateAnimRequestService();

            service.DeleteRequest(id);

            TempData["SaveResult"] = "Your Request was deleted";

            return RedirectToAction("Index");
        }

        private AnimRequestService CreateAnimRequestService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AnimRequestService(userId);
            return service;
        }
    }
}