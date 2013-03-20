using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public enum Direction
    {
        Left, Right
    }

    abstract class Player
    {
        protected Rectangle _hitBox;
        protected Vector2 _pos;
        protected Vector2 _dir;
        protected LaunchableBlock _blockLaunch;

        protected Direction Direction;
        protected bool FrameAttackSens = true;
        protected int FrameColumnAttack;
        protected int FrameColumn;
        protected int FrameLine;
        protected int WidthSprite;
        protected int HeightSprite;
        protected SpriteEffects Effect;
        protected int Timer;
        protected int TimerAttack;
        protected int TimerJump;
        protected int TimerLaunch;
        protected int TimerMax;
        protected int TimerCrouch;
        protected int _throwId;
        protected bool _isSpriting = false;
        protected bool _isJumping = false;
        protected bool _isCrouch = false;
        protected bool _isAttacking = false;
        protected bool _isHiding = false;
        protected bool _beginAttack = false;
        protected bool _hitAttack = false;
        protected bool _endAttack = true;
        protected bool _beginJump = false;
        protected bool _Jump = false;
        protected bool _endJump = true;
        protected int _countJump = 0;
        protected bool playerMove;
        protected bool _isActiveVision = false;
        protected bool _isActiveObject = false;
        protected bool _canMove = true;
        protected bool _isThrowing = false;
        protected bool _isThrowBox = false;
        protected bool _isSwitch = false;

        //link competences
        protected bool _canClimb = false;
        protected bool _canJVision = false;
        protected bool _canHVision = false;
        protected bool _canHide = false;
        protected bool _canJump = false;

        protected float _speed = 1.5f;
        protected float _poids;
        protected int _strength;
        protected int _health;
        protected Texture2D _text;
        protected bool _isFalling;
        protected float _fallingSpeed = 0;
        protected float _impulsion;
        protected float _speedInAir;
        protected int _accelMode;
        protected bool _statut = false;
        protected bool _lookUpDownPhase;
        protected KeyboardState oldKeyboard;
        protected GamePadState oldPad;

        protected HidingBlock _hidingBlock;

        bool decale = false;

        public void Animate()
        {
            if (!this._isCrouch && !this._isJumping && !this._isAttacking && !this._isThrowBox)
            {
                this.Timer++;
                if (this.Timer == this.TimerMax)
                {
                    this.Timer = 0;
                    this.FrameColumn++;
                    if (this.FrameColumn > 11)
                    {
                        this.FrameColumn = 0;
                    }
                }
            }
        }

        public void CrouchAnime()
        {
            if (this._isCrouch)
            {
                this.FrameLine = 1;

                this.TimerCrouch++;
                if (this.TimerCrouch == 4)
                {
                    this.TimerCrouch = 0;
                    if (this.FrameColumn < 2)
                        this.FrameColumn++;

                }
            }
        }

        public void LaunchAnime(Launch cible)
        {
            if (!cible.IsBoxCrash && this._isThrowBox)
            {
                this.FrameLine = 0;

                this.TimerLaunch++;
                if (this.TimerLaunch == 4)
                {
                    this.TimerLaunch = 0;

                    if (this.FrameColumn < 3)
                        this.FrameColumn++;

                    if (FrameColumn == 1)
                    {
                        this._blockLaunch.IsActive = false;
                        cible.IsBoxLaunch = true;
                    }

                    if (FrameColumn == 3)
                    {
                        this.FrameLine = 0;
                        this.FrameColumn = 0;
                        this._isThrowBox = false;
                    }

                }
            }
            
        }

        public void AttackAnime()
        {
            if (this._isAttacking)
            {
                if (this.BeginAttack)
                    this._hitBox.X -= 14;
                this._beginAttack = false;
                this.FrameLine = 1;
                this.WidthSprite = 70;
                this._hitBox.Width = 70;
                this._canMove = false;

                this.TimerAttack++;
                if (this.TimerAttack == 3)
                {
                    this.TimerAttack = 0;
                    if (this.FrameAttackSens)
                    {
                        this.FrameColumn++;
                        if (this.FrameColumn > 4)
                        {
                            this.FrameColumn = 4;
                            this.FrameAttackSens = false;
                            this._hitAttack = true;
                        }
                    }
                    else
                    {
                        this.FrameColumn--;
                        this._hitAttack = false;
                        if (this.FrameColumn < 1)
                        {
                            this.FrameColumn = 4;
                            this.FrameAttackSens = true;
                            this._isAttacking = false;
                            this._endAttack = true;
                            this._canMove = true;
                            this.FrameLine = 0;
                            this.WidthSprite = 42;
                            this._hitBox.Width = 42;
                            this._hitBox.X += 14;
                        }
                    }
                }
            }
        }

        public void JumpAnime()
        {
            if (this._isJumping)
            {
                /*this._beginJump = false;
                this._endJump = false;
                this.FrameLine = 2;
                this.WidthSprite = 48;
                this._hitBox.Width = 48;

                this.TimerJump++;
                if (this.TimerJump == 3)
                {
                    this.TimerJump = 0;
                    if (this.FrameColumn < 3)
                    {
                        this.FrameColumn++;
                        this._Jump = true;
                    }

                    this._countJump++;
                    int check = 0;

                    switch (this._accelMode)
                    {
                        case 1:
                            check = 22;
                            break;
                        case 2:
                            check = 4;
                            break;
                        case 3:
                            check = 2;
                            break;
                    }

                    if (this._countJump >= check)
                    {
                        if(this.FrameColumn < 4)
                            this.FrameColumn++;
                        this._Jump = false;
                    }

                    if (this.FrameColumn == 4 && !this._Jump)
                    {
                        this._endJump = true;
                        this._countJump = 0;
                        this.FrameColumn = 0;
                        this.FrameLine = 0;
                        this.WidthSprite = 42;
                        this._hitBox.Width = 42;
                    }

                }*/


                this.WidthSprite = 48;
                this._hitBox.Width = 48;
                this.FrameColumn = 2;
                this.FrameLine = 2;
            }
        }

        public bool Switch(GamePadState pad)
        {
            if (GameMain.Status == "on")
            {
                if (pad.IsButtonDown(Buttons.LeftShoulder) && oldPad.IsButtonUp(Buttons.LeftShoulder))
                {
                    this.IsActiveObject = false;
                    this._isSwitch = true;

                    if (_statut)
                        this._hitBox.Y += 20;
                    else
                        this._hitBox.Y -= 20;
                    _statut = !_statut;
                }
                oldPad = pad;
            }
            return _statut;
        }

        public void MovePlayerLeft(bool player)
        {
            this.playerMove = true;
            this.Direction = Direction.Left;
            if (player)
            {
                if (this._isJumping)
                {
                    this._hitBox.X -= (int)this._speedInAir;
                }
                else
                {
                    this._hitBox.X -= (int)this._speed;
                }
            }
            if (!this._isJumping && !this._isThrowing)
            {
                this.Animate();
            }
        }

        public void MovePlayerRight(bool player)
        {
            this.playerMove = true;
            this.Direction = Direction.Right;
            if (player)
            {
                if (this._isJumping)
                {
                    this._hitBox.X += (int)this._speedInAir;
                }
                else
                {
                    this._hitBox.X += (int)this._speed;
                }

            }
            if (!this._isJumping && !this._isThrowing)
                this.Animate();
        }

        public void JumpPlayer()
        {
            if (!this._isFalling && this._statut)
            {
                this._isJumping = true;
                this._fallingSpeed -= this._impulsion;
            }
        }

        public void BlockPLayer()
        {
            if (!this._isAttacking && !this._isJumping && !this._isCrouch && this._statut && !this._isThrowBox)
            {
                this.FrameColumn = 4;
                this.FrameLine = 0;
                this._hitBox.Width = 42;
                this.WidthSprite = 42;
            }
            if (this._beginAttack)
            {
                this.FrameColumn = 0;
            }

            this._dir = Vector2.Zero;
            this.Timer = 0;
            this.playerMove = false;
            this._isSpriting = false;
        }


        public void CheckGravity()
        {
            bool colide = false;
            Rectangle futurPos = this._hitBox;
            futurPos.Y += 1 + (int)this._fallingSpeed;
            foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
            {
                if (futurPos.Intersects(block.HitBox) && block.IsActive && block.IsCollidable)
                {
                    colide = true;
                    if (this._fallingSpeed > 0 || this._lookUpDownPhase)
                    {
                        this._hitBox.Y = block.HitBox.Y - this._hitBox.Height;
                    }
                    break;
                }
            }

            foreach (ClimbableBlock block in ClimbableBlock.ClimbableBlockList)
            {
                if (futurPos.Intersects(block.HitBox) && block.IsActive)
                {
                    colide = true;
                    if (this._fallingSpeed > 0 || this._lookUpDownPhase)
                    {
                        this._hitBox.Y = block.HitBox.Y - this._hitBox.Height;
                    }
                    break;
                }
            }

            foreach (MovableNeutralBlock block in MovableNeutralBlock.MovableNeutralList)
            {
                if (futurPos.Intersects(block.HitBox) && block.IsActive)
                {
                    colide = true;
                    if (this._fallingSpeed > 0 || this._lookUpDownPhase)
                    {
                        this._hitBox.Y = block.HitBox.Y - this._hitBox.Height;
                    }
                    break;
                }
            }

            if (!colide)
            {
                bool lad = false;
                foreach (Ladder ladder in Ladder.LadderList)
                {
                    if (this._hitBox.Intersects(ladder.HitBox) && this.GetType() == typeof(Jekyll))
                    {
                        lad = true;
                    }
                }
                if (!lad)
                {
                    int i = 0;
                    bool playerMove = false;


                    Rectangle UpCut = new Rectangle(this._hitBox.X, this._hitBox.Y, this._hitBox.Width, this._hitBox.Height / 2);
                    Rectangle DownCut = new Rectangle(this._hitBox.X, this._hitBox.Y + (this.HitBox.Height / 2), this._hitBox.Width, this._hitBox.Height / 2);
                    Rectangle UpSide = Map._upSide.HitBox;
                    Rectangle DownSide = Map._downSide.HitBox;
                    /*DownSide.Y -= (int)sp;
                    DownSide.Height -= (int)sp;
                    UpSide.Height -= (int)sp;*/

                    if (UpCut.Intersects(DownSide) || DownCut.Intersects(UpSide))
                    {
                        playerMove = true;
                    }


                    if (playerMove)
                    {
                        if (this._fallingSpeed >= 0)
                            this._fallingSpeed += 0.15f * (this._poids / 4);
                        else
                            this._fallingSpeed += 0.10f * (this._poids / 4);
                        
                        this._isFalling = true;
                        int diff = this._hitBox.Y - futurPos.Y;
                        this._hitBox.Y -= diff;
                    }
                    else
                    {
                        if (this._fallingSpeed >= 0)
                            this._fallingSpeed += 0.15f * (this._poids / 4);
                        else
                            this._fallingSpeed += 0.10f * (this._poids / 4);
                        
                        this._isFalling = true;
                        foreach (Blocks block in Blocks.BlockList)
                        {
                            block.DecreaseCoordBlockY(1 + (int)this._fallingSpeed);
                        }
                    }
                }
                else
                {

                }
            }
            else
            {
                this._isFalling = false;

                if (this._fallingSpeed >= 0)
                {
                    if (_statut)
                    {
                        this.FrameLine = 0;
                        this._hitBox.Width = 42;
                        this.WidthSprite = 42;
                    }
                    this._isJumping = false;
                }

                this._fallingSpeed = 0;
            }
        }

        public void SetAccelSpeed()
        {
            if (this._statut)
            {
                switch (this._accelMode)
                {
                    case 1:
                        this._speedInAir = 1f;
                        this._impulsion = 10.5f;
                        this.TimerMax = 7;
                        break;

                    case 2:
                        this._speedInAir = 4f;
                        this._impulsion = 9f;
                        this.TimerMax = 6;
                        break;
                    case 3:
                        this._speedInAir = 8f;
                        this._impulsion = 7.5f;
                        this.TimerMax = 5;
                        break;
                }
            }
            else
            {
                switch (this._accelMode)
                {
                    case 1:
                        this._speedInAir = 0f;
                        this._impulsion = 0f;
                        this.TimerMax = 7;
                        break;

                    case 2:
                        this._speedInAir = 0f;
                        this._impulsion = 0f;
                        this.TimerMax = 6;
                        break;
                }
            }
        }

        public void InitChange(int x, int y, Direction dir)
        {
            this._hitBox.X = x;
            this._hitBox.Y = y;
            this.Direction = dir;
            this._isActiveVision = false;
        }


        public void IncreaseCoordY(int speed)
        {
            this._hitBox.Y += speed;
        }
        public void DecreaseCoordY(int speed)
        {
            this._hitBox.Y -= speed;
        }

        /*
        * Fonction pour grimper sur un caisse
        */
        public void ClimbBox(Blocks block, int dir)
        {
            if (dir == 1)
            {
                this._hitBox.X -= block.HitBox.Width;
                this._hitBox.Y -= block.HitBox.Height;
            }
            else if (dir == 0)
            {
                this._hitBox.X += block.HitBox.Width;
                this._hitBox.Y -= block.HitBox.Height;
            }
        }

        /**
         * Fonction pour se baisser
         */
        public void stoop(int step)
        {
            if (step == 1)
            {
                if (!this._isCrouch)
                {
                    this.FrameColumn = 0;
                    this._isCrouch = true;
                }
            }
            else if (step == 0)
            {
                if (this._isCrouch)
                {
                    
                    this._isCrouch = false;
                    this.FrameColumn = 4;
                    this.FrameLine = 0;
                }
            }
        }

        /**
         * Fonction pour se dissimuler derriere une caisse
         */
        public void hide(HidingBlock block, int sens)
        {
            this._hidingBlock = sens == 0 ? block : null;
            this._isHiding = sens == 0;
            this._text = sens == 0 ? Ressources.Jekyll_Dissi : (this._statut == true ? Ressources.Hide : Ressources.Jekyll);
        }

        /**
        * Fonctions pour detruire un bloc en fonction de la vie de celui ci
        */
        public void destroy(Blocks block)
        {
            if (this.TimerAttack == 2)
            {
                block.Health -= this._strength;
                if (block.Health <= 0)
                    block.IsActive = false;
            }
        }

        /**
        * Fonctions pour lancer un objet pour Jekyll
        */

        public void throwItem(Launch cible, GamePadState pad)
        {
            if (this._isThrowing)
            {
                cible.CiblePos(this._hitBox.X, this._hitBox.Y, pad);
            }
        }

        /**
        * Fonctions pour lancer une box pour Hide
        */

        public void throwBox(LaunchableBlock cible, GamePadState pad)
        {
            this.FrameColumn = 0;
        }

        public void takeElevators(MovableNeutralBlock blocks, DelimiterZone down, DelimiterZone up)
        {
            
            if (blocks.Activate)
            {
                if (!this._hitBox.Intersects(down.HitBox) || this._hitBox.Intersects(up.HitBox))
                {
                    if (blocks.Reverse && blocks.IsAnimate)
                    {
                        decale = false;
                        int i = (int) -blocks.Speed;
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
                this._hitBox.Y = blocks.HitBox.Y - this.HitBox.Height;
            }
        }

        public bool CheckMove()
        {
            bool value = true;
            if (this.GetType() == typeof(Jekyll))
            {
                foreach (Ladder lad in Ladder.LadderList)
                {
                    Rectangle feet = new Rectangle(this.HitBox.X, this.HitBox.Y + this.HitBox.Height - 1, this.HitBox.Width, 1);
                    Rectangle feetplus = feet;
                    Rectangle feetmoins = feet;
                    feetmoins.Y--;
                    feetplus.Y++;
                    if (lad.HitBox.Intersects(feet) && lad.HitBox.Intersects(feetplus))
                    {
                        value = false;
                    }
                    if (lad.HitBox.Intersects(feet) && !lad.HitBox.Intersects(feetmoins))
                    {
                        value = true;
                    }
                }
            }
            this._canMove = value;
            return value;
        }


        //getter setter
        public bool IsJumping
        {
            get { return this._isJumping; }
            set { this._isJumping = value; }
        }

        public float Speed
        {
            get { return this._speed; }
            set { this._speed = value; }
        }

        public float FallingSpeed
        {
            get { return this._fallingSpeed; }
            set { this._fallingSpeed = value; }
        }

        public Rectangle HitBox
        {
            get { return this._hitBox; }
            set { this._hitBox = value; }
        }

        public float SpeedInAir
        {
            get { return this._speedInAir; }
            set { this._speedInAir = value; }
        }

        public int AccelMode
        {
            get { return this._accelMode; }
            set { this._accelMode = value; }
        }

        public bool LookUpDownPhase
        {
            get { return this._lookUpDownPhase; }
            set { this._lookUpDownPhase = value; }
        }

        public Direction DirectionPlayer
        {
            get { return this.Direction; }
            set { this.Direction = value; }
        }

        public bool IsActiveVision
        {
            get { return this._isActiveVision; }
            set { this._isActiveVision = value; }
        }
        public Boolean Statut
        {
            get { return this._statut; }
            set { this._statut = value; }
        }

        public Boolean PlayerMove
        {
            get { return this.playerMove; }
            set { this.playerMove = value; }
        }

        public Boolean CanMove
        {
            get { return this._canMove; }
            set { this._canMove = value; }
        }

        public bool IsActiveObject
        {
            get { return this._isActiveObject; }
            set { this._isActiveObject = value; }
        }

        public bool IsCrouch
        {
            get { return this._isCrouch; }
            set { this._isCrouch = value; }
        }

        public int Health
        {
            get { return this._health; }
            set { this._health = value; }
        }

        public bool CanClimb
        {
            get { return this._canClimb; }
            set { this._canClimb = value; }
        }

        public bool CanHide
        {
            get { return this._canHide; }
            set { this._canHide = value; }
        }

        public bool CanJump
        {
            get { return this._canJump; }
            set { this._canJump = value; }
        }

        public bool CanJVision
        {
            get { return this._canJVision; }
            set { this._canJVision = value; }
        }

        public bool CanHVision
        {
            get { return this._canHVision; }
            set { this._canHVision = value; }
        }

        public Boolean IsAttacking
        {
            get { return this._isAttacking; }
            set { this._isAttacking = value; }
        }

        public Boolean HitAttack
        {
            get { return this._hitAttack; }
            set { this._hitAttack = value; }
        }

        public Boolean BeginAttack
        {
            get { return this._beginAttack; }
            set { this._beginAttack = value; }
        }

        public Boolean EndAttack
        {
            get { return this._endAttack; }
            set { this._endAttack = value; }
        }

        public int Strength
        {
            get { return this._strength; }
            set { this._strength = value; }
        }

        public bool IsHiding
        {
            get { return this._isHiding; }
            set { this._isHiding = value; }
        }

        public bool IsSpriting
        {
            get { return this._isSpriting; }
            set { this._isSpriting = value; }
        }

        public bool BeginJump
        {
            get { return this._beginJump; }
            set { this._beginJump = value; }
        }

        public bool Jump
        {
            get { return this._Jump; }
            set { this._Jump = value; }
        }

        public bool EndJump
        {
            get { return this._endJump; }
            set { this._endJump = value; }
        }

        public bool IsThrowing
        {
            get { return this._isThrowing; }
            set { this._isThrowing = value; }
        }

        public bool IsSwitch
        {
            get { return this._isSwitch; }
            set { this._isSwitch = value; }
        }

        public HidingBlock HideBlock
        {
            get { return this._hidingBlock; }
            set { this._hidingBlock = value; }
        }

        public int ThrowId
        {
            get { return this._throwId; }
            set { this._throwId = value; }
        }

        public LaunchableBlock BlockLaunch
        {
            get { return this._blockLaunch; }
            set { this._blockLaunch = value; }
        }

        public bool IsThrowBox
        {
            get { return this._isThrowBox; }
            set { this._isThrowBox = value; }
        }
    }
}
