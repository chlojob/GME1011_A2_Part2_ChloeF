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
            _texture = texture;
            _position = position;
            _isTop = true;
        }

        // Accessors
        public Vector2 GetPosition() { return _position; }
        public bool IsTop() { return _isTop; }

        // Mutators
        public void SetPosition(Vector2 position) { _position = position; }
        public void SetIsTop(bool isTop) { _isTop = isTop; } // Bool for carrot being at top of screen. It alternates. Special alternating carrot.

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                _position,
                null,
                Color.White,
                0f,
                new Vector2(_texture.Width / 2f, _texture.Height / 2f),
                1.0f,
                SpriteEffects.None,
                0f
            );
        }

        // Collision
        public Rectangle GetCollisionRect()
        {
            return new Rectangle(
                (int)(_position.X - _texture.Width / 2f),
                (int)(_position.Y - _texture.Height / 2f),
                _texture.Width,
                _texture.Height
            );
        }
    }
}
