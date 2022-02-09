using Keyframe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Models.AnimRequestModels
{
    public class AnimRequestListItem
    {
        public string Title { get; set; }
        public Progress Progress { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
