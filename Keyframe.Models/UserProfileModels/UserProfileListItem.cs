using Keyframe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Models.UserProfileModels
{
    public class UserProfileListItem
    {
        public int userId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public HashSet<AnimRequest> AecceptedRequests { get; set; }

    }
}
