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
        static void GraphMake()
        {
            Graph graph = new Graph();

            graph.AddTalent("Shoot", 1, 1);
            graph.AddTalent("Faster shots", 3, 0);
            graph.AddTalent("Quick reload", 3, 0);
            graph.AddTalent("Speed", 3, 0);
            graph.AddTalent("Spray shot", 1, 0);
            graph.AddTalent("Damage", 3, 0);
            graph.AddTalent("Bigger projectiles", 2, 0);
            graph.AddTalent("Piercing shot", 1, 0);
            graph.AddTalent("Dash", 1, 0);
            graph.AddTalent("Bullet shield", 2, 0);
            graph.AddTalent("Explosive shot", 1, 0);
            graph.AddTalent("Explode", 1, 0);

            graph.AddEdge("Shoot", "Faster shots");
            graph.AddEdge("Shoot", "Quick reload");
            graph.AddEdge("Shoot", "Speed");
            graph.AddEdge("Faster shots", "Spray shot");
            graph.AddEdge("Spray shot", "Damage");
            graph.AddEdge("Spray shot", "Bullet shield");
            graph.AddEdge("Quick reload", "Bigger projectiles");
            graph.AddEdge("Quick reload", "Piercing shot");
            graph.AddEdge("Bigger projectiles", "Explosive shot");
            graph.AddEdge("Explosive shot", "Explode");
            graph.AddEdge("Speed", "Dash");


            static Talent DFS(Talent start, Talent goal)
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


            static void Draw(SpriteBatch spBt, Texture2D shot)
            {
                while (Game1.gameState == GameStates.InGame)
                {
                    spBt.Draw(shot, new Vector2(50, 50), Color.White);
                }
            }


        }
    }
}

