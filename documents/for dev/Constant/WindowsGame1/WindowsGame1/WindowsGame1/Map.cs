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

        bool playerMove;
        bool blockMove = true;
        Rectangle futurePos;
        KeyboardState oldKeyboard;
        GamePadState oldPad;
        Keys jumpInitKey;
        int showMax = 200;
        int showCount = 0;
        int showTimer = 0;
        Puzzle puzzle = null;
        public Launch cible = new Launch(0,0,Direction.Right);

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

        public void Update(KeyboardState keyboard, GamePadState pad, MouseState mouse, GameTime gameTime, Player player)
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

                if(!player.IsAttacking)
                    player.CheckMove();
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
                    if (player.CanMove && player.EndAttack)
                    {
                        this.SetPlayerAccelMode(gameTime, player);
                        this.Move(Keys.Left, player);
                    }
                }
                else if (pad.IsButtonDown(Buttons.LeftThumbstickRight))
                {
                    if (player.CanMove && player.EndAttack)
                    {
                        this.SetPlayerAccelMode(gameTime, player);
                        this.Move(Keys.Right, player);
                    }
                }

                if (pad.IsButtonDown(Buttons.LeftThumbstickUp) && player.FallingSpeed == 0)
                {
                    bool lad = false;
                    foreach (Ladder ladder in Ladder.LadderList)
                    {
                        if (player.GetType() == typeof (Jekyll))
                        {
                            if (player.HitBox.Intersects(ladder.HitBox) && !player.IsCrouch)
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
                        if (player.HitBox.Intersects(ladder.HitBox) && !player.IsCrouch)
                        {
                            player.DecreaseCoordY(1);
                        }
                    }
                }
                else
                {
                    player.LookUpDownPhase = false;
                }

                if (pad.IsButtonDown(Buttons.LeftThumbstickDown) && player.FallingSpeed == 0)
                {
                    foreach (Ladder ladder in Ladder.LadderList)
                    {
                        if (player.GetType() == typeof(Jekyll))
                        {
                            if (player.HitBox.Intersects(ladder.HitBox))
                            {
                                Rectangle feet = new Rectangle(player.HitBox.X, player.HitBox.Y + player.HitBox.Height -1, player.HitBox.Width, 1);
                                Rectangle feetplus = feet;
                                feetplus.Y++;
                                if ((feet.Intersects(ladder.HitBox) || feetplus.Intersects(ladder.HitBox)) && !player.IsCrouch)
                                {
                                    player.IncreaseCoordY(1);
                                }
                            }
                        }
                    }
                }

                if (pad.IsButtonDown(Buttons.LeftThumbstickDown) && player.FallingSpeed == 0 && oldPad.IsButtonUp(Buttons.LeftThumbstickDown))
                {
                    var lad = false;
                    foreach (Ladder ladder in Ladder.LadderList)
                    {
                        if (player.GetType() == typeof(Jekyll))
                        {
                            if (player.HitBox.Intersects(ladder.HitBox))
                            {
                                lad = true;
                                break;
                            }
                        }
                    }
                    if (!player.Statut && !lad && !player.IsJumping)
                        player.stoop(1);
                }
                else if (pad.IsButtonUp(Buttons.LeftThumbstickDown) && oldPad.IsButtonDown(Buttons.LeftThumbstickDown))
                {
                    if (!player.Statut)
                        player.stoop(0);
                }

                /**
                 * Actions avec les boutons
                 */

                if (pad.IsButtonDown(Buttons.A) && oldPad.IsButtonUp(Buttons.A) && !player.IsJumping)
                {
                    if (player.Statut && player.EndAttack)
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
                                    if(block.IsActive && block.IsClimbable)
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
                                    if(block.IsActive && block.IsClimbable)
                                        player.ClimbBox(block, 1);
                                }
                            }
                        }
                    }
                }


                if (pad.IsButtonDown(Buttons.RightShoulder) && oldPad.IsButtonUp(Buttons.RightShoulder))
                {
                    player.IsActiveVision = !player.IsActiveVision;
                }


                if ((pad.IsButtonDown(Buttons.RightTrigger) || pad.IsButtonDown(Buttons.LeftTrigger)) && oldPad.IsButtonUp(Buttons.RightTrigger) && oldPad.IsButtonUp(Buttons.LeftTrigger))
                {
                    if (player.Statut)
                    {
                        player.Speed = 5;
                        player.IsSpriting = true;
                    }
                    
                }

                if (player.IsSpriting && pad.IsButtonUp(Buttons.RightTrigger) && pad.IsButtonUp(Buttons.LeftTrigger))
                {
                    if (player.Statut)
                    {
                        player.Speed = 3;
                        player.IsSpriting = false;
                    }
                }

                if (pad.IsButtonDown(Buttons.B) && oldPad.IsButtonUp(Buttons.B))
                {
                    if (player.Statut && player.EndAttack)
                    {
                        player.IsAttacking = true;
                        player.BeginAttack = true;
                        player.EndAttack = false;
                    }
                    else
                    {
                        foreach (HidingBlock block in HidingBlock.HidingBlockList)
                        {
                            if (block.HitBox.Intersects(futurePos))
                            {
                                if (block.IsHide)
                                {
                                    player.hide(block, 0);
                                }
                            }
                        }
                    }
                }


                if (pad.IsButtonDown(Buttons.Y) && oldPad.IsButtonUp(Buttons.Y))
                {
                    if (!player.Statut)
                    {
                        player.IsActiveObject = !player.IsActiveObject;
                    }
                }


                if (pad.IsButtonDown(Buttons.X) && oldPad.IsButtonUp(Buttons.X))
                {
                    if (!player.Statut)
                    {
                        if (!player.IsThrowing)
                        {
                            if (player.DirectionPlayer == Direction.Left)
                                cible = new Launch((player.HitBox.X - 50), (player.HitBox.Y + 56), Direction.Left);
                            else if (player.DirectionPlayer == Direction.Right)
                                cible = new Launch((player.HitBox.X + 50), (player.HitBox.Y + 56), Direction.Right);

                            player.IsThrowing = true;
                            player.BlockPLayer();
                            player.CanMove = false;
                        }
                        else
                        {
                            player.IsThrowing = false;
                            if (cible.sens == Direction.Left)
                            {
                                int distance = player.HitBox.X - cible.HitBox.X;
                                int ratio = 0;
                                if (distance <= 100)
                                {
                                    cible.Vitesse = 1;
                                    ratio = 3;
                                }
                                else if (distance > 100 && distance <= 300)
                                {
                                    cible.Vitesse = 2;
                                    ratio = 2;
                                }
                                else if (distance > 300 && distance <= 410)
                                    cible.Vitesse = 3;


                                cible.FSpeed = distance * 8f / 400 * -1 - ratio;

                            }
                            else if (cible.sens == Direction.Right)
                            {
                                int distance = cible.HitBox.X - player.HitBox.X;
                                int ratio = 0;
                                if (distance <= 100)
                                {
                                    cible.Vitesse = 1;
                                    ratio = 3;
                                }
                                else if (distance > 100 && distance <= 300)
                                {
                                    cible.Vitesse = 2;
                                    ratio = 2;
                                }
                                else if (distance > 300 && distance <= 410)
                                    cible.Vitesse = 3;


                                cible.FSpeed = distance * 8f / 400 * -1 - ratio;
                            }
                            cible.IsItemThrow = true;
                        }
                    }
                }

                /**
                 * Statut des actions
                 */
                if (player.IsHiding)
                {
                    if (!player.HideBlock.HitBox.Intersects(futurePos) && player.IsHiding)
                        player.hide(player.HideBlock, 1);
                }

                if (player.IsAttacking)
                {
                    player.AttackAnime();
                    player.Speed = 0;

                    /*
                     * Test de collision quand on attaque sur les blocs grimpable
                     */
                    foreach (ClimbableBlock block in ClimbableBlock.ClimbableBlockList)
                    {
                        if (block.HitBox.Intersects(futurePos))
                        {
                            if (block.IsBreakable && player.HitAttack)
                            {
                                player.destroy(block);
                            }
                        }
                    }
                }

                if (player.IsJumping)
                {
                    player.JumpAnime();
                }

                if (player.IsThrowing)
                {
                    player.BlockPLayer();
                    player.CanMove = false;
                    player.Speed = 0;
                    player.throwItem(cible, pad);
                }

            }
            oldKeyboard = keyboard;
            oldPad = pad;
        }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            foreach (Blocks block in Blocks.BlockList)
            {
                if (block.IsActive)
                {
                    if (!player.Statut && player.IsActiveVision)
                    {
                        if (block.IsJekyllVisible)
                            spriteBatch.Draw(block.Texture, block.HitBox, Color.Yellow);
                        else
                            spriteBatch.Draw(block.Texture, block.HitBox, Color.Blue);
                    }
                    else if (player.Statut && player.IsActiveVision)
                    {
                        if (block.IsHideVisible)
                            spriteBatch.Draw(block.Texture, block.HitBox, Color.Yellow);
                        else
                            spriteBatch.Draw(block.Texture, block.HitBox, Color.Red);
                    }
                    else if (!player.Statut && player.IsActiveObject)
                    {
                        spriteBatch.Draw(block.Texture, block.HitBox, Color.LightGreen);
                    }
                    else
                    {
                        spriteBatch.Draw(block.Texture, block.HitBox, Color.White);
                    }
                }
            }

            if (player.IsThrowing)
            {
                spriteBatch.Draw(cible.Texture, cible.HitBox, Color.White);
            }

            if (cible.IsItemThrow)
            {
                spriteBatch.Draw(Ressources.TextureList[0], cible.ItemBox, Color.White);
                cible.CheckMove();
            }

            if (puzzle != null)
            {
                puzzle.Draw(spriteBatch);
            }
        }

        public void SetPlayerAccelMode(GameTime gameTime, Player player)
        {
            if (player.Statut)
            {
                if (!player.IsJumping && !player.IsAttacking && player.CanMove && player.Speed <= 3)
                    player.Speed += 0.01f;


                if (player.Speed <= 2)
                {
                    player.AccelMode = 1;
                }
                else if (player.Speed > 2 && player.Speed <= 3.1f)
                {
                    player.AccelMode = 2;
                }
                else if (player.Speed == 5)
                {
                    player.AccelMode = 3;
                }
            }
            else
            {
                if (player.CanMove && player.Speed <= 3)
                    player.Speed += 0.01f;


                if (player.Speed <= 2)
                {
                    player.AccelMode = 1;
                }
                else if (player.Speed > 2 && player.Speed <= 3)
                {
                    player.AccelMode = 2;
                }
                
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
                if(block.IsCollidable && block.IsActive)
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
