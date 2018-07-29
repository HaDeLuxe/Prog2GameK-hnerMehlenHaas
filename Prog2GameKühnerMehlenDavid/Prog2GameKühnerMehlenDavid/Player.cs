using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Reggie {
    public class Player : GameObject {
        KeyboardState PreviousState;
        bool FirstJump;
        bool SecondJump;
        int JumpCounter;
        int PreviousJumpCounter;
        float JumpSpeed;
        float Cooldown;
        bool PlayerAttackPressed;
        int PlayerHP;
        bool StillAlive;
        public Rectangle AttackRectangle
        {
            get
            {
                if(FacingDirectionRight)
                    return new Rectangle((int)(CollisionBoxPosition.X+ CollisionBoxSize.X), (int)(CollisionBoxPosition.Y-100), (int)CollisionBoxSize.X+100, (int)CollisionBoxSize.Y+100);
                else
                    return new Rectangle((int)(CollisionBoxPosition.X - CollisionBoxSize.X-100), (int)(CollisionBoxPosition.Y - 100), (int)CollisionBoxSize.X + 100, (int)CollisionBoxSize.Y + 100);
            }
        }
        public Player(Texture2D texture,Vector2 SpriteSize, Vector2 Position) : base(texture,SpriteSize, Position) {
            GravityActive = true;
            FirstJump = false;
            SecondJump = false;
            AirDirectionLeft = false;
            AirDirectionRight = false;
            IsStanding = false;
            StillAlive = true;
            PlayerAttackPressed = false;
            PlayerHP = 50;
            FacingDirectionRight = true;
            JumpCounter = 0;
            PreviousJumpCounter = 0;
            ChangeCollisionBox = new Vector2(SpriteSheetSizes.SpritesSizes["Reggie_Move_Hitbox_Pos_X"], SpriteSheetSizes.SpritesSizes["Reggie_Move_Hitbox_Pos_Y"]);
            CollisionBoxPosition = new Vector2(Position.X + ChangeCollisionBox.X, Position.Y + ChangeCollisionBox.Y);
            CollisionBoxSize = new Vector2(SpriteSheetSizes.SpritesSizes["Reggie_Move_Hitbox_Size_X"], SpriteSheetSizes.SpritesSizes["Reggie_Move_Hitbox_Size_Y"]);
            MovementSpeed = 6f;
            JumpSpeed = -10f;
        }

        public void Update(GameTime gameTime, List<GameObject> SpriteList, List<Enemy> EnemyList) {
            PlayerControls(gameTime,EnemyList);
            Position.Y = CollisionBoxPosition.Y - ChangeCollisionBox.Y;
            PlayerPositionCalculation(gameTime, SpriteList);
        }


        public void changeTexture(Texture2D texture) {
            this.SpriteTexture = texture;
        }
        private void PlayerPositionCalculation(GameTime gameTime, List<GameObject> SpriteList) {

            foreach (var sprite in SpriteList)
            {
                //Checks collision on the left side and right side of each sprite when player is on the ground/air
                if (Velocity.X > 0 && IsTouchingLeftSide(sprite) ||
                   (Velocity.X < 0 && IsTouchingRightSide(sprite)))
                {
                    Velocity.X = 0;
                    AirDirectionLeft = false;
                    AirDirectionRight = false;
                }
                //checks collision on the bottom side of each sprite and makes a smoother contact between player/sprite if the player should hit the sprite
                //Activate Gravity boolean and stops translation in UP direction if the bottom side of a sprite was hit
                else if (IsTouchingBottomSide(sprite,Gravity))
                {
                    Velocity.Y = 0;
                    JumpSpeed = 0;
                    GravityActive = true;
                    CollisionBoxPosition.Y = sprite.Position.Y + sprite.SpriteRectangle.Height;
                    Position.Y = CollisionBoxPosition.Y - ChangeCollisionBox.Y;
                    if (FirstJump == true && SecondJump == false)
                        FirstJump = false;
                    else if (FirstJump == true && SecondJump == true)
                    {
                        FirstJump = false;
                        SecondJump = false;
                    }
                }
                // Resets AirDirection booleans, jump booleans, the number of times the worm has jumped and stops translations in DOWN direction 
                else if (IsTouchingTopSide(sprite, Gravity))
                {
                    Velocity.Y = 0;
                    GravityActive = false;
                    Gravity = Vector2.Zero;
                    FirstJump = false;
                    SecondJump = false;
                    IsStanding = true;
                    JumpCounter = 0;
                    CollisionBoxPosition.Y = sprite.Position.Y - CollisionBoxSize.Y;
                    Position.Y = CollisionBoxPosition.Y - ChangeCollisionBox.Y;
                    AirDirectionLeft = false;
                    AirDirectionRight = false;
                }

                if (!IsTouchingTopSide(sprite, Gravity) && IsStanding == false)
                {
                    GravityActive = true;
                    if (AirDirectionRight)
                        Velocity.X = MovementSpeed;
                    else if (AirDirectionLeft)
                        Velocity.X = -MovementSpeed;
                    if (IsTouchingLeftSide(sprite) || IsTouchingRightSide(sprite))
                    {
                        Velocity.X = 0;
                        AirDirectionLeft = false;
                        AirDirectionRight = false;
                    }
                    if (PreviousState.IsKeyDown(Keys.Space) && JumpCounter < 3)
                    {
                        JumpSpeed = -10f;
                        if (JumpCounter != PreviousJumpCounter)
                            Gravity = Vector2.Zero;
                        if (JumpCounter == 1)
                            FirstJump = true;
                        else if (JumpCounter == 2)
                            SecondJump = true;
                    }
                    PreviousJumpCounter = JumpCounter;
                }
            }
            if ((FirstJump == true || SecondJump == true))
                PlayerJump();
            if (GravityActive && IsStanding == false)
            {
                //if (!PreviousState.IsKeyDown(Keys.Space))
                Gravity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 15;
                if (Gravity.Y > -JumpSpeed && PreviousState.IsKeyDown(Keys.Space))
                    Gravity.Y = 12f;
                //else
                //    Gravity.Y = 5;
                //CollisionBoxPosition.Y += Gravity.Y;
                CollisionBoxPosition.Y += Gravity.Y;
            }
            else
                Gravity = Vector2.Zero;
            //if (IsStanding == false)
            //    CollisionBoxPosition.Y += Gravity.Y;
            CollisionBoxPosition += Velocity;
            Position = CollisionBoxPosition - ChangeCollisionBox;
            Velocity = Vector2.Zero;
            IsStanding = false;
        }

        private void PlayerJump() {
            Velocity.Y = JumpSpeed;
            if (PreviousState.IsKeyDown(Keys.Down))
                Velocity.Y = MovementSpeed;
        }

        //Contains Player Movement in all 4 directions and the attack
        private void PlayerControls(GameTime gameTime, List<Enemy> EnemyList)
        {

            if (!FirstJump && !SecondJump)
            {
                if(AnimationManager.currentAnimation == AnimationManager.Animations.Jump_Left)
                    AnimationManager.currentAnimation = AnimationManager.Animations.Walk_Left;
                if (AnimationManager.currentAnimation == AnimationManager.Animations.Jump_Right)
                    AnimationManager.currentAnimation = AnimationManager.Animations.Walk_Right;
                else;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (FirstJump == true || SecondJump == true)
                {
                    AnimationManager.currentAnimation = AnimationManager.Animations.Jump_Left;
                }
                else    AnimationManager.currentAnimation = AnimationManager.Animations.Walk_Left;
                Velocity.X = -MovementSpeed;
                AirDirectionLeft = true;
                FacingDirectionRight = false;
                AirDirectionRight = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (FirstJump || SecondJump) AnimationManager.currentAnimation = AnimationManager.Animations.Jump_Right;
                else    AnimationManager.currentAnimation = AnimationManager.Animations.Walk_Right;
                Velocity.X = MovementSpeed;
                FacingDirectionRight = true;
                AirDirectionLeft = false;
                AirDirectionRight = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                Velocity.Y = MovementSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !PreviousState.IsKeyDown(Keys.Space) && JumpCounter < 3)
            {
                if (FacingDirectionRight) AnimationManager.currentAnimation = AnimationManager.Animations.Jump_Right;
                else AnimationManager.currentAnimation = AnimationManager.Animations.Jump_Left;
                if (FirstJump == false && SecondJump == false)
                    //JumpSpeed = -5f?
                    JumpSpeed = -10f;
                PlayerJump();
                JumpCounter++;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.A) && Cooldown ==0)
            {
                foreach (var enemy in EnemyList)
                {
                    if(PlayerAttackCollision(enemy) && enemy.EnemyAliveState() == true)
                    {
                        enemy.KnockBackPosition(FacingDirectionRight);
                    }
                }
                PlayerAttackPressed = true;
            }
            if(PlayerAttackPressed)
                Cooldown += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            if(Cooldown>=1.5)
            {
                Cooldown = 0;
                PlayerAttackPressed = false;
            }


            PreviousState = Keyboard.GetState();
        }

        private bool PlayerAttackCollision(Enemy EnemyEntity)
        {
            if (AttackRectangle.Right + Velocity.X >= EnemyEntity.CollisionRectangle.Left &&
                AttackRectangle.Left <= EnemyEntity.CollisionRectangle.Left &&
                AttackRectangle.Bottom > EnemyEntity.CollisionRectangle.Top &&
                AttackRectangle.Top < EnemyEntity.CollisionRectangle.Bottom)
                return true;
            else if (AttackRectangle.Left + Velocity.X <= EnemyEntity.CollisionRectangle.Right &&
              AttackRectangle.Right >= EnemyEntity.CollisionRectangle.Right &&
              AttackRectangle.Bottom > EnemyEntity.CollisionRectangle.Top &&
              AttackRectangle.Top < EnemyEntity.CollisionRectangle.Bottom)
                return true;
            else if (AttackRectangle.Bottom /*+ Velocity.Y + Gravity.Y*/ > EnemyEntity.CollisionRectangle.Top &&
            AttackRectangle.Top < EnemyEntity.CollisionRectangle.Top &&
            AttackRectangle.Right > EnemyEntity.CollisionRectangle.Left &&
            AttackRectangle.Left < EnemyEntity.CollisionRectangle.Right)
                return true;
            else if (AttackRectangle.Top /*+ Velocity.Y*/ < EnemyEntity.CollisionRectangle.Bottom &&
           AttackRectangle.Bottom > EnemyEntity.CollisionRectangle.Bottom &&
           AttackRectangle.Right > EnemyEntity.CollisionRectangle.Left &&
           AttackRectangle.Left < EnemyEntity.CollisionRectangle.Right)
                return true;
            else
                return false;
        }
        public void ReducePlayerHP()
        {
            if (PlayerHP > 0)
                PlayerHP--;
            else
                StillAlive = false;
        }
    }
}
