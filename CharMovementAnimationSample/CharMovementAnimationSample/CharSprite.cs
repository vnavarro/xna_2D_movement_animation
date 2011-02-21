using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CharMovementAnimationSample
{
    class CharSprite : Sprite
    {
        private string StateLeft = "Left";
        private string StateRight = "Right";
        private string StateUp = "Up";
        private string StateDown = "Down";
        private string currentState = "Left";

        private float timePerFrame;
        private float framesPerSec = 10.0f;
        private float totalElapsed;

        private Vector2 leftDirection = new Vector2(-1, 0);
        private Vector2 rightDirection = new Vector2(1, 0);
        private Vector2 upDirection = new Vector2(0, -1);
        private Vector2 downDirection = new Vector2(0, 1);
        private Vector2 horizontalSpeed;
        private Vector2 verticalSpeed;

        Vector2 direction = Vector2.Zero;
        Vector2 speedComposition = Vector2.Zero;
        public ContentManager content { get; set; }

        private int charHeight = 25;
        private int charWidth = 17;
        private int charSpeed = 160;

        private Rectangle actualCharPosition;
        private int actualCharAnimation = 0;
        private int charX = 0;
        private Dictionary<string, int> states = new Dictionary<string, int>();
        private KeyboardState previousKeyboardState;



        public CharSprite()
        {
            AssetName = "Sprites/Bomberman";
            states.Add(StateLeft, 0);
            states.Add(StateRight, 3);
            states.Add(StateDown, 6);
            states.Add(StateUp, 9);
            timePerFrame = 1.0f / framesPerSec;
            totalElapsed = 0;
            horizontalSpeed = new Vector2(charSpeed, 0);
            verticalSpeed = new Vector2(0, charSpeed);
        }

        public void LoadContent(ContentManager gameContent)
        {
            this.content = gameContent;
            base.LoadContent(gameContent, AssetName);
            Position = new Vector2(400, 150);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int x = charX * (states[currentState] + actualCharAnimation);
            actualCharPosition = new Rectangle(x, 0, charWidth, charHeight);
            Draw(spriteBatch, actualCharPosition);
        }

        public void Update(GameTime gameTime)
        {
            totalElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (totalElapsed > timePerFrame)
            {
                totalElapsed -= timePerFrame;
                KeyboardState currentKeyboardState = Keyboard.GetState();
                UpdateMovement(currentKeyboardState);

                previousKeyboardState = currentKeyboardState;
                base.Update(gameTime, speedComposition, direction);
            }
        }

        private void UpdateMovement(KeyboardState currentKeyboardState)
        {
            if (currentKeyboardState.IsKeyDown(Keys.Left) == true)
                MoveLeft();
            else if (currentKeyboardState.IsKeyDown(Keys.Right) == true)
                MoveRight();
            else if (currentKeyboardState.IsKeyDown(Keys.Up) == true)
                MoveUp();
            else if (currentKeyboardState.IsKeyDown(Keys.Down) == true)
                MoveDown();
            else
                StayStill();
        }

        private void StayStill()
        {
            if (previousKeyboardState.IsKeyDown(Keys.Right))
                currentState = StateRight;
            else if (previousKeyboardState.IsKeyDown(Keys.Left))
                currentState = StateLeft;

            charX = charWidth;
            actualCharAnimation = 1;
            speedComposition = Vector2.Zero;
            direction = Vector2.Zero;
        }

        private void MoveUp()
        {
            speedComposition = verticalSpeed;
            direction = upDirection;

            ChangeAnimation(StateUp);
        }

        private void MoveDown()
        {
            speedComposition = verticalSpeed;
            direction = downDirection;

            ChangeAnimation(StateDown);
        }

        private void MoveRight()
        {
            speedComposition = horizontalSpeed;
            direction = rightDirection;

            ChangeAnimation(StateRight);
        }

        private void MoveLeft()
        {
            speedComposition = horizontalSpeed;
            direction = leftDirection;

            ChangeAnimation(StateLeft);
        }

        private void ChangeAnimation(string state)
        {
            actualCharAnimation++;
            actualCharAnimation = actualCharAnimation % 3;
            charX = charWidth;
            currentState = state;
        }
    }

}

