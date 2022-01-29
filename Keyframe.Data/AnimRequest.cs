using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Data
{
    public class AnimRequest
    {
        [Key]
        public int RequestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Uri> References { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime? DateAccepted { get; set; }
        public DateTime? DateCompleted { get; set; }

        [ForeignKey(nameof(ClientId))]
        public int ClientId { get; set; }
        public virtual ClientProfile ClientProfile { get; set; }

        [ForeignKey(nameof(AnimatorId))]
        public int? AnimatorId { get; set; }
        public virtual AnimatorProfile AnimatorProfile { get; set; }

      
    }
}
