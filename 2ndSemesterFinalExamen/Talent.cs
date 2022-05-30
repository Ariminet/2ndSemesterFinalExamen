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

        public string Description { get; private set; }

        public List<Edge> edges { get; private set; } = new List<Edge>();

        public bool Discovered { get; set; } = false;

        public Talent Parent { get; set; }

        public bool Locked { get; set; }

        public Talent(string Tag, int MaxLevel, int CurrentLevel, string Description)
		{
            this.Tag = Tag;
            this.MaxLevel = MaxLevel;
            this.CurrentLevel = CurrentLevel;
            this.Description = Description;

        }

        public void AddEdge(Talent other)
        {
            edges.Add(new Edge(this, other));
        }
}
}
