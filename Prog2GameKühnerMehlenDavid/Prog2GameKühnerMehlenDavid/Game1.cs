using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Media; //AUDIOSTUFF //SONGS
using Microsoft.Xna.Framework.Audio; //Sounds
using Reggie.Menus;
using Reggie.Animations;
using Reggie.Enemies;

namespace Reggie
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        public enum GameState { MAINMENU, GAMELOOP, LEVELEDITOR, CREDITS, SPLASHSCREEN, LOADSCREEN, WINSCREEN, LOSESCREEN, MINIMAP, GAMEMENU}
        public static GameState currentGameState {get;set; }
        //forces an update before draw action
        public GameState lastGameState;

        public static Player wormPlayer;
        public Enemy ant;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<GameObject> allGameObjectList;
        List<GameObject> levelObjectList;
        
        List<Platform> platformList;
        List<Enemy> enemyList;
        List<Enemy> viewableEnemies;
        List<GameObject> gameObjectsToRender;
        List<GameObject> interactiveObject;

        Dictionary<string, Texture2D> texturesDictionnary;
        public static Dictionary<string, Song> songDictionnary;
        Dictionary<string, SoundEffect> soundEffectDictionnary;
        Dictionary<String, Texture2D> playerSpriteSheets; 
        Dictionary<String, Texture2D> enemySpriteSheets;
        
        Texture2D enemySkinTexture;

        private float timeUntilNextFrame1 = 0;
        private float timeUntilNextFrame2 = 0;

        AnimationManager animManager;
        LevelEditor levelEditor;
        SpriteSheetSizes input = new SpriteSheetSizes();
        FrameCounter frameCounter = new FrameCounter();
        SpriteFont font;
        Camera camera = new Camera();
        ItemUIManager gameManager;
        Levels levelManager;
        Minimap minimap;
        LoadAndSave loadAndSave;


        

        //for switching LevelEditor
        public static KeyboardState previousState;

        SplashScreen splashScreen;
        MainMenu mainMenu;
        GameMenu gameMenu;
        
        Enums Enums;

        Matrix transformationMatrix;

        public static Vector2 cameraOffset;



        //check dumb stuff
        public Texture2D enemytexture;
        public Color[] colordata;
        public Vector2 enemyaggroposition;

        public Color[] playercolorData;
        public Texture2D playertexture;
        public Vector2 playeraggroposition;




        public Game1()
        {
            currentGameState = GameState.SPLASHSCREEN;
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1080-40-20;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.ApplyChanges();
            Window.AllowUserResizing = true;
            input.ReadImageSizeDataSheet();
            playerSpriteSheets = new Dictionary<string, Texture2D>();
            enemySpriteSheets = new Dictionary<string, Texture2D>();
            songDictionnary = new Dictionary<string, Song>();
            soundEffectDictionnary = new Dictionary<string, SoundEffect>();
            levelEditor = new LevelEditor();
            cameraOffset = new Vector2(0, 0);
            texturesDictionnary = new Dictionary<string, Texture2D>();
            enemyList = new List<Enemy>();
            Enums = new Enums();
            splashScreen = new SplashScreen();
            mainMenu = new MainMenu();
            gameMenu = new GameMenu();
            gameManager = new ItemUIManager();
            minimap = new Minimap();
            loadAndSave = new LoadAndSave(allGameObjectList, texturesDictionnary);
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
            font = Content.Load<SpriteFont>("Fonts/LuckiestGuy");
            enemySkinTexture = Content.Load<Texture2D>("Images\\door");

            
            LoadAndSave loading = new LoadAndSave(allGameObjectList, texturesDictionnary);
            loading.loadEverything(this.Content, ref playerSpriteSheets, ref texturesDictionnary, ref enemySpriteSheets, ref songDictionnary, ref soundEffectDictionnary);
            levelEditor.loadTextures(Content, ref texturesDictionnary, graphics.GraphicsDevice);

            animManager = new AnimationManager(playerSpriteSheets);
            wormPlayer = new Player(playerSpriteSheets["playerMoveSpriteSheet"], new Vector2(SpriteSheetSizes.spritesSizes["Reggie_Move_X"]/5, SpriteSheetSizes.spritesSizes["Reggie_Move_Y"] / 5), new Vector2(13444, 1500), (int) Enums.ObjectsID.PLAYER);

           
           
            platformList = new List<Platform>();
            allGameObjectList = new List<GameObject>();
            interactiveObject = new List<GameObject>();
            
            
            LoadGameObjects();

            allGameObjectList.Add(new Item(texturesDictionnary["SnailShell"], new Vector2(64, 64), new Vector2(13000, 1600), (int)Enums.ObjectsID.SNAILSHELL));
            allGameObjectList.Add(new Item(texturesDictionnary["Armor_64x64"], new Vector2(64, 64), new Vector2(12800, 1600), (int)Enums.ObjectsID.ARMOR));
            allGameObjectList.Add(new Item(texturesDictionnary["Shovel_64x64"], new Vector2(64, 64), new Vector2(12500, 1600), (int)Enums.ObjectsID.SHOVEL));
            allGameObjectList.Add(new Item(texturesDictionnary["Scissors_64x64"], new Vector2(64, 64), new Vector2(12400, 1600), (int)Enums.ObjectsID.SCISSORS));
            allGameObjectList.Add(new Item(texturesDictionnary["HealthItem"], new Vector2(64, 64), new Vector2(12300, 1600), (int)Enums.ObjectsID.HEALTHPOTION));
            allGameObjectList.Add(new Item(texturesDictionnary["PowerPotion"], new Vector2(64, 64), new Vector2(12200, 1600), (int)Enums.ObjectsID.POWERPOTION));
            allGameObjectList.Add(new Item(texturesDictionnary["JumpPotion"], new Vector2(64, 64), new Vector2(12100, 1600), (int)Enums.ObjectsID.JUMPPOTION));
            allGameObjectList.Add(new Item(texturesDictionnary["GoldenUmbrella"], new Vector2(100, 29), new Vector2(11900, 1600), (int)Enums.ObjectsID.GOLDENUMBRELLA));
            allGameObjectList.Add(new Platform(texturesDictionnary["VineDoor"], new Vector2(64, 64), new Vector2(11800, 1600), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.VINEDOOR, false));
            allGameObjectList.Add(new Platform(texturesDictionnary["Spiderweb_64x64"], new Vector2(64, 64), new Vector2(11700, 1600), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.SPIDERWEB, false));


            levelObjectList = new List<GameObject>();
            foreach (GameObject gameObject in allGameObjectList) levelObjectList.Add(gameObject);
            levelManager = new Levels(wormPlayer.gameObjectPosition, ref levelObjectList, ref allGameObjectList);
            levelManager.sortGameObjects();

           
            FillLists();
            
            loadAndSave = new LoadAndSave(allGameObjectList, texturesDictionnary);
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

            //Update Timer
            float animationFrameTime = 0.02f;

            float gameFrameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeUntilNextFrame1 -= gameFrameTime;
            timeUntilNextFrame2 -= gameFrameTime;

            switch (currentGameState)
            {
                case GameState.SPLASHSCREEN:
                    splashScreen.ClickedButton();
                   break;
                case GameState.MAINMENU:
                    mainMenu.Update(this);
                    this.IsMouseVisible = true;
                    break;
                case GameState.LEVELEDITOR:

                    levelEditor.HandleLevelEditorEvents();
                  
                    levelEditor.moveOrDeletePlatforms(ref allGameObjectList, transformationMatrix);
                        this.IsMouseVisible = true;
                        levelEditor.moveCamera(ref cameraOffset);
                    // Makes player movable in the leveleditor //Enemies are alive but not visible
                    gameObjectsToRender = camera.GameObjectsToRender(wormPlayer.gameObjectPosition, allGameObjectList, ref interactiveObject);
                    wormPlayer.Update(gameTime, gameObjectsToRender, viewableEnemies, interactiveObject, ref allGameObjectList);
                    break;

                case GameState.GAMELOOP:
                    this.IsMouseVisible = false;
                    //switch to LevelEditor
                    levelManager.ManageLevels();
                    gameManager.ManageItems(ref wormPlayer, ref levelObjectList);

                    if (currentGameState == GameState.GAMELOOP)
                    {
                        //cameraOffset = new Vector2(0, 0);
                        if (Keyboard.GetState().IsKeyDown(Keys.L) && !previousState.IsKeyDown(Keys.L))
                            currentGameState = GameState.LEVELEDITOR;
                        //previousState = Keyboard.GetState();
                        if (Keyboard.GetState().IsKeyDown(Keys.M) && !previousState.IsKeyDown(Keys.M) && levelManager.currentLevel != Enums.Level.TUTORIAL)
                            currentGameState = GameState.MINIMAP;
                        if (Keyboard.GetState().IsKeyDown(Keys.P) && !previousState.IsKeyDown(Keys.P))
                            currentGameState = GameState.GAMEMENU;
                        previousState = Keyboard.GetState();
                    }


                    if(timeUntilNextFrame1 <= 0)
                    {
                        gameObjectsToRender = camera.GameObjectsToRender(wormPlayer.gameObjectPosition, levelObjectList, ref interactiveObject);
                        timeUntilNextFrame1 += animationFrameTime;

                    }

                    camera.SpawnEnemyOffScreen(wormPlayer, platformList, ref enemyList, enemySkinTexture, enemySpriteSheets, levelManager.PlayerLevelLocation());
                    viewableEnemies = camera.RenderedEnemies(wormPlayer.gameObjectPosition, enemyList);
                    wormPlayer.Update(gameTime, gameObjectsToRender, viewableEnemies, interactiveObject, ref levelObjectList);

                    if(timeUntilNextFrame2 <= 0)
                    {
                        foreach (var enemy in enemyList.ToList())
                        {
                            enemy.Update(gameTime, levelObjectList);
                            if (enemy.EnemyAliveState() == false || enemy.fallOutOfMap)
                                enemyList.RemoveAt(enemyList.IndexOf(enemy));
                        }
                        timeUntilNextFrame2 += animationFrameTime;
                    }
                    break;

                case GameState.MINIMAP:
                    if (Keyboard.GetState().IsKeyDown(Keys.M) && !previousState.IsKeyDown(Keys.M))
                        currentGameState = GameState.GAMELOOP;
                    previousState = Keyboard.GetState();
                    break;
                case GameState.GAMEMENU:
                    gameMenu.Update(this, loadAndSave);
                    //if (Keyboard.GetState().IsKeyDown(Keys.P) && !previousState.IsKeyDown(Keys.P))
                    //    currentGameState = GameState.GAMELOOP;
                    //previousState = Keyboard.GetState();
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
            Vector2 screenCenter = new Vector2(viewport.Width / 2-(SpriteSheetSizes.spritesSizes["Reggie_Move_X"]/5) + cameraOffset.X, viewport.Height / 2-(SpriteSheetSizes.spritesSizes["Reggie_Move_Y"]/5)+50 + cameraOffset.Y);
            camera.setCameraWorldPosition(wormPlayer.gameObjectPosition);
            transformationMatrix = camera.cameraTransformationMatrix(viewport, screenCenter);

            if(currentGameState == GameState.SPLASHSCREEN || currentGameState == GameState.MAINMENU || currentGameState == GameState.GAMEMENU)
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
                        mainMenu.RenderMainMenu(texturesDictionnary, spriteBatch, font, levelManager);
                        break;
                    case GameState.MINIMAP:
                        //it is intended that the break is missing, so that while the minimap is opened, the background of the gameloop is still drawn but no gameplay updates are done.
                    case GameState.GAMELOOP:

                        if(lastGameState == currentGameState)
                        {
                            levelManager.drawLevelsBackground(spriteBatch, texturesDictionnary);


                            //this draws the platforms visible in the viewport
                            foreach (var platformSprite in gameObjectsToRender)
                                if (platformSprite.IsThisAVisibleObject())
                                    platformSprite.DrawSpriteBatch(spriteBatch);

                            if (levelManager.currentLevel == Enums.Level.TUTORIAL)
                            {
                                for (int i = 0; i < enemyList.Count(); i++)
                                {
                                    //enemytexture = new Texture2D(this.GraphicsDevice, (int)(enemyList[i].enemyAggroAreaSize.Z), (int)(enemyList[i].enemyAggroAreaSize.W));
                                    //colordata = new Color[(int)((enemyList[i].enemyAggroAreaSize.W) * (enemyList[i].enemyAggroAreaSize.Z))];
                                    //for (int j = 0; j < (enemyList[i].enemyAggroAreaSize.W) * (enemyList[i].enemyAggroAreaSize.Z); j++)
                                    //    colordata[j] = Color.White;
                                    //enemytexture.SetData<Color>(colordata);
                                    //enemyaggroposition = new Vector2(enemyList[i].enemyAggroArea.X, enemyList[i].enemyAggroArea.Y);
                                    //spriteBatch.Draw(enemytexture, enemyaggroposition, Color.White);
                                    enemyList[i].EnemyAnimationUpdate(gameTime, spriteBatch);
                                }
                            }

                            //This draws the player
                            //playertexture = new Texture2D(this.GraphicsDevice, (int)(wormPlayer.collisionBoxSize.X), (int)(wormPlayer.collisionBoxSize.Y));
                            //playercolorData = new Color[(int)((wormPlayer.collisionBoxSize.X) * (wormPlayer.collisionBoxSize.Y))];
                            //for (int i = 0; i < (wormPlayer.collisionBoxSize.X) * (wormPlayer.collisionBoxSize.Y); i++)
                            //    playercolorData[i] = Color.Black;
                            //playertexture.SetData<Color>(playercolorData);
                            //playeraggroposition = new Vector2(wormPlayer.collisionBoxPosition.X, wormPlayer.collisionBoxPosition.Y);
                            //spriteBatch.Draw(playertexture, playeraggroposition, Color.Black);
                            animManager.animation(gameTime, ref wormPlayer, spriteBatch);

                            //This draws the UI
                            gameManager.drawUI(texturesDictionnary,spriteBatch,transformationMatrix,GraphicsDevice, wormPlayer.PlayersCurrentHP());
                            if(levelManager.currentLevel != Enums.Level.TUTORIAL)
                                minimap.drawMinimap(transformationMatrix,spriteBatch,wormPlayer.gameObjectPosition, ref texturesDictionnary, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                        }
                        
                        break;

                    case GameState.LEVELEDITOR:
                        levelManager.drawLevelsBackground(spriteBatch, texturesDictionnary);
                        
                        Vector2 mouse = Vector2.Transform(new Vector2(-Mouse.GetState().Position.X, -Mouse.GetState().Position.Y), transformationMatrix);
                        string mouseCoordinates = "(x: " + (-mouse.X) + ", y: " + (-mouse.Y) + ")";
                        spriteBatch.DrawString(font, mouseCoordinates, Vector2.Transform(new Vector2(Mouse.GetState().Position.X+10, Mouse.GetState().Position.Y-15), Matrix.Invert(transformationMatrix)), Color.Black);
                        
                        //this draws all the platforms in the game
                        foreach (var platformSprite in allGameObjectList)
                            platformSprite.DrawSpriteBatch(spriteBatch);
                        levelEditor.DrawLvlEditorUI(texturesDictionnary, spriteBatch, transformationMatrix, ref allGameObjectList,ref levelObjectList, GraphicsDevice, ref loadAndSave, ref levelManager);
                        //This draws the player
                        animManager.animation(gameTime, ref wormPlayer, spriteBatch);

                        //Writes Leveleditor Text when Level Editor is enabled
                        string lvlEditorString = "Level Editor Enabled!";
                        spriteBatch.DrawString(font, lvlEditorString, Vector2.Transform(new Vector2(10, 30), Matrix.Invert(transformationMatrix)), Color.DarkRed);
                        break;
                    case GameState.GAMEMENU:
                        gameMenu.DrawGameMenu(texturesDictionnary, spriteBatch, font);
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
                
                if (dataSeperated[i] == Enums.ObjectsID.INVISIBLE_WALL_500x50.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionnary["Transparent_500x50"], new Vector2(512, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM,(int)Enums.ObjectsID.INVISIBLE_WALL_500x50, true));
                    allGameObjectList.Last().DontDrawThisObject();
                }
                if (dataSeperated[i] == Enums.ObjectsID.INVSIBLE_WALL_1000x50.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionnary["Transparent_1000x50"], new Vector2(1024, 64),  new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.INVSIBLE_WALL_1000x50, true));
                    allGameObjectList.Last().DontDrawThisObject();
                }
                if(dataSeperated[i] == Enums.ObjectsID.VINE.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionnary["Climbingplant_38x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.VINE,(int)Enums.ObjectsID.VINE, false));
                }
                if (dataSeperated[i] == Enums.ObjectsID.INVISIBLE_WALL_64x64.ToString())
                {
                    allGameObjectList.Add(new Platform(texturesDictionnary["Transparent_64x64"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.INVISIBLE_WALL_64x64, false));
                    allGameObjectList.Last().DontDrawThisObject();
                }
                if (dataSeperated[i] == Enums.ObjectsID.APPLE.ToString())
                {
                    allGameObjectList.Add(new Item(texturesDictionnary["Apple"], new Vector2(128, 128), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.APPLE));
                }
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_01.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_02.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_02, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_03.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_03, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_04.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_04, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_05.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_05, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_06.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_06, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_07.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_07, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_08.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_08, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_09.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_09, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_10.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_11.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_12.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_13.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_14.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_15.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_16.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_17.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_17, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_18.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_18, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_19.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_20.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_21.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_22.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_23.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_24.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_24, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_25.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_25, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_26.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_26, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBrown_27.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBrown_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBrown_27, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_01.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_02.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_02, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_03.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_03, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_04.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_04, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_05.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_05, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_06.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_06, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_07.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_07, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_08.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_08, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_09.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_09, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_10.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_11.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_12.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_13.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_14.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_15.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_16.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_17.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_17, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_18.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_18, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_19.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_20.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_21.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_22.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_23.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_24.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_24, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_25.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_25, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_26.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_26, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileYellow_27.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileYellow_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileYellow_27, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_01.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_02.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_02, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_03.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_03, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_04.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_04, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_05.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_05, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_06.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_06, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_07.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_07, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_08.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_08, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_09.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_09, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_10.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_11.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_12.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_13.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_14.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_15.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_16.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_17.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_17, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_18.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_18, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_19.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_20.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_21.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_22.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_23.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_24.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_24, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_25.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_25, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_26.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_26, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileBlue_27.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileBlue_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileBlue_27, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_01.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_01"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_01, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_02.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_02"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_02, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_03.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_03"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_03, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_04.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_04"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_04, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_05.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_05"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_05, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_06.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_06"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_06, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_07.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_07"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_07, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_08.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_08"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_08, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_09.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_09"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_09, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_10.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_10"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_10, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_11.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_11"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_11, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_12.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_12"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_12, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_13.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_13"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_13, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_14.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_14"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_14, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_15.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_15"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_15, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_16.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_16"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_16, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_17.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_17"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_17, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_18.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_18"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_18, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_19.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_19"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_19, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_20.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_20"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_20, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_21.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_21"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_21, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_22.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_22"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_22, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_23.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_23"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_23, false));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_24.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_24"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_24, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_25.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_25"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_25, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_26.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_26"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_26, true));
                if (dataSeperated[i] == Enums.ObjectsID.tileGreen_27.ToString())
                    allGameObjectList.Add(new Platform(texturesDictionnary["tileGreen_27"], new Vector2(64, 64), new Vector2(Int32.Parse(dataSeperated[i + 1]), Int32.Parse(dataSeperated[i + 2])), (int)Enums.ObjectsID.PLATFORM, (int)Enums.ObjectsID.tileGreen_27, true));
                if(dataSeperated[i] == "Playerposition")
                {
                    wormPlayer.gameObjectPosition.X = float.Parse(dataSeperated[i + 1]);
                    wormPlayer.gameObjectPosition.Y = float.Parse(dataSeperated[i + 2]);
                }
                if (dataSeperated[i] == "Armor") ItemUIManager.armorPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "Helmet") ItemUIManager.snailShellPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "HealthPotion") ItemUIManager.healthPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "JumpPotion") ItemUIManager.jumpPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "PowerPotion") ItemUIManager.powerPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "Scissors") ItemUIManager.scissorsPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "Shovel") ItemUIManager.shovelPickedUp = bool.Parse(dataSeperated[i + 1]);
                if (dataSeperated[i] == "GoldenUmbrella") ItemUIManager.goldenUmbrellaPickedUp = bool.Parse(dataSeperated[i + 1]);
            }
        }



        private void FillLists()
        {
            for(int i = 0; i < allGameObjectList.Count(); i++)
            {
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.PLATFORM)
                    platformList.Add((Platform)allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.VINE)
                    interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.SNAILSHELL) interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.SCISSORS) interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.ARMOR) interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.SHOVEL) interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.HEALTHPOTION)
                    interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA) interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.JUMPPOTION) interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.POWERPOTION) interactiveObject.Add(allGameObjectList[i]);


                foreach (Platform platform in allGameObjectList.Cast<GameObject>().OfType<Platform>())
                {
                    if (platform.PlatformType == (int)Enums.ObjectsID.SPIDERWEB)
                        interactiveObject.Add(platform);
                }
            }
        }


    }
}
