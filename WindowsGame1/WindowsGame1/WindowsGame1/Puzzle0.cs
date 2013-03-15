using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class Puzzle0 : Puzzle
    {
        private int level;
        private Texture2D reloadImg = Ressources.reload;
        private Texture2D ready = Ressources.ready;
        private Texture2D go = Ressources.go;
        private Texture2D imgA = Ressources.imgA;
        private Texture2D imgB = Ressources.imgB;
        private Texture2D imgY = Ressources.imgY;
        private Texture2D imgX = Ressources.imgX;
        private Texture2D imgUP = Ressources.imgUp;
        private Texture2D imgLEFT = Ressources.imgLeft;
        private Texture2D imgDOWN = Ressources.imgDown;
        private Texture2D imgRIGHT = Ressources.imgRight;
        private Rectangle ImgRec;
        private Rectangle ReloadRec = new Rectangle(FirstGame.W / 2 - 400 / 2, FirstGame.H / 2 - 200 / 2, 400, 200);

        private int _showTimer;
        private int _spentTime;
        private int _interTimer;
        private int _readyTime;
        private int _time;

        private bool play = false;
        private bool success = false;
        private int count;
        private int tempCount;
        private int winCount;
        private bool next = false;

        private bool status;
        private bool pressA = false;
        private bool pressB = false;
        private bool pressY = false;
        private bool pressX = false;
        private bool pressUp = false;
        private bool pressDown = false;
        private bool pressLeft = false;
        private bool pressRight = false;
        private bool reload = false;

        GamePadState oldpaPadState;

        public Puzzle0()
        {
            this._text = Ressources.enigmes_fond;
            this._x = FirstGame.W / 2 - this._text.Width / 2;
            this._y = FirstGame.H / 2 - this._text.Height / 2;
            this._hitBox = new Rectangle(_x, _y, _text.Width, _text.Height);
            this.ImgRec = new Rectangle(FirstGame.W / 2 - 200 / 2, FirstGame.H / 2 - 200 / 2, 200, 200);
            this.level = 1;
            this.initLevel();
            this._time = 0;
            this.tempCount = 0;
            this.winCount = 2;
            this.status = true;
            this.count = 0;
            PuzzleList.Add(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._text, this._hitBox, Color.White);
            
                if (play == true)
                {
                    if (this.reload)
                    {
                        spriteBatch.Draw(this.reloadImg, this.ReloadRec, Color.White);
                    }
                    else
                    {
                        if (this.pressA)
                            spriteBatch.Draw(this.imgA, this.ImgRec, Color.White);
                        else if (this.pressB)
                            spriteBatch.Draw(this.imgB, this.ImgRec, Color.White);
                        else if (this.pressY)
                            spriteBatch.Draw(this.imgY, this.ImgRec, Color.White);
                        else if (this.pressX)
                            spriteBatch.Draw(this.imgX, this.ImgRec, Color.White);
                        else if (this.pressUp)
                            spriteBatch.Draw(this.imgUP, this.ImgRec, Color.White);
                        else if (this.pressRight)
                            spriteBatch.Draw(this.imgRIGHT, this.ImgRec, Color.White);
                        else if (this.pressDown)
                            spriteBatch.Draw(this.imgDOWN, this.ImgRec, Color.White);
                        else if (this.pressLeft)
                            spriteBatch.Draw(this.imgLEFT, this.ImgRec, Color.White);
                    }
                }
                else
                {
                    if (level == 1)
                    {
                        if (this._time <= this._readyTime)
                        {
                            spriteBatch.Draw(this.ready, this.ImgRec, Color.White);
                        }
                        else if (this._time <= this._readyTime + this._showTimer)
                        {
                            spriteBatch.Draw(this.imgA, this.ImgRec, Color.White);
                        }
                        else if (this._time <= this._readyTime + this._showTimer + this._interTimer)
                        {

                        }
                        else if (this._time <= this._readyTime + this._showTimer * 2 + this._interTimer)
                        {
                            spriteBatch.Draw(this.imgX, this.ImgRec, Color.White);
                        }
                        else if (this._time <= this._readyTime + this._showTimer * 2 + this._interTimer * 2)
                        {

                        }
                        else if (this._time <= this._readyTime + this._showTimer * 3 + this._interTimer * 2)
                        {
                            spriteBatch.Draw(this.go, this.ImgRec, Color.White);
                        }
                    }
                    if (level == 2)
                    {
                        
                        if (this._time <= this._readyTime)
                        {
                            spriteBatch.Draw(this.ready, this.ImgRec, Color.White);
                        }
                        else if (this._time <= this._readyTime + this._showTimer)
                        {
                            spriteBatch.Draw(this.imgA, this.ImgRec, Color.White);
                        }
                        else if (this._time <= this._readyTime + this._showTimer + this._interTimer)
                        {

                        }
                        else if (this._time <= this._readyTime + this._showTimer * 2 + this._interTimer)
                        {
                            spriteBatch.Draw(this.imgX, this.ImgRec, Color.White);
                        }
                        else if (this._time <= this._readyTime + this._showTimer * 2 + this._interTimer * 2)
                        {

                        }
                        else if (this._time <= this._readyTime + this._showTimer * 3 + this._interTimer*2)
                        {
                            spriteBatch.Draw(this.imgUP, this.ImgRec, Color.White);
                        }
                        else if (this._time <= this._readyTime + this._showTimer * 3 + this._interTimer * 3)
                        {

                        }
                        else if (this._time <= this._readyTime + this._showTimer * 4 + this._interTimer * 3)
                        {
                            spriteBatch.Draw(this.imgY, this.ImgRec, Color.White);
                        }
                        else if (this._time <= this._readyTime + this._showTimer * 4 + this._interTimer * 4)
                        {

                        }
                        else if (this._time <= this._readyTime + this._showTimer * 5 + this._interTimer * 4)
                        {
                            spriteBatch.Draw(this.go, this.ImgRec, Color.White);
                        }
                    }
                }
            
        }

        public void Update(GamePadState pad, GameTime time)
        {
            this._time += time.ElapsedGameTime.Milliseconds;
            if ((this._time > this._readyTime + this._showTimer * 3 + this._interTimer * 2) && play == false && level == 1)
            {
                this._time = 0;
                play = true;
            }
            if ((this._time > this._readyTime + this._showTimer * 5 + this._interTimer * 4) && play == false && level == 2)
            {
                this._time = 0;
                play = true;
                
            }

            if (success)
            {
                status = false;
            }
            if (next)
            {
                next = false;
                level++;
                changeLevel(level);
                if (level == 3)
                    success = true;
            }
            if (this.play)
            {
                if (this._time >= this._spentTime)
                {
                    this.reload = true;
                }

                if (this.reload)
                {
                    if (pad.IsButtonDown(Buttons.A) && oldpaPadState.IsButtonUp(Buttons.A))
                    {
                        this.reload = false;
                        this.play = false;
                        this.level = 1;
                        changeLevel(level);
                        this.pressA = false;
                        this.pressB = false;
                        this.pressX = false;
                        this.pressY = false;
                        this.pressUp = false;
                        this.pressRight = false;
                        this.pressDown = false;
                        this.pressLeft = false;
                    }
                    if (pad.IsButtonDown(Buttons.B) && oldpaPadState.IsButtonUp(Buttons.B))
                    {
                        this.status = false;
                    }
                }
                else
                {
                    if (this.count < this.winCount)
                    {
                        if (pad.IsButtonDown(Buttons.A) && oldpaPadState.IsButtonUp(Buttons.A))
                        {
                            this.pressA = true;
                            this.pressB = false;
                            this.pressX = false;
                            this.pressY = false;
                            this.pressUp = false;
                            this.pressRight = false;
                            this.pressDown = false;
                            this.pressLeft = false;
                            tempCount++;
                        }
                        else if (pad.IsButtonDown(Buttons.B) && oldpaPadState.IsButtonUp(Buttons.B))
                        {
                            this.pressA = false;
                            this.pressB = true;
                            this.pressX = false;
                            this.pressY = false;
                            this.pressUp = false;
                            this.pressRight = false;
                            this.pressDown = false;
                            this.pressLeft = false;
                            tempCount++;
                        }
                        else if (pad.IsButtonDown(Buttons.Y) && oldpaPadState.IsButtonUp(Buttons.Y))
                        {
                            this.pressA = false;
                            this.pressB = false;
                            this.pressX = false;
                            this.pressY = true;
                            this.pressUp = false;
                            this.pressRight = false;
                            this.pressDown = false;
                            this.pressLeft = false;
                            tempCount++;
                        }
                        else if (pad.IsButtonDown(Buttons.X) && oldpaPadState.IsButtonUp(Buttons.X))
                        {
                            this.pressA = false;
                            this.pressB = false;
                            this.pressX = true;
                            this.pressY = false;
                            this.pressUp = false;
                            this.pressRight = false;
                            this.pressDown = false;
                            this.pressLeft = false;
                            tempCount++;
                        }
                        else if (pad.IsButtonDown(Buttons.LeftThumbstickUp) && oldpaPadState.IsButtonUp(Buttons.LeftThumbstickUp))
                        {
                            this.pressA = false;
                            this.pressB = false;
                            this.pressX = false;
                            this.pressY = false;
                            this.pressUp = true;
                            this.pressRight = false;
                            this.pressDown = false;
                            this.pressLeft = false;
                            tempCount++;
                        }
                        else if (pad.IsButtonDown(Buttons.LeftThumbstickRight) && oldpaPadState.IsButtonUp(Buttons.LeftThumbstickRight))
                        {
                            this.pressA = false;
                            this.pressB = false;
                            this.pressX = false;
                            this.pressY = false;
                            this.pressUp = false;
                            this.pressRight = true;
                            this.pressDown = false;
                            this.pressLeft = false;
                            tempCount++;
                        }
                        else if (pad.IsButtonDown(Buttons.LeftThumbstickDown) && oldpaPadState.IsButtonUp(Buttons.LeftThumbstickDown))
                        {
                            this.pressA = false;
                            this.pressB = false;
                            this.pressX = false;
                            this.pressY = false;
                            this.pressUp = false;
                            this.pressRight = false;
                            this.pressDown = true;
                            this.pressLeft = false;
                            tempCount++;
                        }
                        else if (pad.IsButtonDown(Buttons.LeftThumbstickLeft) && oldpaPadState.IsButtonUp(Buttons.LeftThumbstickLeft))
                        {
                            this.pressA = false;
                            this.pressB = false;
                            this.pressX = false;
                            this.pressY = false;
                            this.pressUp = false;
                            this.pressRight = false;
                            this.pressDown = false;
                            this.pressLeft = true;
                            tempCount++;
                        }

                        if (level == 1)
                        {
                            if (tempCount > 0)
                            {
                                if (tempCount == 1 && pad.IsButtonDown(Buttons.A) && oldpaPadState.IsButtonUp(Buttons.A))
                                {
                                    count++;
                                }
                                else if (tempCount == 1 && (pressB || pressX || pressY || pressDown || pressUp || pressLeft || pressRight))
                                {
                                    reload = true;
                                }
                                if (tempCount == 2 && pad.IsButtonDown(Buttons.X) && oldpaPadState.IsButtonUp(Buttons.X))
                                {
                                    count++;
                                    Console.WriteLine("TRUE");
                                }
                                else if (tempCount == 2 && (pressB || pressA || pressY || pressDown || pressUp || pressLeft || pressRight))
                                {
                                    reload = true;
                                }
                            }
                        } 
                        else if (level == 2)
                        {
                            
                            if (tempCount > 0)
                            {
                                
                                if (tempCount == 1 && pad.IsButtonDown(Buttons.A) && oldpaPadState.IsButtonUp(Buttons.A))
                                {
                                    Console.WriteLine("TRUE2");
                                    count++;
                                }
                                else if (tempCount == 1 && (pressB || pressX || pressY || pressDown || pressUp || pressLeft || pressRight))
                                {
                                    reload = true;
                                }
                                if (tempCount == 2 && pad.IsButtonDown(Buttons.X) && oldpaPadState.IsButtonUp(Buttons.X))
                                {
                                    count++;
                                }
                                else if (tempCount == 2 && (pressB || pressA || pressY || pressDown || pressUp || pressLeft || pressRight))
                                {
                                    reload = true;
                                }
                                if (tempCount == 3 && pad.IsButtonDown(Buttons.LeftThumbstickUp) && oldpaPadState.IsButtonUp(Buttons.LeftThumbstickUp))
                                {
                                    count++;
                                }
                                else if (tempCount == 3 && (pressB || pressA || pressY || pressDown || pressX || pressLeft || pressRight))
                                {
                                    reload = true;
                                }
                                if (tempCount == 4 && pad.IsButtonDown(Buttons.Y) && oldpaPadState.IsButtonUp(Buttons.Y))
                                {
                                    count++;
                                }
                                else if (tempCount == 4 && (pressB || pressA || pressX || pressDown || pressUp || pressLeft || pressRight))
                                {
                                    reload = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        next = true;
                    }
                }
            }
            oldpaPadState = pad;
        }

        public void initLevel()
        {
            if (level == 1)
            {
                this._interTimer = 500;
                this._showTimer = 1000;
                this._readyTime = 1500;
                this._spentTime = 2000;
                this.winCount = 2;
            }
            else if (level == 2)
            {
                this._interTimer = 500;
                this._showTimer = 1000;
                this._readyTime = 1500;
                this._spentTime = 4000;
                this.winCount = 4;
            }
            else if (level == 3)
            {
                this._interTimer = 500;
                this._showTimer = 1000;
                this._readyTime = 1500;
                this._spentTime = 2000;
                this.winCount = 2;
            }
            else if (level == 4)
            {
                this._interTimer = 500;
                this._showTimer = 1000;
                this._readyTime = 1500;
                this._spentTime = 4000;
                this.winCount = 2;
            }
            else if (level == 5)
            {
                this._interTimer = 500;
                this._showTimer = 1000;
                this._readyTime = 1500;
                this._spentTime = 2000;
                this.winCount = 2;
            }
        }

        public void changeLevel(int i)
        {
            
            this.pressA = false;
            this.pressB = false;
            this.pressX = false;
            this.pressY = false;
            this.pressUp = false;
            this.pressRight = false;
            this.pressDown = false;
            this.pressLeft = false;
            this.level = i;
            
            this.initLevel();
            this._time = 0;
            this.count = 0;
            this.tempCount = 0;
            this.play = false;
        }

        public bool Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }
}
