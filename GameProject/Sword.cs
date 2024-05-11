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
    internal class Sword:Entity
    {
        public Sword(Vector2 position) : base(position) { }

        bool isSwordAttack;
        bool isAttFrward;
        float attackDis;
        FacingDir nowDirect;

        public override void Initialize()
        {
            isSwordAttack = false;
            isAttFrward = false;
            attackDis = 0.0f;
            nowDirect = FacingDir.Right;
            animator = new(this);
            Disable();
        }
        public override void LoadContent(ContentManager content)
        {
            animator.CreateAnimation(content, "SwordUp", "SwordUp", 1, 1, 1, true);
            animator.CreateAnimation(content, "SwordDown", "SwordDown", 1, 1, 1, true);
            animator.CreateAnimation(content, "SwordLeft", "SwordLeft", 1, 1, 1, true);
            animator.CreateAnimation(content, "SwordRight", "SwordRight", 1, 1, 1, true);
            animator.SetDefaultAnimation("SwordUp");
        }
        public override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Z) || kstate.IsKeyDown(Keys.N))
            {
                if (!isSwordAttack)
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
                        position.Y += 10.0f;
                    }
                    else
                    {
                        position.X += 5.0f;
                        position.Y += 9.0f;
                    }
                    isSwordAttack = true;
                    isAttFrward = true;
                    Enable();
                }
            }
            if (isSwordAttack)
            {
                if (nowDirect != Player.instance.facingDir)
                {
                    attackDis = 0.0f;
                }
                nowDirect = Player.instance.facingDir;
                if (nowDirect == FacingDir.Up)
                {
                    animator.ChangeAnimation("SwordUp");
                    if (isAttFrward)
                    {
                        attackDis -= 1.0f;
                        if (attackDis <= -10.0f)
                        {
                            isAttFrward = false;
                        }
                    }
                    else
                    {
                        attackDis += 1.0f;
                        if (attackDis == 0.0f)
                        {
                            isSwordAttack = false;
                            Disable();
                        }
                    }
                    position.Y = Player.instance.position.Y + attackDis - 5.0f;
                }
                else if (nowDirect == FacingDir.Down)
                {
                    animator.ChangeAnimation("SwordDown");
                    if (isAttFrward)
                    {
                        attackDis += 1.0f;
                        if (attackDis >= 10.0f)
                        {
                            isAttFrward = false;
                        }
                    }
                    else
                    {
                        attackDis -= 1.0f;
                        if (attackDis == 0.0f)
                        {
                            isSwordAttack = false;
                            Disable();
                        }
                    }
                    position.Y = Player.instance.position.Y + attackDis + 5.0f;
                    position.X = Player.instance.position.X + 15.0f;
                }
                else if (nowDirect == FacingDir.Left)
                {
                    animator.ChangeAnimation("SwordLeft");
                    if (isAttFrward)
                    {
                        attackDis -= 1.0f;
                        if (attackDis <= -10.0f)
                        {
                            isAttFrward = false;
                        }
                    }
                    else
                    {
                        attackDis += 1.0f;
                        if (attackDis == 0.0f)
                        {
                            isSwordAttack = false;
                            Disable();
                        }
                    }
                    position.X = Player.instance.position.X + attackDis - 5.0f;
                    position.Y = Player.instance.position.Y + 10.0f;
                }
                else
                {
                    animator.ChangeAnimation("SwordRight");
                    if (isAttFrward)
                    {
                        attackDis += 1.0f;
                        if (attackDis >= 10.0f)
                        {
                            isAttFrward = false;
                        }
                    }
                    else
                    {
                        attackDis -= 1.0f;
                        if (attackDis == 0.0f)
                        {
                            isSwordAttack = false;
                            Disable();
                        }
                    }
                    position.X = Player.instance.position.X + attackDis + 5.0f;
                    position.Y = Player.instance.position.Y + 9.0f;
                }
            }
        }
    }
}
