using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    class MapOld
    {
        private int _width;
        private int _height;
        private List<BlockOld> _blockList;

        bool blockMove = true;
        bool playerMove;
        Rectangle futurePos;
        int i;

        public MapOld(int x, int y, List<BlockOld> blocks)
        {
            _width = x;
            _height = y;
            _blockList = blocks;
        }

        public void Update(KeyboardState keyboard, MouseState mouse, PlayersOld player)
        {
            //SAUT
            if (keyboard.IsKeyDown(Keys.Space))
            {
                if (!player.IsInJump)
                {
                    player.IsInJump = true;
                    player.Speed = -15;
                }
            }

            if (player.IsInJump)
            {
                player.Jump();
            }
            else
            {
                player.Speed = 15;
            }


            // CHUTE
                futurePos = player.Player;
                futurePos.Y += player.Player.Height;
                blockMove = true;
                if (player.IsInJump && player.Speed < 0)
                {
                    blockMove = true;
                }
                else
                {
                    foreach (BlockOld block in _blockList)
                    {
                        if (block.Collidable && futurePos.Intersects(block.Hitbox))
                        {
                            player.IsInJump = false;
                            blockMove = false;
                        }
                    }
                }
                
                futurePos.Y -= player.Player.Height;
                if (blockMove)
                {
                    i = 0;
                    playerMove = false;
                    foreach (BlockOld block in _blockList)
                    {
                        i++;

                        if (i <= 3)
                        {
                            if ((i == 3 && player.Player.Y + 455 > block.Hitbox.Y) /*|| (i == 2 && player.Player.X > block.Hitbox.X - 300)*/)
                            {
                                playerMove = true;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (playerMove)
                    {
                        player.IncreaseCoordBlockY();
                    }
                    else
                    {
                        foreach (BlockOld block in _blockList)
                        {
                            block.DecreaseCoordBlockY(player.Speed);
                        }
                    }
                }

            

            //Mouvement Gauche
            if(keyboard.IsKeyDown(Keys.Left)){
                
                futurePos.X -= player.Speed;
                blockMove = true;
                foreach (BlockOld block in _blockList)
                {
                    if (block.Collidable && futurePos.Intersects(block.Hitbox))
                    {
                        player.IsInJump = false;
                        blockMove = false;
                    }
                }
                if (blockMove)
                {
                    i = 0;
                    playerMove = false;
                    foreach (BlockOld block in _blockList)
                    {
                        i++;
                        
                        if (i <= 4)
                        {
                            if ((i == 4 && player.Player.X - 300 < block.Hitbox.X) || (i == 2 && player.Player.X > block.Hitbox.X - 300))
                            {
                                playerMove = true;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (playerMove)
                    {
                        if (player.IsInJump && player.Speed < 0)
                            player.IncreaseCoordBlockX();
                        else
                            player.DecreaseCoordBlockX();
                    }
                    else
                    {
                        if (player.IsInJump && player.Speed < 0)
                        {
                            foreach (BlockOld block in _blockList)
                            {
                                block.DecreaseCoordBlockX(player.Speed);
                            }
                        }
                        else
                        {
                            foreach (BlockOld block in _blockList)
                            {
                                block.IncreaseCoordBlockX(player.Speed);
                            }
                        }
                    }
                }
            }
            
            //mouvement droite
            if (keyboard.IsKeyDown(Keys.Right))
            {
                futurePos.X += player.Speed;
                blockMove = true;
                foreach (BlockOld block in _blockList)
                {
                    if (block.Collidable && futurePos.Intersects(block.Hitbox))
                    {
                        player.IsInJump = false;
                        blockMove = false;
                    }
                }
                if (blockMove)
                {
                    i = 0;
                    playerMove = false;
                    foreach (BlockOld block in _blockList)
                    {
                        i++;
                        if (i <= 4)
                        {
                            if ((i == 2 && player.Player.X > block.Hitbox.X - 300) || (i == 4 && player.Player.X - 300 < block.Hitbox.X))
                            {
                                playerMove = true;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (playerMove)
                    {
                        if(player.IsInJump && player.Speed < 0)
                            player.DecreaseCoordBlockX();
                        else
                            player.IncreaseCoordBlockX(); 
                    }
                    else
                    {
                        if (player.IsInJump && player.Speed < 0){
                            foreach (BlockOld block in _blockList)
                            {
                                block.IncreaseCoordBlockX(player.Speed);
                            }
                        }
                        else
                        {
                            foreach (BlockOld block in _blockList)
                            {
                                block.DecreaseCoordBlockX(player.Speed);
                            }
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (BlockOld block in _blockList)
            {
                spriteBatch.Draw(block.Texture, block.Hitbox, Color.White);
            }
        }
    }
}
