using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Transmission.Helpers
{
    public class SpriteFontHelpers
    {
        public static void RenderTextWithWrapping(SpriteBatch batch, SpriteFont font, int lineHeight, string text, Rectangle rect) {

            string textToRender = text;

            int line = 0;

            while (textToRender.Length > 0)
            {
                int c = 0;

                while (c < textToRender.Length && font.MeasureString(textToRender.Substring(0, c)).X < rect.Width)
                {
                    c++;
                }

                if (c != 0)
                {
                    batch.DrawString(font, textToRender.Substring(0, c), new Vector2(rect.Left, rect.Top + (line * lineHeight)), Color.White);
                }

                line++;
                textToRender = textToRender.Substring(c);
            }
        }
    }
}
