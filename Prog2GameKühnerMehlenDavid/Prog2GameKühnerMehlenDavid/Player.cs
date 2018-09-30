using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using Reggie.Animations;
using Reggie.Enemies;
using Reggie.Menus;
//SOUNDEFFECTS
using Microsoft.Xna.Framework.Audio;


namespace Reggie
{
    public class Player : GameObject
    {
        private KeyboardState previousState;
        GamePadState previousGamepadState;
        Texture2D UmbrellaTexture = null;
        Texture2D itemPlayerTexture = null;
        bool firstJump;
        bool secondJump;
        bool jumpButtonPressed;
        bool playerAttackPressed;
        bool playerGameElementInteraction;
        public bool stillAlive { set; get; }
        float jumpSpeed;
        float cooldown;
        public float playerHP { set; get; }
        bool climbAllowed;
        protected bool knockedBack;
        protected float knockBackValue;
        protected float attackTimer;
        public bool invincibilityFrames;
        public bool isFloating;
        public float invincibilityTimer;
        public float playerDamage;
        private float currentTime = 1f;


        public float invincibilityFixedTimer { get;  set; }
        public bool playerSlowed { get; set; }
        private bool controllerVibration = false;
        private bool firstVibrationCycle = true;
        private float defaultJumpValue;
        //MUSIC
        AudioManager audioManager;
        bool justTouchedBottom;



        MouseState mouseState;

        Camera camera = new Camera();
       
        //Player Hitbox
        public Rectangle attackRectangle
        {
            get
            {
                if(facingDirectionRight)
                    return new Rectangle((int)(collisionBoxPosition.X+ collisionBoxSize.X/2+5), (int)(collisionBoxPosition.Y-30), (int)collisionBoxSize.X+40, (int)collisionBoxSize.Y+60);
                else
                    return new Rectangle((int)(collisionBoxPosition.X - collisionBoxSize.X*3/2+15), (int)(collisionBoxPosition.Y - 30), (int)collisionBoxSize.X + 40, (int)collisionBoxSize.Y + 60);
            }
        }

        //Player Constructor
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
            knockedBack = false;
            playerDamage = 1;
            playerSlowed = false;
            invincibilityFixedTimer = 5f;
            //changeCollisionBox = new Vector2(SpriteSheetSizes.spritesSizes["Reggie_Move_Hitbox_Pos_X"], SpriteSheetSizes.spritesSizes["Reggie_Move_Hitbox_Pos_Y"]);
            changeCollisionBox = new Vector2(0, 0);
            collisionBoxPosition = new Vector2(playerPosition.X + changeCollisionBox.X, playerPosition.Y + changeCollisionBox.Y);
            collisionBoxSize = new Vector2(SpriteSheetSizes.spritesSizes["Reggie_Move_Hitbox_Size_X"], SpriteSheetSizes.spritesSizes["Reggie_Move_Hitbox_Size_Y"]);
            playerHP = 1f;
            movementSpeed = 10f;
            jumpSpeed = -20f;
            defaultJumpValue = -20f;
            invincibilityTimer = 0;
            attackTimer = 0;

            //MUSIC
            audioManager = AudioManager.AudioManagerInstance();
            justTouchedBottom = true;
        }

        //Player Update Function that covers collision,controls,movement and inputs
        internal void Update(GameTime gameTime, List<GameObject> gameObjectsToRender, ref List<Enemy> enemyList, List<GameObject> interactiveObject, ref List<GameObject> gameObjects, LoadAndSave loadAndSave, IngameMenus ingameMenus, Levels levelManager,ref List<GameObject> allGameObjects, ShopKeeper shopKeeper, ItemUIManager itemUIManager, ref Boss hakume)
        {
            if (!playerSlowed)
                movementSpeed = 10f;
            if (!facingDirectionRight)
                changeCollisionBox.X = 0;
            else
                changeCollisionBox.X = 50;
            if(!shopKeeper.shopOpen)
            PlayerControls(gameTime, enemyList, interactiveObject, ref gameObjects, loadAndSave, ingameMenus, gameObjects, shopKeeper, itemUIManager, hakume);
            collisionBoxPosition = gameObjectPosition + changeCollisionBox;
            PlayerPositionCalculation(gameTime, gameObjectsToRender, interactiveObject);
            ItemCollisionManager(ref interactiveObject, ref gameObjects, levelManager, ref allGameObjects);
            if (invincibilityFrames)
                InvincibleFrameState(gameTime);

            Vibration();
        }

