using System;
using System.Collections.Generic;
using System.Text;

namespace _2ndSemesterFinalExamen
{
    class Ability
    {
        public string Tag { get; private set; }
        public int Level { get; private set; }

        public Ability(string Tag, int Level)
		{
            this.Tag = Tag;
            this.Level = Level;
		}

        public void UseAbility()
		{
			switch (Tag)
			{
				case "Spray  Shots":
					break;
				case "Bigger Projectiles":
					break;
				case "Piercing Shot":
					break;
				case "Dash":
					break;
				case "Bullet Shield":
					break;
				case "Explosive Shot":
					break;
				case "Explode":
					break;
				default:
					break;
			}
		}
    }
}
