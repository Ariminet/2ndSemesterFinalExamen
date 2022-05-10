using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text;


namespace GamePatternsProject
{
	public class Component : ICloneable
	{
		public bool isEnabled { get; set; }
		public GameObject gameObject { get; set; }
        public string BoxName { get; private set; }

		public virtual void Awake()
        {

        }

		public virtual void Start()
        {

        }

		public virtual void Update()
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
