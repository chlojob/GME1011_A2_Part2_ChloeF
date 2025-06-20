using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;

namespace GME1011_A2_Part2_ChloeF
{
    public class Ped
    {
        // Attributes
        private Texture2D _texture;
        private Vector2 _position;
        private Color _color;
        private float _speed;
        private float _scale;
        private bool _isRushing;

        // Constructor
        public Ped(Color color, float speed, float scale, bool isRushing, Texture2D texture)
        {
            this._color = color;
            this._speed = speed;
            this._scale = scale;
            this._isRushing = isRushing;
            this._texture = texture;
            this._position = new Vector2(400, 550);
        }

        // Accessors
        public Color GetColor() { return _color; }
        public float GetSpeed() { return _speed; }
        public float GetScale() { return _scale; }
        public bool GetIsRushing() { return _isRushing; }
        public Vector2 GetPosition() { return _position; }

        // Mutators
        public void SetColor(Color color) { _color = color; }
        public void SetSpeed(float speed) { _speed = speed; }
        public void SetScale(float scale) { _scale = scale; }
        public void SetIsRushing(bool isRushing) { _isRushing = isRushing; }
        public void SetPosition(Vector2 position) { _position = position; }

        public void Update()
        {
            // Checks state of keyboard
            KeyboardState keyState = Keyboard.GetState();

            // Listens for space bar press, toggles rushing bool to true if so
            _isRushing = keyState.IsKeyDown(Keys.Space);

            // Boosts speed if rushing, otherwise use normal speed
            float currentSpeed;

            if (_isRushing)
            {
                currentSpeed = _speed + 2.5f;
            }
            else
            {
                currentSpeed = _speed;
            }

            // Movement from the WASD and arrow keys
            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
                _position.Y -= currentSpeed;
            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S))
                _position.Y += currentSpeed;
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
                _position.X -= currentSpeed;
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
                _position.X += currentSpeed;
        }

        // Draws pedestrian on screen using its texture, position, scale
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
            {
                spriteBatch.Draw(
                    _texture,
                    _position,
                    null,
                    _color,
                    0f,
                    new Vector2(32f, 32f),
                    _scale,
                    SpriteEffects.None,
                    0f);

            }

        }
        public Rectangle GetCollisionRect()
        {
            return new Rectangle(
                (int)(_position.X - (_texture.Width / 2f) * _scale),
                (int)(_position.Y - (_texture.Height / 2f) * _scale),
                (int)(_texture.Width * _scale),
                (int)(_texture.Height * _scale)
            );
        }

    }
}

