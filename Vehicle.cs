using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GME1011_A2_Part2_ChloeF
{
    public class Vehicle
    {
        // Attributes
        private Texture2D _texture;
        private Vector2 _position;
        private float _speed;
        private Color _color;
        private int _direction; // 1 = right, -1 = left

        private static Random _rng = new Random();

        // Constructor
        public Vehicle(Texture2D texture, Vector2 position, float speed, int direction, Color color)
        {
            this._texture = texture;
            this._position = position;
            this._speed = speed;
            this._direction = direction;
            this._color = color;
        }

        // Accessors
        public Texture2D GetTexture() { return _texture; }
        public Vector2 GetPosition() { return _position; }
        public float GetSpeed() { return _speed; }
        public Color GetColor() { return _color; }
        public int GetDirection() { return _direction; }

        // Mutators
        public void SetTexture(Texture2D texture) { this._texture = texture; }
        public void SetPosition(Vector2 position) { this._position = position; }
        public void SetSpeed(float speed) { this._speed = speed; }
        public void SetColor(Color color) { this._color = color; }
        public void SetDirection(int direction) { this._direction = direction; }

        public virtual void Update()
        {
            this._position.X += this._speed * this._direction;

            if (this._direction == 1 && this._position.X > 850)
            {
                this._position.X = -this._texture.Width - _rng.Next(100, 300);
            }
            else if (this._direction == -1 && this._position.X < -this._texture.Width)
            {
                this._position.X = 850 + _rng.Next(100, 300);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            float rotation = (this._direction == -1) ? MathHelper.Pi : 0f;

            spriteBatch.Draw(
                this._texture,
                this._position,
                null,
                this._color,
                rotation,
                new Vector2(this._texture.Width / 2f, this._texture.Height / 2f),
                1.0f,
                SpriteEffects.None,
                0f
            );
        }

        public Rectangle GetCollisionRect()
        {
            int padding = 10; // shrink box by 10 pixels on all sides

            return new Rectangle(
                (int)(_position.X - _texture.Width / 2f) + padding,
                (int)(_position.Y - _texture.Height / 2f) + padding,
                _texture.Width - padding * 2,
                _texture.Height - padding * 2
            );
        }
    }
}