        //Player's itemcollisionChecker
        private void ItemCollisionManager(ref List<GameObject> interactiveObject, ref List<GameObject> gameObjectList, Levels levelManager, ref List<GameObject> allGameObjectsList)
        {
            for (int i = 0; i < interactiveObject.Count(); i++)
            {
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.SNAILSHELL)
                {
                    if (DetectCollision(interactiveObject[i]))
                    {
                        audioManager.Play("ReggieEquipedSomething");
                        ItemUIManager.snailShellPickedUp = true;
                    }

                }
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.SCISSORS)
                {
                    if (DetectCollision(interactiveObject[i]))
                    {
                        audioManager.Play("ReggiePickupAnyItem");
                        ItemUIManager.scissorsPickedUp = true;
                    }
                }
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.ARMOR)
                {
                    if (DetectCollision(interactiveObject[i]))
                    {
                        audioManager.Play("ReggieEquipedSomething");
                        ItemUIManager.armorPickedUp = true;
                    }
                }
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.SHOVEL)
                {
                    if (DetectCollision(interactiveObject[i]))
                    {
                        audioManager.Play("ReggiePickupAnyItem");
                        ItemUIManager.shovelPickedUp = true;
                    }
                }
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.HEALTHPOTION)
                {
                    if (DetectCollision(interactiveObject[i]))
                    {
                        audioManager.Play("ReggiePickupAnyItem");
                        ItemUIManager.healthPickedUp = true;
                        ItemUIManager.healthPotionsCount++;
                    }
                }
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.JUMPPOTION)
                {
                    if (DetectCollision(interactiveObject[i]))
                    {
                        audioManager.Play("ReggiePickupAnyItem");
                        ItemUIManager.jumpPickedUp = true;
                        ItemUIManager.jumpPotionsCount++;
                    }
                }
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.POWERPOTION)
                {
                    if (DetectCollision(interactiveObject[i]))
                    {
                        audioManager.Play("ReggiePickupAnyItem");
                        ItemUIManager.powerPickedUp = true;
                        ItemUIManager.powerPotionsCount++;
                    }
                }
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA)
                {
                    if (DetectCollision(interactiveObject[i]))
                    {
                        audioManager.Play("ReggiePickupAnyItem");
                        ItemUIManager.goldenUmbrellaPickedUp = true;
                    }
                }
                if (interactiveObject[i].objectID == (int)Enums.ObjectsID.CORNNENCY)
                {
                    if (DetectCollision(interactiveObject[i]))
                    {
                        audioManager.Play("ReggiePickupAnyItem");
                        for(int k = 0; k < allGameObjectsList.Count(); k++)
                        {
                            if(allGameObjectsList[k].objectID == (int)Enums.ObjectsID.CORNNENCY)
                            {

                                if (allGameObjectsList[k].gameObjectPosition == interactiveObject[i].gameObjectPosition)
                                    gameObjectList.Remove(allGameObjectsList[k]);
                            }
                        }
                        ItemUIManager.cornnencyQuantity++;
                        break;
                    }
                }
            }
        }

        //Change player's texture if needed
        public void changeTexture(Texture2D texture)
        {
            this.gameObjectTexture = texture;
        }

        //Change player's texture if needed
        public void changeSecondTexture(Texture2D texture) 
        {
            this.UmbrellaTexture = texture;
        }
        //Change player's texture if needed
        public void changeThirdTexture(Texture2D texture) 
        {
            this.itemPlayerTexture = texture;
        }

        //Calculate player's post update position
        private void PlayerPositionCalculation(GameTime gameTime, List<GameObject> gameObjectsToRender,List <GameObject> interactiveObject)
        {

            foreach (var platform in gameObjectsToRender)
            {
                if (((previousState.IsKeyDown(Keys.A) || previousState.IsKeyDown(Keys.D) || previousState.IsKeyDown(Keys.S) || previousState.IsKeyDown(Keys.W) || previousState.IsKeyDown(Keys.Space)) || gravityActive || previousGamepadState.IsButtonDown(Buttons.DPadDown) || previousGamepadState.IsButtonDown(Buttons.DPadUp)|| previousGamepadState.IsButtonDown(Buttons.DPadRight)|| previousGamepadState.IsButtonDown(Buttons.DPadLeft)|| previousGamepadState.IsButtonDown(Buttons.A) || previousGamepadState.IsButtonDown(Buttons.B)) && !playerGameElementInteraction && platform.objectID == (int)Enums.ObjectsID.PLATFORM)
                {
                    if(velocity.X !=0 && isStanding)
                            audioManager.Play("ReggieMoves");
                    //Checks collision on the left side and right side of each sprite when player is on the ground/air
                    if (velocity.X > 0 && IsTouchingLeftSide(platform))
                    {
                        velocity.X = 0;
                    collisionBoxPosition.X = platform.gameObjectPosition.X - collisionBoxSize.X;
                    pressedLeftKey = false;
                        pressedRightKey = false;
                    }
                    else if(velocity.X < 0 && IsTouchingRightSide(platform))
                    {
                    velocity.X = 0;
                    collisionBoxPosition.X = platform.gameObjectPosition.X + platform.gameObjectRectangle.Width;
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
                        if (!justTouchedBottom && gravityActive == false)
                        {
                            audioManager.Play("ReggieHitsGround");
                            justTouchedBottom = true;
                        }
                        firstJump = false;
                        secondJump = false;
                        isStanding = true;
                        pressedLeftKey = false;
                        pressedRightKey = false;
                        jumpButtonPressed = false;
                        collisionBoxPosition.Y = platform.gameObjectPosition.Y - collisionBoxSize.Y;
                    }

                    if (!IsTouchingTopSide(platform, gravity) && isStanding == false)
                    {
                        gravityActive = true;
                        if (pressedRightKey && !knockedBack)
                            velocity.X = movementSpeed;
                        else if (pressedLeftKey && !knockedBack)
                            velocity.X = -movementSpeed;
                        else if (pressedRightKey && knockedBack)
                            velocity.X = knockBackValue;
                        else if (pressedLeftKey && knockedBack)
                            velocity.X = -knockBackValue;
                        if (IsTouchingLeftSide(platform))
                        {
                            velocity.X = 0;
                            pressedLeftKey = false;
                            pressedRightKey = false;
                            collisionBoxPosition.X = platform.gameObjectPosition.X - collisionBoxSize.X;
                        }
                        if ( IsTouchingRightSide(platform))
                        {
                            velocity.X = 0;
                            pressedLeftKey = false;
                            pressedRightKey = false;
                            collisionBoxPosition.X = platform.gameObjectPosition.X + platform.gameObjectRectangle.Width;
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
          

            if ((firstJump == true || secondJump == true))
                PlayerJump();
            if (gravityActive && isStanding == false)
            {
                justTouchedBottom = false;
                gravity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 51;
                if (gravity.Y > 20 && (previousState.IsKeyDown(Keys.Space) || previousGamepadState.IsButtonDown(Buttons.A)))
                {
                    if(!isFloating)
                    audioManager.Play("ReggieOpensSchirm");
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
            if ((velocity.X == 0 && velocity.Y == 0) || (!isStanding && velocity.Y !=0) ||(gravityActive))
                audioManager.Break("ReggieMoves");
            if (gameObjectPosition != collisionBoxPosition -changeCollisionBox)
            {
                gameObjectPosition = collisionBoxPosition - changeCollisionBox;
                isStanding = false;
            }
           
                //Stop MovementSound, -->kein temp sondern variable(move) in klasse
            velocity = Vector2.Zero;
        }

        //Execute player's jump
        private void PlayerJump()
        {
            velocity.Y = jumpSpeed;
            if (previousState.IsKeyDown(Keys.S) || previousGamepadState.ThumbSticks.Left.Y < -0.5f || previousGamepadState.IsButtonDown(Buttons.DPadDown))
                velocity.Y = movementSpeed;

            //MediaPlayer.Play(Game1.songDictionnary["houseChord"]);
        }

        //Contains Player Movement in all 4 directions and the attack
        private void PlayerControls(GameTime gameTime, List<Enemy> enemyList, List<GameObject> interactiveObject, ref List<GameObject> GameObjectsList, LoadAndSave loadAndSave, IngameMenus ingameMenus, List<GameObject> levelGameObjects, ShopKeeper shopKeeper, ItemUIManager itemUIManager, Boss hakume)
        {
            //using item:
            if ((Keyboard.GetState().IsKeyDown(Keys.F) || GamePad.GetState(0).IsButtonUp(Buttons.B)) && !previousState.IsKeyDown(Keys.B) && previousGamepadState.IsButtonDown(Buttons.B))
            {
                int temp = itemUIManager.RemoveObject();
                if(temp == (int)Enums.ObjectsID.HEALTHPOTION)
                {
                    playerHP += 0.1f;
                    if (playerHP >= 1f) playerHP = 1f;
                }
                if(temp == (int)Enums.ObjectsID.POWERPOTION)
                {
                    playerDamage *= 2;
                }
                if(temp == (int)Enums.ObjectsID.JUMPPOTION)
                {
                    defaultJumpValue = -22f;
                }
            }


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
                //if (Keyboard.GetState().IsKeyDown(Keys.P))
                //    ReducePlayerHP();
                if (Keyboard.GetState().IsKeyDown(Keys.A) || GamePad.GetState(0).ThumbSticks.Left.X < -0.5f || GamePad.GetState(0).IsButtonDown(Buttons.DPadLeft))
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
                        

                    if(!playerGameElementInteraction)
                    velocity.X = -movementSpeed;
                    pressedLeftKey = true;
                    facingDirectionRight = false;
                    pressedRightKey = false;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D) || GamePad.GetState(0).ThumbSticks.Left.X > 0.5f || GamePad.GetState(0).IsButtonDown(Buttons.DPadRight))
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
                     if(!playerGameElementInteraction)
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
                    jumpSpeed = defaultJumpValue;
                PlayerJump();

                //MUSIC
                if (secondJump != true)
                    audioManager.Play("ReggieJump");
            }

            //Player Attack Input
            if ((ButtonState.Pressed == mouseState.LeftButton && cooldown == 0 && !playerGameElementInteraction)
                || GamePad.GetState(0).IsButtonDown(Buttons.X) && cooldown == 0 && !playerGameElementInteraction)
            {
                audioManager.Play("ReggieAttack");
                audioManager.Play("ReggieGroaning");

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
                //foreach (var enemy in enemyList)
                //{
                //    if (PlayerAttackCollision(enemy) && enemy.EnemyAliveState() == true && !enemy.invincibilityFrames)
                //    {
                //        enemy.invincibilityFrames = true;
                //        //worm.KnockBackPosition(facingDirectionRight, 35);
                //        enemy.KnockBackPosition(facingDirectionRight);
                //    }
                //}

                if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SCISSORS)
                {
                    //Platform temp = null;
                    playerDamage = 3;
                    foreach (Platform platform in levelGameObjects.Cast<GameObject>().OfType<Platform>().ToList())
                    {
                        if (DetectCollision(platform) && platform.PlatformType == (int)Enums.ObjectsID.SPIDERWEB)
                        {
                            //temp = platform;
                            levelGameObjects.Remove(platform);
                            break;
                        }
                    }
                    //GameObjectsList.Remove(temp);
                }
                if (ItemUIManager.currentItemEquipped.objectID == (int)Enums.ObjectsID.SHOVEL)
                {
                    movementSpeed = 20f;
                    defaultJumpValue = 20f;
                }
                    //TODO:Destroyable? temp
                    // Platform temp = null;
                    foreach (Platform platform in levelGameObjects.Cast<GameObject>().OfType<Platform>().ToList())
                {
                    if (DetectCollision(platform) && platform.PlatformType == (int)Enums.ObjectsID.VINEDOOR)
                    {
                        levelGameObjects.Remove(platform);
                        break;
                    }
                }
                //GameObjectsList.Remove(temp);



                foreach (Item item in levelGameObjects.Cast<GameObject>().OfType<Item>().ToList())
                {
                    if (item.objectID == (int)Enums.ObjectsID.APPLE)
                    {
                        if (item.gameObjectRectangle.Contains(this.gameObjectPosition))
                        {
                            ingameMenus.saveAnimStart();
                            loadAndSave.Save();
                            Console.WriteLine("Game Saved");
                            break;
                        }
                    }
                }

                foreach (ShopKeeper shopkeeper in levelGameObjects.Cast<GameObject>().OfType<ShopKeeper>().ToList())
                {
                    if (shopkeeper.objectID == (int)Enums.ObjectsID.SHOPKEEPER)
                    {
                        if (shopkeeper.gameObjectRectangle.Contains(this.gameObjectPosition))
                            shopKeeper.shopOpen = true;
                        else
                            shopKeeper.shopOpen = false;
                    }
                    
                }

                playerAttackPressed = true;
            
            }

            if (playerAttackPressed)
            {
                attackTimer += (float)gameTime.ElapsedGameTime.Milliseconds / 100;
                if (attackTimer != 0 && attackTimer <= 0.5f)
                {
                    foreach (var enemy in enemyList)
                    {
                        if (PlayerAttackCollision(enemy) && enemy.EnemyAliveState() == true && !enemy.invincibilityFrames)
                        {
                            enemy.invincibilityFrames = true;
                            enemy.KnockBackPosition(facingDirectionRight, playerDamage);
                            //enemy.KnockBackPosition(facingDirectionRight);
                        }
                    }
                    if (PlayerAttackCollision(hakume))
                        hakume.ReduceEnemyHP(playerDamage);
                }
                else
                    attackTimer = 0;

                cooldown += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            }
            if (cooldown>=.75)
            {
                cooldown = 0;
                playerAttackPressed = false;
            }
           

            //Player Gameelement Interactive Input
            if((ButtonState.Pressed == mouseState.RightButton || Keyboard.GetState().IsKeyDown(Keys.W) || GamePad.GetState(0).IsButtonDown(Buttons.DPadUp) && !playerGameElementInteraction && !previousState.IsKeyDown(Keys.W) && !previousGamepadState.IsButtonDown(Buttons.DPadUp)))
            {
                foreach(var vine in interactiveObject)
                {
                    if(DetectCollision(vine) )
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
                        velocity.X = 0;
                        collisionBoxPosition.X = vine.gameObjectRectangle.X;

                        if (gameObjectPosition != collisionBoxPosition - changeCollisionBox)
                        {
                            gameObjectPosition = collisionBoxPosition - changeCollisionBox;
                            
                        }
                    }
                }
            }
            if((Keyboard.GetState().IsKeyDown(Keys.W)|| GamePad.GetState(0).IsButtonDown(Buttons.DPadUp)) && playerGameElementInteraction)
            {
                climbAllowed = false;
                velocity.X = 0;
                velocity.Y = -movementSpeed - 2;
                foreach (var vine in interactiveObject)
                {
                    if(Math.Abs(collisionBoxPosition.X - vine.gameObjectRectangle.X) <= 5)
                    {
                        collisionBoxPosition.X = vine.gameObjectRectangle.X;
                        if (collisionRectangle.Bottom + velocity.Y >= vine.gameObjectRectangle.Top + 30)
                            climbAllowed = true;

                    }
                }
                if (gameObjectPosition != collisionBoxPosition - changeCollisionBox)
                {
                    gameObjectPosition = collisionBoxPosition - changeCollisionBox;

                }
                if (climbAllowed)
                    velocity.Y = -movementSpeed - 2;
                else
                    velocity.Y = 0;
            }
            else if((Keyboard.GetState().IsKeyDown(Keys.S) || GamePad.GetState(0).IsButtonDown(Buttons.DPadDown)) && playerGameElementInteraction)
            {
                climbAllowed = false;
                velocity.X = 0;
                velocity.Y = movementSpeed + 2;
                foreach (var vine in interactiveObject)
                {
                    if (Math.Abs(collisionBoxPosition.X - vine.gameObjectRectangle.X) <= 5)
                    {
                        collisionBoxPosition.X = vine.gameObjectRectangle.X;
                        if (collisionRectangle.Top + velocity.Y <= vine.gameObjectRectangle.Bottom - 80)
                            climbAllowed = true;

                    }
                }
                if (gameObjectPosition != collisionBoxPosition - changeCollisionBox)
                {
                    gameObjectPosition = collisionBoxPosition - changeCollisionBox;

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

        //Draw function that can draw the player
        public void drawUpdate(List<GameObject> GameObjectsList, ref IngameMenus ingameMenus)
        {
            for (int i = 0; i < GameObjectsList.Count(); i++)
            {
                if (GameObjectsList[i].objectID == (int)Enums.ObjectsID.APPLE)
                {
                    if (GameObjectsList[i].gameObjectRectangle.Contains(this.gameObjectPosition))
                    {
                        ingameMenus.drawSaveIcon(this.gameObjectPosition);
                        break;
                    }
                }
            }
        }

        //Collision with levelObjects
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
        public void ReducePlayerHP(float damage)
        {
            controllerVibration = true;
            
            if (playerHP > 0)
            {
                playerHP -= damage;
                audioManager.Play("ReggieHurt");
            }
            else
            {
                stillAlive = false;
                audioManager.Play("AnnouncerInsult");
            }
        }
        public void ReducePlayerHP(float damage, float decreaseSpeed)
        {
            if (playerHP > 0)
            {
                movementSpeed = decreaseSpeed;
                playerSlowed = true;
                playerHP -= damage;
                audioManager.Play("ReggieHurt");
            }
            else
            {
                stillAlive = false;
                audioManager.Play("AnnouncerInsult");
            }
        }

        // Returns player's current HP
        public float PlayersCurrentHP()
        {
            return playerHP;
        }

        //Returns player's current state
        public bool PlayerIsStillAlive()
        {
            return stillAlive;
        }

        //Activate invincibleframes if the player was hit
        public void InvincibleFrameState(GameTime gameTime)
        {
            invincibilityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            if(invincibilityTimer > invincibilityFixedTimer)
            {
                invincibilityTimer = 0;
                invincibilityFrames = false;
                playerSlowed = false;
                invincibilityFixedTimer = 5f;
            }
        }
      

      

        public void drawSecondTexture(SpriteBatch spriteBatch, Rectangle sourceRectangle, SpriteEffects spriteEffects, Vector2 offset, Color color) 
        {
            spriteBatch.Draw(UmbrellaTexture, gameObjectPosition + offset, sourceRectangle, color, 0, Vector2.Zero, Vector2.One, spriteEffects, 0);
        }

        public void drawThirdTexture(SpriteBatch spriteBatch, Rectangle sourceRectangle, SpriteEffects spriteEffects, Vector2 offset, Color color) 
        {
            spriteBatch.Draw(itemPlayerTexture, gameObjectPosition + offset, sourceRectangle, color, 0, Vector2.Zero, Vector2.One, spriteEffects, 0);
        }

        private void Vibration()
        {
            if (controllerVibration)
            {
                if (firstVibrationCycle)
                {
                    currentTime = 0;
                    firstVibrationCycle = false;
                }
                if (currentTime < 1.2f)
                {
                    GamePad.SetVibration(0, 1f, 1f);
                    currentTime += 0.1f;
                }
                else
                {
                    GamePad.SetVibration(0, 0f, 0f);
                    firstVibrationCycle = true;
                    controllerVibration = false;
                }
            }
        }
    }
}
