using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Data
{
    public enum Progress
    {
        NotStarted,
        Design,
        Animatic,
        Production,
        FinalDraft
    }
    public class AnimRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<string> ImageURLs { get; set; }
        public Progress Progress { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime? DateAccepted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public string ResultURL { get; set; }

        public bool IsAccepted { get; set; }
        public bool IsComplete { get; set; }

        public AnimRequest()
        {
            this.UserProfiles = new HashSet<UserProfile>();
        }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
