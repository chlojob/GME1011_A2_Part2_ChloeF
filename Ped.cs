using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GME1011_A2_Part2_ChloeF
{
    public class Ped
    {
        // Attributes
        private string color;
        private float speed;
        private float scale;
        private bool isRushing;

        // Position
        public Vector2 position;

        // Zero-argument constructor
        public Ped()
        {
            color = "white";
            speed = 2.5f;
            scale = 1.0f;
            isRushing = false;
            position = new Vector2(200, 600);
        }

        // Argumented constructor
        public Ped(string color, float speed, float scale, bool isRushing)
        {
            this.color = color;
            this.speed = speed;
            this.scale = scale;
            this.isRushing = isRushing;
            position = new Vector2(600, 600);
        }
    }
}
