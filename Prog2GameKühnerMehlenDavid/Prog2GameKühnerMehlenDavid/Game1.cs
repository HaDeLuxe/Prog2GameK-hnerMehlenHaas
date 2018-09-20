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
        
        List<Platform> enemySpawnList;
        List<Enemy> enemyList;
        List<Enemy> viewableEnemies;
        List<GameObject> gameObjectsToRender;
        List<GameObject> interactiveObject;
        List<GameObject> cornnencyList;

        Dictionary<string, Texture2D> texturesDictionary;
        public static Dictionary<string, Song> songDictionnary;
        public static Dictionary<string, SoundEffect> soundEffectDictionnary;
        Dictionary<String, Texture2D> playerSpriteSheets; 
        Dictionary<String, Texture2D> enemySpriteSheets;
        

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
        IngameMenus ingameMenus;


        

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



        //MUSIC
        public bool turnOnMusic;
        AudioManager audioManager;




        public Game1()
        {
            currentGameState = GameState.SPLASHSCREEN;
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1080 - 40 - 20;
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
            texturesDictionary = new Dictionary<string, Texture2D>();
            enemyList = new List<Enemy>();
            Enums = new Enums();
            splashScreen = new SplashScreen();
            mainMenu = new MainMenu();
            gameMenu = new GameMenu();
            gameManager = new ItemUIManager();
            minimap = new Minimap();
            loadAndSave = new LoadAndSave(allGameObjectList, texturesDictionary);

            //MUSIC
            turnOnMusic = true;
            //must be the first instance!
            audioManager = AudioManager.AudioManagerInstance();

            //SHOP
           

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


            LoadAndSave loading = new LoadAndSave(allGameObjectList, texturesDictionary);
            //MUSIC
            audioManager.LoadSongsAndSound(this.Content);
            loading.loadEverything(this.Content, ref playerSpriteSheets, ref texturesDictionary, ref enemySpriteSheets);

            levelEditor.loadTextures(Content, ref texturesDictionary, graphics.GraphicsDevice);

            animManager = new AnimationManager(playerSpriteSheets);
            wormPlayer = new Player(playerSpriteSheets["playerMoveSpriteSheet"], new Vector2(SpriteSheetSizes.spritesSizes["Reggie_Move_X"] / 5, SpriteSheetSizes.spritesSizes["Reggie_Move_Y"] / 5), new Vector2(13444, 1500), (int)Enums.ObjectsID.PLAYER);
            //shopKeeper = new ShopKeeper();


            enemySpawnList = new List<Platform>();
            allGameObjectList = new List<GameObject>();
            interactiveObject = new List<GameObject>();
            cornnencyList = new List<GameObject>();
            levelObjectList = new List<GameObject>();



            loadAndSave.LoadGameObjects(ref allGameObjectList, ref wormPlayer);

            
            levelManager = new Levels(ref wormPlayer.gameObjectPosition, ref levelObjectList, ref allGameObjectList);
            levelManager.sortGameObjects();
            
            loadAndSave = new LoadAndSave(allGameObjectList, texturesDictionary);
            ingameMenus = new IngameMenus(spriteBatch, texturesDictionary, playerSpriteSheets);
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

            //MUSIC Updating Timer
            audioManager.UpdateAudioManagerTimer(gameTime);

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
                    if (MediaPlayer.State != 0)
                    {
                        MediaPlayer.Stop();
                        turnOnMusic = true;
                    }
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
                    wormPlayer.Update(gameTime, gameObjectsToRender, viewableEnemies, interactiveObject, ref allGameObjectList, loadAndSave, ingameMenus, levelManager, ref allGameObjectList);
                    break;

                case GameState.GAMELOOP:

                    this.IsMouseVisible = false;

                    //MUSIC
                    if (turnOnMusic)
                    {
                        audioManager.Play("IngameMusic");
                        turnOnMusic = false;
                    }
                   
                    //switch to LevelEditor
                    levelManager.ManageLevels(wormPlayer.gameObjectPosition);
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

                    camera.SpawnEnemyOffScreen(wormPlayer, enemySpawnList, ref enemyList, enemySpriteSheets, levelManager.PlayerLevelLocation());
                    viewableEnemies = camera.RenderedEnemies(wormPlayer.gameObjectPosition, enemyList);
                    wormPlayer.Update(gameTime, gameObjectsToRender, viewableEnemies, interactiveObject, ref levelObjectList, loadAndSave, ingameMenus, levelManager, ref allGameObjectList);

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
                        mainMenu.RenderMainMenu(texturesDictionary, spriteBatch, font, levelManager, loadAndSave, ref allGameObjectList, ref wormPlayer, this);
                        break;
                    case GameState.MINIMAP:
                        //it is intended that the break is missing, so that while the minimap is opened, the background of the gameloop is still drawn but no gameplay updates are done.
                    case GameState.GAMELOOP:


                        if (lastGameState == currentGameState)
                        {
                            levelManager.drawLevelsBackground(spriteBatch, texturesDictionary);


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

                            //----Draw Hitbox----//
                            //playertexture = new Texture2D(this.GraphicsDevice, (int)(wormPlayer.collisionBoxSize.X), (int)(wormPlayer.collisionBoxSize.Y));
                            //playercolorData = new Color[(int)((wormPlayer.collisionBoxSize.X) * (wormPlayer.collisionBoxSize.Y))];
                            //for (int i = 0; i < (wormPlayer.collisionBoxSize.X) * (wormPlayer.collisionBoxSize.Y); i++)
                            //    playercolorData[i] = Color.Black;
                            //playertexture.SetData<Color>(playercolorData);
                            //playeraggroposition = new Vector2(wormPlayer.collisionBoxPosition.X, wormPlayer.collisionBoxPosition.Y);
                            //spriteBatch.Draw(playertexture, playeraggroposition, Color.Black);
                            //----End Draw Hitbox----//

                            if (wormPlayer.invincibilityFrames || (wormPlayer.invincibilityTimer>0 && wormPlayer.invincibilityTimer<0.25f) || (wormPlayer.invincibilityTimer > 0.5 && wormPlayer.invincibilityTimer < 0.75f) || (wormPlayer.invincibilityTimer > 1 && wormPlayer.invincibilityTimer < 1.25f) || (wormPlayer.invincibilityTimer > 1.5 && wormPlayer.invincibilityTimer < 1.75f) )
                                animManager.animation(gameTime, ref wormPlayer, spriteBatch);
                            else
                            animManager.animation(gameTime, ref wormPlayer, spriteBatch);

                            wormPlayer.drawUpdate(levelObjectList, ref ingameMenus);

                            //This draws the UI
                            gameManager.drawUI(texturesDictionary,spriteBatch,transformationMatrix,GraphicsDevice, wormPlayer.PlayersCurrentHP(), font);
                            if(levelManager.currentLevel != Enums.Level.TUTORIAL)
                                minimap.drawMinimap(transformationMatrix,spriteBatch,wormPlayer.gameObjectPosition, ref texturesDictionary, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                        }
                        ingameMenus.drawUpdate(gameTime, transformationMatrix, ref wormPlayer);

                        break;

                    case GameState.LEVELEDITOR:
                        levelManager.drawLevelsBackground(spriteBatch, texturesDictionary);
                        
                        Vector2 mouse = Vector2.Transform(new Vector2(-Mouse.GetState().Position.X, -Mouse.GetState().Position.Y), transformationMatrix);
                        string mouseCoordinates = "(x: " + (-mouse.X) + ", y: " + (-mouse.Y) + ")";
                        spriteBatch.DrawString(font, mouseCoordinates, Vector2.Transform(new Vector2(Mouse.GetState().Position.X+10, Mouse.GetState().Position.Y-15), Matrix.Invert(transformationMatrix)), Color.Black);
                        
                        //this draws all the platforms in the game
                        foreach (var platformSprite in allGameObjectList)
                            platformSprite.DrawSpriteBatch(spriteBatch);
                        levelEditor.DrawLvlEditorUI(texturesDictionary, spriteBatch, transformationMatrix, ref allGameObjectList,ref levelObjectList, GraphicsDevice, ref loadAndSave, ref levelManager);
                        //This draws the player
                        animManager.animation(gameTime, ref wormPlayer, spriteBatch);

                        //Writes Leveleditor Text when Level Editor is enabled
                        string lvlEditorString = "Level Editor Enabled!";
                        spriteBatch.DrawString(font, lvlEditorString, Vector2.Transform(new Vector2(10, 30), Matrix.Invert(transformationMatrix)), Color.DarkRed);
                        break;
                    case GameState.GAMEMENU:
                        gameMenu.DrawGameMenu(texturesDictionary, spriteBatch, font);
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

        



        private void FillLists()
        {
            for (int i = 0; i < allGameObjectList.Count(); i++)
            {
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.VINE)
                    interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.SNAILSHELL)
                    interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.SCISSORS)
                    interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.ARMOR)
                    interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.SHOVEL)
                    interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.HEALTHPOTION)
                    interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.GOLDENUMBRELLA)
                    interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.JUMPPOTION)
                    interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.POWERPOTION)
                    interactiveObject.Add(allGameObjectList[i]);
                if (allGameObjectList[i].objectID == (int)Enums.ObjectsID.CORNNENCY)
                    cornnencyList.Add(allGameObjectList[i]);



                foreach (Platform platform in allGameObjectList.Cast<GameObject>().OfType<Platform>())
                {
                    if (platform.PlatformType == (int)Enums.ObjectsID.ENEMYSPAWNPOINT)
                        enemySpawnList.Add(platform);
                    if (platform.PlatformType == (int)Enums.ObjectsID.SPIDERWEB)
                        interactiveObject.Add(platform);
                }
            }
        }

        public void NewGame()
        {
            loadAndSave.LoadGameObjectsNewGame(ref allGameObjectList, ref wormPlayer);
            levelManager.sortGameObjects();
            FillLists();
        }

    }
}
