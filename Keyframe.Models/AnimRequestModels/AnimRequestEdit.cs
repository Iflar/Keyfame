using Keyframe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Models.AnimRequestModels
{
    public class AnimRequestEdit
    {
        public int RequestId { get; set; }
        public Progress Progress { get; set; }
        public DateTime? DateAccepted { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
