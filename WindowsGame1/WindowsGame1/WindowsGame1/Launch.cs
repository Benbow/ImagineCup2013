using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Overload
{
    class Launch
    {
        private Rectangle _cible;
        private Rectangle futurCible;
        private Rectangle box;
        private int decalage = 0;
        private int x_ini;
        private Texture2D _text = Ressources.cible;
        private Texture2D _text_ob = Ressources.bottle;
        private Rectangle ob;
        private float _fspeed = 0;
        private float _speedInAir = 0;
        private int _vit = 0;
        public Direction sens;
        private int _col = 0;
        private int _timer = 0;

        private Rectangle effectZone;
        private Texture2D effect_text;

        private bool _isItemThrow = false;
        private bool _isItemCrash = false;
        private bool _isBoxCrash = false;
        private bool _isBoxThrow = false;
        private bool _isBoxLaunch = false;

        public Launch(int x, int y, Direction dir)
        {
            this.decalage = 50;
            this.sens = dir;

            if(dir == Direction.Left)
                this.x_ini = x + 50;
            else if (dir == Direction.Right)
                this.x_ini = x - 50;

            this._cible = new Rectangle(x, y, _text.Width, _text.Height);
            this.ob = new Rectangle(x_ini, y - 20, 15, _text_ob.Height);
            this.box = new Rectangle(x_ini + 5, y - 50, Ressources.boxH.Width, Ressources.boxH.Height);
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
                    this.ob.Y = block.HitBox.Y - ob.Height;
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
                    //if(ob.X > _cible.X)
                        this.ob.X -= _vit;
                }
                else if (sens == Direction.Right)
                {
                    //if (ob.X < _cible.X)
                        this.ob.X += _vit;
                }

                int diff = this.ob.Y - futurPos.Y;
                this.ob.Y -= diff;
            }
            else
            {
                this._fspeed = 0;
                if (!this._isItemCrash)
                    this._col = 0;
                this._isItemCrash = true;
                this._text_ob = Ressources.bottle_crash;
            }
        }

        public void CheckBoxMove()
        {
            bool colide = false;
            Rectangle futurPos = this.box;
            futurPos.Y += 1 + (int)this._fspeed;
            foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
            {
                if (block != StaticNeutralBlock.StaticNeutralList[0] && block != StaticNeutralBlock.StaticNeutralList[1] &&
                    block != StaticNeutralBlock.StaticNeutralList[2] && block != StaticNeutralBlock.StaticNeutralList[3])
                {
                    if (futurPos.Intersects(block.HitBox))
                    {
                        colide = true;
                        this.box.Y = block.HitBox.Y - box.Height;
                        break;
                    }
                }
            }

            if (!colide)
            {
                if (this._fspeed >= 0)
                    this._fspeed += 0.15f * (5 / 4);
                else
                    this._fspeed += 0.10f * (5 / 4);

                if (sens == Direction.Left)
                {
                    this.box.X -= _vit;
                }
                else if (sens == Direction.Right)
                {
                    this.box.X += _vit;
                }

                int diff = this.box.Y - futurPos.Y;
                this.box.Y -= diff;
            }
            else
            {
                this._fspeed = 0;
                foreach (MovableEnnemyBlock block in MovableEnnemyBlock.MovableEnnemyList)
                {
                    if (block.HitBox.Intersects(this.box))
                    {
                        block.IsActive = false;
                    }
                }
                this._isBoxCrash = true;
                this._isBoxThrow = false;
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

        public Rectangle EffetZone
        {
            get { return effectZone; }
            set { effectZone = value; }
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

        public int ItemColumn
        {
            get { return _col; }
            set { _col = value; }
        }

        public int TimerThrow
        {
            get { return _timer; }
            set { _timer = value; }
        }

        public bool IsItemCrash
        {
            get { return _isItemCrash; }
            set { _isItemCrash = value; }
        }

        public bool IsBoxLaunch
        {
            get { return _isBoxLaunch; }
            set { _isBoxLaunch = value; }
        }

        public bool IsBoxThrow
        {
            get { return _isBoxThrow; }
            set { _isBoxThrow = value; }
        }

        public Rectangle BoxBox
        {
            get { return box; }
            set { box = value; }
        }

        public bool IsBoxCrash
        {
            get { return _isBoxCrash; }
            set { _isBoxCrash = value; }
        }
    }
}
