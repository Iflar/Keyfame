using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyframe.Data
{
    public enum Qualification
    {
        ThreeD,
        TwoD,
        Videogame,
        Film
    }
    public class UserProperties
    {
        public Qualification Qualification { get; set; }

    }
}
