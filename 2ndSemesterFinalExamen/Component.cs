using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace _2ndSemesterFinalExamen
{
	 class Component : ICloneable
	{
		public bool isEnabled { get; set; }
		public GameObject gameObject { get; set; }

		public virtual void Awake()
        {

        }

		public virtual void Start()
        {

        }

		public virtual void Update(GameTime gameObject)
        {
            
        }

		public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

		public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
	}
}
