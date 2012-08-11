using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SkyCrane
<<<<<<< HEAD
{
=======
{   
>>>>>>> local

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ProjectSkyCrane : Microsoft.Xna.Framework.Game
    {
        public List<AIable> aiAbles = new List<AIable>();
        public List<PhysicsAble> physicsAbles = new List<PhysicsAble>();
        public Dictionary<String, Texture2D> textureDict = new Dictionary<String, Texture2D>();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

<<<<<<< HEAD
        /// <summary>
        /// Create the main instance of the project and run.
        /// </summary>
=======
        Level activeLevel;

>>>>>>> local
        public ProjectSkyCrane()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            return;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            return;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
<<<<<<< HEAD
            return;
=======
            Texture2D testLevel = this.Content.Load<Texture2D>("testlevel");
            textureDict.Add("testlevel", testLevel);

            activeLevel = Level.generateLevel(spriteBatch, this);
>>>>>>> local
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            return;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            UpdateGameControls(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
            return;
        }

        /// <summary>
        /// Update control-related information for all players.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        private void UpdateGameControls(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }
            return;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

<<<<<<< HEAD
            // All the real drawing happens inside the screen manager component
=======
            

>>>>>>> local
            base.Draw(gameTime);
            return;
        }
    }

    /// <summary>
    /// Main entry point for the game. Removes the unnecessary "program" file.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ProjectSkyCrane game = new ProjectSkyCrane())
            {
                game.Run();
            }
        }

    }
}
