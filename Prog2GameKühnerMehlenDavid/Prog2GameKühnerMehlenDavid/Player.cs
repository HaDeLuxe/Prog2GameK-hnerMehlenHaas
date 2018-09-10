using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

namespace Reggie
{
    public class Player : GameObject
    {
        KeyboardState previousState;
        GamePadState previousGamepadState;
        Texture2D UmbrellaTexture = null;
        Texture2D itemPlayerTexture = null;
        bool firstJump;
        bool secondJump;
        bool jumpButtonPressed;
        bool playerAttackPressed;
        bool playerGameElementInteraction;
        bool stillAlive;
        float jumpSpeed;
        float cooldown;
        float playerHP;
        bool climbAllowed;
        public bool invincibilityFrames;
        public bool isFloating;
        public float invincibilityTimer;
        


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


        public Player(Texture2D playerTexture,Vector2 playerSize, Vector2 playerPosition, int gameObjectID) : base(playerTexture,playerSize, playerPosition, gameObjectID)
        {
            gravityActive = true;
            firstJump = false;
            secondJump = false;
            pressedLeftKey = false;
            pressedRightKey = false;
            isStanding = false;
            stillAlive = true;
            playerAttackPressed = false;
            facingDirectionRight = false;
            jumpButtonPressed = false;
            playerGameElementInteraction = false;
            invincibilityFrames = false;
            //changeCollisionBox = new Vector2(SpriteSheetSizes.spritesSizes["Reggie_Move_Hitbox_Pos_X"], SpriteSheetSizes.spritesSizes["Reggie_Move_Hitbox_Pos_Y"]);
            changeCollisionBox = new Vector2(0, 0);
            collisionBoxPosition = new Vector2(playerPosition.X + changeCollisionBox.X, playerPosition.Y + changeCollisionBox.Y);
            collisionBoxSize = new Vector2(SpriteSheetSizes.spritesSizes["Reggie_Move_Hitbox_Size_X"], SpriteSheetSizes.spritesSizes["Reggie_Move_Hitbox_Size_Y"]);
            playerHP = 1f;
            movementSpeed = 10f;
            jumpSpeed = -20f;
            invincibilityTimer = 0;
        }

