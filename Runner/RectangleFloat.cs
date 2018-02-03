using Microsoft.Xna.Framework;

namespace Runner
{
    public class RectangleFloat
    {
        public Vector2 pos { get; set; }
        float width;
        float height;

        float left
        {
            get
            {
                return pos.X;
            }
        }

        float right
        {
            get
            {
                return pos.X + width;
            }
        }

        float bottom
        {
            get
            {
                return pos.Y;
            }
        }

        float top
        {
            get
            {
                return pos.Y + height; // TODO: check this!!!
            }
        }

        public RectangleFloat(Vector2 pos, float width, float height)
        {
            this.pos = pos;
            this.width = width;
            this.height = height;
        }

        public bool intersects(RectangleFloat rectangle2)
        {
            return (this.left < rectangle2.right && this.right > rectangle2.left &&
                    this.top > rectangle2.bottom && this.bottom < rectangle2.top);
        }
    }
}
