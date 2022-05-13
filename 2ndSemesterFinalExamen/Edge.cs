using System;
using System.Collections.Generic;
using System.Text;

namespace _2ndSemesterFinalExamen
{
    class Edge
{
        public Talent From { get; private set; }

        public Talent To { get; private set; }

        public Edge(Talent from, Talent to)
        {
            this.From = from;
            this.To = to;
        }
}
}
