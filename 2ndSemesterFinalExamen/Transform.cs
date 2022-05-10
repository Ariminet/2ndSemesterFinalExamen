using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2ndSemesterFinalExamen
{
     class Transform
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public bool isMoving { get;  set; } = false;
        public Dir direction { get; set; } = Dir.Down;
        public void Translate(Vector2 translation)
        {
            Position += translation;
        }

        

    }
}
