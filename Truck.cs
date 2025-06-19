using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GME1011_A2_Part2_ChloeF
{
    public class Truck : Vehicle
    {
        public Truck(Texture2D texture, Vector2 position, float speed, int direction, Color color)
            : base(texture, position, speed, direction, color)
        {
        }

        public override void Update()
        {
            _position.X += _speed * _direction * 0.8f;

            if (_direction == 1 && _position.X > 850)
                _position.X = -_texture.Width;

            if (_direction == -1 && _position.X < -_texture.Width)
                _position.X = 850;
        }
    }
}
