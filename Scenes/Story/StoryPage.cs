using System;
namespace Transmission.Scenes.Story
{
    public class NextScene
    {
        public string Type { get; set; }
        public string File { get; set; }
    }

    public class StoryPage
    {
        public StoryPage()
        {
            
        }

        public string Image { get; set; }

        public string Text { get; set; }

        public string Voiceover { get; set; }

        public NextScene Next { get; set; }
    }
}
