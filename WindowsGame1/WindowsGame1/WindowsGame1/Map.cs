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
        private int _width;
        private int _height;
        private DelimiterZone _leftSide;
        private DelimiterZone _rightSide;
        private DelimiterZone _upSide;
        private DelimiterZone _downSide;

        bool blockMove = true;
        bool playerMove;
        Rectangle futurePos;
        int i;
        KeyboardState oldKeyboard;
        Keys jumpInitKey;
        int showMax = 200;
        int showCount = 0;
        int showTimer = 0;
        

        int accelTimer;

        public Map(int x, int y)
        {
            _width = x;
            _height = y;
            _leftSide = new DelimiterZone(0, 0, FirstGame.W / 2, _height );
            _rightSide = new DelimiterZone(_width - FirstGame.W / 2, 0, FirstGame.W / 2, _height);
            _upSide = new DelimiterZone(0, 0, _width , FirstGame.H/2);
            _downSide = new DelimiterZone(0, _height-FirstGame.H, _width, FirstGame.H / 2);
        }

        public void Update(KeyboardState keyboard, MouseState mouse, GameTime gameTime, Player player)
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
                this.LookUp(true, gameTime, player);
            }
            else if (keyboard.IsKeyUp(Keys.Up) && showCount > 0)
            {
                this.LookUp(false, gameTime, player);
                player.LookUpDownPhase = true;
            }
            else
            {
                player.LookUpDownPhase = false;
            }

            if (keyboard.IsKeyDown(Keys.Space) && oldKeyboard.IsKeyUp(Keys.Space) && !player.IsJumping)
            { 
                if(keyboard.IsKeyDown(Keys.Left))
                    jumpInitKey = Keys.Left;
                else if(keyboard.IsKeyDown(Keys.Right))
                    jumpInitKey = Keys.Right;
                else if (keyboard.IsKeyDown(Keys.Right) && keyboard.IsKeyDown(Keys.Left))
                    jumpInitKey = 0;
                else
                    jumpInitKey = 0;
                player.JumpPlayer();
            }

            oldKeyboard = keyboard;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Blocks block in Blocks.BlockList)
            {
                spriteBatch.Draw(block.Texture, block.HitBox, Color.White);
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
                Rectangle LeftCut = new Rectangle(player.HitBox.X + (player.HitBox.Width / 2) - (int)sp, player.HitBox.Y, player.HitBox.Width / 2, player.HitBox.Height);
                Rectangle RightCut = new Rectangle(player.HitBox.X + (int)sp, player.HitBox.Y, player.HitBox.Width / 2, player.HitBox.Height);
              
                    if (LeftCut.Intersects(this._leftSide.HitBox) || RightCut.Intersects(this._rightSide.HitBox))
                    {
                        playerMove = true;
                    }
                    

                 
                /*foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
                {
                    i++;
                    if (i <= 4)
                    {
                        if(key == Keys.Left)
                        {
                            if ((i == 4 && player.HitBox.X - (FirstGame.W / 2) < block.HitBox.X) || (i == 2 && (player.HitBox.X + (FirstGame.W / 2) + sp - block.HitBox.Width > block.HitBox.X)))
                            {
                                playerMove = true;
                            }
                        }
                        else if (key == Keys.Right)
                        {
                            if ((i == 2 && player.HitBox.X + ((FirstGame.W / 2)) - block.HitBox.Width + sp > block.HitBox.X) || (i == 4 && player.HitBox.X - (FirstGame.W / 2) < block.HitBox.X))
                            {
                                playerMove = true;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }*/
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
