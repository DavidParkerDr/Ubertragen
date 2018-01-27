using System;
using System.Collections.Generic;

namespace Transmission.Scenes.Story
{
    public enum Speaker
    {
        Bird
    }

    public class Segment
    {
        public Speaker Speaker
        {
            get;set;
        }

        public string Text
        {
            get;
            set;
        }
    }

    public class Convo
    {
        
        public Convo()
        {
        }

        public List<Segment> Segments
        {
            get;
            set;
        }
    }
}
