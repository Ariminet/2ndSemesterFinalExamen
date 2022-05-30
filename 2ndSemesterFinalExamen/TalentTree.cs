using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2ndSemesterFinalExamen
{
    class TalentTree
    {
        public List<Talent> talentTree = new List<Talent>();
        public List<TalentEdges> talentConnections = new List<TalentEdges>();

        public Graph graph = new Graph();

        private static TalentTree instance;
        public static TalentTree Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TalentTree();
                }
                return instance;
            }
        }



        public void GraphFill()
        {

            talentTree = Game1.Instance.GameDB.GetTalents(((Player)Game1.Instance.Player.GetComponent<Player>()));
            foreach (Talent t in talentTree)
            {
                graph.AddTalent(t.Tag, t.MaxLevel, t.CurrentLevel, t.Description);

            }
            //graph.AddTalent("Shoot", 1, 1,"blabla");
            //graph.AddTalent("Faster shots", 3, 0, "blabla");
            //graph.AddTalent("Quick reload", 3, 0, "blabla");
            //graph.AddTalent("Speed", 3, 0, "blabla");
            //graph.AddTalent("Spray shot", 1, 0, "blabla");
            //graph.AddTalent("Damage", 3, 0, "blabla");
            //graph.AddTalent("Bigger projectiles", 2, 0, "blabla");
            //graph.AddTalent("Piercing shot", 1, 0, "blabla");
            //graph.AddTalent("Dash", 1, 0, "blabla");
            //graph.AddTalent("Bullet shield", 2, 0, "blabla");
            //graph.AddTalent("Explosive shot", 1, 0, "blabla");
            //graph.AddTalent("Explode", 1, 0, "blabla");

            talentConnections = Game1.Instance.GameDB.GetTalentConnections();
            foreach (TalentEdges t in talentConnections)
            {
                graph.AddEdge(t.StartEdge, t.EndEdge);
            }
            //graph.AddEdge("Shoot", "Faster shots");
            //graph.AddEdge("Shoot", "Quick reload");
            //graph.AddEdge("Shoot", "Speed");
            //graph.AddEdge("Faster shots", "Spray shot");
            //graph.AddEdge("Spray shot", "Damage");
            //graph.AddEdge("Spray shot", "Bullet shield");
            //graph.AddEdge("Quick reload", "Bigger projectiles");
            //graph.AddEdge("Quick reload", "Piercing shot");
            //graph.AddEdge("Bigger projectiles", "Explosive shot");
            //graph.AddEdge("Explosive shot", "Explode");
            //graph.AddEdge("Speed", "Dash");

            Game1.Instance.talenTreeCreated = true;
        }

            public Talent DFS(Talent start, Talent goal)
            {
                Stack<Edge> edges = new Stack<Edge>();
                edges.Push(new Edge(start, start));

                while (edges.Count > 0)
                {
                    Edge edge = edges.Pop();
                    if (!edge.To.Discovered)
                    {
                        edge.To.Discovered = true;
                        edge.To.Parent = edge.From;

                    }
                    if (edge.To == goal)
                    {
                        return edge.To;
                    }

                    foreach (Edge e in edge.To.edges)
                    {
                        if (!e.To.Discovered)
                        {
                            edges.Push(e);
                        }
                    }
                }
                return null;
            }


           
            

        
            void Draw(SpriteBatch spBt, Texture2D shot)
            {
                while (Game1.Instance.gameState == GameStates.InGame)
                {
                    spBt.Draw(shot, new Vector2(50, 50), Color.White);
                }
            }
    }
}

