using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class Launch
    {
        private Rectangle _cible;
        private Rectangle futurCible;
        private int decalage = 0;
        private int x_ini;
        private Texture2D _text = Ressources.cible;
        private Texture2D _text_ob = Ressources.TextureList[0];
        private Rectangle ob;
        private float _fspeed = 0;
        private float _speedInAir = 0;
        private int _vit = 0;
        public Direction sens;

        private bool _isItemThrow = false;

        public Launch(int x, int y, Direction dir)
        {
            this.decalage = 50;
            this.sens = dir;

            if(dir == Direction.Left)
                this.x_ini = x + 50;
            else if (dir == Direction.Right)
                this.x_ini = x - 50;

            this._cible = new Rectangle(x, y, _text.Width, _text.Height);
            this.ob = new Rectangle(x - 45, y - 20, _text_ob.Width, _text_ob.Height);
            this._fspeed -= 7;
        }

        public void CiblePos(int x, int y, GamePadState pad)
        {
            bool colide = false;
            futurCible = _cible;
            futurCible.Y -= 8;
            if (pad.IsButtonDown(Buttons.LeftThumbstickLeft))
            {
                if (_cible.X >= (x - 400))
                {
                    futurCible.X -= 3;
                    foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
                    {
                        if (futurCible.Intersects(block.HitBox))
                        {
                            colide = true;
                        }
                    }

                    if (!colide)
                        this._cible.X -= 2;

                    if(_cible.X < x_ini)
                        sens = Direction.Left;
                }
            }
            else if (pad.IsButtonDown(Buttons.LeftThumbstickRight))
            {
                if (_cible.X <= (x + 400))
                {
                    futurCible.X += 3;
                    foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
                    {
                        if (futurCible.Intersects(block.HitBox))
                        {
                            colide = true;
                        }
                    }

                    if (!colide)
                        this._cible.X += 2;

                    if (_cible.X > x_ini)
                        sens = Direction.Right;
                }

            }
        }


        public void CheckMove()
        {
            bool colide = false;
            Rectangle futurPos = this.ob;
            futurPos.Y += 1 + (int)this._fspeed;
            foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
            {
                if (futurPos.Intersects(block.HitBox))
                {
                    colide = true;
                    this.ob.Y = block.HitBox.Y - block.HitBox.Height;
                    break;
                }
            }

            if (!colide)
            {
                if (this._fspeed >= 0)
                    this._fspeed += 0.15f*(5/4);
                else
                    this._fspeed += 0.10f*(5/4);

                if (sens == Direction.Left)
                {
                    if(ob.X > _cible.X)
                        this.ob.X -= _vit;
                }
                else if (sens == Direction.Right)
                {
                    if (ob.X < _cible.X)
                        this.ob.X += _vit;
                }

                int diff = this.ob.Y - futurPos.Y;
                this.ob.Y -= diff;
            }
            else
            {
                this._fspeed = 0;
                this._isItemThrow = false;
            }
        }

        public Rectangle HitBox
        {
            get { return _cible; }
            set { _cible = value; }
        }

        public Rectangle ItemBox
        {
            get { return ob; }
            set { ob = value; }
        }

        public Texture2D Texture
        {
            get { return _text; }
            set { _text = value; }
        }

        public bool IsItemThrow
        {
            get { return _isItemThrow; }
            set { _isItemThrow = value; }
        }

        public float FSpeed
        {
            get { return _fspeed; }
            set { _fspeed = value; }
        }

        public float SpeedInAir
        {
            get { return _speedInAir; }
            set { _speedInAir = value; }
        }

        public int Vitesse
        {
            get { return _vit; }
            set { _vit = value; }
        }
    }
}
