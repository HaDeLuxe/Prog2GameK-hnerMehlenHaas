using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2GameKühnerMehlenDavid {
    public class Player : Sprite {
        KeyboardState PreviousState;
        Vector2 Gravity;
        bool GravityActive;
        bool FirstJump;
        bool SecondJump;
        bool AirDirectionLeft;
        bool AirDirectionRight;
        int JumpCounter;
        int PreviousJumpCounter;
        float MovementSpeed;
        float JumpSpeed;
        public Player(Texture2D texture) : base(texture) {
            GravityActive = true;
            FirstJump = false;
            SecondJump = false;
            AirDirectionLeft = false;
            AirDirectionRight = false;
            JumpCounter = 0;
            PreviousJumpCounter = 0;
            Position = new Vector2(100, 100);
            MovementSpeed = 5f;
            JumpSpeed = -10f;
        }

        public override void Update(GameTime gameTime, List<Sprite> spriteList) {
            PlayerMovement();
            PlayerCollision(gameTime, spriteList);
        }

        private void PlayerCollision(GameTime gameTime, List<Sprite> spriteList) {
            foreach (var sprite in spriteList)
            {
                //Checks collision on the left side and right side of each sprite when player is on the ground/air
                if (Velocity.X > 0 && IsTouchingLeftSide(sprite) ||
                   (Velocity.X < 0 && IsTouchingRightSide(sprite)))
                    Velocity.X = 0;
                //checks collision on the bottom side of each sprite and makes a smoother contact between player/sprite if the player should hit the sprite
                //Activate Gravity boolean and stops translation in UP direction if the bottom side of a sprite was hit
                else if (IsTouchingBottomSide(sprite))
                {
                    Velocity.Y = 0;
                    JumpSpeed = 0;
                    GravityActive = true;
                    Position.Y = sprite.Position.Y + sprite.SpriteRectangle.Height;
                    if (FirstJump == true && SecondJump == false)
                        FirstJump = false;
                    else if (FirstJump == true && SecondJump == true)
                    {
                        FirstJump = false;
                        SecondJump = false;
                    }
                }
                // Resets AirDirection booleans, jump booleans, the number of times the worm has jumped and stops translations in DOWN direction 
                else if (IsTouchingTopSide(sprite, Gravity))
                {
                    Velocity.Y = 0;
                    GravityActive = false;
                    Gravity = Vector2.Zero;
                    FirstJump = false;
                    SecondJump = false;
                    JumpCounter = 0;
                    Position.Y = sprite.Position.Y - SpriteRectangle.Height;
                    AirDirectionLeft = false;
                    AirDirectionRight = false;
                }

                if (!IsTouchingTopSide(sprite, Gravity))
                {
                    GravityActive = true;
                    if (AirDirectionRight)
                        Velocity.X = MovementSpeed;
                    else if (AirDirectionLeft)
                        Velocity.X = -MovementSpeed;
                    if (IsTouchingLeftSide(sprite) || IsTouchingRightSide(sprite))
                        Velocity.X = 0;
                    if (PreviousState.IsKeyDown(Keys.Space) && JumpCounter < 3)
                    {
                        JumpSpeed = -10f;
                        if (JumpCounter != PreviousJumpCounter)
                            Gravity = Vector2.Zero;
                        if (JumpCounter == 1)
                            FirstJump = true;
                        else if (JumpCounter == 2)
                            SecondJump = true;
                    }
                    PreviousJumpCounter = JumpCounter;
                }
            }
            if ((FirstJump == true || SecondJump == true))
                PlayerJump();
            if (GravityActive)
            {
                Gravity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 15;
                Position += Gravity;
            }
            else
                Gravity = Vector2.Zero;
            Position += Velocity;
            Velocity = Vector2.Zero;
        }

        private void PlayerJump() {
            Velocity.Y = JumpSpeed;
            if (PreviousState.IsKeyDown(Keys.Down))
                Velocity.Y = MovementSpeed;
        }

        private void PlayerMovement() {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Velocity.X = -MovementSpeed;
                AirDirectionLeft = true;
                AirDirectionRight = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Velocity.X = MovementSpeed;
                AirDirectionLeft = false;
                AirDirectionRight = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                Velocity.Y = MovementSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !PreviousState.IsKeyDown(Keys.Space) && JumpCounter < 3)
            {
                if (FirstJump == false && SecondJump == false)
                    //JumpSpeed = -5f?
                    JumpSpeed = -10f;
                PlayerJump();
                JumpCounter++;
            }

            PreviousState = Keyboard.GetState();
        }
    }
}
