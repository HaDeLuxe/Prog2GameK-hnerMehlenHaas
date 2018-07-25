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
        Vector2 playerSpriteSheetPosition;

        SpriteSheetSizes input = new SpriteSheetSizes();

        Camera camera = new Camera();
        

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1800;
            graphics.ApplyChanges();
            playerSpriteSheetPosition = new Vector2();
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
            Texture2D EnemyTexture = Content.Load<Texture2D>("Images\\door");
            Texture2D PlatformTexture = Content.Load<Texture2D>("Images\\floor");
            Texture2D PlayerJumpSpriteSheet = Content.Load<Texture2D>("Images\\Reggie_Jump");
            Texture2D PlayerMoveSpriteSheet = Content.Load<Texture2D>("Images\\Reggie_Move_Smaller");
            Texture2D Player = Content.Load<Texture2D>("Images\\enemyRed1");
            WormPlayer = new Player(PlayerMoveSpriteSheet, new Vector2(310,186));
            Ant = new Enemy(EnemyTexture, new Vector2(50, 50));
            Ant.setPlayer(WormPlayer);
            SpriteList = new List<GameObject>()
            {
                new Platform(PlatformTexture, new Vector2(1800,100))
                { Position = new Vector2(0,900),},

                new Platform(PlatformTexture, new Vector2(1800,100))
                { Position = new Vector2(-500,600),},
            };

            input.ReadImageSizeDataSheet();
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
            Ant.Update(gameTime, SpriteList);
            WormPlayer.Update(gameTime, SpriteList);
            
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
            Vector2 screenCentre = new Vector2(viewport.Width / 2-155, viewport.Height / 2-93);
            camera.setCameraWorldPosition(WormPlayer.Position);

            spriteBatch.Begin(0, null, null, null,null,null,camera.cameraTransformationMatrix(viewport, screenCentre) );

            foreach (var PlatformSprite in SpriteList)
                PlatformSprite.DrawSpriteBatch(spriteBatch);

            animManager.animation(gameTime, ref playerSpriteSheetPosition);
            
            Rectangle rec = new Rectangle((int)playerSpriteSheetPosition.X* 310, (int)playerSpriteSheetPosition.Y * 186, 310, 186);
            Ant.DrawSpriteBatch(spriteBatch);
            WormPlayer.DrawSpriteBatch(spriteBatch, rec);
     

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
