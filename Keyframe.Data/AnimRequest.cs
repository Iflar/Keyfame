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
        [ForeignKey(nameof(ClientId))]
        public int ClientId { get; set; }
        public virtual ClientProfile ClientProfile { get; set; }

        [ForeignKey(nameof(AnimatorId))]
        public int? AnimatorId { get; set; }
        public virtual AnimatorProfile AnimatorProfile { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public List<Uri> References { get; set; }
        public Progress Progress { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime? DateAccepted { get; set; }
        public DateTime? DateCompleted { get; set; }

        public bool IsAccepted { get; set; }
        public bool IsComplete { get; set; }
    }
}
