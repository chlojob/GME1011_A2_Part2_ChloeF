using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System.Threading.Tasks;

namespace GME1011_A2_Part2_ChloeF
{
    public class Ped
    {
        // Attributes
        private string _color;
        private float _speed;
        private float _scale;
        private bool _isRushing;

        // Position
        private Vector2 _position;

        // Zero-argument constructor
        public Ped()
        {
            _color = "white";
            _speed = 2.5f;
            _scale = 1.0f;
            _isRushing = false;
            _position = new Vector2(200, 600);
        }

        // Argumented constructor
        public Ped(string color, float speed, float scale, bool isRushing)
        {
            this._color = color;
            this._speed = speed;
            this._scale = scale;
            this._isRushing = isRushing;
            _position = new Vector2(600, 600);
        }

        // Accessors
        public string GetColor() { return _color; }
        public float GetSpeed() { return _speed; }
        public float GetScale() { return _scale; }
        public bool GetIsRushing() { return _isRushing; }
        public Vector2 GetPosition() { return _position; }

        // Mutators
        public void SetColor(string color) { _color = color; }
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
                currentSpeed = _speed + 2.0f;
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
    }
}
