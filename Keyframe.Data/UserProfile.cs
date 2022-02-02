using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Data
{
    public class UserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public string ProfilePictureURL { get; set; }

        public UserProfile()
        {
            this.Requests = new HashSet<AnimRequest>();
        }
        public virtual ICollection<AnimRequest> Requests { get; set; }
    }
}
