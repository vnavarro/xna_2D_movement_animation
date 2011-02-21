using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CharMovementAnimationSample
{
    public class Sprite
    {
        public Vector2 Position = new Vector2(0, 0);
        protected Texture2D _spriteTexture;
        public string AssetName;
        public Rectangle Size;
        protected float _scale = 1.0f;
        public float Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
            }
        }

        public void RescaleTexture()
        {
            if (_spriteTexture != null)
                Size = new Rectangle(0, 0, (int)(_spriteTexture.Width * Scale), (int)(_spriteTexture.Height * Scale));
        }

        public void LoadContent(ContentManager gameContent, string assetName)
        {
            this.AssetName = assetName;
            _spriteTexture = gameContent.Load<Texture2D>(AssetName);
            RescaleTexture();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, new Rectangle(0, 0, _spriteTexture.Width, _spriteTexture.Height));
        }

        public virtual void Draw(SpriteBatch spriteBatch, Rectangle sourceRectangle)
        {
            spriteBatch.Draw(_spriteTexture, Position, sourceRectangle,
                Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime, Vector2 speed, Vector2 direction)
        {
            Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
