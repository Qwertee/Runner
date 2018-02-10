using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Runner
{
    public class Obstacle
    {
        // public
        public Vector2 pos { get; set; }
        public Texture2D texture { get; set; }
        public RectangleFloat rect { get; set; } // rect for boundaries of the charcter texture
        public bool scored { get; set; }

        public Obstacle(Vector2 pos)
        {
            this.pos = pos;

            rect = new RectangleFloat(pos, 8, 8);

            scored = false;

            //rect = new Rectangle()
            //{
            //    Height = 8,
            //    Width = 8,
            //    Location = new Point((int)pos.X, (int)pos.Y - 8), // -8 becuase Rect start from the upper left
            //    Size = new Point(8, 8),
            //    X = (int) pos.X,
            //    Y = (int) pos.Y
            //};
        }

        public void LoadTexture(Texture2D texture)
        {
            this.texture = texture;
        }
    }
}
