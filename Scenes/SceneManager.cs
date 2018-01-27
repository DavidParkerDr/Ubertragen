using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transmission.Scenes.Story;

namespace Transmission.Scenes
{
    public class SceneManager
    {
        private List<IScene> mScenes;

        public SceneManager()
        {
            mScenes = new List<IScene>();
        }

        public void Push(IScene p_Scene)
        {
            mScenes.Add(p_Scene);
        }

        public void Pop()
        {
            if (Count > 0)
            {
                mScenes.RemoveAt(mScenes.Count - 1);
            }
        }

        public IScene Top
        {
            get
            {
                if (Count > 0)
                {
                    return mScenes.Last();
                }
                return null;
            }
        }


        public int Count
        {
            get { return mScenes.Count; }
        }

        public void Update(float pSeconds)
        {
            if (Count > 0)
            {
                Top.Update(pSeconds);
            }
        }

        public void Draw(float pSeconds)
        {
            if (Count > 0)
            {
                Top.Draw(pSeconds);
            }
        }

        public void GotoScene(NextScene nextScene) {
            var newScene = default(IScene);

            switch (nextScene.Type)
            {
                case "game":
                    newScene = new GameScene(nextScene.File);
                    break;
                case "story":
                    newScene = new StoryScene(nextScene.File);
                    break;
                default:
                    throw new InvalidOperationException("Unknown scene type");
            }

            this.Pop();
            this.Push(newScene);
        }
    }
}
