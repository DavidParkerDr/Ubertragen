using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Transmission.Scenes.Story;

namespace Transmission.Scenes
{
    public class SceneManager
    {
        private List<IScene> mScenes;
        private List<IScene> mScenesToAdd;

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
            var scenes = new List<IScene>(mScenes);

            foreach (var scene in scenes)
            {
                scene.Update(pSeconds);
            }

            if (Count > 0)
            {
                Top.HandleInput(pSeconds);
            }
        }

        public void Draw(float pSeconds)
        {
            foreach (var scene in mScenes) {
                scene.Draw(pSeconds);
            }
        }

        public void GotoScene(NextScene nextScene) {
            this.Pop();

            switch (nextScene.Type)
            {
                case "story":
                    this.Push(new StoryScene(nextScene.File));
                    break;
                case "stage":
                    this.GotoStage(nextScene.File);
                    break;
                default:
                    throw new InvalidOperationException("Unknown scene type");
            }
        }

        public void GotoStage(string filename)
        {
            var stage = JsonConvert.DeserializeObject<Stage>(File.ReadAllText(filename));

            if (stage.Level != null)
            {
                var gameScene = new GameScene(stage);
                this.Push(gameScene);
            }

            if (stage.Convo != null)
            {
                var convoScene = new ConvoScene(stage.Convo);
                this.Push(convoScene);
            }
        }
    }
}
