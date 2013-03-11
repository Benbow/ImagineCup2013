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
        Rectangle futurePos;
        int i;
        KeyboardState oldKeyboard;
        GamePadState oldPad;
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
            _leftSide = new DelimiterZone(0, 0, FirstGame.W / 2, _height);
            _rightSide = new DelimiterZone(_width - FirstGame.W / 2, 0, FirstGame.W / 2, _height);
            _upSide = new DelimiterZone(0, 0, _width, FirstGame.H / 2);
            _downSide = new DelimiterZone(0, _height - FirstGame.H, _width, FirstGame.H / 2);
        }

        public void Update(KeyboardState keyboard, GamePadState pad, MouseState mouse, GameTime gameTime, Player player)
        {
            if (player.GetType() == typeof(Jekyll))
            {
                foreach (InteractZoneBlock interBlock in InteractZoneBlock.InteractZoneBlockList)
                {
                    if (player.HitBox.Intersects(interBlock.HitBox))
                    {
                        if (interBlock.IsActivate)
                        {
                            puzzle = Puzzle.PuzzleList[interBlock.Id];
                            if (pad.IsButtonDown(Buttons.A) && oldPad.IsButtonUp(Buttons.A))
                            {
                                interBlock.IsActivate = false;
                                puzzle = null;
                                GameMain.Status = "on";
                            }
                        }
                        else
                        {
                            if (pad.IsButtonDown(Buttons.A) && oldPad.IsButtonUp(Buttons.A))
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

                if (pad.IsButtonUp(Buttons.LeftThumbstickLeft) && pad.IsButtonUp(Buttons.LeftThumbstickRight) && !player.IsJumping)
                {
                    player.AccelMode = 1;
                    player.Speed = 1.5f;
                    accelTimer = 0;
                    player.SetAccelSpeed();
                    player.BlockPLayer();
                }
                else if (pad.IsButtonDown(Buttons.LeftThumbstickLeft))
                {
                    this.SetPlayerAccelMode(gameTime, player);
                    this.Move(Keys.Left, player);
                }
                else if (pad.IsButtonDown(Buttons.LeftThumbstickRight))
                {
                    this.SetPlayerAccelMode(gameTime, player);
                    this.Move(Keys.Right, player);
                }

                if (pad.IsButtonDown(Buttons.LeftThumbstickUp) && player.FallingSpeed == 0)
                {
                    bool lad = false;
                    foreach (Ladder ladder in Ladder.LadderList)
                    {
                        if (player.GetType() == typeof(Jekyll))
                        {
                            if (player.HitBox.Intersects(ladder.HitBox))
                            {
                                lad = true;
                                player.DecreaseCoordY(1);
                            }
                        }
                    }
                    if (!lad && !player.PlayerMove)
                    {
                        this.LookUp(true, gameTime, player);
                    }
                }
                else if (pad.IsButtonUp(Buttons.LeftThumbstickUp) && showCount > 0)
                {
                    this.LookUp(false, gameTime, player);
                    player.LookUpDownPhase = true;
                }
                else if (pad.IsButtonDown(Buttons.LeftThumbstickUp))
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

                if (pad.IsButtonDown(Buttons.A) && oldPad.IsButtonUp(Buttons.A) && !player.IsJumping)
                {
                    if (player.Statut)
                    {
                        if (pad.IsButtonDown(Buttons.LeftThumbstickLeft))
                            jumpInitKey = Keys.Left;
                        else if (pad.IsButtonDown(Buttons.LeftThumbstickRight))
                            jumpInitKey = Keys.Right;
                        else if (pad.IsButtonDown(Buttons.LeftThumbstickRight) && pad.IsButtonDown(Buttons.LeftThumbstickLeft))
                            jumpInitKey = 0;
                        else
                            jumpInitKey = 0;

                        player.JumpPlayer();
                    }
                    else
                    {
                        if (pad.IsButtonDown(Buttons.LeftThumbstickRight))
                        {
                            futurePos.X += (int)player.Speed;
                            foreach (ClimbableBlock block in ClimbableBlock.ClimbableBlockList)
                            {
                                if (block.HitBox.Intersects(futurePos))
                                {
                                    if(block.IsActive)
                                        player.ClimbBox(block, 0);
                                }
                            }

                        }
                        else if (pad.IsButtonDown(Buttons.LeftThumbstickLeft))
                        {
                            futurePos.X -= (int)player.Speed;
                            foreach (ClimbableBlock block in ClimbableBlock.ClimbableBlockList)
                            {
                                if (block.HitBox.Intersects(futurePos))
                                {
                                    if (block.IsActive)
                                        player.ClimbBox(block, 1);
                                }
                            }
                        }
                    }
                }

                if (pad.IsButtonDown(Buttons.B) && oldPad.IsButtonUp(Buttons.B))
                {
                    if (player.Statut)
                    {
                        if (player.DirectionPlayer == Direction.Left)
                        {
                            futurePos.X -= (int)player.Speed;

                            /*
                             * Test de collision quand on attaque sur les box
                             */
                            foreach (ClimbableBlock block in ClimbableBlock.ClimbableBlockList)
                            {
                                if (block.HitBox.Intersects(futurePos))
                                {
                                    if (block.IsBreakable)
                                    {
                                        block.IsActive = false;
                                    }
                                }
                            }
                        }
                        else if (player.DirectionPlayer == Direction.Right)
                        {
                            futurePos.X += (int)player.Speed;

                            /*
                             * Test de collision quand on attaque sur les box
                             */
                            foreach (ClimbableBlock block in ClimbableBlock.ClimbableBlockList)
                            {
                                if (block.HitBox.Intersects(futurePos))
                                {
                                    if (block.IsBreakable)
                                    {
                                        block.IsActive = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            oldKeyboard = keyboard;
            oldPad = pad;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Blocks block in Blocks.BlockList)
            {
                if(block.IsActive)
                    spriteBatch.Draw(block.Texture, block.HitBox, Color.White);
            }
            if (puzzle != null)
            {
                puzzle.Draw(spriteBatch);
            }
        }

        public void SetPlayerAccelMode(GameTime gameTime, Player player)
        {
            if (!player.IsJumping && player.Speed <= 5)
                player.Speed += 0.01f;

            if (player.Speed <= 2)
            {
                player.AccelMode = 1;
            }
            else if (player.Speed > 2 && player.Speed <= 4)
            {
                player.AccelMode = 2;
            }
            else if (player.Speed > 4)
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
                if (block.IsCollidable)
                {
                    if (block.HitBox.Intersects(futurePos))
                    {
                        if (player.FallingSpeed < 0 && player.AccelMode != 1)
                            player.FallingSpeed = 0;
                        blockMove = false;
                        player.AccelMode = 1;
                        accelTimer = 0;

                        break;
                    }
                }
            }
            foreach (ClimbableBlock block in ClimbableBlock.ClimbableBlockList)
            {
                if (block.IsCollidable && block.IsActive)
                {
                    if (block.HitBox.Intersects(futurePos))
                    {
                        if (player.FallingSpeed < 0 && player.AccelMode != 1)
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
                player.PlayerMove = false;
                float sp = player.Speed;
                Rectangle LeftCut = new Rectangle(player.HitBox.X + (player.HitBox.Width / 2) - (int)sp, player.HitBox.Y, player.HitBox.Width / 2, player.HitBox.Height);
                Rectangle RightCut = new Rectangle(player.HitBox.X + (int)sp, player.HitBox.Y, player.HitBox.Width / 2, player.HitBox.Height);

                if (LeftCut.Intersects(this._leftSide.HitBox) || RightCut.Intersects(this._rightSide.HitBox))
                {
                    player.PlayerMove = true;
                }

                if (player.PlayerMove)
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
                            if (player.IsJumping)
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