        public void Update(GameTime gameTime, List<GameObject> gameObjectsToRender, List<Enemy> enemyList, List<GameObject> interactiveObject, ref List<GameObject> gameObjects)
        {
            if (!facingDirectionRight)
                changeCollisionBox.X = 0;
            else
                changeCollisionBox.X = 50;
            PlayerControls(gameTime,enemyList, interactiveObject, ref gameObjects);
            collisionBoxPosition = gameObjectPosition + changeCollisionBox;
            PlayerPositionCalculation(gameTime, gameObjectsToRender,interactiveObject);

            for (int i = 0; i < interactiveObject.Count(); i++)
            {
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.SNAILSHELL)
                {
                    if (DetectCollision(interactiveObject[i]))
                        ItemUIManager.snailShellPickedUp = true;
                }
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.SCISSORS)
                {
                    if (DetectCollision(interactiveObject[i]))
                        ItemUIManager.scissorsPickedUp = true;
                }
                if(interactiveObject[i].objectID == (int)Enums.ObjectsID.ARMOR)
                {
                    if (DetectCollision(interactiveObject[i]))
                        ItemUIManager.armorPickedUp = true;
                }
                if(interactiveObject[i].objectID == (int)Enums.ObjectsID.SHOVEL)
                {
                    if (DetectCollision(interactiveObject[i]))
                        ItemUIManager.shovelPickedUp = true;
                }
                if(interactiveObject[i].objectID == (int)Enums.ObjectsID.HEALTHPOTION)
                {
                    if (DetectCollision(interactiveObject[i]))
                        ItemUIManager.healthPickedUp = true;
                }
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.JUMPPOTION)
                {
                    if (DetectCollision(interactiveObject[i]))
                        ItemUIManager.jumpPickedUp = true;
                }
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.POWERPOTION)
                {
                    if (DetectCollision(interactiveObject[i]))
                        ItemUIManager.powerPickedUp = true;
                }
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA)
                {
                    if (DetectCollision(interactiveObject[i]))
                        ItemUIManager.goldenUmbrellaPickedUp = true;
                }
            }
            if (invincibilityFrames)
                InvincibleFrameState(gameTime);
        }


        public void changeTexture(Texture2D texture)
        {
            this.gameObjectTexture = texture;
        }

        public void changeSecondTexture(Texture2D texture) 
        {
            this.UmbrellaTexture = texture;
        }

        public void changeThirdTexture(Texture2D texture) 
        {
            this.itemPlayerTexture = texture;
        }


        private void PlayerPositionCalculation(GameTime gameTime, List<GameObject> gameObjectsToRender,List <GameObject> interactiveObject)
        {

            foreach (var platform in gameObjectsToRender)
            {
                if (((previousState.IsKeyDown(Keys.A) || previousState.IsKeyDown(Keys.D) || previousState.IsKeyDown(Keys.S) || previousState.IsKeyDown(Keys.Space)) || gravityActive || previousGamepadState.ThumbSticks.Left.Y != 0 || previousGamepadState.ThumbSticks.Left.X != 0 || previousGamepadState.IsButtonDown(Buttons.A) || previousGamepadState.IsButtonDown(Buttons.B)) && !playerGameElementInteraction && platform.objectID == (int)Enums.ObjectsID.PLATFORM)
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
                        if ((previousState.IsKeyDown(Keys.Space)||previousGamepadState.IsButtonDown(Buttons.A)) && jumpButtonPressed)
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
            //foreach(var gameObject in interactiveObject)
            //{
            //    if (!ClimbingPositionCheckTopSide(gameObject))
            //        velocity.Y = 0;
            //    else
            //        velocity.Y = -movementSpeed - 2;
            //}
            if ((firstJump == true || secondJump == true))
                PlayerJump();
            if (gravityActive && isStanding == false)
            {
                gravity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 51;
                if (gravity.Y > 20 && (previousState.IsKeyDown(Keys.Space) || previousGamepadState.IsButtonDown(Buttons.A)))
                {
                    gravity.Y = 23f;
                    isFloating = true;
                }
                else
                    isFloating = false;
                    

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

        private void PlayerJump()
        {
            velocity.Y = jumpSpeed;
            if (previousState.IsKeyDown(Keys.S) || previousGamepadState.ThumbSticks.Left.Y < -0.5f)
                velocity.Y = movementSpeed;

            //MediaPlayer.Play(Game1.songDictionnary["houseChord"]);
        }

        //Contains Player Movement in all 4 directions and the attack
        private void PlayerControls(GameTime gameTime, List<Enemy> enemyList, List<GameObject> interactiveObject, ref List<GameObject> GameObjectsList)
        {

            mouseState = Mouse.GetState();
            if (!firstJump && !secondJump)
            {
                if (AnimationManager.currentAnimation == AnimationManager.Animations.Jump_Left 
                    || AnimationManager.currentAnimation == AnimationManager.Animations.Jump_Hat_Left
                    || AnimationManager.currentAnimation == AnimationManager.Animations.Jump_Armor_Left
                    || AnimationManager.currentAnimation == AnimationManager.Animations.Jump_Armor_Hat_Left)
                {
                    if (ItemUIManager.snailShellPickedUp && ItemUIManager.armorPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Armor_Hat_Left;
                    else if (ItemUIManager.snailShellPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Hat_Left;
                    else if (ItemUIManager.armorPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Armor_Left;
                    else AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Left;
                }
                if (AnimationManager.currentAnimation == AnimationManager.Animations.Jump_Right 
                    || AnimationManager.currentAnimation == AnimationManager.Animations.Jump_Hat_Right
                    || AnimationManager.currentAnimation == AnimationManager.Animations.Jump_Armor_Right
                    || AnimationManager.currentAnimation == AnimationManager.Animations.Jump_Armor_Hat_Right)
                {
                    if (ItemUIManager.snailShellPickedUp && ItemUIManager.armorPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Armor_Hat_Right;
                    else if (ItemUIManager.snailShellPickedUp) AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Hat_Right;
                    else if (ItemUIManager.armorPickedUp) AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Armor_Right;
                    else AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Right;

                }

            }
            if (!playerGameElementInteraction)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                    ReducePlayerHP();
                if (Keyboard.GetState().IsKeyDown(Keys.A) || GamePad.GetState(0).ThumbSticks.Left.X < -0.5f)
                {

                    //Camera won't move after simple turning
                    camera.IncreaseLeftCounter();
                    camera.ResetRightCounter();

                    //Camera moves to a direction so that you see better what is coming to you
                    camera.cameraOffset(gameTime, false, true);

                    if (firstJump == true || secondJump == true)
                    {
                        if (ItemUIManager.armorPickedUp && ItemUIManager.snailShellPickedUp)
                            AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Armor_Hat_Left;
                        else if (ItemUIManager.snailShellPickedUp)
                            AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Hat_Left;
                        else if (ItemUIManager.armorPickedUp)
                            AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Armor_Left;
                        else AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Left;
                    }
                        
                    else
                    {
                        if (ItemUIManager.armorPickedUp && ItemUIManager.snailShellPickedUp)
                            AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Armor_Hat_Left;
                        else if (ItemUIManager.snailShellPickedUp)
                            AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Hat_Left;
                        else if (ItemUIManager.armorPickedUp)
                            AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Armor_Left;
                        else AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Left;
                    }
                        


                    velocity.X = -movementSpeed;
                    pressedLeftKey = true;
                    facingDirectionRight = false;
                    pressedRightKey = false;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D) || GamePad.GetState(0).ThumbSticks.Left.X > 0.5f)
                {
                    //Camera won't move after simple turning
                    camera.IncreaseRightCounter();
                    camera.ResetLeftCounter();

                    //Camera moves to a direction so that you see better what is coming to you
                    camera.cameraOffset(gameTime, true, true);

                    if (firstJump || secondJump)
                    {
                        if (ItemUIManager.armorPickedUp && ItemUIManager.snailShellPickedUp)
                            AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Armor_Hat_Right;
                        else if (ItemUIManager.armorPickedUp)
                            AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Armor_Right;
                        else if (ItemUIManager.snailShellPickedUp)
                            AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Hat_Right;
                        else AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Right;

                    }
                    else
                    {
                        if (ItemUIManager.armorPickedUp && ItemUIManager.snailShellPickedUp)
                            AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Armor_Hat_Right;
                        else if (ItemUIManager.armorPickedUp)
                            AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Armor_Right;
                        else if (ItemUIManager.snailShellPickedUp)
                            AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Hat_Right;
                        else AnimationManager.nextAnimation = AnimationManager.Animations.Walk_Right;
                    }

                    velocity.X = movementSpeed;
                    facingDirectionRight = true;
                    pressedLeftKey = false;
                    pressedRightKey = true;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S) /*|| GamePad.GetState(0).IsButtonDown()*/)
                    velocity.Y = movementSpeed;
            }
            //Player Jump Input
            if ((Keyboard.GetState().IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space) && !jumpButtonPressed)
                || (GamePad.GetState(0).IsButtonDown(Buttons.A) && !previousGamepadState.IsButtonDown(Buttons.A) && !jumpButtonPressed))
            {
                if (facingDirectionRight)
                {
                    if (ItemUIManager.armorPickedUp && ItemUIManager.snailShellPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Armor_Hat_Right;
                    else if (ItemUIManager.armorPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Armor_Right;
                    else if (ItemUIManager.snailShellPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Hat_Right;
                    else AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Right;
                }
                else
                {
                    if (ItemUIManager.armorPickedUp && ItemUIManager.snailShellPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Armor_Hat_Left;
                    else if (ItemUIManager.armorPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Armor_Left;
                    else if (ItemUIManager.snailShellPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Hat_Left;
                    else AnimationManager.nextAnimation = AnimationManager.Animations.Jump_Left;
                }
                jumpButtonPressed = true;
                playerGameElementInteraction = false;
                climbAllowed = false;
                isStanding = false;
                gravityActive = true;
                if (firstJump == false || secondJump == false)
                    jumpSpeed = -20f;
                PlayerJump();

                //SOUNDS
                //MediaPlayer.Play(Game1.songDictionnary["houseChord"]);
            }

            //Player Attack Input
            if((ButtonState.Pressed == mouseState.LeftButton && cooldown ==0 && !playerGameElementInteraction)
                || GamePad.GetState(0).IsButtonDown(Buttons.X) && cooldown == 0 && !playerGameElementInteraction)
            {
                if (facingDirectionRight)
                {
                    if (ItemUIManager.armorPickedUp && ItemUIManager.snailShellPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Attack_Armor_Hat_Right;
                    else if (ItemUIManager.armorPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Attack_Armor_Right;
                    else if 
                        (ItemUIManager.snailShellPickedUp) AnimationManager.nextAnimation = AnimationManager.Animations.Attack_Hat_Right;
                    else AnimationManager.nextAnimation = AnimationManager.Animations.Attack_Right;
                }
                else
                {
                    if (ItemUIManager.armorPickedUp && ItemUIManager.snailShellPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Attack_Armor_Hat_Left;
                    else if (ItemUIManager.armorPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Attack_Armor_Left;
                    else if (ItemUIManager.snailShellPickedUp)
                        AnimationManager.nextAnimation = AnimationManager.Animations.Attack_Hat_Left;
                    else AnimationManager.nextAnimation = AnimationManager.Animations.Attack_Left;

                }
                // TODO: Step1 activate enemyknockback at the specific currentframe, Step2 depending on the size of an enemy (how tall)
                foreach (var enemy in enemyList)
                {
                    if(PlayerAttackCollision(enemy) && enemy.EnemyAliveState() == true )
                    {
                        enemy.KnockBackPosition(facingDirectionRight);
                    }
                }

                if (ItemUIManager.currentItemEquipped.objectID == (int) Enums.ObjectsID.SCISSORS)
                {
                    //Platform temp = null;
                    foreach(Platform platform in GameObjectsList.Cast<GameObject>().OfType<Platform>().ToList())
                    {
                        if (DetectCollision(platform) && platform.PlatformType == (int)Enums.ObjectsID.SPIDERWEB)
                        {
                            //temp = platform;
                            GameObjectsList.Remove(platform);
                            break;
                        }
                    }
                    //GameObjectsList.Remove(temp);
                }

                //TODO:Destroyable? temp
               // Platform temp = null;
                foreach (Platform platform in GameObjectsList.Cast<GameObject>().OfType<Platform>().ToList())
                {
                    if (DetectCollision(platform) && platform.PlatformType == (int)Enums.ObjectsID.VINEDOOR)
                    {
                        GameObjectsList.Remove(platform);
                        break;
                    }
                }
                //GameObjectsList.Remove(temp);

                //Remove Spiderweb when scissors equipped
                if (ItemUIManager.scissorsPickedUp)
                {
                    //if(DetectCollision() && )
                }
                playerAttackPressed = true;
            }
            if(playerAttackPressed)
                cooldown += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            if(cooldown>=.75)
            {
                cooldown = 0;
                playerAttackPressed = false;
            }
           

            //Player Gameelement Interactive Input
            if((ButtonState.Pressed == mouseState.RightButton || Keyboard.GetState().IsKeyDown(Keys.W)) && !playerGameElementInteraction && !previousState.IsKeyDown(Keys.W))
            {
                foreach(var vine in interactiveObject)
                {
                    if(DetectCollision(vine))
                    {
                        jumpSpeed = 0;
                        gravityActive = false;
                        isStanding = true;
                        firstJump = false;
                        secondJump = false;
                        jumpButtonPressed = false;
                        playerGameElementInteraction = true;
                        pressedLeftKey = false;
                        pressedRightKey = false;
                        collisionBoxPosition.X = vine.gameObjectRectangle.X;
                        if (gameObjectPosition != collisionBoxPosition - changeCollisionBox)
                        {
                            gameObjectPosition = collisionBoxPosition - changeCollisionBox;
                            
                        }
                    }
                }
            }
            if(Keyboard.GetState().IsKeyDown(Keys.W) && playerGameElementInteraction)
            {
                climbAllowed = false;
                velocity.Y = -movementSpeed - 2;
                foreach (var vine in interactiveObject)
                {
                    if (collisionRectangle.Bottom + velocity.Y >= vine.gameObjectRectangle.Top+30)
                        climbAllowed = true;
                }
                if (climbAllowed)
                    velocity.Y = -movementSpeed - 2;
                else
                    velocity.Y = 0;
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.S) && playerGameElementInteraction)
            {
                bool climbAllowed = false;
                velocity.Y = movementSpeed + 2;
                foreach (var vine in interactiveObject)
                {
                    if (collisionRectangle.Top + velocity.Y <= vine.gameObjectRectangle.Bottom-80)
                        climbAllowed = true;
                }
                if (climbAllowed)
                    velocity.Y = movementSpeed + 2;
                else
                    velocity.Y = 0;
            }




            if (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A) && !firstJump && !secondJump && !playerGameElementInteraction)
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
            previousGamepadState = GamePad.GetState(0);
        }

        private bool DetectCollision(GameObject gameObject)
        {
            if (collisionRectangle.Right + velocity.X >= gameObject.gameObjectRectangle.Left &&
               collisionRectangle.Left <= gameObject.gameObjectRectangle.Left &&
               collisionRectangle.Bottom > gameObject.gameObjectRectangle.Top &&
               collisionRectangle.Top < gameObject.gameObjectRectangle.Bottom)
                return true;
            else if (collisionRectangle.Left + velocity.X <= gameObject.gameObjectRectangle.Right &&
                collisionRectangle.Right >= gameObject.gameObjectRectangle.Right &&
                collisionRectangle.Bottom > gameObject.gameObjectRectangle.Top &&
                collisionRectangle.Top < gameObject.gameObjectRectangle.Bottom)
                return true;
            else if (collisionRectangle.Bottom > gameObject.gameObjectRectangle.Top &&
                collisionRectangle.Top < gameObject.gameObjectRectangle.Top &&
                collisionRectangle.Right > gameObject.gameObjectRectangle.Left &&
                collisionRectangle.Left < gameObject.gameObjectRectangle.Right)
                return true;
            else if (collisionRectangle.Top < gameObject.gameObjectRectangle.Bottom &&
                collisionRectangle.Bottom > gameObject.gameObjectRectangle.Bottom &&
                collisionRectangle.Right > gameObject.gameObjectRectangle.Left &&
                collisionRectangle.Left < gameObject.gameObjectRectangle.Right)
                return true;
            else
                return false;
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
                playerHP -=0.05f;
            else
                stillAlive = false;
        }

        public float PlayersCurrentHP()
        {
            return playerHP;
        }

        public bool PlayerIsStillAlive()
        {
            return stillAlive;
        }

        public void InvincibleFrameState(GameTime gameTime)
        {
            invincibilityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            if(invincibilityTimer > 5f)
            {
                invincibilityTimer = 0;
                invincibilityFrames = false;
            }
        }


        public void drawSecondTexture(SpriteBatch spriteBatch, Rectangle sourceRectangle, SpriteEffects spriteEffects, Vector2 offset) 
        {
            spriteBatch.Draw(UmbrellaTexture, gameObjectPosition + offset, sourceRectangle, Color.White, 0, Vector2.Zero, Vector2.One, spriteEffects, 0);
        }

        public void drawThirdTexture(SpriteBatch spriteBatch, Rectangle sourceRectangle, SpriteEffects spriteEffects, Vector2 offset) 
        {
            spriteBatch.Draw(itemPlayerTexture, gameObjectPosition + offset, sourceRectangle, Color.White, 0, Vector2.Zero, Vector2.One, spriteEffects, 0);
        }
    }
}
