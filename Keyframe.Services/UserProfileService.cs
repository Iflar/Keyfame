using Keyframe.Data;
using Keyframe.Models.UserProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Services
{
    public class UserProfileService
    {
        private readonly Guid _userId;

        public UserProfileService(Guid userId)
        {
            _userId = userId;
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
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Biography = entity.Biography,
                        ProfilePictureURL = entity.ProfilePictureURL
                    };
            }
        }
        public bool UpdateUser(UserProfileEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UsersProfiles
                        .Single(e => e.UserId == model.UserId && e.OwnerId == _userId);
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
    }
}
