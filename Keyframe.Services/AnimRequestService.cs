using Keyframe.Data;
using Keyframe.Models.AnimRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Services
{
    public class AnimRequestService
    {
        private readonly Guid _userId;

        public AnimRequestService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateRequest(AnimRequestCreate model)
        {
            var entity =
                new AnimRequest()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Description = model.Description,
                    DatePosted = DateTime.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Requests.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<AnimRequestListItem> GetRequests()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Requests
                        .Where(e => e.OwnerId != _userId && e.IsAccepted == false)
                        .Select(
                            e =>
                                new AnimRequestListItem
                                {
                                    RequestId = e.RequestId,
                                    Title = e.Title,
                                    Progress = e.Progress,
                                    DatePosted = e.DatePosted,
                                    DateCompleted = e.DateCompleted
                                }
                        );

                return query.ToArray();
            }
        }
        public IEnumerable<AnimRequestListItem> GetMyRequests()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Requests
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new AnimRequestListItem
                                {
                                    RequestId = e.RequestId,
                                    Title = e.Title,
                                    Progress = e.Progress,
                                    DatePosted = e.DatePosted,
                                    DateCompleted = e.DateCompleted
                                }
                        );

                return query.ToArray();
            }
        }
        public AnimRequestDetail GetRequestById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Requests
                        .Single(e => e.RequestId == id);
                return
                    new AnimRequestDetail
                    {
                        RequestId = entity.RequestId,
                        Title = entity.Title,
                        Description = entity.Description,
                        Progress = entity.Progress,
                        DatePosted = entity.DatePosted,
                        DateAccepted = entity.DateAccepted,
                        DateCompleted = entity.DateCompleted
                    };
            }
        }
        public bool UpdateRequest(AnimRequestEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Requests
                        .Single(e => e.RequestId == model.RequestId);
                entity.Progress = model.Progress;
                entity.DateAccepted = model.DateAccepted;
                entity.DateCompleted = model.DateCompleted;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteRequest(int userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Requests
                        .Single(e => e.RequestId == userId);

                ctx.Requests.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
