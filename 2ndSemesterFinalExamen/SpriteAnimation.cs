using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2ndSemesterFinalExamen
{
     class SpriteManager : Component
    {
        protected Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        public Color Color = Color.White;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;
        protected Rectangle[] Rectangles;
        protected int FrameIndex = 0;
       
        public SpriteManager(Texture2D Texture, int frames)
        {
            this.Texture = Texture;
            int width = Texture.Width / frames;
            Rectangles = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
                Rectangles[i] = new Rectangle(i * width, 0, width, Texture.Height);
        }



		
        public override void  Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Rectangles[FrameIndex], Color, Rotation, Origin, Scale, SpriteEffect, 0f);
        }
    }

     class SpriteAnimation : SpriteManager
    {
        private float timeElapsed;
        public bool IsLooping = true;
        private float timeToUpdate; //default, you may have to change it
        public int FramesPerSecond { set { timeToUpdate = (1f / value); } }
        public SpriteAnimation anim;

        public SpriteAnimation[] animations = new SpriteAnimation[4];
        public SpriteAnimation(Texture2D Texture, int frames, int fps) : base(Texture, frames) {
            FramesPerSecond = fps;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (FrameIndex < Rectangles.Length - 1)
                    FrameIndex++;

                else if (IsLooping)
                    FrameIndex = 0;
            }

                
                anim = animations[(int)gameObject.transform.direction];

                anim.Position = new Vector2(gameObject.transform.Position.X - 48, gameObject.transform.Position.Y - 48);
                if (kState.IsKeyDown(Keys.Space))
                {
                    anim.setFrame(0);
                }
                else if (gameObject.transform.isMoving)
                {
                    anim.Update(gameTime);
                }
                else
                {
                    anim.setFrame(1);
                }
        }

        public void setFrame(int frame)
        {
            FrameIndex = frame;
        }

       
    }
}