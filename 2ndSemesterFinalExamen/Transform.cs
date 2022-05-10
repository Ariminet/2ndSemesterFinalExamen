using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GamePatternsProject
{
    public class Transform
    {
        public Vector2 Position { get; set; } = Vector2.Zero;

        public void Translate(Vector2 translation)
        {
            Position += translation;
        }
    }
}
