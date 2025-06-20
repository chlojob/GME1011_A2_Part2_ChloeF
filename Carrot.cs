using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GME1011_A2_Part2_ChloeF
{
    public class Carrot
    {
        // Attributes
        private Texture2D _texture;
        private Vector2 _position;
        private bool _isTop;

        // Constructor
        public Carrot(Texture2D texture, Vector2 position)
        {
            this._texture = texture;
            this._position = position;
            this._isTop = true;
        }

        // Accessors
        public Vector2 GetPosition() { return _position; }
        public bool IsTop() { return _isTop; }

        // Mutators
        public void SetPosition(Vector2 position) { _position = position; }
        public void SetIsTop(bool isTop) { _isTop = isTop; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this._texture,
                this._position,
                null,
                Color.White,
                0f,
                new Vector2(this._texture.Width / 2f, this._texture.Height / 2f),
                1.0f,
                SpriteEffects.None,
                0f
            );
        }

        // Collision
        public Rectangle GetCollisionRect()
        {
            return new Rectangle(
                (int)(this._position.X - this._texture.Width / 2f),
                (int)(this._position.Y - this._texture.Height / 2f),
                this._texture.Width,
                this._texture.Height
            );
        }
    }
}
