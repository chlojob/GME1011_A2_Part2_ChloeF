using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GME1011_A2_Part2_ChloeF
{
    public class Car : Vehicle
    {
        public Car(Texture2D texture, Vector2 position, float speed, int direction, Color color)
            : base(texture, position, speed, direction, color)
        {
        }
    }
}
