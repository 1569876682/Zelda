using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace GameProject
{
    public enum FacingDir
    {
        Right, Left, Up,Down
    }
    internal class Player:Entity
    {
        public static Player instance;
        
        public FacingDir facingDir;
        float moveSpeed;
        public Player(Vector2 position) : base(position) { }
        public override void Initialize()
        {
            animator = new Animator(this);
            moveSpeed = 100.0f;
            facingDir = FacingDir.Right;
            if(instance == null)
                instance = this;
        }
        public override void LoadContent(ContentManager content)
        {
            animator.CreateAnimation(content, "PlayerMoveUp","PlayerMoveUp", 6, 6,9,true);
            animator.CreateAnimation(content, "PlayerMoveDown","PlayerMoveDown", 6, 6,9, true);
            animator.CreateAnimation(content, "PlayerMoveLeft","PlayerMoveLeft", 6, 6, 9, true);
            animator.CreateAnimation(content, "PlayerMoveRight","PlayerMoveRight", 6, 6,9, true);
            animator.CreateAnimation(content, "PlayerMoveRight", "PlayerIdleRight", 6, 1, 9, true);
            animator.CreateAnimation(content, "PlayerMoveLeft", "PlayerIdleLeft", 6, 1, 9, true);
            animator.CreateAnimation(content, "PlayerMoveUp", "PlayerIdleUp", 6, 1, 9, true);
            animator.CreateAnimation(content, "PlayerMoveDown", "PlayerIdleDown", 6, 1, 9, true);
            animator.CreateAnimation(content, "PlayerHurt", "PlayerHurt", 1, 1, 1, false);
            animator.SetDefaultAnimation("PlayerIdleRight");
        }
        public override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();
            bool isMove = false;
            bool isHurt = false;
            if (kstate.IsKeyDown(Keys.E))
            {
                animator.ChangeAnimation("PlayerHurt");
                isHurt = true;
            }
            else if (kstate.IsKeyDown(Keys.Up)|| kstate.IsKeyDown(Keys.W))
            {
                position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                animator.ChangeAnimation("PlayerMoveUp");
                facingDir = FacingDir.Up;
                animator.SetDefaultAnimation("PlayerIdleUp");
                isMove = true;
            }
            else if (kstate.IsKeyDown(Keys.Down)|| kstate.IsKeyDown(Keys.S))
            {
                position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                animator.ChangeAnimation("PlayerMoveDown");
                facingDir = FacingDir.Down;
                animator.SetDefaultAnimation("PlayerIdleDown");
                isMove = true;
            }
            else if (kstate.IsKeyDown(Keys.Left)|| kstate.IsKeyDown(Keys.A))
            {
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                animator.ChangeAnimation("PlayerMoveLeft");
                facingDir = FacingDir.Left;
                animator.SetDefaultAnimation("PlayerIdleLeft");
                isMove = true;
            }
            else if (kstate.IsKeyDown(Keys.Right)|| kstate.IsKeyDown(Keys.D))
            {
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                animator.ChangeAnimation("PlayerMoveRight");
                facingDir = FacingDir.Right;
                animator.SetDefaultAnimation("PlayerIdleRight");
                isMove = true;
            }
            else
            {
                if(facingDir == FacingDir.Right)
                {
                    animator.ChangeAnimation("PlayerIdleRight");
                }
                else if(facingDir == FacingDir.Left)
                {
                    animator.ChangeAnimation("PlayerIdleLeft");
                }
                else if(facingDir == FacingDir.Up)
                {
                    animator.ChangeAnimation("PlayerIdleUp");
                }
                else
                {
                    animator.ChangeAnimation("PlayerIdleDown");
                }
            }
            
            animator.Update();
        }
        
    }
}
