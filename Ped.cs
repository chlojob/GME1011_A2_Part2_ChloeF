using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GME1011_A2_Part2_ChloeF
{
    public class Ped
    {
        private Color _color;
        private float _speed;
        private float _scale;
        private bool _isRushing;

        private Texture2D _texture;
        private Vector2 _position;

        private Color[] _textureData;

        public Ped(Color color, float speed, float scale, bool isRushing, Texture2D texture)
        {
            _color = color;
            _speed = speed;
            _scale = scale;
            _isRushing = isRushing;
            _texture = texture;
            _position = new Vector2(400, 550);

            _textureData = new Color[_texture.Width * _texture.Height];
            _texture.GetData(_textureData);
        }

        public Color GetColor() { return _color; }
        public float GetSpeed() { return _speed; }
        public float GetScale() { return _scale; }
        public bool GetIsRushing() { return _isRushing; }
        public Vector2 GetPosition() { return _position; }

        public void SetColor(Color color) { _color = color; }
        public void SetSpeed(float speed) { _speed = speed; }
        public void SetScale(float scale) { _scale = scale; }
        public void SetIsRushing(bool isRushing) { _isRushing = isRushing; }
        public void SetPosition(Vector2 position) { _position = position; }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            _isRushing = keyState.IsKeyDown(Keys.Space);

            float currentSpeed = _isRushing ? _speed + 2.5f : _speed;

            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
                _position.Y -= currentSpeed;
            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S))
                _position.Y += currentSpeed;
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
                _position.X -= currentSpeed;
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
                _position.X += currentSpeed;

            // Keep player in screen bounds
            _position.X = MathHelper.Clamp(_position.X, 32, 800 - 32);
            _position.Y = MathHelper.Clamp(_position.Y, 32, 600 - 32);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                _position,
                null,
                _color,
                0f,
                new Vector2(_texture.Width / 2f, _texture.Height / 2f),
                _scale,
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

        public bool PixelPerfectCollision(Vehicle other)
        {
            Rectangle rectA = this.GetCollisionRect();
            Rectangle rectB = other.GetCollisionRect();

            Rectangle intersect = Rectangle.Intersect(rectA, rectB);

            if (intersect.IsEmpty)
                return false;

            for (int y = 0; y < intersect.Height; y++)
            {
                for (int x = 0; x < intersect.Width; x++)
                {
                    int ax = intersect.X - rectA.X + x;
                    int ay = intersect.Y - rectA.Y + y;

                    int bx = intersect.X - rectB.X + x;
                    int by = intersect.Y - rectB.Y + y;

                    Color colorA = _textureData[ay * _texture.Width + ax];

                    // Treat vehicle as simple rectangle — always solid
                    if (colorA.A > 10)
                        return true;
                }
            }

            return false;
        }
    }
}
