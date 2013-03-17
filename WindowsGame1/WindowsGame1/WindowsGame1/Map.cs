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
        Puzzle0 puzzle0 = null;
        Puzzle1 puzzle1 = null;

        bool slide = false;

        int accelTimer;

        public Map(int x, int y)
        {
            _width = x;
            _height = y;
            _leftSide = new DelimiterZone(0, 0, FirstGame.W / 2, _height, Ressources.invisible);
            _rightSide = new DelimiterZone(_width - FirstGame.W / 2, 0, FirstGame.W / 2, _height, Ressources.invisible);
            _upSide = new DelimiterZone(0, 0, _width, FirstGame.H / 2, Ressources.invisible);
            _downSide = new DelimiterZone(0, _height - FirstGame.H / 2, _width, FirstGame.H / 2, Ressources.invisible);
        }

        public void Update(KeyboardState keyboard, GamePadState pad, MouseState mouse, GameTime gameTime, Player player)
        {
            if (FirstGame.checkpoint && !slide)
            {
                this.Slide(600, player);
                slide = true;
            }

            if (GameMain.Status != "inventory")
            {
                if (player.GetType() == typeof(Jekyll))
                {
                    foreach (InteractZoneBlockWithPuzzle interBlock in InteractZoneBlockWithPuzzle.InteractZoneBlockList)
                    {
                        if (player.HitBox.Intersects(interBlock.HitBox))
                        {
                            if (interBlock.IsActivate)
                            {
                                if (interBlock.Id == 0)
                                {
                                    if (puzzle0 == null)
                                        puzzle0 = new Puzzle0();
                                    else
                                        puzzle0.Update(pad, gameTime);
                                }
                                else if (interBlock.Id == 1)
                                {
                                    if (puzzle1 == null)
                                        puzzle1 = new Puzzle1();
                                    else
                                        puzzle1.Update(pad, gameTime);
                                }

                                if (puzzle0 != null && !puzzle0.Status)
                                {
                                    interBlock.IsActivate = false;
                                    if (puzzle0.Success)
                                    {
                                        MovableNeutralBlock.MovableNeutralList[0].Activate = true;
                                    }

                                    puzzle0 = null;
                                    puzzle1 = null;
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
            }

            if (GameMain.Status == "on")
            {

                futurePos = player.HitBox;
                player.CheckMove();
                // Animation des blocs mouvants
                foreach (MovableNeutralBlock block in MovableNeutralBlock.MovableNeutralList)
                {
                    if (block.IsActive)
                        block.Update(gameTime);
                }
                foreach (MovableEnnemyBlock block in MovableEnnemyBlock.MovableEnnemyList)
                {
                    if (block.IsActive)
                    {
                        block.Update(gameTime, player, keyboard);
                        if (player.HitBox.Intersects(block.SpotZone))
                        {
                            block.HaveSpotted = true;
                        }
                    }
                }
                foreach (BulletBlock bullet in BulletBlock.BulletBlockList)
                {
                    bullet.Update();
                    if (bullet.HitBox.Intersects(player.HitBox) && bullet.IsActive)
                    {
                        bullet.IsActive = false;
                        player.Health -= bullet.Strength + 1;
                        if (player.Health <= 0)
                        {
                            FirstGame.reload = true;
                        }
                    }
                }

                foreach (ItemBlock item in ItemBlock.ItemBlockList)
                {
                    if (item.IsActive)
                    {
                        if (item.HitBox.Intersects(player.HitBox) && item.IsActive && !player.Statut)
                        {
                            item.IsActive = false;
                            InventoryCase.InventoryCaseList[item.Id].IsEmpty = false;
                        }
                    }
                }

                

                foreach (Camera cam in Camera.CamerasBlockList)
                {
                    if (cam.IsActive)
                    {
                        cam.Update(gameTime);
                        if (!player.Statut && !player.IsHiding && player.HitBox.Intersects(cam.Spot_rec))
                        {
                            FirstGame.reload = true;
                        }
                    }
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
                    if (player.CanMove)
                    {
                        this.SetPlayerAccelMode(gameTime, player);
                        this.Move(Keys.Left, player);
                    }
                }
                else if (pad.IsButtonDown(Buttons.LeftThumbstickRight))
                {
                    if (player.CanMove)
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
                        if (player.GetType() == typeof(Jekyll))
                        {
                            if (player.HitBox.Intersects(ladder.HitBox) && !player.IsCrouch)
                            {
                                Rectangle left = new Rectangle(player.HitBox.X, player.HitBox.Y, 5, player.HitBox.Height);
                                Rectangle right = new Rectangle(player.HitBox.X + player.HitBox.Width - 5, player.HitBox.Y, 5, player.HitBox.Height);
                                if (ladder.HitBox.Intersects(left) && ladder.HitBox.Intersects(right))
                                {
                                    lad = true;
                                    player.DecreaseCoordY(1);
                                }
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
                            Rectangle left = new Rectangle(player.HitBox.X, player.HitBox.Y, 5, player.HitBox.Height);
                            Rectangle right = new Rectangle(player.HitBox.X + player.HitBox.Width - 5, player.HitBox.Y, 5, player.HitBox.Height);
                            if (ladder.HitBox.Intersects(left) && ladder.HitBox.Intersects(right))
                            {
                                player.DecreaseCoordY(1);
                            }
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
                                Rectangle feet = new Rectangle(player.HitBox.X, player.HitBox.Y + player.HitBox.Height - 1, player.HitBox.Width, 1);
                                Rectangle feetplus = feet;
                                feetplus.Y++;
                                if (feet.Intersects(ladder.HitBox) || feetplus.Intersects(ladder.HitBox) && !player.IsCrouch)
                                {
                                    Rectangle left = new Rectangle(player.HitBox.X, player.HitBox.Y, 5, player.HitBox.Height);
                                    Rectangle right = new Rectangle(player.HitBox.X + player.HitBox.Width - 5, player.HitBox.Y, 5, player.HitBox.Height);
                                    if (ladder.HitBox.Intersects(left) && ladder.HitBox.Intersects(right))
                                    {
                                        player.IncreaseCoordY(1);
                                    }
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

                if (pad.IsButtonDown(Buttons.A) && oldPad.IsButtonUp(Buttons.A) && !player.IsJumping)
                {
                    if (player.Statut && player.CanJump)
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
                    else if (!player.Statut && player.CanClimb)
                    {
                        if (pad.IsButtonDown(Buttons.LeftThumbstickRight))
                        {
                            futurePos.X += (int)player.Speed;
                            foreach (ClimbableBlock block in ClimbableBlock.ClimbableBlockList)
                            {
                                if (block.HitBox.Intersects(futurePos))
                                {
                                    if (block.IsActive && block.IsClimbable)
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
                                    if (block.IsActive && block.IsClimbable)
                                        player.ClimbBox(block, 1);
                                }
                            }
                        }
                    }
                }

                if (pad.IsButtonDown(Buttons.RightShoulder) && oldPad.IsButtonUp(Buttons.RightShoulder))
                {
                    if (player.Statut)
                    {
                        if (player.CanHVision)
                        {
                            if (player.IsActiveVision)
                                player.IsActiveVision = false;
                            else
                                player.IsActiveVision = true;
                        }
                    }
                    else
                    {
                        if (player.CanJVision)
                        {
                            if (player.IsActiveVision)
                                player.IsActiveVision = false;
                            else
                                player.IsActiveVision = true;
                        }
                    }
                }

                if (player.IsAttacking)
                {
                    player.Attack();
                    player.BlockPLayer();

                    if (player.DirectionPlayer == Direction.Left)
                    {
                        futurePos.X -= 14;
                    }
                    else if (player.DirectionPlayer == Direction.Right)
                    {
                        futurePos.X += 14;
                    }

                    /*
                     * Test de collision quand on attaque sur les blocs grimpables
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

                    foreach (MovableEnnemyBlock block in MovableEnnemyBlock.MovableEnnemyList)
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

                if (pad.IsButtonDown(Buttons.B) && oldPad.IsButtonUp(Buttons.B))
                {
                    if (player.Statut && player.EndAttack)
                    {
                        player.IsAttacking = true;
                        player.BeginAttack = true;
                        player.EndAttack = false;
                    }
                    else if (!player.Statut && player.CanHide)
                    {
                        foreach (HidingBlock block in HidingBlock.HidingBlockList)
                        {
                            if (block.HitBox.Intersects(futurePos))
                            {
                                if (!block.IsHide)
                                {
                                    player.hide(block, 0);
                                }
                            }
                        }
                    }
                }

                if (player.IsHiding)
                {
                    foreach (HidingBlock block in HidingBlock.HidingBlockList)
                    {
                        if (!block.HitBox.Intersects(futurePos) && player.IsHiding)
                        {
                            player.hide(block, 1);
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

                foreach (MovableNeutralBlock block in MovableNeutralBlock.MovableNeutralList)
                {
                    Rectangle futurPos = player.HitBox;
                    futurPos.Y++;
                    if (futurPos.Intersects(block.HitBox) && block.IsActive)
                    {
                        player.takeElevators(block, _downSide, _upSide);
                    }
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
                    if (player.GetType() == typeof(Jekyll) && player.IsActiveVision)
                    {
                        if (block.IsJekyllVisible)
                            spriteBatch.Draw(block.Texture, block.HitBox, Color.Yellow);
                        else
                            spriteBatch.Draw(block.Texture, block.HitBox, Color.Blue);
                    }
                    else if (player.GetType() == typeof(Hide) && player.IsActiveVision)
                    {
                        if (block.IsHideVisible)
                            spriteBatch.Draw(block.Texture, block.HitBox, Color.Yellow);
                        else
                            spriteBatch.Draw(block.Texture, block.HitBox, Color.Red);
                    }
                    else if (!player.Statut && player.IsActiveObject && InventoryCase.InventoryCaseList[0].Status)
                    {
                        spriteBatch.Draw(block.Texture, block.HitBox, Color.LightGreen);
                    }

                    else
                    {
                        spriteBatch.Draw(block.Texture, block.HitBox, Color.White);
                    }
                }


            }
            foreach (Camera cam in Camera.CamerasBlockList)
            {
                if (cam.IsActive)
                    cam.Draw(spriteBatch);
            }
            foreach (MovableEnnemyBlock block in MovableEnnemyBlock.MovableEnnemyList)
            {
                if (block.IsActive)
                    block.Draw(spriteBatch);
            }

            if (puzzle0 != null)
                puzzle0.Draw(spriteBatch);
            else if (puzzle1 != null)
                puzzle1.Draw(spriteBatch);
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
                        foreach (Camera cam in Camera.CamerasBlockList)
                        {
                            cam.IncreaseSpotCoordBlockY(1);
                        }
                        foreach (MovableEnnemyBlock block in MovableEnnemyBlock.MovableEnnemyList)
                        {
                            block.IncreaseCoordBlockX(1);
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
                    foreach (Camera cam in Camera.CamerasBlockList)
                    {
                        cam.DecreaseSpotCoordBlockY(speedShow);
                    }
                    foreach (MovableEnnemyBlock block in MovableEnnemyBlock.MovableEnnemyList)
                    {
                        block.DecreaseCoordBlockX(1);
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
            }
            if (blockMove)
            {
                playerMove = false;
                float sp = player.Speed;
                Rectangle RightCut = new Rectangle(player.HitBox.X + (player.HitBox.Width / 2), player.HitBox.Y, player.HitBox.Width / 2, player.HitBox.Height);
                Rectangle LeftCut = new Rectangle(player.HitBox.X, player.HitBox.Y, player.HitBox.Width / 2, player.HitBox.Height);
                Rectangle RightSide = Map._rightSide.HitBox;
                Rectangle LeftSide = Map._leftSide.HitBox;
                RightSide.X += (int)sp;
                RightSide.Width -= (int)sp;
                LeftSide.Width -= (int)sp;

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
                            if (player.IsJumping)
                                block.IncreaseCoordBlockX((int)player.SpeedInAir);
                            else
                                block.IncreaseCoordBlockX((int)player.Speed);
                        }
                        foreach (Camera cam in Camera.CamerasBlockList)
                        {
                            if (player.IsJumping)
                                cam.IncreaseSpotCoordBlockX((int)player.SpeedInAir);
                            else
                                cam.IncreaseSpotCoordBlockX((int)player.Speed);
                        }
                        foreach (MovableEnnemyBlock block in MovableEnnemyBlock.MovableEnnemyList)
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
                        foreach (Camera cam in Camera.CamerasBlockList)
                        {
                            if (player.IsJumping)
                                cam.DecreaseSpotCoordBlockX((int)player.SpeedInAir);
                            else
                                cam.DecreaseSpotCoordBlockX((int)player.Speed);
                        }
                        foreach (MovableEnnemyBlock block in MovableEnnemyBlock.MovableEnnemyList)
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


        public void Slide(int i, Player player)
        {
            foreach (Blocks block in Blocks.BlockList)
            {
                if (player.IsJumping)
                    block.DecreaseCoordBlockX(i);
                else
                    block.DecreaseCoordBlockX(i);
            }
            foreach (Camera cam in Camera.CamerasBlockList)
            {
                if (player.IsJumping)
                    cam.DecreaseSpotCoordBlockX(i);
                else
                    cam.DecreaseSpotCoordBlockX(i);
            }
            foreach (MovableEnnemyBlock block in MovableEnnemyBlock.MovableEnnemyList)
            {
                if (player.IsJumping)
                    block.DecreaseCoordBlockX(i);
                else
                    block.DecreaseCoordBlockX(i);
            }
        }

        public void SlideY(int i)
        {
            foreach (Blocks block in Blocks.BlockList)
            {

                block.DecreaseCoordBlockY(i);

            }
            foreach (Camera cam in Camera.CamerasBlockList)
            {

                cam.DecreaseSpotCoordBlockY(i);
            }
            foreach (MovableEnnemyBlock block in MovableEnnemyBlock.MovableEnnemyList)
            {

                block.DecreaseCoordBlockY(i);
            }
        }
    }

}
