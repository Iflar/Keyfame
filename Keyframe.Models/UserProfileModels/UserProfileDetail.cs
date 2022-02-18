using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Models.UserProfileModels
{
    public class UserProfileDetail
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public string ProfilePictureURL { get; set; }
    }
}
