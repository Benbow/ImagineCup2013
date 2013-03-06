using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class Map
    {
        public static DelimiterZone _leftSide;
        public static DelimiterZone _rightSide;
        public static DelimiterZone _upSide;
        public static DelimiterZone _downSide;

        private int _width;
        private int _height;
        

        bool blockMove = true;
        bool playerMove;
        Rectangle futurePos;
        int i;
        KeyboardState oldKeyboard;
        Keys jumpInitKey;
        int showMax = 200;
        int showCount = 0;
        int showTimer = 0;
        Puzzle puzzle = null;

        int accelTimer;

        public Map(int x, int y)
        {
            _width = x;
            _height = y;
            _leftSide = new DelimiterZone(0, 0, FirstGame.W / 2, _height, Ressources.invisible);
            _rightSide = new DelimiterZone(_width - FirstGame.W / 2, 0, FirstGame.W / 2, _height, Ressources.invisible);
            _upSide = new DelimiterZone(0, 0, _width , FirstGame.H/2, Ressources.invisible);
            _downSide = new DelimiterZone(0, _height-FirstGame.H/2, _width, FirstGame.H / 2, Ressources.invisible);
        }

        public void Update(KeyboardState keyboard, MouseState mouse, GameTime gameTime, Player player)
        {
            if (player.GetType() == typeof (Jekyll))
            {
                foreach (InteractZoneBlockWithPuzzle interBlock in InteractZoneBlockWithPuzzle.InteractZoneBlockList)
                {
                    if (player.HitBox.Intersects(interBlock.HitBox))
                    {
                        if (interBlock.IsActivate)
                        {
                            puzzle = Puzzle.PuzzleList[interBlock.Id];
                            if (keyboard.IsKeyDown(Keys.C) && !oldKeyboard.IsKeyDown(Keys.C))
                            {
                                interBlock.IsActivate = false;
                                puzzle = null;
                                GameMain.Status = "on";
                            }
                        }
                        else
                        {
                            if (keyboard.IsKeyDown(Keys.C) && !oldKeyboard.IsKeyDown(Keys.C))
                            {
                                interBlock.IsActivate = true;
                                GameMain.Status = "pause";
                            }
                        }
                    }
                }
            }

            if (GameMain.Status == "on")
            {

                futurePos = player.HitBox;
                // Animation des blocs mouvants
                foreach (MovableNeutralBlock block in MovableNeutralBlock.MovableNeutralList)
                {
                    block.Update(gameTime);
                }
                foreach (MovableEnnemyBlock block in MovableEnnemyBlock.MovableEnnemyList)
                {
                    block.Update(gameTime, player, keyboard);
                }

                //Déplacements joueurs/cartes

                if (keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Left) && !player.IsJumping)
                {
                    player.AccelMode = 1;
                    accelTimer = 0;
                    player.SetAccelSpeed();
                    player.BlockPLayer();
                }
                else if (keyboard.IsKeyDown(Keys.Left))
                {
                    this.SetPlayerAccelMode(gameTime, player);
                    this.Move(Keys.Left, player);
                }
                else if (keyboard.IsKeyDown(Keys.Right))
                {
                    this.SetPlayerAccelMode(gameTime, player);
                    this.Move(Keys.Right, player);
                }

                if (keyboard.IsKeyDown(Keys.Up) && player.FallingSpeed == 0)
                {
                    bool lad = false;
                    foreach (Ladder ladder in Ladder.LadderList)
                    {
                        if (player.GetType() == typeof (Jekyll)){
                            if (player.HitBox.Intersects(ladder.HitBox))
                            {
                                lad = true;
                                player.DecreaseCoordY(1);
                            }
                        }
                    }
                    if (!lad)
                    {
                        this.LookUp(true, gameTime, player);
                    }
                }
                else if (keyboard.IsKeyUp(Keys.Up) && showCount > 0)
                {
                    this.LookUp(false, gameTime, player);
                    player.LookUpDownPhase = true;
                }
                else if (keyboard.IsKeyDown(Keys.Up))
                {
                    foreach (Ladder ladder in Ladder.LadderList)
                    {
                        if (player.HitBox.Intersects(ladder.HitBox))
                        {
                            player.DecreaseCoordY(1);
                        }
                    }
                }
                else
                {
                    player.LookUpDownPhase = false;
                }

                if (keyboard.IsKeyDown(Keys.Down) && player.FallingSpeed == 0)
                {
                    foreach (Ladder ladder in Ladder.LadderList)
                    {
                        if (player.GetType() == typeof(Jekyll))
                        {
                            if (player.HitBox.Intersects(ladder.HitBox))
                            {
                                player.IncreaseCoordY(1);
                            }
                        }
                    }
                }

                if (keyboard.IsKeyDown(Keys.Space) && oldKeyboard.IsKeyUp(Keys.Space) && !player.IsJumping)
                {
                    if (keyboard.IsKeyDown(Keys.Left))
                        jumpInitKey = Keys.Left;
                    else if (keyboard.IsKeyDown(Keys.Right))
                        jumpInitKey = Keys.Right;
                    else if (keyboard.IsKeyDown(Keys.Right) && keyboard.IsKeyDown(Keys.Left))
                        jumpInitKey = 0;
                    else
                        jumpInitKey = 0;
                    player.JumpPlayer();
                } 
            }
            oldKeyboard = keyboard;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Blocks block in Blocks.BlockList)
            {
                spriteBatch.Draw(block.Texture, block.HitBox, Color.White);
            }
            if (puzzle != null)
            {
                puzzle.Draw(spriteBatch);
            }
        }

        public void SetPlayerAccelMode(GameTime gameTime, Player player)
        {
            if(!player.IsJumping)
                accelTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (accelTimer <= 600)
            {
                player.AccelMode = 1;
            }
            else if (accelTimer > 600 && accelTimer <= 2000)
            {
                player.AccelMode = 2;
            }
            else if(accelTimer > 2000)
            {
                player.AccelMode = 3;
            }
        }

        public void LookUp(bool active, GameTime gameTime, Player player)
        {
            if (active)
            {
                showTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (showTimer > 1000)
                {
                    if (showCount < showMax)
                    {
                        showCount++;
                        foreach (Blocks block in Blocks.BlockList)
                        {
                            block.IncreaseCoordBlockY(1);
                        }
                    }
                }
            }
            else
            {
                showTimer = 0;
                int speedShow = 5;
                showCount -= speedShow;
                if (showCount < 0)
                {
                    showCount += speedShow;
                    speedShow = 1;
                    showCount -= speedShow;
                }
                if (showCount >= 0)
                {
                    foreach (Blocks block in Blocks.BlockList)
                    {
                        block.DecreaseCoordBlockY(speedShow);
                    }
                    player.CheckGravity();
                }
            }
        }

        public void Move(Keys key, Player player)
        {
            player.SetAccelSpeed();
            if (player.IsJumping)
            {
                key = jumpInitKey;
                if (key == Keys.Left)
                {
                    futurePos.X -= (int)player.Speed;
                }
                else if (key == Keys.Right)
                {
                    futurePos.X += (int)player.Speed;
                }
            }
            else
            {
                if (key == Keys.Left)
                {
                    futurePos.X -= (int)player.Speed;
                }
                else if (key == Keys.Right)
                {
                    futurePos.X += (int)player.Speed;
                }
            }
            
            blockMove = true;
            foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
            {
                if(block.IsCollidable)
                {
                    if (block.HitBox.Intersects(futurePos))
                    {
                        if(player.FallingSpeed < 0 && player.AccelMode != 1)
                            player.FallingSpeed = 0;
                        blockMove = false;
                        player.AccelMode = 1;
                        accelTimer = 0;
                        
                        break;
                    }
                }
            }
            if (blockMove)
            {
                i = 0;
                playerMove = false;
                float sp = player.Speed;
                Rectangle RightCut = new Rectangle(player.HitBox.X + (player.HitBox.Width / 2), player.HitBox.Y, player.HitBox.Width / 2, player.HitBox.Height);
                Rectangle LeftCut = new Rectangle(player.HitBox.X, player.HitBox.Y, player.HitBox.Width / 2, player.HitBox.Height);
                Rectangle RightSide = Map._rightSide.HitBox;
                Rectangle LeftSide = Map._leftSide.HitBox;
                RightSide.X += (int) sp;
                RightSide.Width -= (int) sp;
                LeftSide.Width -= (int) sp;

                if (RightCut.Intersects(LeftSide) || LeftCut.Intersects(RightSide))
                {
                    playerMove = true;
                }
                    
                if (playerMove)
                {
                    if (key == Keys.Left)
                        player.MovePlayerLeft(true);
                    else if (key == Keys.Right)
                        player.MovePlayerRight(true);
                }     
                else
                {
                    if (key == Keys.Left)
                    {
                        player.MovePlayerLeft(false);
                        foreach (Blocks block in Blocks.BlockList)
                        {
                            if(player.IsJumping)
                                block.IncreaseCoordBlockX((int)player.SpeedInAir);
                            else
                                block.IncreaseCoordBlockX((int)player.Speed);
                        }
                    }
                    else if (key == Keys.Right)
                    {
                        player.MovePlayerRight(false);
                        foreach (Blocks block in Blocks.BlockList)
                        {
                            if (player.IsJumping)
                                block.DecreaseCoordBlockX((int)player.SpeedInAir);
                            else
                                block.DecreaseCoordBlockX((int)player.Speed);
                        }
                    }
                }
            }
        }
    }

}
