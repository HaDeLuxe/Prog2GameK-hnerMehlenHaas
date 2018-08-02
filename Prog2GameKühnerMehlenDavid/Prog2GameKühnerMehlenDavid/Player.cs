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
        bool JumpButtonPressed;
        bool PlayerAttackPressed;
        bool StillAlive;
        float JumpSpeed;
        float Cooldown;
        int PlayerHP;
       
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
            PressedLeftKey = false;
            PressedRightKey = false;
            IsStanding = false;
            StillAlive = true;
            PlayerAttackPressed = false;
            FacingDirectionRight = true;
            JumpButtonPressed = false;
            ChangeCollisionBox = new Vector2(SpriteSheetSizes.SpritesSizes["Reggie_Move_Hitbox_Pos_X"], SpriteSheetSizes.SpritesSizes["Reggie_Move_Hitbox_Pos_Y"]);
            CollisionBoxPosition = new Vector2(Position.X + ChangeCollisionBox.X, Position.Y + ChangeCollisionBox.Y);
            CollisionBoxSize = new Vector2(SpriteSheetSizes.SpritesSizes["Reggie_Move_Hitbox_Size_X"], SpriteSheetSizes.SpritesSizes["Reggie_Move_Hitbox_Size_Y"]);
            PlayerHP = 50;
            MovementSpeed = 10f;
            JumpSpeed = -10f;
        }

        public void Update(GameTime gameTime, List<GameObject> gameObjectsToRender, List<Enemy> EnemyList) {
            PlayerControls(gameTime,EnemyList);
            Position.Y = CollisionBoxPosition.Y - ChangeCollisionBox.Y;
            PlayerPositionCalculation(gameTime, gameObjectsToRender);
        }


        public void changeTexture(Texture2D texture) {
            this.SpriteTexture = texture;
        }
        private void PlayerPositionCalculation(GameTime gameTime, List<GameObject> gameObjectsToRender) {

            foreach (var sprite in gameObjectsToRender)
            {
                if ((PreviousState.IsKeyDown(Keys.Left) || PreviousState.IsKeyDown(Keys.Right) || PreviousState.IsKeyDown(Keys.Down) || PreviousState.IsKeyDown(Keys.Up) || PreviousState.IsKeyDown(Keys.Space)) || GravityActive)
                {
                    //Checks collision on the left side and right side of each sprite when player is on the ground/air
                    if (Velocity.X > 0 && IsTouchingLeftSide(sprite) ||
                       (Velocity.X < 0 && IsTouchingRightSide(sprite)))
                    {
                        Velocity.X = 0;
                        PressedLeftKey = false;
                        PressedRightKey = false;
                    }
                    //checks collision on the bottom side of each sprite and makes a smoother contact between player/sprite if the player should hit the sprite
                    //Activate Gravity boolean and stops translation in UP direction if the bottom side of a sprite was hit
                    else if (IsTouchingBottomSide(sprite, Gravity))
                    {
                        Velocity.Y = 0;
                        JumpSpeed = 0;
                        CollisionBoxPosition.Y = sprite.Position.Y + sprite.SpriteRectangle.Height;
                        GravityActive = true;
                    }
                    // Resets AirDirection booleans, jump booleans, the number of times the worm has jumped and stops translations in DOWN direction 
                    else if (IsTouchingTopSide(sprite, Gravity))
                    {
                        Velocity.Y = 0;
                        Gravity = Vector2.Zero;
                        GravityActive = false;
                        FirstJump = false;
                        SecondJump = false;
                        IsStanding = true;
                        PressedLeftKey = false;
                        PressedRightKey = false;
                        CollisionBoxPosition.Y = sprite.Position.Y - CollisionBoxSize.Y;
                    }

                    if (!IsTouchingTopSide(sprite, Gravity) && IsStanding == false)
                    {
                        GravityActive = true;
                        if (PressedRightKey)
                            Velocity.X = MovementSpeed;
                        else if (PressedLeftKey)
                            Velocity.X = -MovementSpeed;
                        if (IsTouchingLeftSide(sprite) || IsTouchingRightSide(sprite))
                        {
                            Velocity.X = 0;
                            PressedLeftKey = false;
                            PressedRightKey = false;
                        }
                        if (PreviousState.IsKeyDown(Keys.Space) && JumpButtonPressed)
                        {
                            if (!FirstJump || !SecondJump)
                                Gravity = Vector2.Zero;
                            if (!FirstJump)
                                FirstJump = true;
                            else
                                SecondJump = true;
                        }
                        JumpButtonPressed = false;
                    }
                }
            }
            if ((FirstJump == true || SecondJump == true))
                PlayerJump();
            if (GravityActive && IsStanding == false)
            {
                Gravity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 15;
                if (Gravity.Y > -JumpSpeed && PreviousState.IsKeyDown(Keys.Space))
                    Gravity.Y = 12f;
                CollisionBoxPosition.Y += Gravity.Y;
            }
            else
                Gravity = Vector2.Zero;
            CollisionBoxPosition += Velocity;
            if(Position != CollisionBoxPosition -ChangeCollisionBox)
            {
                Position = CollisionBoxPosition - ChangeCollisionBox;
                IsStanding = false;
            }
            else
            {
                IsStanding = true;
                GravityActive = false;
            }
            Velocity = Vector2.Zero;
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
                PressedLeftKey = true;
                FacingDirectionRight = false;
                PressedRightKey = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (FirstJump || SecondJump) AnimationManager.currentAnimation = AnimationManager.Animations.Jump_Right;
                else    AnimationManager.currentAnimation = AnimationManager.Animations.Walk_Right;
                Velocity.X = MovementSpeed;
                FacingDirectionRight = true;
                PressedLeftKey = false;
                PressedRightKey = true;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                Velocity.Y = MovementSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !PreviousState.IsKeyDown(Keys.Space) && !JumpButtonPressed)
            {
                if (FacingDirectionRight) AnimationManager.currentAnimation = AnimationManager.Animations.Jump_Right;
                else AnimationManager.currentAnimation = AnimationManager.Animations.Jump_Left;
                JumpButtonPressed = true;
                if (FirstJump == false && SecondJump == false)
                    JumpSpeed = -10f;
                PlayerJump();
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


        //Checks if Players standard attack is hitting the enemy
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
            else if (AttackRectangle.Bottom > EnemyEntity.CollisionRectangle.Top &&
            AttackRectangle.Top < EnemyEntity.CollisionRectangle.Top &&
            AttackRectangle.Right > EnemyEntity.CollisionRectangle.Left &&
            AttackRectangle.Left < EnemyEntity.CollisionRectangle.Right)
                return true;
            else if (AttackRectangle.Top < EnemyEntity.CollisionRectangle.Bottom &&
           AttackRectangle.Bottom > EnemyEntity.CollisionRectangle.Bottom &&
           AttackRectangle.Right > EnemyEntity.CollisionRectangle.Left &&
           AttackRectangle.Left < EnemyEntity.CollisionRectangle.Right)
                return true;
            else
                return false;
        }

        //Reduces player's hp if he is hit by the enemy
        public void ReducePlayerHP()
        {
            if (PlayerHP > 0)
                PlayerHP--;
            else
                StillAlive = false;
        }
    }
}
