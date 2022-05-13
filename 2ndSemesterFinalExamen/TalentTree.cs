using System;
using System.Collections.Generic;
using System.Text;

namespace _2ndSemesterFinalExamen
{
    class TalentTree
{
        static void Main()
        {
            Graph graph = new Graph();

            graph.AddTalent("Shoot", 1, 1);
            graph.AddTalent("Faster shots", 3, 0);
            graph.AddTalent("Quick reload", 3, 0);
            graph.AddTalent("Speed", 3, 0);
            graph.AddTalent("Spray shot", 1, 0);
            graph.AddTalent("Bigger projectiles", 2, 0);
            graph.AddTalent("Piercing shot", 1, 0);
            graph.AddTalent("Dash", 1, 0);
            graph.AddTalent("Bullet shield", 2, 0);
            graph.AddTalent("Explosive shot", 1, 0);
            graph.AddTalent("Explode", 1, 0);


        }


}
}
