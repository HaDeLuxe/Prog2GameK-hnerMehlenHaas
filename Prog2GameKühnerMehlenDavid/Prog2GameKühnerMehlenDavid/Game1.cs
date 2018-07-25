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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<GameObject> SpriteList;
        public Player WormPlayer;
        public Enemy Ant;
        private AnimationManager animManager;
        Vector2 playerSpritePosition;
        List<GameObject> gameObjectsToRender;
        SpriteSheetSizes input = new SpriteSheetSizes();

        Camera camera = new Camera();
        

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1800;
            graphics.ApplyChanges();
            playerSpritePosition = new Vector2();
            animManager = new AnimationManager();
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
            input.ReadImageSizeDataSheet();
            Texture2D EnemyTexture = Content.Load<Texture2D>("Images\\door");
            Texture2D PlatformTexture = Content.Load<Texture2D>("Images\\floor");
            Texture2D PlayerJumpSpriteSheet = Content.Load<Texture2D>("Images\\Reggie_Jump");
            Texture2D PlayerMoveSpriteSheet = Content.Load<Texture2D>("Images\\Reggie_Move_Even_Smaller");
            //Texture2D Player = Content.Load<Texture2D>("Images\\enemyRed1");
            WormPlayer = new Player(PlayerMoveSpriteSheet, new Vector2(SpriteSheetSizes.SpritesSizes["Reggie_Move_X"]/5, SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"] / 5));
            Ant = new Enemy(EnemyTexture, new Vector2(50, 50));
            Ant.setPlayer(WormPlayer);
            SpriteList = new List<GameObject>()
            {
                new Platform(PlatformTexture, new Vector2(1800,100))
                { Position = new Vector2(0,900),},

                new Platform(PlatformTexture, new Vector2(1800,100))
                { Position = new Vector2(-500,600),},
            };
            
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
            Ant.Update(gameTime, gameObjectsToRender);
            WormPlayer.Update(gameTime, gameObjectsToRender);
            
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

           

            foreach (var PlatformSprite in gameObjectsToRender)
                PlatformSprite.DrawSpriteBatch(spriteBatch);

            animManager.animation(gameTime, ref playerSpritePosition);
            
            Rectangle rec = new Rectangle((int)playerSpritePosition.X * SpriteSheetSizes.SpritesSizes["Reggie_Move_X"] / 5, 
                                           (int)playerSpritePosition.Y * SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"] / 5,
                                           SpriteSheetSizes.SpritesSizes["Reggie_Move_X"] / 5,
                                           SpriteSheetSizes.SpritesSizes["Reggie_Move_Y"] / 5);
            Ant.DrawSpriteBatch(spriteBatch);
            WormPlayer.DrawSpriteBatch(spriteBatch, rec);
     

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
