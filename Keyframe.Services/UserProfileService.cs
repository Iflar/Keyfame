﻿using Keyframe.Data;
using Keyframe.Models.AnimRequestModels;
using Keyframe.Models.UserProfileModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Keyframe.Services
{
    public class UserProfileService
    {
        public UserProfile MainLayoutViewModel { get; set; }

        ApplicationDbContext context;

        public UserProfileService()
        {
            context = new ApplicationDbContext();
        }
        private readonly Guid _userId;

        public UserProfileService(Guid userId)
        {
            context = new ApplicationDbContext();
            _userId = userId;
        }

        public string GetRoleNameByUserId(Guid userId)
        {

            IdentityUserRole roleContext = new IdentityUserRole();

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = userManager.Users.Single(u => u.Id == userId.ToString());

            var roleName = userManager.GetRoles(user.Id).FirstOrDefault();

            return roleName;
        }

        public string GetCurrentAppUser()
        {
            var userId = Guid.Parse(HttpContext.Current.User.Identity.GetUserId());

            return userId.ToString();
        }
        public int GetNumberAppUserRoles(ApplicationUser user)
        {
            int count = user.Roles.Count();

            return count;
        }

        public bool UserOwnsProfile()
        {
            var users = GetUsers();
            users = users.ToList();
            int dbCount = users.Count();

            if (dbCount == 0)
            {
                return false;
            }

            return true;
        }
        public bool CreateProfile(UserProfileCreate model)
        {
            var entity =
                new UserProfile()
                {
                    OwnerId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Biography = model.Biography,
                    Role = GetRoleNameByUserId(_userId),
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.UsersProfiles.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<UserProfileListItem> GetUsers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .UsersProfiles
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new UserProfileListItem
                                {
                                    userId = e.UserId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName
                                }
                        );

                return query.ToArray();
            }
        }

        public UserProfileDetail GetUserById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UsersProfiles
                        .Single(e => e.UserId == id && e.OwnerId == _userId);
                return
                    new UserProfileDetail
                    {
                        UserId = entity.UserId,
                        Role = entity.Role,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Biography = entity.Biography,
                        ProfilePictureURL = entity.ProfilePictureURL,
                    };
            }
        }

        public UserProfile GetWholeUser()
        {
            var entity =
                    context
                        .UsersProfiles
                        .Single(e => e.OwnerId == _userId);
            return entity;
        }

        public bool UpdateUser(UserProfileEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UsersProfiles
                        .Single(e => e.OwnerId == _userId);
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Biography = model.Biography;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteUser(int userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UsersProfiles
                        .Single(e => e.UserId == userId && e.OwnerId == _userId);

                ctx.UsersProfiles.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public AnimRequest GetRequestById(int Id)
        {
            var entity = context.Requests.Single(e => e.RequestId == Id);

            return entity;

        }

        public bool AcceptRequest(int requestId)
        {
            var request = GetRequestById(requestId);
            var user = GetWholeUser();

            user.Requests.Add(request);

            context.SaveChanges();

            request.DateAccepted = DateTime.Now;
            request.IsAccepted = true;

            request.UserProfiles.Add(user);

            return context.SaveChanges() >= 1;

        }

        public IEnumerable<AnimRequestListItem> GetAcceptedRequests()
        {
            var acceptedRequests =
                context.Requests
                .Where(r => r.IsAccepted == true).ToArray();

            var user = GetWholeUser();

            List<AnimRequestListItem> userAcceptedRequest = new List<AnimRequestListItem>();

            foreach (var request in acceptedRequests)
            {
                var userProfile = request.UserProfiles.Single(p => p.UserId == user.UserId);

                if (userProfile != null)
                {
                    var animRequest = new AnimRequestListItem()
                    {
                        RequestId = request.RequestId,
                        Title = request.Title,
                        Progress = request.Progress,
                        DatePosted = request.DatePosted,
                        DateCompleted = request.DateCompleted
                    };
                    userAcceptedRequest.Add(animRequest);
                }
            }
            return userAcceptedRequest;
        }
    }
}
