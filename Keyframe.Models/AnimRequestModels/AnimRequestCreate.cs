using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Models.AnimRequestModels
{
    public class AnimRequestCreate
    {
        [Required]
        [MaxLength(18, ErrorMessage = "Title is too long")]
        [MinLength(5, ErrorMessage = "Title is too short")]
        public string Title { get; set; }
        [Required]
        [MaxLength(900000, ErrorMessage = "Description is far too long")]
        [MinLength(20, ErrorMessage = "Must be more descriptive")]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
