using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace animate_export
{
    class Animation
    {

        public class FrameInfo
        {
            public int type;
            public int xpos;
            public int ypos;
            public int zoom;
            public int angle;
            public bool mirror;
            public int opacity;
            public int blend;

            public FrameInfo(int _type, int _xpos, int _ypos, int _zoom, int _angle, bool _mirrow, int _opacity, int _blend)
            {
                type = _type;
                xpos = _xpos;
                ypos = _ypos;
                zoom = _zoom;
                angle = _angle;
                mirror = _mirrow;
                opacity = _opacity;
                blend = _blend;
            }

        }

        public class Frame
        {

            public int cell_max;
            public List<FrameInfo> cell_data;

            public Frame(int _cellmax, List<FrameInfo> _celldata)
            {
                cell_max = _cellmax;
                cell_data = _celldata;
            }

        }


        public int id;
        public string name;
        public string animation_name;
        public int animation_hue;
        public int position;
        public int frame_max;

        public List<Frame> frames;

        public Animation(int _id, string _name, string _animationName, int _animationHue, int _position, int _frameMax)
        {
            id = _id;
            name = _name;
            animation_name = _animationName;
            animation_hue = _animationHue;
            position = _position;
            frame_max = _frameMax;
            frames = null;
        }

        public void setFrames(List<Frame> _frames)
        {
            frames = _frames;
        }
    }

    class AnimationExport
    {
        public List<string> bitmaps { get; set; }
        public int frame_max { get; set; }
        public List<List<List<int>>> frames { get; set; }
    }
}
