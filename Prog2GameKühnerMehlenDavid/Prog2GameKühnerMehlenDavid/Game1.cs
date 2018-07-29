﻿using Microsoft.Xna.Framework;
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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<GameObject> SpriteList;
        List<Enemy> EnemyList;
        List<Enemy> HittableEnemies;
        List<GameObject> gameObjectsToRender;
        public Player WormPlayer;
        public Enemy Ant;
        Texture2D EnemyTexture;
        private AnimationManager animManager;
        Vector2 playerSpritePosition;
        SpriteSheetSizes input = new SpriteSheetSizes();
        private FrameCounter _frameCounter = new FrameCounter();
        private SpriteFont font;
        Camera camera = new Camera();
        Texture2D enemytexture;
        Color[] colorData;
        Vector2 enemyaggroposition;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1800;
            graphics.ApplyChanges();
            playerSpritePosition = new Vector2();
            input.ReadImageSizeDataSheet();
            animManager = new AnimationManager(SpriteSheetSizes.SpritesSizes["Reggie_Move_X"]/5, SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"]/5);
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
            Texture2D PlayerJumpSpriteSheet = Content.Load<Texture2D>("Images\\Reggie_Jump");
            Texture2D PlayerMoveSpriteSheet = Content.Load<Texture2D>("Images\\Reggie_Move_Even_Smaller");
            //Texture2D Player = Content.Load<Texture2D>("Images\\enemyRed1");
            WormPlayer = new Player(PlayerMoveSpriteSheet, new Vector2(SpriteSheetSizes.SpritesSizes["Reggie_Move_X"]/5, SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"] / 5), new Vector2(0,0));
            //Ant = new Enemy(EnemyTexture, new Vector2(50, 50));
            //Ant.setPlayer(WormPlayer);
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
            gameObjectsToRender = camera.objectsToRender(WormPlayer.Position, SpriteList);
            HittableEnemies = camera.RenderedEnemies(WormPlayer.Position, EnemyList);
            //Ant.Update(gameTime, SpriteList);
            WormPlayer.Update(gameTime, gameObjectsToRender,HittableEnemies);
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
            base.Update(gameTime);
            

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            
            // TODO: Add your drawing code here
            Viewport viewport = GraphicsDevice.Viewport;
            Vector2 screenCentre = new Vector2(viewport.Width / 2-(SpriteSheetSizes.SpritesSizes["Reggie_Move_X"]/10)-200, viewport.Height / 2-(SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"]/10)+50);
            camera.setCameraWorldPosition(WormPlayer.Position);

            spriteBatch.Begin(0, null, null, null,null,null,camera.cameraTransformationMatrix(viewport, screenCentre) );
            //added block for better visibility
            {
                //Comment: SEE Framecounter.cs for additional commentary
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                _frameCounter.Update(deltaTime);
                var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);
                spriteBatch.DrawString(font, fps, new Vector2(WormPlayer.Position.X-620, WormPlayer.Position.Y-490), Color.Black);
                //end comment.

                //this draws the enemy
                spriteBatch.Draw(enemytexture, enemyaggroposition, Color.White);
                foreach (var enemy in EnemyList.ToList())
                    enemy.DrawSpriteBatch(spriteBatch);
             
                


                //this draws the platforms
                foreach (var PlatformSprite in gameObjectsToRender)
                    PlatformSprite.DrawSpriteBatch(spriteBatch);
                
                //This draws the player
                animManager.animation(gameTime,ref WormPlayer, spriteBatch);

               
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
