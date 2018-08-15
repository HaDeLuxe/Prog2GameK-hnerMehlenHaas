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
        List<GameObject> gameObjectList;
        List<Platform> platformList;
        List<Enemy> enemyList;
        List<Enemy> viewableEnemies;
        List<GameObject> gameObjectsToRender;
        List<GameObject> interactiveObject;
        Dictionary<string, Texture2D> texturesDictionnary;

        int enemycounter;
        //Texture2D enemytexture;
        Texture2D enemySkinTexture;
        Texture2D background;
        Texture2D Sky_2000_500;
        Texture2D Platform_320_64;
        Texture2D Transparent_Wall_500x50;
        Texture2D Transparent_Wall_1000x50;
        Texture2D ClimbinPlant_38_64;
        Texture2D levelEditorUIBackButton;
        AnimationManager animManager;
        LevelEditor levelEditor;
        SpriteSheetSizes input = new SpriteSheetSizes();
        FrameCounter frameCounter = new FrameCounter();
        SpriteFont font;
        Camera camera = new Camera();
        //for switching LevelEditor
        public static KeyboardState previousState;

        SplashScreen splashScreen;
        MainMenu MainMenu;

       // Color[] colorData;
       // Vector2 enemyaggroposition;
        Enums Enums;

        Matrix transformationMatrix;

        public static Vector2 cameraOffset;




        Dictionary<String, Texture2D> playerSpriteSheets;

        public Game1()
        {
            currentGameState = GameState.SPLASHSCREEN;
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            //graphics.PreferredBackBufferHeight = 1000;
            //graphics.PreferredBackBufferWidth = 1800;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.ApplyChanges();
            Window.AllowUserResizing = true;
            input.ReadImageSizeDataSheet();
            playerSpriteSheets = new Dictionary<string, Texture2D>();
            levelEditor = new LevelEditor();
            cameraOffset = new Vector2(0, 0);
            texturesDictionnary = new Dictionary<string, Texture2D>();
            Enums = new Enums();
            splashScreen = new SplashScreen();
            MainMenu = new MainMenu();
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
            Texture2D playerJumpSpriteSheet = Content.Load<Texture2D>("Images\\Reggie_Jump_Small");
            playerSpriteSheets.Add("playerJumpSpriteSheet", playerJumpSpriteSheet);
            Texture2D playerMoveSpriteSheet = Content.Load<Texture2D>("Images\\Reggie_Move_Even_Smaller");
            playerSpriteSheets.Add("playerMoveSpriteSheet", playerMoveSpriteSheet);
            Texture2D playerAttackSpritesheet = Content.Load<Texture2D>("Images\\Reggie_Attack");
            playerSpriteSheets.Add("playerAttackSpriteSheet", playerAttackSpritesheet);

            animManager = new AnimationManager(playerSpriteSheets);
            wormPlayer = new Player(playerMoveSpriteSheet, new Vector2(SpriteSheetSizes.spritesSizes["Reggie_Move_X"]/5, SpriteSheetSizes.spritesSizes["Reggie_Move_Y"] / 5), new Vector2(-2000,500), (int) Enums.ObjectsID.PLAYER);

            enemyList = new List<Enemy>();
            //{
            //    new Enemy(enemySkinTexture, new Vector2(50,50),new Vector2(700,200)),
            //};
            platformList = new List<Platform>();
            gameObjectList = new List<GameObject>();
            interactiveObject = new List<GameObject>();
            //foreach (var enemy in enemyList)
            //    enemy.SetPlayer(wormPlayer);

            background = Content.Load<Texture2D>("Images\\Lvl1_Background");
            Sky_2000_500 = Content.Load<Texture2D>("Images\\Sky_2000x1000");
            Platform_320_64 = Content.Load<Texture2D>("Images\\Platform_320_64");
            texturesDictionnary.Add("Green_320_64", Platform_320_64);
            Transparent_Wall_500x50 = Content.Load<Texture2D>("Images\\Transparent_Wall_500x50");
            texturesDictionnary.Add("Transparent_500x50", Transparent_Wall_500x50);
            Transparent_Wall_1000x50 = Content.Load<Texture2D>("Images\\Transparent_Wall_1000x50");
            texturesDictionnary.Add("Transparent_1000x50", Transparent_Wall_1000x50);
            levelEditorUIBackButton = Content.Load<Texture2D>("Images\\UI\\LvlEdtorSaveButton");
            texturesDictionnary.Add("LevelEditorUIBackButton", levelEditorUIBackButton);
            ClimbinPlant_38_64 = Content.Load<Texture2D>("Images\\WorldObjects\\plantLeaves_1");
            texturesDictionnary.Add("Climbingplant_38x64", ClimbinPlant_38_64);

            levelEditor.loadTextures(Content, ref texturesDictionnary, graphics.GraphicsDevice);
            for(int i = 0; i < texturesDictionnary.Count(); i++)
            Console.WriteLine(texturesDictionnary.ElementAt(i));
            LoadGameObjects();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
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
                  
                    levelEditor.moveOrDeletePlatforms(ref gameObjectList, transformationMatrix);
                        this.IsMouseVisible = true;
                        levelEditor.moveCamera(ref cameraOffset);
                    // Makes player movable in the leveleditor //Enemies are alive but not visible
                    wormPlayer.Update(gameTime, gameObjectsToRender, viewableEnemies, interactiveObject);
                    break;

                case GameState.GAMELOOP:
                    this.IsMouseVisible = false;
                    //switch to LevelEditor
                    if (currentGameState == GameState.GAMELOOP)
                    {
                        cameraOffset = new Vector2(0, 0);
                        if (Keyboard.GetState().IsKeyDown(Keys.L) && !previousState.IsKeyDown(Keys.L))
                            currentGameState = GameState.LEVELEDITOR;
                        previousState = Keyboard.GetState();
                    }

                    gameObjectsToRender = camera.GameObjectsToRender(wormPlayer.gameObjectPosition, gameObjectList, ref interactiveObject);

                    camera.SpawnEnemyOffScreen(wormPlayer, platformList, ref enemyList, enemySkinTexture);
                    viewableEnemies = camera.RenderedEnemies(wormPlayer.gameObjectPosition, enemyList);
                    wormPlayer.Update(gameTime, gameObjectsToRender, viewableEnemies, interactiveObject);

                    foreach (var enemy in enemyList.ToList())
                    {
                        enemy.Update(gameTime, gameObjectList);
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
                    //    enemy.Update(gameTime, gameObjectList);
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
                                //TODO: LEVEL CLASS --> Draw Function
                                //BACKGROUND
                                spriteBatch.Draw(background, new Vector2(0, -1025), null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                                spriteBatch.Draw(background, new Vector2(-4000, -1025), null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                                spriteBatch.Draw(background, new Vector2(-8000, -1025), null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                                spriteBatch.Draw(Sky_2000_500, new Vector2(0, -3025), null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                                spriteBatch.Draw(Sky_2000_500, new Vector2(-4000, -3025), null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                                spriteBatch.Draw(Sky_2000_500, new Vector2(-8000, -3025), null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);

                            //this draws the platforms visible in the viewport
                            foreach (var platformSprite in gameObjectsToRender)
                                if (platformSprite.IsThisAVisibleObject())
                                    platformSprite.DrawSpriteBatch(spriteBatch);

                            //this draws the enemy
                            //spriteBatch.Draw(enemytexture, enemyaggroposition, Color.White);
                            foreach (var enemy in viewableEnemies)
                                enemy.DrawSpriteBatch(spriteBatch);

                            //This draws the player
                            animManager.animation(gameTime, ref wormPlayer, spriteBatch);
                            
                        }
                        break;

                    case GameState.LEVELEDITOR:

                       
                         //TODO: LEVEL CLASS --> Draw Function
                         //BACKGROUND
                            spriteBatch.Draw(background, new Vector2(0, -1025), null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(background, new Vector2(-4000, -1025), null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(background, new Vector2(-8000, -1025), null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(Sky_2000_500, new Vector2(0, -3025), null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(Sky_2000_500, new Vector2(-4000, -3025), null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(Sky_2000_500, new Vector2(-8000, -3025), null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                       
                        //this draws all the platforms in the game
                        foreach (var platformSprite in gameObjectList)
                            platformSprite.DrawSpriteBatch(spriteBatch);
                        levelEditor.DrawLvlEditorUI(texturesDictionnary, spriteBatch, transformationMatrix, ref gameObjectList, GraphicsDevice);

                        //This draws the player
                        animManager.animation(gameTime, ref wormPlayer, spriteBatch);

                        //Writes Leveleditor Text when Level Editor is enabled
                        string lvlEditorString = "Level Editor Enabled!";
                        spriteBatch.DrawString(font, lvlEditorString, new Vector2(wormPlayer.gameObjectPosition.X - 620, wormPlayer.gameObjectPosition.Y - 470), Color.DarkRed);
                        break;
                }

                //Comment: SEE Framecounter.cs for additional commentary
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                frameCounter.Update(deltaTime);
                var fps = string.Format("FPS: {0}", frameCounter.averageFramesPerSecond);
                spriteBatch.DrawString(font, fps, new Vector2(wormPlayer.gameObjectPosition.X - 620, wormPlayer.gameObjectPosition.Y - 490), Color.Black);
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
                    gameObjectList.Add(new Platform(texturesDictionnary["Green_320_64"], new Vector2(320, 64), new Vector2(Int32.Parse(dataSeperated[i+1]), Int32.Parse(dataSeperated[i+2])), (int)Enums.ObjectsID.PLATFORM,(int)Enums.ObjectsID.GREEN_PLATFORM_320_64, true));
                }
                if (dataSeperated[i] == Enums.ObjectsID.INVISIBLE_WALL_500x50.ToString())
                {
                    gameObjectList.Add(new Platform(texturesDictionnary["Transparent_500x50"], new Vector2(500, 50), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM,(int)Enums.ObjectsID.INVISIBLE_WALL_500x50, true));
                    gameObjectList.Last().DontDrawThisObject();
                }
                if (dataSeperated[i] == Enums.ObjectsID.INVSIBLE_WALL_1000x50.ToString())
                {
                    gameObjectList.Add(new Platform(texturesDictionnary["Transparent_1000x50"], new Vector2(1000, 50),  new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.INVSIBLE_WALL_1000x50, true));
                    gameObjectList.Last().DontDrawThisObject();
                }
                if(dataSeperated[i] == Enums.ObjectsID.VINE.ToString())
                {
                    gameObjectList.Add(new Platform(texturesDictionnary["Climbingplant_38x64"], new Vector2(38, 88), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.VINE,(int)Enums.ObjectsID.VINE, false));
                }
                if (dataSeperated[i].All(char.IsDigit) && i % 3 == 0)
                {
                    if (Int32.Parse(dataSeperated[i]) == (int)Enums.ObjectsID.tileBrown_01)
                    {
                        Console.WriteLine("brown tile added");
                        gameObjectList.Add(new Platform(texturesDictionnary["tileBrown_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.tileBrown_01, (int)Enums.ObjectsID.VINE, false));
                    }
                }
            }
        }

        private void FillLists()
        {
            for(int i = 0; i <gameObjectList.Count; i++)
            {
                if (gameObjectList[i].objectID == (int)Enums.ObjectsID.PLATFORM)
                    platformList.Add((Platform)gameObjectList[i]);
                else if (gameObjectList[i].objectID == (int)Enums.ObjectsID.VINE)
                    interactiveObject.Add((Platform)gameObjectList[i]);
            }
        }



        
    }
}
