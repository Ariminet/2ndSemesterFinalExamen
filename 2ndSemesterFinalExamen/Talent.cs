using System;
using System.Collections.Generic;
using System.Text;

namespace _2ndSemesterFinalExamen
{
    class Talent
{
        public string Tag { get; private set; }
        public int MaxLevel { get; private set; }
        public int CurrentLevel { get; set; }

        public Talent(string Tag, int MaxLevel, int CurrentLevel)
		{
            this.Tag = Tag;
            this.MaxLevel = MaxLevel;
            this.CurrentLevel = CurrentLevel;
		}
}
}
