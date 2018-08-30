using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Reggie {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {

        public enum GameState { MAINMENU, GAMELOOP, LEVELEDITOR, CREDITS, SPLASHSCREEN, LOADSCREEN, WINSCREEN, LOSESCREEN }
        public static GameState currentGameState {get;set; }
        //forces an update before draw action
        public GameState lastGameState;

        public static Player wormPlayer;
        public Enemy ant;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<GameObject> AllGameObjectList;
        List<GameObject> LevelObjectList;
        
        List<Platform> platformList;
        List<Enemy> enemyList;
        List<Enemy> viewableEnemies;
        List<GameObject> gameObjectsToRender;
        List<GameObject> interactiveObject;
        
        Dictionary<string, Texture2D> texturesDictionnary;
        Dictionary<String, Texture2D> PlayerSpriteSheets;
        Dictionary<String, Texture2D> EnemySpriteSheets;
        
        //Texture2D enemytexture;
        Texture2D enemySkinTexture;
        Texture2D background;
        Texture2D Sky_2000_1000;
        Texture2D Ground_Tutorial_2000_1000;
        Texture2D Hub_Background;
        Texture2D Ant_Cave_Background;
        Texture2D Tree_Background;
        Texture2D Roof_Background;
        Texture2D Greenhouse_Background;
        Texture2D Crown_Background;
        Texture2D Dunghill_Background;
        Texture2D Platform_320_64;
        Texture2D Transparent_Wall_500x50;
        Texture2D Transparent_Wall_1000x50;
        Texture2D ClimbinPlant_38_64;
        Texture2D levelEditorUIBackButton;
        Texture2D UserInterface;
        Texture2D playerHealthbar;
        Texture2D L1ButtonIcon;
        Texture2D L2ButtonIcon;
        Texture2D R1ButtonIcon;
        Texture2D R2ButtonIcon;
        Texture2D SnailShell;
        Texture2D SpiderWeb;
        Texture2D Scissors;
        Texture2D Armor;
        Texture2D Shovel;
        AnimationManager animManager;
        LevelEditor levelEditor;
        SpriteSheetSizes input = new SpriteSheetSizes();
        FrameCounter frameCounter = new FrameCounter();
        SpriteFont font;
        Camera camera = new Camera();
        GameManager GameManager;
        Levels LevelManager;

        //for switching LevelEditor
        public static KeyboardState previousState;

        SplashScreen splashScreen;
        MainMenu MainMenu;

       // Color[] colorData;
       // Vector2 enemyaggroposition;
        Enums Enums;

        Matrix transformationMatrix;

        public static Vector2 cameraOffset;
      

        
        public Game1()
        {
            currentGameState = GameState.SPLASHSCREEN;
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            //graphics.PreferredBackBufferHeight = 1000;
            //graphics.PreferredBackBufferWidth = 1800;
            graphics.PreferredBackBufferHeight = 1080-40-20;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.ApplyChanges();
            Window.AllowUserResizing = true;
            input.ReadImageSizeDataSheet();
            PlayerSpriteSheets = new Dictionary<string, Texture2D>();
            EnemySpriteSheets = new Dictionary<string, Texture2D>();
            levelEditor = new LevelEditor();
            cameraOffset = new Vector2(0, 0);
            texturesDictionnary = new Dictionary<string, Texture2D>();
            enemyList = new List<Enemy>();
            Enums = new Enums();
            splashScreen = new SplashScreen();
            MainMenu = new MainMenu();
            GameManager = new GameManager();
            LevelManager = new Levels();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // MONO: Add your initialization logic here

            base.Initialize();
           
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Arial");
            enemySkinTexture = Content.Load<Texture2D>("Images\\door");
            Texture2D platformTexture = Content.Load<Texture2D>("Images\\floor");


            ///Load Player Sprite Sheets
            Texture2D playerJumpSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Jump_Small");
            PlayerSpriteSheets.Add("playerJumpSpriteSheet", playerJumpSpriteSheet);
            Texture2D playerJumpHatSpritesSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Jump_Hat");
            PlayerSpriteSheets.Add("playerJumpHatSpriteSheet", playerJumpHatSpritesSheet);
            Texture2D playerJumpArmorSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Jump_Armor");
            PlayerSpriteSheets.Add("playerJumpArmorSpriteSheet", playerJumpArmorSpriteSheet);
            Texture2D playerJumpArmorHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Jump_Armor_Hat");
            PlayerSpriteSheets.Add("playerJumpArmorHatSpriteSheet", playerJumpArmorHatSpriteSheet);

            Texture2D playerMoveSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Even_Smaller");
            PlayerSpriteSheets.Add("playerMoveSpriteSheet", playerMoveSpriteSheet);
            Texture2D playerMoveHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Hat");
            PlayerSpriteSheets.Add("playerMoveHatSpriteSheet", playerMoveHatSpriteSheet);
            Texture2D playerMoveArmorSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Armor");
            PlayerSpriteSheets.Add("playerMoveArmorSpriteSheet", playerMoveArmorSpriteSheet);
            Texture2D playerMoveArmorHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Move_Armor_Hat");
            PlayerSpriteSheets.Add("playerMoveArmorHatSpriteSheet", playerMoveArmorHatSpriteSheet);

            Texture2D playerAttackSpritesheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack");
            PlayerSpriteSheets.Add("playerAttackSpriteSheet", playerAttackSpritesheet);
            Texture2D playerAttackHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack_Hat");
            PlayerSpriteSheets.Add("playerAttackHatSpriteSheet", playerAttackHatSpriteSheet);
            Texture2D playerAttackArmorSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack_Armor");
            PlayerSpriteSheets.Add("playerAttackArmorSpriteSheet", playerAttackArmorSpriteSheet);
            Texture2D playerAttackArmorHatSpriteSheet = Content.Load<Texture2D>("Images\\PlayerSpriteSheets\\Reggie_Attack_Armor_Hat");
            PlayerSpriteSheets.Add("playerAttackArmorHatSpritesheet", playerAttackArmorHatSpriteSheet);

            //Load EnemySpriteSheets
            Texture2D Ladybug_Fly = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\ladybug_floating_Left_Small");
            EnemySpriteSheets.Add("Ladybug_Fly_Spritesheet", Ladybug_Fly);
            Texture2D Ladybug_Attack = Content.Load<Texture2D>("Images\\Enemies Sprite Sheets\\Ladybug_Attack_Small");
            EnemySpriteSheets.Add("Ladybug_Attack_Spritesheet", Ladybug_Attack);




            animManager = new AnimationManager(PlayerSpriteSheets);
            wormPlayer = new Player(playerMoveSpriteSheet, new Vector2(SpriteSheetSizes.spritesSizes["Reggie_Move_X"]/5, SpriteSheetSizes.spritesSizes["Reggie_Move_Y"] / 5), new Vector2(13444, 1500) /*new Vector2(-7500,-7404)*/, (int) Enums.ObjectsID.PLAYER);

           
            //{
            //    new Enemy(enemySkinTexture, new Vector2(50,50),new Vector2(700,200)),
            //};
            platformList = new List<Platform>();
            AllGameObjectList = new List<GameObject>();
            interactiveObject = new List<GameObject>();
            
            //foreach (var enemy in enemyList)
            //    enemy.SetPlayer(wormPlayer);

            background = Content.Load<Texture2D>("Images\\World\\Lvl1_Background");
            Sky_2000_1000 = Content.Load<Texture2D>("Images\\World\\Himmel_Level_Tutorial");
            Ground_Tutorial_2000_1000 = Content.Load<Texture2D>("Images\\World\\Erde_Level_Tutorial");
            Ant_Cave_Background = Content.Load<Texture2D>("Images\\World\\Ameisenhöhle");
            Tree_Background = Content.Load<Texture2D>("Images\\World\\Baum");
            Roof_Background = Content.Load<Texture2D>("Images\\World\\Dach");
            Greenhouse_Background = Content.Load<Texture2D>("Images\\World\\Gewächshaus");
            Hub_Background = Content.Load<Texture2D>("Images\\World\\Hub");
            Crown_Background = Content.Load<Texture2D>("Images\\World\\krone");
            Dunghill_Background = Content.Load<Texture2D>("Images\\World\\Misthaufen");
            Platform_320_64 = Content.Load<Texture2D>("Images\\Platform_320_64");
            texturesDictionnary.Add("Green_320_64", Platform_320_64);
            Transparent_Wall_500x50 = Content.Load<Texture2D>("Images\\Transparent_Wall_500x50");
            texturesDictionnary.Add("Transparent_500x50", Transparent_Wall_500x50);
            Texture2D Transparent_Wall_64x64 = Content.Load<Texture2D>("Images\\WorldObjects\\Transparent - 64x64");
            texturesDictionnary.Add("Transparent_64x64", Transparent_Wall_64x64);
            Transparent_Wall_1000x50 = Content.Load<Texture2D>("Images\\Transparent_Wall_1000x50");
            texturesDictionnary.Add("Transparent_1000x50", Transparent_Wall_1000x50);
            levelEditorUIBackButton = Content.Load<Texture2D>("Images\\UI\\LvlEdtorSaveButton");
            texturesDictionnary.Add("LevelEditorUIBackButton", levelEditorUIBackButton);
            ClimbinPlant_38_64 = Content.Load<Texture2D>("Images\\WorldObjects\\plantLeaves_1");
            texturesDictionnary.Add("Climbingplant_38x64", ClimbinPlant_38_64);
            SnailShell = Content.Load<Texture2D>("Images\\Schneckenhaus");
            texturesDictionnary.Add("SnailShell", SnailShell);
            SpiderWeb = Content.Load<Texture2D>("Images\\WorldObjects\\SpiderWeb");
            texturesDictionnary.Add("Spiderweb_64x64", SpiderWeb);
            Scissors = Content.Load<Texture2D>("Images\\Schere");
            texturesDictionnary.Add("Scissors_64x64", Scissors);
            Armor = Content.Load<Texture2D>("Images\\Rüstung");
            texturesDictionnary.Add("Armor_64x64", Armor);
            Shovel = Content.Load<Texture2D>("Images\\Schaufel");
            texturesDictionnary.Add("Shovel_64x64", Shovel);
            UserInterface = Content.Load<Texture2D>("Images\\UI\\UI");
            texturesDictionnary.Add("UI", UserInterface);
            playerHealthbar = Content.Load<Texture2D>("Images\\UI\\Healthbar");
            texturesDictionnary.Add("Healthbar", playerHealthbar);
            L1ButtonIcon = Content.Load<Texture2D>("Images\\UI\\buttonL1");
            texturesDictionnary.Add("buttonL1", L1ButtonIcon);
            L2ButtonIcon = Content.Load<Texture2D>("Images\\UI\\buttonL2");
            texturesDictionnary.Add("buttonL2", L2ButtonIcon);
            R1ButtonIcon = Content.Load<Texture2D>("Images\\UI\\buttonR1");
            texturesDictionnary.Add("buttonR1", R1ButtonIcon);
            R2ButtonIcon = Content.Load<Texture2D>("Images\\UI\\buttonR2");
            texturesDictionnary.Add("buttonR2", R2ButtonIcon);




            levelEditor.loadTextures(Content, ref texturesDictionnary, graphics.GraphicsDevice);
            for(int i = 0; i < texturesDictionnary.Count(); i++)
            Console.WriteLine(texturesDictionnary.ElementAt(i));
            LoadGameObjects();
            AllGameObjectList.Add(new Item(SnailShell, new Vector2(64, 64), new Vector2(13000, 1600), (int)Enums.ObjectsID.SNAILSHELL));
            //AllGameObjectList.Add(new Platform(SpiderWeb, new Vector2(64, 64), new Vector2(-2800, 400), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.SPIDERWEB, false));
            //AllGameObjectList.Add(new Platform(SpiderWeb, new Vector2(64, 64), new Vector2(-3000, 400), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.SPIDERWEB, false));
            //AllGameObjectList.Add(new Platform(SpiderWeb, new Vector2(64, 64), new Vector2(-3200, 400), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.SPIDERWEB, false));
            //AllGameObjectList.Add(new Platform(SpiderWeb, new Vector2(64, 64), new Vector2(-3400, 400), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.SPIDERWEB, false));
            AllGameObjectList.Add(new Item(Armor, new Vector2(64, 64), new Vector2(12800, 1600), (int)Enums.ObjectsID.ARMOR));
            AllGameObjectList.Add(new Item(Shovel, new Vector2(64, 64), new Vector2(12500, 1600), (int)Enums.ObjectsID.SHOVEL));
            AllGameObjectList.Add(new Item(Scissors, new Vector2(64, 64), new Vector2(12400, 1600), (int)Enums.ObjectsID.SCISSORS));


            LevelObjectList = new List<GameObject>();
            //LevelObjectList = AllGameObjectList;
            foreach (GameObject gameObject in AllGameObjectList) LevelObjectList.Add(gameObject);
            LevelManager.sortGameObjects(AllGameObjectList);

           
            FillLists();
            
            // MONO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // MONO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // MONO: Add your update logic here

            //In every State you are able to quit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || !wormPlayer.PlayerIsStillAlive())
                Exit();

            lastGameState = currentGameState;
          
            switch (currentGameState)
            {
                case GameState.SPLASHSCREEN:
                    splashScreen.ClickedButton();
                   break;
                case GameState.MAINMENU:
                    MainMenu.Update(this);
                    this.IsMouseVisible = true;
                    break;
                case GameState.LEVELEDITOR:

                    levelEditor.HandleLevelEditorEvents();
                  
                    levelEditor.moveOrDeletePlatforms(ref AllGameObjectList, transformationMatrix);
                        this.IsMouseVisible = true;
                        levelEditor.moveCamera(ref cameraOffset);
                    // Makes player movable in the leveleditor //Enemies are alive but not visible
                    gameObjectsToRender = camera.GameObjectsToRender(wormPlayer.gameObjectPosition, AllGameObjectList, ref interactiveObject);
                    wormPlayer.Update(gameTime, gameObjectsToRender, viewableEnemies, interactiveObject, ref AllGameObjectList);
                    break;

                case GameState.GAMELOOP:
                    this.IsMouseVisible = false;
                    //switch to LevelEditor
                    LevelManager.ManageLevels( wormPlayer.gameObjectPosition ,ref LevelObjectList);
                    GameManager.ManageItems(ref wormPlayer, ref LevelObjectList);

                    if (currentGameState == GameState.GAMELOOP)
                    {
                        //cameraOffset = new Vector2(0, 0);
                        if (Keyboard.GetState().IsKeyDown(Keys.L) && !previousState.IsKeyDown(Keys.L))
                            currentGameState = GameState.LEVELEDITOR;
                        previousState = Keyboard.GetState();
                    }

                    gameObjectsToRender = camera.GameObjectsToRender(wormPlayer.gameObjectPosition, LevelObjectList, ref interactiveObject);

                    camera.SpawnEnemyOffScreen(wormPlayer, platformList, ref enemyList, enemySkinTexture, EnemySpriteSheets);
                    viewableEnemies = camera.RenderedEnemies(wormPlayer.gameObjectPosition, enemyList);
                    wormPlayer.Update(gameTime, gameObjectsToRender, viewableEnemies, interactiveObject, ref LevelObjectList);

                    foreach (var enemy in enemyList.ToList())
                    {
                        enemy.Update(gameTime, LevelObjectList);
                        if (enemy.EnemyAliveState() == false || enemy.fallOutOfMap)
                            enemyList.RemoveAt(enemyList.IndexOf(enemy));
                    }

                    // calculates players collision rect(visual)
                    //enemytexture = new Texture2D(this.GraphicsDevice, (int)(WormPlayer.CollisionBoxSize.X), (int)(WormPlayer.CollisionBoxSize.Y));
                    //colorData = new Color[(int)((WormPlayer.CollisionBoxSize.X) * (WormPlayer.CollisionBoxSize.Y))];
                    //for (int i = 0; i < (WormPlayer.CollisionBoxSize.X) * (WormPlayer.CollisionBoxSize.Y); i++)
                    //    colorData[i] = Color.White;
                    //enemytexture.SetData<Color>(colorData);
                    //enemyaggroposition = new Vector2(WormPlayer.CollisionRectangle.X, WormPlayer.CollisionRectangle.Y);


                    //enemycounter = 0;
                    ////if (EnemyList.Count != 0)

                    //foreach (var enemy in enemyList.ToList())
                    //{
                    //    enemy.Update(gameTime, AllGameObjectList);
                    //    if (enemy.EnemyAliveState() == false || enemy.fallOutOfMap)
                    //        enemyList.RemoveAt(enemycounter);
                    //    if (!enemyList.Any())
                    //    {
                    //        Random rand = new Random();
                    //        int randomizedNumber = rand.Next(0, 3);
                    //        if (randomizedNumber == 0)
                    //            enemyList.Add(new Enemy(enemySkinTexture, new Vector2(50, 50), new Vector2(100, 200)));
                    //        else
                    //            enemyList.Add(new Enemy(enemySkinTexture, new Vector2(50, 50), new Vector2(600, 200)));
                    //        enemyList.Last().SetPlayer(wormPlayer);
                    //    }
                    //    if (enemyList.Count < 2)
                    //    {
                    //        Random rand = new Random();
                    //        int randomizedNumber = rand.Next(0, 3);
                    //        if (randomizedNumber == 0)
                    //            enemyList.Add(new Enemy(enemySkinTexture, new Vector2(50, 50), new Vector2(400, 200)));
                    //        else if (randomizedNumber == 1)
                    //            enemyList.Add(new Enemy(enemySkinTexture, new Vector2(50, 50), new Vector2(900, 200)));
                    //        else
                    //            enemyList.Add(new Enemy(enemySkinTexture, new Vector2(50, 50), new Vector2(300, 200)));
                    //        enemyList.Last().SetPlayer(wormPlayer);
                    //    }
                    //    enemycounter++;
                    //    //enemytexture = new Texture2D(this.GraphicsDevice, (int)(enemy.EnemyAggroAreaSize.W), (int)(enemy.EnemyAggroAreaSize.Z));
                    //    //colorData = new Color[(int)((enemy.EnemyAggroAreaSize.W) * (enemy.EnemyAggroAreaSize.Z))];
                    //    //for (int i = 0; i < (enemy.EnemyAggroAreaSize.W) * (enemy.EnemyAggroAreaSize.Z); i++)
                    //    //    colorData[i] = Color.White;
                    //    //enemytexture.SetData<Color>(colorData);
                    //    //enemyaggroposition = new Vector2(enemy.EnemyAggroArea.X, enemy.EnemyAggroArea.Y);
                    //}

                    //for (int i = 0; i < enemyList.Count(); i++)
                    //{
                    //    //Enemy tempEnemy = enemyList[i];
                    //    //AnimationManagerEnemy tempAnimationManager = enemyAnimationManagerList[i];
                    //    //if (enemyList[i].facingLeft) tempAnimationManager.nextAnimation = Enums.EnemyAnimations.LADYBUG_FLY_LEFT;
                    //    //else if (!enemyList[i].facingLeft) tempAnimationManager.nextAnimation = Enums.EnemyAnimations.LADYBUG_FLY_RIGHT;
                    //}


                    break;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
          //  if (lastGameState != GameState.GAMELOOP)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }
            
            
            // MONO: Add your drawing code here
            Viewport viewport = GraphicsDevice.Viewport;
            Vector2 screenCenter = new Vector2(viewport.Width / 2-(SpriteSheetSizes.spritesSizes["Reggie_Move_X"]/10) + cameraOffset.X, viewport.Height / 2-(SpriteSheetSizes.spritesSizes["Reggie_Move_Y"]/10)+50 + cameraOffset.Y);
            camera.setCameraWorldPosition(wormPlayer.gameObjectPosition);
            transformationMatrix = camera.cameraTransformationMatrix(viewport, screenCenter);

            if(currentGameState == GameState.SPLASHSCREEN || currentGameState == GameState.MAINMENU)
            spriteBatch.Begin(0, null, null, null, null, null, null);
            else
            spriteBatch.Begin(0, null, null, null, null, null, transformationMatrix);
            //added block for better readability
            {
                
                switch (currentGameState)
                {
                    case GameState.SPLASHSCREEN:
                       
                        splashScreen.RenderSplashScreen(Content, spriteBatch);

                        break;
                    case GameState.MAINMENU:
                        MainMenu.RenderMainMenu(Content, spriteBatch, font);
                        break;
                    case GameState.GAMELOOP:

                        if(lastGameState == currentGameState)
                        {
                            LevelManager.drawLevelsBackground(spriteBatch, background, Ground_Tutorial_2000_1000, Hub_Background, Dunghill_Background, Ant_Cave_Background, Greenhouse_Background, Tree_Background, Crown_Background, Roof_Background);

                            
                            //this draws the platforms visible in the viewport
                            foreach (var platformSprite in gameObjectsToRender)
                                if (platformSprite.IsThisAVisibleObject())
                                    platformSprite.DrawSpriteBatch(spriteBatch);

                            //this draws the enemy
                            //spriteBatch.Draw(enemytexture, enemyaggroposition, Color.White);
                            //foreach (var enemy in viewableEnemies)
                            //    enemy.DrawSpriteBatch(spriteBatch);

                            if (LevelManager.currentLevel == Enums.Level.TUTORIAL)
                            {
                                for (int i = 0; i < enemyList.Count(); i++)
                                {
                                    enemyList[i].EnemyAnimationUpdate(gameTime, spriteBatch);
                                }
                            }

                            //This draws the player
                            animManager.animation(gameTime, ref wormPlayer, spriteBatch);

                            
                            //This draws the UI
                            GameManager.drawUI(texturesDictionnary,spriteBatch,transformationMatrix,GraphicsDevice, wormPlayer.PlayersCurrentHP());



                        }
                        break;

                    case GameState.LEVELEDITOR:


                        LevelManager.drawLevelsBackground(spriteBatch, background, Ground_Tutorial_2000_1000, Hub_Background, Dunghill_Background, Ant_Cave_Background, Greenhouse_Background, Tree_Background, Crown_Background, Roof_Background);


                        //spriteBatch.DrawString(font, fps, new Vector2(wormPlayer.gameObjectPosition.X - 620, wormPlayer.gameObjectPosition.Y - 490), Color.Black);


                        
                        Vector2 mouse = Vector2.Transform(new Vector2(-Mouse.GetState().Position.X, -Mouse.GetState().Position.Y), transformationMatrix);
                        string mouseCoordinates = "(x: " + (-mouse.X) + ", y: " + (-mouse.Y) + ")";
                        spriteBatch.DrawString(font, mouseCoordinates, Vector2.Transform(new Vector2(Mouse.GetState().Position.X+10, Mouse.GetState().Position.Y-15), Matrix.Invert(transformationMatrix)), Color.Black);


                        //this draws all the platforms in the game
                        foreach (var platformSprite in AllGameObjectList)
                            platformSprite.DrawSpriteBatch(spriteBatch);
                        levelEditor.DrawLvlEditorUI(texturesDictionnary, spriteBatch, transformationMatrix, ref AllGameObjectList, GraphicsDevice);

                        //This draws the player
                        animManager.animation(gameTime, ref wormPlayer, spriteBatch);

                        //Writes Leveleditor Text when Level Editor is enabled
                        string lvlEditorString = "Level Editor Enabled!";
                        spriteBatch.DrawString(font, lvlEditorString, Vector2.Transform(new Vector2(10, 30), Matrix.Invert(transformationMatrix)), Color.DarkRed);
                        break;
                }

                //Comment: SEE Framecounter.cs for additional commentary
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                frameCounter.Update(deltaTime);
                var fps = string.Format("FPS: {0}", frameCounter.averageFramesPerSecond);
                spriteBatch.DrawString(font, fps, Vector2.Transform(new Vector2(10,10), Matrix.Invert(transformationMatrix)), Color.Black);
                //end comment.

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


        private void LoadGameObjects()
        {
            List<string> data = new List<string>(System.IO.File.ReadAllLines(@"SaveFile.txt"));

            List<String> dataSeperated = new List<String>();
            foreach (String s in data)
            {
                List<String> tempStringList = s.Split(',').ToList();
                foreach (String st in tempStringList) dataSeperated.Add(st);
            }

            for (int i = 0; i < dataSeperated.Count(); i++)
            {
                if (dataSeperated[i] == Enums.ObjectsID.GREEN_PLATFORM_320_64.ToString())
                {
                    AllGameObjectList.Add(new Platform(texturesDictionnary["Green_320_64"], new Vector2(320, 64), new Vector2(Int32.Parse(dataSeperated[i+1]), Int32.Parse(dataSeperated[i+2])), (int)Enums.ObjectsID.PLATFORM,(int)Enums.ObjectsID.GREEN_PLATFORM_320_64, true));
                }
                if (dataSeperated[i] == Enums.ObjectsID.INVISIBLE_WALL_500x50.ToString())
                {
                    AllGameObjectList.Add(new Platform(texturesDictionnary["Transparent_500x50"], new Vector2(500, 50), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM,(int)Enums.ObjectsID.INVISIBLE_WALL_500x50, true));
                    AllGameObjectList.Last().DontDrawThisObject();
                }
                if (dataSeperated[i] == Enums.ObjectsID.INVSIBLE_WALL_1000x50.ToString())
                {
                    AllGameObjectList.Add(new Platform(texturesDictionnary["Transparent_1000x50"], new Vector2(1000, 50),  new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.INVSIBLE_WALL_1000x50, true));
                    AllGameObjectList.Last().DontDrawThisObject();
                }
                if(dataSeperated[i] == Enums.ObjectsID.VINE.ToString())
                {
                    AllGameObjectList.Add(new Platform(texturesDictionnary["Climbingplant_38x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.VINE,(int)Enums.ObjectsID.VINE, false));
                }
                if (dataSeperated[i] == Enums.ObjectsID.INVISIBLE_WALL_64x64.ToString())
                {
                    AllGameObjectList.Add(new Platform(texturesDictionnary["Transparent_64x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.VINE, (int)Enums.ObjectsID.VINE, false));
                }
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_01.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_02.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_02, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_03.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_03, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_04.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_04, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_05.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_05, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_06.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_06, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_07.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_07, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_08.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_08, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_09.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_09, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_10.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_11.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_12.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_13.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_14.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_15.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_16.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_17.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_17, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_18.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_18, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_19.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_20.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_21.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_22.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_23.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_24.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_24, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_25.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_25, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_26.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_26, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_27.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_27, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_01.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_02.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_02, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_03.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_03, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_04.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_04, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_05.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_05, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_06.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_06, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_07.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_07, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_08.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_08, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_09.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_09, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_10.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_11.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_12.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_13.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_14.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_15.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_16.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_17.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_17, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_18.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_18, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_19.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_20.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_21.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_22.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_23.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_24.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_24, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_25.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_25, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_26.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_26, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_27.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_27, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_01.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_02.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_02, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_03.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_03, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_04.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_04, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_05.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_05, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_06.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_06, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_07.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_07, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_08.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_08, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_09.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_09, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_10.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_11.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_12.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_13.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_14.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_15.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_16.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_17.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_17, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_18.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_18, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_19.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_20.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_21.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_22.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_23.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_24.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_24, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_25.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_25, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_26.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_26, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_27.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_27, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_01.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_02.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_02, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_03.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_03, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_04.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_04, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_05.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_05, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_06.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_06, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_07.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_07, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_08.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_08, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_09.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_09, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_10.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_11.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_12.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_13.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_14.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_15.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_16.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_17.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_17, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_18.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_18, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_19.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_20.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_21.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_22.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_23.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_24.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_24, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_25.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_25, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_26.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_26, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_27.ToString())
                    AllGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_27, true));

            }
        }

        private void FillLists()
        {
            for(int i = 0; i < AllGameObjectList.Count(); i++)
            {
                if (AllGameObjectList[i].objectID == (int)Enums.ObjectsID.PLATFORM)
                    platformList.Add((Platform)AllGameObjectList[i]);
                if (AllGameObjectList[i].objectID == (int)Enums.ObjectsID.VINE)
                    interactiveObject.Add(AllGameObjectList[i]);
                if (AllGameObjectList[i].objectID == (int)Enums.ObjectsID.SNAILSHELL) interactiveObject.Add(AllGameObjectList[i]);
                if (AllGameObjectList[i].objectID == (int)Enums.ObjectsID.SCISSORS) interactiveObject.Add(AllGameObjectList[i]);
                if (AllGameObjectList[i].objectID == (int)Enums.ObjectsID.ARMOR) interactiveObject.Add(AllGameObjectList[i]);
                if (AllGameObjectList[i].objectID == (int)Enums.ObjectsID.SHOVEL) interactiveObject.Add(AllGameObjectList[i]);


                foreach (Platform platform in AllGameObjectList.Cast<GameObject>().OfType<Platform>())
                {
                    if (platform.PlatformType == (int)Enums.ObjectsID.SPIDERWEB)
                        interactiveObject.Add(platform);
                }
               
                


            }

            for (int i = 0; i < interactiveObject.Count(); i++)
            {
                Console.WriteLine(interactiveObject[i]);
            }
        }



        
    }
}
