using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace animate_export
{

    class AnimationExport
    {
        public List<string> bitmaps { get; set; }
        public int frame_max { get; set; }
        public List<List<List<int>>> frames { get; set; }
    }
}
