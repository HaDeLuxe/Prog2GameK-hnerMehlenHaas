using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reggie.Animations;

namespace Reggie
{
    public class ShopKeeper : GameObject
    {
        NPC_Animations nPC_Animations;
        //MUSIC
        AudioManager audioManager;

        public bool shopOpen;

        bool buttonPressed;
        GamePadState lastPadInput;

        //randomise wich line of text gets printed
        float randomTimer;
        //how long the text is displayed
        float timer;
        float boxPopUpCooldown;

        string temp = "";

        //ButtonStuff
        Enums.ShopKeeperItemButtons currentButton;
        Enums.ShopKeeperItemButtons lastButton;

        //for the textbox "logic" that the player doesnt always see the same boxes
        bool alreadydrawn_0;
        bool alreadydrawn_1;
        bool alreadydrawn_2;
        bool alreadydrawn_3;
        bool alreadydrawn_4;
        bool alreadydrawn_5;
        bool alreadydrawn_6;
        bool alreadydrawn_7;
        bool alreadydrawn_8;
        bool alreadydrawn_9;
        bool alreadydrawn_10;

        int totalTextBoxesCount;
        int currentDrawnTextBoxesCount;
        int everySecondTime;


        public ShopKeeper(Texture2D gameObjectTexture, Vector2 gameObejctSize, Vector2 position, int gameObjectID, Dictionary<string, Texture2D> texturesDictionary) : base(gameObjectTexture, gameObejctSize, position, gameObjectID)
        {
            nPC_Animations = new NPC_Animations(texturesDictionary);
            nPC_Animations.currentAnimation = NPC_Animations.NPCAnimations.IdleShopkeeper;
            nPC_Animations.nextAnimation = NPC_Animations.NPCAnimations.IdleShopkeeper;

            audioManager = AudioManager.AudioManagerInstance();

            shopOpen = false;

            lastButton = Enums.ShopKeeperItemButtons.STRENGTHPOTION;
            currentButton = Enums.ShopKeeperItemButtons.STRENGTHPOTION;

            buttonPressed = false;
            randomTimer = 0;
            timer = 0;

            //HOW MANY DO WE HAVE?
            totalTextBoxesCount = 11;
            currentDrawnTextBoxesCount = 0;
            boxPopUpCooldown = 5;
            everySecondTime = 0;
            temp = "";
        }

        internal void DrawShopKeeper(SpriteBatch spriteBatch, GameTime gameTime, Dictionary<string, Texture2D> texturesDictionary, Matrix transformationMatrix, SpriteFont font, Levels levelManager)
        {
            nPC_Animations.Animation(gameTime, spriteBatch, this);
            if (shopOpen == true)
                drawShopInterface(spriteBatch, texturesDictionary, transformationMatrix);

            //TextBoxTimingStuff
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            randomTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (levelManager.currentLevel == Enums.Level.HUB)
                DrawAlmostRandomMessages(spriteBatch, gameTime, transformationMatrix, font, levelManager);
        }

