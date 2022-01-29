using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Data
{
    public class AnimatorProfile
    {
        [Key]
        public int AnimatorId { get; set; }
        public Guid OwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Uri ProfilePicture { get; set; }
        public string Biography { get; set; }
        public virtual HashSet<AnimRequest> AcceptedRequests { get; set; }
    }
}
