using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    class Character
    {
        // public
        public Vector2 pos { get; set; }
        public Texture2D texture { get; set; }
        public float velocityX { get; set; }
        public float velocityY { get; set; }
        public bool jumping { get; set; }
        public RectangleFloat rect { get; set; } // rect for boundaries of the charcter texture
        public Camera camera { get; set; }
        public int score { get; set; }

        // private



        // Constructor
        public Character()
        {
            score = 0;
            pos = new Vector2(0, 0);
            velocityX = 1;
            velocityY = 0;
            jumping = false;
            rect = new RectangleFloat(new Vector2(0, 0), 8, 8);

            //rect = new Rectangle()
            //{
            //    Height = 8,
            //    Width = 8,
            //    Location = new Point(0, -8),
            //    Size = new Point(8, 8),
            //    X = 0,
            //    Y = -8
            //};       
        }

        public void LoadTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Update()
        {
            // JUMP
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                // give an upwards velocity to the runner
                Jump();

            }

            // process gravity updates
            if (pos.Y < 0) // negative is up
            {
                velocityY += 0.25f;
            }
            else if (this.pos.Y > 0)
            {
                velocityY = 0; // done with the jump

                pos = new Vector2(pos.X, 0);
            }


            // character position update 
            pos = new Vector2(pos.X + velocityX, pos.Y + velocityY);

            // update the rect for the character  
            rect.pos = pos;

            // check for collision with obstacles
            foreach (var obs in RunnerGame.obstacles)
            {
                if (rect.intersects(obs.rect))
                {
                    pos = new Vector2(0, 0);
                    Console.WriteLine("HIT!");
                }
            }

            // sync the camera to the players position
            camera.Location = new Vector2(pos.X, camera.Location.Y);
        }

        public void Jump()
        {
            if (pos.Y != 0) { return; }

            velocityY = -3; // negative becuase zero is towards the top of the display
        }
    }
}
