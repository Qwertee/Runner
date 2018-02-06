using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Runner
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class RunnerGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;

        Character character;

        Texture2D character_texture;
        Texture2D grass_texture;
        Texture2D rock_spike_texture;

        SpriteFont font;

        private int grass_draw_range;

        public static List<Obstacle> obstacles;

        public RunnerGame()
        {
            graphics = new GraphicsDeviceManager(this);
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            character = new Character();

            obstacles = new List<Obstacle>();

            obstacles.Add(new Obstacle(new Vector2(7 * 8, 0)));

            grass_draw_range = 2; // the x coordinate which the grass tiles should be centered around (keeps memory free)

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // assume character is already allocated because the constructor SHOULD be called before
            // Initialize()
            character.camera = new Camera(this.GraphicsDevice.Viewport)
            {
                Zoom = 5,
                Rotation = 0,
                Location = new Vector2(0, 0)
            };

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load all the textures
            character_texture = Content.Load<Texture2D>("runner");
            character.LoadTexture(character_texture);

            grass_texture = Content.Load<Texture2D>("grass");

            rock_spike_texture = Content.Load<Texture2D>("rock_spike");
            foreach (var obs in obstacles)
            {
                obs.LoadTexture(rock_spike_texture);
            }

            font = Content.Load<SpriteFont>("BasicFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // main character update method
            character.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            int tiles_to_draw = 45;
            
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, null, null, null, character.camera.TransformMatrix);

            // draw

            // ground
            if (character.pos.X > grass_draw_range)
            {
                grass_draw_range += 23;
            }
            for (int i = -(grass_draw_range + (tiles_to_draw / 2)); i < grass_draw_range + tiles_to_draw / 2; i++)
            {
                spriteBatch.Draw(grass_texture, new Vector2(i * 8, 8), Color.White);
            }

            // draw spikes
            foreach (var obs in obstacles)
            {
                spriteBatch.Draw(obs.texture, obs.pos, Color.White);
            }

            // character
            spriteBatch.Draw(character.texture, character.pos, Color.White);

            spriteBatch.End();

            // seperate spritebatch calls to draw the text independent of the camera
            spriteBatch.Begin(SpriteSortMode.BackToFront);
            spriteBatch.DrawString(font, "Score: " + character.score, new Vector2(10, 10), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
