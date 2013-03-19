using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    class BulletBlock : Blocks
    {
        private Vector2 _dir;
        private int speed;
        private int strength;

        public static  List<BulletBlock> BulletBlockList = new List<BulletBlock>(); 

        public BulletBlock(int x, int y, Vector2 dir, int st)
        {
            this._texture = Ressources.bullet;
            this._hitBox = new Rectangle(x, y, 2, 1);
            this.speed = 5;
            this._dir = dir;
            this.strength = st;

            BulletBlockList.Add(this);
            BlockList.Add(this);
        }

        public void Update()
        {
            if (this.IsActive)
            {
                this._hitBox.X += this.speed*(int) this._dir.X;
                foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
                {
                    if (block.IsCollidable && block.IsActive)
                    {
                        if (this._hitBox.Intersects(block.HitBox))
                        {
                            this.IsActive = false;
                        }
                    }
                }
                foreach (ClimbableBlock block in ClimbableBlock.ClimbableBlockList)
                {
                    if (block.IsCollidable && block.IsActive)
                    {
                        if (this._hitBox.Intersects(block.HitBox))
                        {
                            this.IsActive = false;
                        }
                    }
                }
            }
        }
        public int Strength
        {
            get { return this.strength; }
        }
    }
}
