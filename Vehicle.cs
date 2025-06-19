using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GME1011_A2_Part2_ChloeF
{
    public class Vehicle
    {
        protected Texture2D _texture;
        protected Vector2 _position;
        protected float _speed;
        protected Color _color;
        protected int _direction; // 1 = right, -1 = left

        public Vehicle(Texture2D texture, Vector2 position, float speed, int direction, Color color)
        {
            _texture = texture;
            _position = position;
            _speed = speed;
            _direction = direction;
            _color = color;
        }

        public virtual void Update()
        {
            _position.X += _speed * _direction;

            if (_direction == 1 && _position.X > 850)
                _position.X = -_texture.Width;

            if (_direction == -1 && _position.X < -_texture.Width)
                _position.X = 850;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            float rotation = (_direction == -1) ? MathHelper.Pi : 0f;

            spriteBatch.Draw(
                _texture,
                _position,
                null,
                _color,
                rotation,
                new Vector2(_texture.Width / 2f, _texture.Height / 2f),
                1.0f,
                SpriteEffects.None,
                0f
            );
        }

        public Rectangle GetCollisionRect()
        {
            return new Rectangle(
                (int)(_position.X - _texture.Width / 2f),
                (int)(_position.Y - _texture.Height / 2f),
                _texture.Width,
                _texture.Height
            );
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public int GetDirection()
        {
            return _direction;
        }

        public float GetTargetSpeed()
        {
            return _speed;
        }
    }
}
