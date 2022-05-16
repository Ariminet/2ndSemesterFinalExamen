using System;
using System.Collections.Generic;
using System.Text;

namespace _2ndSemesterFinalExamen
{
    class Graph
{
        public List<Talent> Talents { get; private set; } = new List<Talent>();

        public void AddTalent(string tag, int maxlevel, int Currentlevel, string Description)
        {
            Talents.Add(new Talent(tag, maxlevel, Currentlevel, Description));
        }

        public void AddDirectionalEdge (string from, string to)
        {
            Talent fromTalent = Talents.Find(x => x.Tag.Equals(from));
            Talent toTalent = Talents.Find(x => x.Tag.Equals(to));

            if (!fromTalent.Equals(default(string)) && !toTalent.Equals(default(string)))
            {
                fromTalent.AddEdge(toTalent);
            }
            else
            {
                Console.WriteLine("Node not found!");
            }
        }

        public void AddEdge(string from, string to)
        {
            Talent fromTalent = Talents.Find(x => x.Tag.Equals(from));
            Talent toTalent = Talents.Find(x => x.Tag.Equals(to));

            if (!fromTalent.Equals(default(string)) && !toTalent.Equals(default(string)))
            {
                fromTalent.AddEdge(toTalent);
                toTalent.AddEdge(fromTalent);
            }

            else
            {
                Console.WriteLine("Node not found!");
            }
        }
}
}
