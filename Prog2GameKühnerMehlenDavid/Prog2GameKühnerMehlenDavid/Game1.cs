﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Prog2GameKühnerMehlenDavid {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<Sprite> SpriteList;
        public Player WormPlayer;
        //Texture2D PlayerMoveSpriteSheet;
        


        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1800;
            graphics.ApplyChanges();
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
            Texture2D WormTexture = Content.Load<Texture2D>("Images\\door");
            Texture2D PlatformTexture = Content.Load<Texture2D>("Images\\floor");
            Texture2D PlayerJumpSpriteSheet = Content.Load<Texture2D>("Images\\Reggie_Jump");
            //PlayerMoveSpriteSheet = Content.Load<Texture2D>("Images\\Reggie_Move_Smaller");
            WormPlayer = new Player(WormTexture);
            SpriteList = new List<Sprite>()
            {
                new Platform(PlatformTexture)
                { Position = new Vector2(0,900),},

                new Platform(PlatformTexture)
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
            spriteBatch.Begin();

            foreach (var PlatformSprite in SpriteList)
                PlatformSprite.DrawSpriteBatch(spriteBatch);
            WormPlayer.DrawSpriteBatch(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
