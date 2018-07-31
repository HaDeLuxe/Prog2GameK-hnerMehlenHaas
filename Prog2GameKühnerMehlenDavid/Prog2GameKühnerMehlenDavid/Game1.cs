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
        public static GameState CurrentGameState {get;set; }

        public Player WormPlayer;
        public Enemy Ant;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<GameObject> SpriteList;
        List<Enemy> EnemyList;
        List<Enemy> ViewableEnemies;
        List<GameObject> gameObjectsToRender;
        Dictionary<string, Texture2D> platformTextures;

        Texture2D enemytexture;
        Texture2D EnemyTexture;
        Texture2D background;
        Texture2D Platform_320_64;

        StateMachine StateMachine;
        AnimationManager animManager;
        LevelEditor levelEditor;
        SpriteSheetSizes input = new SpriteSheetSizes();
        FrameCounter _frameCounter = new FrameCounter();
        SpriteFont font;
        Camera camera = new Camera();
        Color[] colorData;
        Vector2 enemyaggroposition;

        bool LevelEditorActivated = false;
        Matrix TransformationMatrix;

        private Vector2 cameraOffset;




        Dictionary<String, Texture2D> playerSpriteSheets;

        public Game1() {
            StateMachine = new StateMachine();
            CurrentGameState = GameState.GAMELOOP;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1800;
            graphics.ApplyChanges();
            input.ReadImageSizeDataSheet();
            playerSpriteSheets = new Dictionary<string, Texture2D>();
            levelEditor = new LevelEditor();
            cameraOffset = new Vector2(0, 0);
            platformTextures = new Dictionary<string, Texture2D>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

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
            EnemyTexture = Content.Load<Texture2D>("Images\\door");
            Texture2D PlatformTexture = Content.Load<Texture2D>("Images\\floor");
            Texture2D PlayerJumpSpriteSheet = Content.Load<Texture2D>("Images\\Reggie_Jump_Small");
            playerSpriteSheets.Add("playerJumpSpriteSheet", PlayerJumpSpriteSheet);
            Texture2D PlayerMoveSpriteSheet = Content.Load<Texture2D>("Images\\Reggie_Move_Even_Smaller");
            playerSpriteSheets.Add("playerMoveSpriteSheet", PlayerMoveSpriteSheet);

            animManager = new AnimationManager(playerSpriteSheets);
            WormPlayer = new Player(PlayerMoveSpriteSheet, new Vector2(SpriteSheetSizes.SpritesSizes["Reggie_Move_X"]/5, SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"] / 5), new Vector2(0,0));
            
            EnemyList = new List<Enemy>()
            {
                new Enemy(EnemyTexture, new Vector2(50,50),new Vector2(700,200)),
            };
            SpriteList = new List<GameObject>()
            {
                new Platform(PlatformTexture, new Vector2(1800,100), new Vector2(0,900)),
                
                new Platform(PlatformTexture, new Vector2(1800,100), new Vector2(-500,1100)),
      

                //new Enemy(EnemyTexture, new Vector2(50, 50)),
            };
            foreach (var enemy in EnemyList)
                enemy.SetPlayer(WormPlayer);

            background = Content.Load<Texture2D>("Images\\Lvl1_Background");
            Platform_320_64 = Content.Load<Texture2D>("Images\\Platform_320_64");
            platformTextures.Add("Green_320_64", Platform_320_64);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Manage Game States:
            StateMachine.ManageGamestates();

            gameObjectsToRender = camera.objectsToRender(WormPlayer.Position, SpriteList);
            ViewableEnemies = camera.RenderedEnemies(WormPlayer.Position, EnemyList);
            //Ant.Update(gameTime, SpriteList);
            WormPlayer.Update(gameTime, gameObjectsToRender,ViewableEnemies);
            enemytexture = new Texture2D(this.GraphicsDevice, (int)(WormPlayer.CollisionBoxSize.X), (int)(WormPlayer.CollisionBoxSize.Y));
            colorData = new Color[(int)((WormPlayer.CollisionBoxSize.X) * (WormPlayer.CollisionBoxSize.Y))];
            for (int i = 0; i < (WormPlayer.CollisionBoxSize.X) * (WormPlayer.CollisionBoxSize.Y); i++)
                colorData[i] = Color.White;
            enemytexture.SetData<Color>(colorData);
            enemyaggroposition = new Vector2(WormPlayer.CollisionRectangle.X, WormPlayer.CollisionRectangle.Y);
            int enemycounter = 0;
            //if (EnemyList.Count != 0)
                foreach (var enemy in EnemyList.ToList())
                {
                    enemy.Update(gameTime, SpriteList);
                    if (enemy.EnemyAliveState() == false || enemy.FallOutOfMap)
                        EnemyList.RemoveAt(enemycounter);
                    if (!EnemyList.Any())
                    {
                    Random rand = new Random();
                    int randomizedNumber = rand.Next(0, 3);
                    if (randomizedNumber == 0)
                        EnemyList.Add(new Enemy(EnemyTexture, new Vector2(50, 50), new Vector2(100, 200)));
                    else
                        EnemyList.Add(new Enemy(EnemyTexture, new Vector2(50, 50), new Vector2(600, 200)));
                    EnemyList.Last().SetPlayer(WormPlayer);
                }
                    if (EnemyList.Count < 2 )
                    {
                    Random rand = new Random();
                    int randomizedNumber = rand.Next(0, 3);
                    if (randomizedNumber == 0)
                        EnemyList.Add(new Enemy(EnemyTexture, new Vector2(50, 50), new Vector2(400, 200)));
                    else if(randomizedNumber == 1)
                        EnemyList.Add(new Enemy(EnemyTexture, new Vector2(50, 50), new Vector2(900, 200)));
                    else
                        EnemyList.Add(new Enemy(EnemyTexture, new Vector2(50, 50), new Vector2(300, 200)));
                    EnemyList.Last().SetPlayer(WormPlayer);
                    }
                enemycounter++;
                    //enemytexture = new Texture2D(this.GraphicsDevice, (int)(enemy.EnemyAggroAreaSize.W), (int)(enemy.EnemyAggroAreaSize.Z));
                    //colorData = new Color[(int)((enemy.EnemyAggroAreaSize.W) * (enemy.EnemyAggroAreaSize.Z))];
                    //for (int i = 0; i < (enemy.EnemyAggroAreaSize.W) * (enemy.EnemyAggroAreaSize.Z); i++)
                    //    colorData[i] = Color.White;
                    //enemytexture.SetData<Color>(colorData);
                    //enemyaggroposition = new Vector2(enemy.EnemyAggroArea.X, enemy.EnemyAggroArea.Y);
                }


            if (CurrentGameState == GameState.LEVELEDITOR)
            {
                levelEditor.movePlatforms(ref SpriteList, TransformationMatrix);
                this.IsMouseVisible = true;
                levelEditor.moveCamera(ref cameraOffset);
            }
            else cameraOffset = new Vector2(0, 0);
            
            

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            
            // TODO: Add your drawing code here
            Viewport viewport = GraphicsDevice.Viewport;
            Vector2 screenCentre = new Vector2(viewport.Width / 2-(SpriteSheetSizes.SpritesSizes["Reggie_Move_X"]/10)-200 + cameraOffset.X, viewport.Height / 2-(SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"]/10)+50 + cameraOffset.Y);
            camera.setCameraWorldPosition(WormPlayer.Position);
            TransformationMatrix = camera.cameraTransformationMatrix(viewport, screenCentre);
            spriteBatch.Begin(0, null, null, null,null,null,TransformationMatrix);
            //added block for better readability
            {
                //spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(background, new Vector2(-5000, -2800), null, Color.White, 0f,Vector2.Zero, 5.0f, SpriteEffects.None, 0f);

                
                //Comment: SEE Framecounter.cs for additional commentary
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                _frameCounter.Update(deltaTime);
                var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);
                spriteBatch.DrawString(font, fps, new Vector2(WormPlayer.Position.X-620, WormPlayer.Position.Y-490), Color.Black);
                //end comment.
                

                //Writes Leveleditor Text when Level Editor is enabled
                if(CurrentGameState == GameState.LEVELEDITOR)
                {
                    string lvlEditorString = "Level Editor Enabled!";
                    spriteBatch.DrawString(font, lvlEditorString, new Vector2(WormPlayer.Position.X - 620, WormPlayer.Position.Y - 470), Color.DarkRed);

                }

                //this draws the enemy
                spriteBatch.Draw(enemytexture, enemyaggroposition, Color.White);
                foreach (var enemy in EnemyList.ToList())
                    enemy.DrawSpriteBatch(spriteBatch);


               

                
                //This draws the player
                animManager.animation(gameTime,ref WormPlayer, spriteBatch);

                switch (CurrentGameState)
                {
                    case GameState.GAMELOOP:
                        //this draws the platforms
                        foreach (var PlatformSprite in gameObjectsToRender)
                            PlatformSprite.DrawSpriteBatch(spriteBatch);
                        break;
                    case GameState.LEVELEDITOR:
                        //this draws the platforms
                        foreach (var PlatformSprite in SpriteList)
                            PlatformSprite.DrawSpriteBatch(spriteBatch);
                        levelEditor.DrawLvlEditorUI(platformTextures, spriteBatch,TransformationMatrix, ref SpriteList);
                        break;
                }

               
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
