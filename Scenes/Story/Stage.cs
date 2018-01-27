using System;
namespace Transmission.Scenes.Story
{
    public class Stage
    {
        public Stage()
        {
        }

        public string Level { get; set; }

        public string Convo { get; set; }

        public NextScene Next { get; set; } 
    }
}
