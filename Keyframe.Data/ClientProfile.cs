using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Data
{
    public class ClientProfile
    {
        [Key]
        public int ClientId { get; set; }
        public Guid OwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Uri ProfilePicture { get; set; }
        public string Biography { get; set; }
        public HashSet<AnimRequest> PostedRequests { get; set; }
    }
}
