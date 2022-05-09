using System;
using System.Collections.Generic;
using System.Text;

namespace _2ndSemesterFinalExamen
{
    class GameUnit
{
        public string Tag { get; private set; }
        public int Health { get; private set; }
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        public GameUnit (string Tag, int Health, int PosX, int PosY)
		{
            this.Tag = Tag;
            this.Health = Health;
            this.PosX = PosX;
            this.PosY = PosY;
		}
}
}
