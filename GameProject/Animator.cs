using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameProject
{
    internal class Animator
    {
        private Animation currentAnimation;
        private Animation defaultAnimation;
        private List<Animation> animations;
        private Entity entity;
        public Animator(Entity _entity)
        {
            animations = new List<Animation>();
            entity = _entity;
        }
        public void ChangeAnimation(string animationName)
        {
            if(currentAnimation != null && currentAnimation.name== animationName)
            {
                return;
            }
            foreach(var animation in animations)
            {
                if(animation.name == animationName)
                {
                    
                    currentAnimation = animation;
                    currentAnimation.InitFrame();
                }
            }
        }
        public void CreateAnimation(ContentManager content,string pictureName,string animationName,int columnsPerRow,int pictureNums,int framesPerPicture,bool isLoop)
        {
            Animation anim = new Animation(content, pictureName, animationName, columnsPerRow, pictureNums, framesPerPicture, isLoop);
            animations.Add(anim);
            anim.OnAnimationOvered += Anim_OnAnimationOvered;
        }

        private void Anim_OnAnimationOvered(object sender, System.EventArgs e)
        {
            currentAnimation = defaultAnimation;
            currentAnimation.InitFrame();
        }

        public void SetDefaultAnimation(string animationName)
        {
            
            foreach(var animation in animations)
            {
                if (animation.name == animationName)
                {
                    if (currentAnimation == null)
                    {
                        currentAnimation = animation;
                    }
                    defaultAnimation = animation;
                }
            }
        }

        public void Update()
        {
            if (currentAnimation != null)
            {
                currentAnimation.Update();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
            if (currentAnimation != null)
            {
                currentAnimation.Draw(spriteBatch, (int)entity.position.X, (int)entity.position.Y);
            }
        }
        
    }
}