        private void DrawAlmostRandomMessages(SpriteBatch spriteBatch, GameTime gameTime, Matrix transformationMatrix, SpriteFont font, Levels levelManager)
        {
            if (timer > boxPopUpCooldown)
            {
                everySecondTime++;
                temp = "";
                if (everySecondTime == 2)
                {
                    temp = "";
                    everySecondTime = 0;
                }
                else if ((int)randomTimer % totalTextBoxesCount == 0 && alreadydrawn_0 == false)
                {
                    temp = "Hey, Wurmling!";
                    alreadydrawn_0 = true;
                    boxPopUpCooldown = 3;
                    currentDrawnTextBoxesCount++;
                    nPC_Animations.nextAnimation = NPC_Animations.NPCAnimations.IdleShopkeeper;
                }
                else if ((int)randomTimer % totalTextBoxesCount == 1 && alreadydrawn_1 == false)
                {
                    temp = "Komm ma ran und kauf jetzt!";
                    alreadydrawn_1 = true;
                    boxPopUpCooldown = 3.5f;
                    currentDrawnTextBoxesCount++;
                    nPC_Animations.nextAnimation = NPC_Animations.NPCAnimations.IdleShopkeeper;
                }
                else if ((int)randomTimer % totalTextBoxesCount == 2 && alreadydrawn_2 == false)
                {
                    temp = "Jaa, ich will dich ja nicht hetzen\naber wenn du hier nur rumstehst und nichts kaufst,\ndann kannst du auch wieder weiter gehn!";
                    alreadydrawn_2 = true;
                    boxPopUpCooldown = 5.5f;
                    currentDrawnTextBoxesCount++;
                }
                else if ((int)randomTimer % totalTextBoxesCount == 3 && alreadydrawn_3 == false)
                {
                    temp = "Nicht genug Korn? Da machste nix!\nHoechstens einen Trinken! HA, HA!";
                    alreadydrawn_3 = true;
                    boxPopUpCooldown = 5;
                    currentDrawnTextBoxesCount++;
                }
                else if ((int)randomTimer % totalTextBoxesCount == 4 && alreadydrawn_4 == false)
                {
                    temp = "Ich kanns nur immer wieder sagen,\n mit Wurmlingen wie dir wird Hakumes Regime nie gestuertzt...";
                    alreadydrawn_4 = true;
                    boxPopUpCooldown = 5;
                    currentDrawnTextBoxesCount++;
                }
                else if ((int)randomTimer % totalTextBoxesCount == 5 && alreadydrawn_5 == false)
                {
                    temp = "Paah! Du willst Hakumes Regime stuerzen?!\nOhne ordentliche Ausruestung kannst du doch gar nichts ausrichten!";
                    alreadydrawn_5 = true;
                    boxPopUpCooldown = 5.5f;
                    currentDrawnTextBoxesCount++;
                }
                else if ((int)randomTimer % totalTextBoxesCount == 6 && alreadydrawn_6 == false)
                {
                    temp = "Ich habe das beste gruene zu verkaufen!\nAber ehrlich gesagt bin ich auch der einzige,\n der hier was verkauft";
                    alreadydrawn_6 = true;
                    boxPopUpCooldown = 5.5f;
                    currentDrawnTextBoxesCount++;
                }
                else if ((int)randomTimer % totalTextBoxesCount == 7 && alreadydrawn_7 == false)
                {
                    temp = "Wo sind denn alle anderen hin?!\nVielleicht wurden sie von Hakumes Lakaien in die Sklaverei getrieben\noder dienen als Tauzugseil fuer die anderen Voegel,\naber was weiss ich schon";
                    alreadydrawn_7 = true;
                    boxPopUpCooldown = 8.5f;
                    currentDrawnTextBoxesCount++;
                }
                else if ((int)randomTimer % totalTextBoxesCount == 8 && alreadydrawn_8 == false)
                {
                    temp = "Seit Hakume hier sein unwesen treibt\nund alle Wurmlinge als niedrige Nahrung klassifiziert hat,\nkommt hier kaum noch einer vorbei.\nSchoen das es dich gibt, kauf was!";
                    alreadydrawn_8 = true;
                    boxPopUpCooldown = 8.5f;
                    currentDrawnTextBoxesCount++;
                }
                else if ((int)randomTimer % totalTextBoxesCount == 9 && alreadydrawn_9 == false)
                {
                    temp = "Die Voegel gehen mir echt auf mein Blatt!";
                    alreadydrawn_9 = true;
                    boxPopUpCooldown = 4;
                    currentDrawnTextBoxesCount++;
                }
                else if ((int)randomTimer % totalTextBoxesCount == 10 && alreadydrawn_10 == false)
                {
                    temp = "Es koennte ja so schoen sein ohne diese Voegel!";
                    alreadydrawn_10 = true;
                    boxPopUpCooldown = 4;
                    currentDrawnTextBoxesCount++;
                }
                else
                {
                    boxPopUpCooldown = 0;
                    everySecondTime = 0;
                }
                    
                timer = 0;
            }

            //resets all alreadydrawn bools if every text box was once displayed
            if (currentDrawnTextBoxesCount >= totalTextBoxesCount)
            {
                alreadydrawn_0 = false;
                alreadydrawn_1 = false;
                alreadydrawn_2 = false;
                alreadydrawn_3 = false;
                alreadydrawn_4 = false;
                alreadydrawn_5 = false;
                alreadydrawn_6 = false;
                alreadydrawn_7 = false;
                alreadydrawn_8 = false;
                alreadydrawn_9 = false;
                alreadydrawn_10 = false;
                currentDrawnTextBoxesCount = 0;
            }
            

            //draws the actual Textbox
            //if the shop isnt open he draws it dynamically onto your screen but only if the player is in hub
            if (!shopOpen)
                spriteBatch.DrawString(font, temp, Vector2.Transform(new Vector2(650, 800), Matrix.Invert(transformationMatrix)), Color.Black, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            else
                spriteBatch.DrawString(font, temp, new Vector2(2850, 4350), Color.Black, 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0);

            spriteBatch.DrawString(font, "Herald, der Shopdude", new Vector2(2650, 4200), Color.Black, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0);
        }

        public void drawShopInterface(SpriteBatch spriteBatch, Dictionary<string, Texture2D> texturesDictionary, Matrix transformationMatrix)
        {

            //Background
            spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_Background"], Vector2.Transform(new Vector2(500, 200), Matrix.Invert(transformationMatrix)), Color.White);
            spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_AllPotions"], Vector2.Transform(new Vector2(500, 200), Matrix.Invert(transformationMatrix)), Color.White); 
            spriteBatch.Draw(texturesDictionary["ShopkeeperInterface_Help"], Vector2.Transform(new Vector2(500, 200), Matrix.Invert(transformationMatrix)), Color.White);

            //ButtonStuff
            if (lastButton == Enums.ShopKeeperItemButtons.STRENGTHPOTION)
                spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_StrengthPotion_Highlighted"], Vector2.Transform(new Vector2(500,200),Matrix.Invert(transformationMatrix)), Color.White);

            if (lastButton == Enums.ShopKeeperItemButtons.JUMPPOTION)
                spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_JumpPotion_Highlighted"], Vector2.Transform(new Vector2(500, 200), Matrix.Invert(transformationMatrix)), Color.White);
            
            if (lastButton == Enums.ShopKeeperItemButtons.HEALTHPOTION)
                spriteBatch.Draw(texturesDictionary["ShopKeeperInterface_HealtPotion_Highlighted"], Vector2.Transform(new Vector2(500, 200), Matrix.Invert(transformationMatrix)), Color.White);


        }

        public void handleShopKeeperEvents()
        {
            lastButton = currentButton;

            if ((Keyboard.GetState().IsKeyDown(Keys.W) || GamePad.GetState(0).ThumbSticks.Left.Y < -0.5f) && !buttonPressed)
            {
                currentButton--;
                if (currentButton < 0)
                currentButton = Enums.ShopKeeperItemButtons.STRENGTHPOTION;

                lastPadInput = GamePad.GetState(0);
                buttonPressed = true;
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.S) || GamePad.GetState(0).ThumbSticks.Left.Y > +0.5f) && !buttonPressed && lastPadInput != GamePad.GetState(0))
            {
                currentButton++;
                if (currentButton <= Enums.ShopKeeperItemButtons.STRENGTHPOTION)
                    currentButton = Enums.ShopKeeperItemButtons.JUMPPOTION;
                if (currentButton > Enums.ShopKeeperItemButtons.JUMPPOTION)
                    currentButton = Enums.ShopKeeperItemButtons.HEALTHPOTION;

                lastPadInput = GamePad.GetState(0);
                buttonPressed = true;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(0).IsButtonDown(Buttons.A)) && !buttonPressed &&lastPadInput!=GamePad.GetState(0))
            {
                buttonPressed = true;

                if (lastButton == Enums.ShopKeeperItemButtons.STRENGTHPOTION)
                {
                    Buy("StrengthPotion");
                    nPC_Animations.nextAnimation = NPC_Animations.NPCAnimations.IdleShopkeeper;
                }


                if (lastButton == Enums.ShopKeeperItemButtons.JUMPPOTION)
                {
                    Buy("JumpPotion");
                    nPC_Animations.nextAnimation = NPC_Animations.NPCAnimations.IdleShopkeeper;
                }

                if (lastButton == Enums.ShopKeeperItemButtons.HEALTHPOTION)
                {
                    Buy("HealthPotion");
                    nPC_Animations.nextAnimation = NPC_Animations.NPCAnimations.IdleShopkeeper;
                }
            }
            //(MouseState.Equals(Mouse) //mit linker Moustaste schließbar?
            if ((Keyboard.GetState().IsKeyDown(Keys.Escape) || GamePad.GetState(0).IsButtonDown(Buttons.B)) && !buttonPressed)
            {
                shopOpen = false;
            }
            if (Keyboard.GetState().GetPressedKeys().Count() == 0 || GamePad.GetState(0).ThumbSticks.Left.Y == 0 && GamePad.GetState(0).ThumbSticks.Left.X == 0)
            {
                buttonPressed = false;
            }
            lastPadInput = GamePad.GetState(0);
        }

        //MUSS DA NOCH NEN UPDATE GETRIGGERT WERDEN ODER NICHT?
        private void Buy(string s)
        {
            if(s.Equals("StrengthPotion"))
            {
                if (ItemUIManager.cornnencyQuantity >= 5)
                {
                    ItemUIManager.cornnencyQuantity -= 5;
                    ItemUIManager.powerPotionsCount++;
                    audioManager.Play("ReggieBoughtSomething");
                }
            }
            if(s.Equals("JumpPotion"))
            {
                if (ItemUIManager.cornnencyQuantity >= 5)
                {
                    ItemUIManager.cornnencyQuantity -= 5;
                    ItemUIManager.jumpPotionsCount++;
                    audioManager.Play("ReggieBoughtSomething");
                }
            }
            if(s.Equals("HealthPotion"))
            {
                if (ItemUIManager.cornnencyQuantity >= 5)
                {
                    ItemUIManager.cornnencyQuantity -= 5;
                    ItemUIManager.healthPotionsCount++;
                    audioManager.Play("ReggieBoughtSomething");
                }
            }
        }
    }
}
