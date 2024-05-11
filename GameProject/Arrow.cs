using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace GameProject
{

    internal class Arrow : Entity
    {

        float moveSpeed;
        bool isFiring = false;
        FacingDir dir;
        public Arrow(Vector2 position) : base(position) { }
        public override void Initialize()
        {
            animator = new Animator(this);
            moveSpeed = 300.0f;
            Disable();
        }
        public override void LoadContent(ContentManager content)
        {
            //第二个参数是文件名，第三个是动画名，第四个是一行有几个，第五个是一共几张图，第六个是一个图显示几帧，第七个是循环播放。
            animator.CreateAnimation(content, "arrowLeft", "arrowLeft", 1, 1, 9, true);
            animator.CreateAnimation(content, "arrowRight", "arrowRight", 1, 1, 9, true);
            animator.CreateAnimation(content, "arrowUp", "arrowUp", 1, 1, 9, true);
            animator.CreateAnimation(content, "arrowDown", "arrowDown", 1, 1, 9, true);
            animator.SetDefaultAnimation("arrowLeft");

        }
        public override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            //发射火焰
            if (isFiring == false && kstate.IsKeyDown(Keys.D2))
            {
                Enable();
                position = Player.instance.position;
                dir = Player.instance.facingDir;
                isFiring = true; // 如果F键被按下，则开始发射火焰
            }
            // Player.instance.facingDir=FacingDir.

            if (isFiring)
            {
                // 当isFiring为true时，无论是否按下F键，火焰都会向右移动


                if (dir == FacingDir.Left)
                {
                    animator.ChangeAnimation("arrowLeft");
                    position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (dir == FacingDir.Right)
                {
                    animator.ChangeAnimation("arrowRight");
                    position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (dir == FacingDir.Up)
                {
                    animator.ChangeAnimation("arrowUp");
                    position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (dir == FacingDir.Down)
                {
                    animator.ChangeAnimation("arrowDown");
                    position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            animator.Update();

            if (position.X <= 0)
            {
                isFiring = false;
                Disable();
            }
            if (position.Y <= 0)
            {
                isFiring = false;
                Disable();
            }
            if (position.X >= 800)
            {
                isFiring = false;
                Disable();
            }
            if (position.Y >= 600)
            {
                isFiring = false;
                Disable();
            }
        }

    }
}
