using System;
using System.IO;
using Newtonsoft.Json;
using Transmission.Scenes.Story;

namespace Transmission.Scenes
{
    public class StageScene : IScene
    {
        Stage stage;
        GameScene gameScene;
        ConvoScene convoScene;

        public StageScene(string filename)
        {
            var game = Transmission.Instance();
            var sm = game.SM();

            stage = JsonConvert.DeserializeObject<Stage>(File.ReadAllText(filename));

            if (stage.Level != null) {
                gameScene = new GameScene(stage);
                sm.Push(gameScene);
            }

            if (stage.Convo != null) {
                convoScene = new ConvoScene(stage.Convo);
                sm.Push(convoScene);
            }
        }

        public void Draw(float pSeconds)
        {
        }

        public void Update(float pSeconds)
        {
            if (convoScene != null) {
                convoScene.Update(pSeconds);
            } else {
                gameScene.Update(pSeconds);
            }
        }

        public void HandleInput(float pSeconds)
        {
        }
    }
}
