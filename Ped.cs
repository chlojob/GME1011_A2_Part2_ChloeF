using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
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
        public Vector2 position;

        // Zero-argument constructor
        public Ped()
        {
            _color = "white";
            _speed = 2.5f;
            _scale = 1.0f;
            _isRushing = false;
            position = new Vector2(200, 600);
        }

        // Argumented constructor
        public Ped(string color, float speed, float scale, bool isRushing)
        {
            this._color = color;
            this._speed = speed;
            this._scale = scale;
            this._isRushing = isRushing;
            position = new Vector2(600, 600);
        }

        // Accessors
        public string GetColor() { return _color; }
        public float GetSpeed() { return _speed; }
        public float GetScale() { return _scale; }
        public bool GetIsRushing() { return _isRushing; }

        // Mutators
        public void SetColor(string color) { _color = color; }
        public void SetSpeed(float speed) { _speed = speed; }
        public void SetScale(float scale) { _scale = scale; }
        public void SetIsRushing(bool isRushing) { _isRushing = isRushing; }

    }
}
