using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace GameProject
{
    internal class Entity
    {
        
        public bool isEnabled;
        public bool isDestroyed { get; private set; }
        public Vector2 position;
        protected Animator animator;
        public Entity(Vector2 position)
        {
            
            this.position = position;
            isEnabled = true;
            isDestroyed = false;
        }
        public virtual void LoadContent(ContentManager content)
        {

        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Initialize()
        {

        }
        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            animator.Draw(spriteBatch);
        }
        public void Destroy()
        {
            isDestroyed = true;
        }
        public void Enable()
        {
            isEnabled = true;
        }
        public void Disable()
        {
            isEnabled = false;
        }
    }
}
