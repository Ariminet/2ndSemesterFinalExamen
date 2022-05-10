using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace _2ndSemesterFinalExamen
{
	 class GameObject : ICloneable
	{
		public Transform transform { get; set; } = new Transform();
		List<Component> components = new List<Component>();
		
		public Component AddComponent(Component c)
        {
			c.gameObject = this;
			components.Add(c);
			return c;
        }

		public Component GetComponent<T>() where T: Component
        {
			return components.Find(x => x.GetType() == typeof(T));
        }


		public void Awake()
        {
			foreach (Component c in components)
            {
				c.Awake();
            }
        }

		public void Start()
		{
			foreach (Component c in components)
			{
				c.Start();
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach (Component c in components)
			{
				c.Update(gameTime);

			}
		}

		public void Draw(SpriteBatch spriteBatch)
        {
			foreach (Component c in components)
            {
				c.Draw(spriteBatch);
            }
		}

		public object Clone()
        {
			GameObject go = new GameObject();
			foreach(Component c in components)
            {
				go.AddComponent(c.Clone() as Component);
            }
			return go;
        }
	

	}
}
