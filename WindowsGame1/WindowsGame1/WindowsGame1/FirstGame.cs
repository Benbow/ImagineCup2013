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


namespace Overload
{

    public class FirstGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        public static bool start = false;
        public static bool end = false;
        public static bool exit = false;
        public static bool reload = false;
        public static bool checkpoint = false;
        public static bool canCheck = false;
        public static bool credits = false;
        public static int reloadCount = 0;
        public static double Jp;
        public static double Hp;
        public static int W;
        public static int H;

        public static bool FallDeath = false;
        public static bool SpottedDeath = false;
        public static bool NoLifeDeath = false;

        GameMain Main;
        AccueilGUI Accueil;
        EndGUI EndScreen;
        GamePadState oldPad;



        public FirstGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
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
            EndScreen = new EndGUI();
            
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();
            if(exit)
                this.Exit();
            if (start)
            {
                if (reload)
                {
                    this.Reload();
                }
                if (FallDeath || NoLifeDeath || SpottedDeath)
                {
                    if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A) && oldPad.IsButtonUp(Buttons.A))
                    {
                        FallDeath = false;
                        NoLifeDeath = false;
                        SpottedDeath = false;
                        reload = true;
                    }
                }
                else
                    Main.Update(Keyboard.GetState(), GamePad.GetState(PlayerIndex.One), Mouse.GetState(), gameTime);
            }
            else if (end)
            {
                EndScreen.Update(GamePad.GetState(PlayerIndex.One));
            }
            else if(credits)
            {
                if ((GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A) && oldPad.IsButtonUp(Buttons.A)) || (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.B) && oldPad.IsButtonUp(Buttons.B)))
                {
                    credits = false;
                }
            }
            else
                Accueil.Update(GamePad.GetState(PlayerIndex.One));

            oldPad = GamePad.GetState(PlayerIndex.One);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (start)
            {
                Main.Draw(spriteBatch);
                if(FallDeath)
                    spriteBatch.Draw(Ressources.FallDeath, new Rectangle(FirstGame.W / 2 - Ressources.FallDeath.Width / 2, FirstGame.H / 2 - Ressources.FallDeath.Height / 2, Ressources.FallDeath.Width, Ressources.FallDeath.Height), Color.White);
                else if(NoLifeDeath)
                    spriteBatch.Draw(Ressources.NoLifeDeath, new Rectangle(FirstGame.W / 2 - Ressources.NoLifeDeath.Width / 2, FirstGame.H / 2 - Ressources.NoLifeDeath.Height / 2, Ressources.NoLifeDeath.Width, Ressources.NoLifeDeath.Height), Color.White);
                else if (SpottedDeath)
                    spriteBatch.Draw(Ressources.SpottedDeath, new Rectangle(FirstGame.W / 2 - Ressources.SpottedDeath.Width / 2, FirstGame.H / 2 - Ressources.SpottedDeath.Height / 2, Ressources.SpottedDeath.Width, Ressources.SpottedDeath.Height), Color.White);
            
            }
            else if (end)
            {
                EndScreen.Draw(spriteBatch);
            }
            else if (credits)
            {
                spriteBatch.Draw(Ressources.credits_bg, new Rectangle(0, 0, FirstGame.W, FirstGame.H), Color.White);
            }
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
            Door.DoorList.Clear();
            EndZoneBlock.EndZoneBlockList.Clear();
            InfectedZoneBlock.InfectedZoneBlockList.Clear();
            LaunchableBlock.LaunchableBlockList.Clear();
            Puzzle.PuzzleList.Clear();
            SkillPointsBonusBlock.SkillPointsBonusList.Clear();
            WallPaperBlock.WallPaperBlockList.Clear();
            CheckpointBlock.CheckpointBlockList.Clear();
            Main = new GameMain();
            reload = false;
            Jekyll._jskillsPoints = 70;
            Hide._hskillPoints = 110;
            reloadCount++;
            if (checkpoint)
                canCheck = true;
        }
    }
}
