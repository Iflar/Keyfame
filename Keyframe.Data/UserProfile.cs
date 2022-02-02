using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Data
{
    public class UserProfile
    {
        [Key]
        public int UserId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Biography { get; set; }
        public string ProfilePictureURL { get; set; }

        public UserProfile()
        {
            this.Requests = new HashSet<AnimRequest>();
        }
        public virtual ICollection<AnimRequest> Requests { get; set; }
    }
}
