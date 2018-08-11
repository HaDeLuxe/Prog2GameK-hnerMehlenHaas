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
        KeyboardState previousState;
        
        bool firstJump;
        bool secondJump;
        bool jumpButtonPressed;
        bool playerAttackPressed;
        bool stillAlive;
        float jumpSpeed;
        float cooldown;
        int playerHP;

        MouseState mouseState;

        Camera camera = new Camera();
       
        public Rectangle attackRectangle
        {
            get
            {
                if(facingDirectionRight)
                    return new Rectangle((int)(collisionBoxPosition.X+ collisionBoxSize.X), (int)(collisionBoxPosition.Y-100), (int)collisionBoxSize.X+100, (int)collisionBoxSize.Y+100);
                else
                    return new Rectangle((int)(collisionBoxPosition.X - collisionBoxSize.X-100), (int)(collisionBoxPosition.Y - 100), (int)collisionBoxSize.X + 100, (int)collisionBoxSize.Y + 100);
            }
        }

        public Player(Texture2D playerTexture,Vector2 playerSize, Vector2 playerPosition) : base(playerTexture,playerSize, playerPosition) {
            gravityActive = true;
            firstJump = false;
            secondJump = false;
            pressedLeftKey = false;
            pressedRightKey = false;
            isStanding = false;
            stillAlive = true;
            playerAttackPressed = false;
            facingDirectionRight = true;
            jumpButtonPressed = false;
            objectID = (int)Enums.ObjectsID.PLAYER;
            changeCollisionBox = new Vector2(SpriteSheetSizes.spritesSizes["Reggie_Move_Hitbox_Pos_X"], SpriteSheetSizes.spritesSizes["Reggie_Move_Hitbox_Pos_Y"]);
            collisionBoxPosition = new Vector2(playerPosition.X + changeCollisionBox.X, playerPosition.Y + changeCollisionBox.Y);
            collisionBoxSize = new Vector2(SpriteSheetSizes.spritesSizes["Reggie_Move_Hitbox_Size_X"], SpriteSheetSizes.spritesSizes["Reggie_Move_Hitbox_Size_Y"]);
            playerHP = 50;
            movementSpeed = 10f;
            jumpSpeed = -20f;
        }

        public void Update(GameTime gameTime, List<GameObject> gameObjectsToRender, List<Enemy> enemyList) {
            PlayerControls(gameTime,enemyList);
            gameObjectPosition.Y = collisionBoxPosition.Y - changeCollisionBox.Y;
            PlayerPositionCalculation(gameTime, gameObjectsToRender);
        }


        public void changeTexture(Texture2D texture) {
            this.gameObjectTexture = texture;
        }
        private void PlayerPositionCalculation(GameTime gameTime, List<GameObject> gameObjectsToRender) {

            foreach (var platform in gameObjectsToRender)
            {
                if ((previousState.IsKeyDown(Keys.A) || previousState.IsKeyDown(Keys.D) || previousState.IsKeyDown(Keys.S) || previousState.IsKeyDown(Keys.W) || previousState.IsKeyDown(Keys.Space)) || gravityActive)
                {
                    //Checks collision on the left side and right side of each sprite when player is on the ground/air
                    if (velocity.X > 0 && IsTouchingLeftSide(platform) ||
                       (velocity.X < 0 && IsTouchingRightSide(platform)))
                    {
                        velocity.X = 0;
                        pressedLeftKey = false;
                        pressedRightKey = false;
                    }
                    //checks collision on the bottom side of each sprite and makes a smoother contact between player/sprite if the player should hit the sprite
                    //Activate Gravity boolean and stops translation in UP direction if the bottom side of a sprite was hit
                    else if (IsTouchingBottomSide(platform, gravity))
                    {
                        velocity.Y = 0;
                        jumpSpeed = 0;
                        gravity.Y = 0;
                        collisionBoxPosition.Y = platform.gameObjectPosition.Y + platform.gameObjectRectangle.Height;
                        gravityActive = true;
                    }
                    // Resets AirDirection booleans, jump booleans, the number of times the worm has jumped and stops translations in DOWN direction 
                    else if (IsTouchingTopSide(platform, gravity))
                    {
                        velocity.Y = 0;
                        gravity = Vector2.Zero;
                        gravityActive = false;
                        firstJump = false;
                        secondJump = false;
                        isStanding = true;
                        pressedLeftKey = false;
                        pressedRightKey = false;
                        collisionBoxPosition.Y = platform.gameObjectPosition.Y - collisionBoxSize.Y;
                    }

                    if (!IsTouchingTopSide(platform, gravity) && isStanding == false)
                    {
                        gravityActive = true;
                        if (pressedRightKey)
                            velocity.X = movementSpeed;
                        else if (pressedLeftKey)
                            velocity.X = -movementSpeed;
                        if (IsTouchingLeftSide(platform) || IsTouchingRightSide(platform))
                        {
                            velocity.X = 0;
                            pressedLeftKey = false;
                            pressedRightKey = false;
                        }
                        if (previousState.IsKeyDown(Keys.Space) && jumpButtonPressed)
                        {
                            if (!firstJump || !secondJump)
                                gravity = Vector2.Zero;
                            if (!firstJump)
                                firstJump = true;
                            else
                                secondJump = true;
                            jumpButtonPressed = false;
                        }
                        
                    }
                }
            }
            if ((firstJump == true || secondJump == true))
                PlayerJump();
            if (gravityActive && isStanding == false)
            {
                gravity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 51;
                if (gravity.Y > 20 && previousState.IsKeyDown(Keys.Space))
                    gravity.Y = 23f;
                collisionBoxPosition.Y += gravity.Y;
            }
            else
                gravity = Vector2.Zero;
            collisionBoxPosition += velocity;
            if(gameObjectPosition != collisionBoxPosition -changeCollisionBox)
            {
                gameObjectPosition = collisionBoxPosition - changeCollisionBox;
                isStanding = false;
            }
            velocity = Vector2.Zero;
        }

        private void PlayerJump() {
            velocity.Y = jumpSpeed;
            if (previousState.IsKeyDown(Keys.S))
                velocity.Y = movementSpeed;
        }

        //Contains Player Movement in all 4 directions and the attack
        private void PlayerControls(GameTime gameTime, List<Enemy> enemyList)
        {

            mouseState = Mouse.GetState();
            if (!firstJump && !secondJump)
            {
                if (AnimationManager.currentAnimation == AnimationManager.Animations.Jump_Left /*|| (AnimationManager.currentAnimation == AnimationManager.Animations.Attack_Left)*/)
                    //AnimationManager.currentAnimation = AnimationManager.Animations.Walk_Left;
                    //AnimationManager.AnimationQueue.Enqueue(AnimationManager.Animations.Walk_Left);
                    AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Left;
                if (AnimationManager.currentAnimation == AnimationManager.Animations.Jump_Right /*|| (AnimationManager.currentAnimation == AnimationManager.Animations.Attack_Right)*/)
                    //AnimationManager.currentAnimation = AnimationManager.Animations.Walk_Right;
                    //AnimationManager.AnimationQueue.Enqueue(AnimationManager.Animations.Walk_Right);
                    AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Right;
            }


            

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
               
               //Camera won't move after simple turning
                camera.IncreaseLeftCounter();
                camera.ResetRightCounter();
                
                //Camera moves to a direction so that you see better what is coming to you
                camera.cameraOffset(gameTime, false, true);

                if (firstJump == true || secondJump == true)    AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Left;
                else    AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Left;
                

                velocity.X = -movementSpeed;
                pressedLeftKey = true;
                facingDirectionRight = false;
                pressedRightKey = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                //Camera won't move after simple turning
                camera.IncreaseRightCounter();
                camera.ResetLeftCounter();

                //Camera moves to a direction so that you see better what is coming to you
                camera.cameraOffset(gameTime, true, true);

                if (firstJump || secondJump)    AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Right;
                else   AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Right;
                
                velocity.X = movementSpeed;
                facingDirectionRight = true;
                pressedLeftKey = false;
                pressedRightKey = true;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                velocity.Y = movementSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space) && !jumpButtonPressed)
            {
                if (facingDirectionRight)   AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Right;
                else    AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Left;
                jumpButtonPressed = true;
                if (firstJump == false || secondJump == false)
                    jumpSpeed = -20f;
                PlayerJump();
            }
            if(ButtonState.Pressed == mouseState.LeftButton && cooldown ==0)
            {
                if (facingDirectionRight)   AnimationManager.nextAnimation = AnimationManager.Animations.Attack_Right;
                else   AnimationManager.nextAnimation = AnimationManager.Animations.Attack_Left;
                // TODO: Step1 activate enemyknockback at the specific currentframe, Step2 depending on the size of an enemy (how tall)
                foreach (var enemy in enemyList)
                {
                    if(PlayerAttackCollision(enemy) && enemy.EnemyAliveState() == true )
                    {
                        enemy.KnockBackPosition(facingDirectionRight);
                    }
                }
                playerAttackPressed = true;
            }
            if(playerAttackPressed)
                cooldown += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            if(cooldown>=1.5)
            {
                cooldown = 0;
                playerAttackPressed = false;
            }
           


            if (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A) && !firstJump && !secondJump)
            {
                if (facingDirectionRight)
                {
                    camera.cameraOffset(gameTime, false, false);
                }
                if (!facingDirectionRight)
                {
                    camera.cameraOffset(gameTime, true, false);
                }
            }
            previousState = Keyboard.GetState();
        }


        //Checks if Players standard attack is hitting the enemy
        private bool PlayerAttackCollision(Enemy enemyEntity)
        {
            if (attackRectangle.Right + velocity.X >= enemyEntity.collisionRectangle.Left &&
                attackRectangle.Left <= enemyEntity.collisionRectangle.Left &&
                attackRectangle.Bottom > enemyEntity.collisionRectangle.Top &&
                attackRectangle.Top < enemyEntity.collisionRectangle.Bottom)
                return true;
            else if (attackRectangle.Left + velocity.X <= enemyEntity.collisionRectangle.Right &&
              attackRectangle.Right >= enemyEntity.collisionRectangle.Right &&
              attackRectangle.Bottom > enemyEntity.collisionRectangle.Top &&
              attackRectangle.Top < enemyEntity.collisionRectangle.Bottom)
                return true;
            else if (attackRectangle.Bottom > enemyEntity.collisionRectangle.Top &&
            attackRectangle.Top < enemyEntity.collisionRectangle.Top &&
            attackRectangle.Right > enemyEntity.collisionRectangle.Left &&
            attackRectangle.Left < enemyEntity.collisionRectangle.Right)
                return true;
            else if (attackRectangle.Top < enemyEntity.collisionRectangle.Bottom &&
           attackRectangle.Bottom > enemyEntity.collisionRectangle.Bottom &&
           attackRectangle.Right > enemyEntity.collisionRectangle.Left &&
           attackRectangle.Left < enemyEntity.collisionRectangle.Right)
                return true;
            else
                return false;
        }

        //Reduces player's hp if he is hit by the enemy
        public void ReducePlayerHP()
        {
            if (playerHP > 0)
                playerHP--;
            else
                stillAlive = false;
        }
    }
}
