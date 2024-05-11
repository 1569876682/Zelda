using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    internal class Boomerang : Entity
    {
        public Boomerang(Vector2 position) : base(position) { }

        bool isBoomerangAttack;
        bool isAttFrward;
        float attackDis;
        FacingDir nowDirect;

        public override void Initialize()
        {
            isBoomerangAttack = false;
            isAttFrward = false;
            attackDis = 0.0f;
            nowDirect = FacingDir.Right;
            animator = new(this);
            Disable();
        }
        public override void LoadContent(ContentManager content)
        {
            animator.CreateAnimation(content, "Boomerang1", "Boomerang1", 3, 3, 7, true);
            animator.CreateAnimation(content, "Boomerang2", "Boomerang2", 3, 3, 7, true);
            animator.SetDefaultAnimation("Boomerang1");
        }
        public override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.NumPad3) || kstate.IsKeyDown(Keys.D3))
            {
                if (!isBoomerangAttack)
                {
                    nowDirect = Player.instance.facingDir;
                    position = Player.instance.position;
                    if (nowDirect == FacingDir.Up)
                    {
                        position.Y -= 5.0f;
                    }
                    else if (nowDirect == FacingDir.Down)
                    {
                        position.Y += 5.0f;
                        position.X += 15.0f;
                    }
                    else if (nowDirect == FacingDir.Left)
                    {
                        position.X -= 5.0f;
                    }
                    else
                    {
                        position.X += 5.0f;
                    }
                    isBoomerangAttack = true;
                    isAttFrward = true;
                    Enable();
                }
            }
            if (isBoomerangAttack)
            {
                if (nowDirect != Player.instance.facingDir)
                {
                    attackDis = 0.0f;
                }
                nowDirect = Player.instance.facingDir;
                if (nowDirect == FacingDir.Up)
                {
                    animator.ChangeAnimation("Boomerang2");
                    if (isAttFrward)
                    {
                        attackDis -= 5.0f;
                        if (attackDis <= -150.0f)
                        {
                            isAttFrward = false;
                        }
                    }
                    else
                    {
                        attackDis += 5.0f;
                        if (attackDis == 0.0f)
                        {
                            isBoomerangAttack = false;
                            Disable();
                        }
                    }
                    position.Y = Player.instance.position.Y + attackDis - 5.0f;
                }
                else if (nowDirect == FacingDir.Down)
                {
                    animator.ChangeAnimation("Boomerang1");
                    if (isAttFrward)
                    {
                        attackDis += 5.0f;
                        if (attackDis >= 150.0f)
                        {
                            isAttFrward = false;
                        }
                    }
                    else
                    {
                        attackDis -= 5.0f;
                        if (attackDis == 0.0f)
                        {
                            isBoomerangAttack = false;
                            Disable();
                        }
                    }
                    position.Y = Player.instance.position.Y + attackDis + 5.0f;
                    position.X = Player.instance.position.X + 15.0f;
                }
                else if (nowDirect == FacingDir.Left)
                {
                    animator.ChangeAnimation("Boomerang1");
                    if (isAttFrward)
                    {
                        attackDis -= 5.0f;
                        if (attackDis <= -150.0f)
                        {
                            isAttFrward = false;
                        }
                    }
                    else
                    {
                        attackDis += 5.0f;
                        if (attackDis == 0.0f)
                        {
                            isBoomerangAttack = false;
                            Disable();
                        }
                    }
                    position.X = Player.instance.position.X + attackDis - 5.0f;
                }
                else
                {
                    animator.ChangeAnimation("Boomerang2");
                    if (isAttFrward)
                    {
                        attackDis += 5.0f;
                        if (attackDis >= 150.0f)
                        {
                            isAttFrward = false;
                        }
                    }
                    else
                    {
                        attackDis -= 5.0f;
                        if (attackDis == 0.0f)
                        {
                            isBoomerangAttack = false;
                            Disable();
                        }
                    }
                    position.X = Player.instance.position.X + attackDis + 5.0f;
                }
                animator.Update();
            }
        }
    }
}
