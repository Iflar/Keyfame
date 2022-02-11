using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Models.UserProfileModels
{
    public class UserProfileCreate
    {
        [MaxLength(12, ErrorMessage = "I'm sorry, but you may need go by a nickname...")]
        [MinLength(2, ErrorMessage = "look, if that's really how it's spelled, just type the pronunciation")]
        public string  FirstName { get; set; }

        [MaxLength(14, ErrorMessage = "Yep. Your name is invalid :)")]
        [MinLength(2, ErrorMessage = "Really? okay, just call this number for support: [REDACTED]")]
        public string LastName { get; set; }

        [MaxLength(80000, ErrorMessage = "This isn't english class")]
        [MinLength(12, ErrorMessage = "Trust me, you're more interesting than that.")]
        public string Biography { get; set; }
    }
}
