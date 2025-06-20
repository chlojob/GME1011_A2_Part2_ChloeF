using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GME1011_A2_Part2_ChloeF
{
    public class Ped
    {
        // Attributes
        private Color _color;
        private float _speed;
        private float _scale;
        private bool _isRushing;

        private Texture2D _texture; 
        private Vector2 _position;  

        private Color[] _textureData;         // pixel data for pixel-perfect collision

        private SpriteEffects _spriteEffects = SpriteEffects.None;  // for flipping ped sprite

        // Constructor
        public Ped(Color color, float speed, float scale, bool isRushing, Texture2D texture)
        {
            _color = color;
            _speed = speed;
            _scale = scale;
            _isRushing = isRushing;
            _texture = texture;
            _position = new Vector2(400, 550);  // start near bottom middle

            // Grab texture pixel data for pixel-perfect collision
            _textureData = new Color[_texture.Width * _texture.Height];
            _texture.GetData(_textureData);
        }

        // Accessors, mutators
        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

            // Speed up when pressing space, aka "rushing"
            _isRushing = keyState.IsKeyDown(Keys.Space);
            float currentSpeed = _isRushing ? _speed + 2.5f : _speed;

            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
                _position.Y -= currentSpeed;
            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S))
                _position.Y += currentSpeed;

            // Move horizontally and flip sprite if moving left
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
            {
                _position.X -= currentSpeed;
                _spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
            {
                _position.X += currentSpeed;
                _spriteEffects = SpriteEffects.None;
            }

            // Keep player inside screen bounds
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
                _spriteEffects,
                0f
            );
        }

        // Collisions
        public Rectangle GetCollisionRect()
        {
            return new Rectangle(
                (int)(_position.X - _texture.Width / 2f),
                (int)(_position.Y - _texture.Height / 2f),
                _texture.Width,
                _texture.Height
            );
        }

        // Pixel-perfect collision check with vehicle rectangle
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

                    // If player pixel is visible (non-transparent), it's a hit
                    if (colorA.A > 10)
                        return true;
                }
            }

            return false;
        }
    }
}
