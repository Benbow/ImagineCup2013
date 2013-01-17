using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FirstGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Balle _balle;
        private Barre _barre1;
        private Barre _barre2;
        private Rectangle _win1;
        private Rectangle _win2;
        private SpriteFont _font;
        private int W;
        private int H;

        public FirstGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            W = Window.ClientBounds.Width;
            H = Window.ClientBounds.Height;

            _balle = new Balle();
            _balle.Initialize(H);
            _barre1 = new Barre();
            _barre1.Initialize(1,H);
            _barre2 = new Barre();
            _barre2.Initialize(2,H);
            _win1 = new Rectangle(0, 0, 10, H);
            _win2 = new Rectangle(W-10, 0, 10, H);


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

            _balle.LoadContent(Content, "Balle");
            _barre1.LoadContent(Content, "Barre");
            _barre2.LoadContent(Content, "Barre");
            _font = Content.Load<SpriteFont>("FontScore");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            _barre1.Update(gameTime);
            _barre2.Update(gameTime);
            _balle.Update(gameTime,_barre1,_barre2);

            if (_win1.Contains(_balle._recBalle))
            {
                _barre2.Score++;
                _balle.Win(2);
            }

            if (_win2.Contains(_balle._recBalle))
            {
                _barre1.Score++;
                _balle.Win(1);
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            _balle.Draw(spriteBatch, gameTime);
            _barre1.Draw(spriteBatch, gameTime);
            _barre2.Draw(spriteBatch, gameTime);
            spriteBatch.DrawString(_font, ""+_barre1.Score, new Vector2(150, 100), Color.White);
            spriteBatch.DrawString(_font, ""+_barre2.Score, new Vector2(500, 100), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
