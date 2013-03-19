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

    public class FirstGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static bool start = false;
        public static bool exit = false;
        public static bool reload = false;
        public static bool checkpoint = false;
        public static int W;
        public static int H;

        GameMain Main;
        AccueilGUI Accueil;

        public FirstGame()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.PreferredBackBufferWidth = 796;
            //graphics.PreferredBackBufferHeight = 480;
            this.graphics.IsFullScreen = true;
            W = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            H = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ressources.LoadContent(Content);
            Accueil = new AccueilGUI();
            Main = new GameMain();
            
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if(exit)
                this.Exit();
            if (start)
            {
                if (reload)
                {
                    this.Reload();
                }
                Main.Update(Keyboard.GetState(), GamePad.GetState(PlayerIndex.One), Mouse.GetState(), gameTime);
            }
            else
                Accueil.Update(GamePad.GetState(PlayerIndex.One));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
                if(start)
                    Main.Draw(spriteBatch);
                else
                    Accueil.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Reload()
        {
            Blocks.BlockList.Clear();
            Camera.CamerasBlockList.Clear();
            ClimbableBlock.ClimbableBlockList.Clear();
            DelimiterZone.DelimiterZoneList.Clear();
            GUI.GUIList.Clear();
            HidingBlock.HidingBlockList.Clear();
            InteractZoneBlockWithPuzzle.InteractZoneBlockList.Clear();
            InventoryCase.InventoryCaseList.Clear();
            ItemBlock.ItemBlockList.Clear();
            Ladder.LadderList.Clear();
            MovableEnnemyBlock.MovableEnnemyList.Clear();
            MovableNeutralBlock.MovableNeutralList.Clear();
            Puzzle.PuzzleList.Clear();
            StaticNeutralBlock.StaticNeutralList.Clear();
            BulletBlock.BulletBlockList.Clear();
            InfectedZoneBlock.InfectedZoneBlockList.Clear();
            Main = new GameMain();
            reload = false;
        }
    }
}
