using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace GameProject
{
    internal class Mono:Entity
    {
        ItemList itemList;
        int currentItemIndex = 0;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        public Mono(Vector2 position) : base(position)
        {
            
        }
        public override void Initialize()
        {
            animator = new Animator(this);
            itemList = new ItemList();
        }
        public override void LoadContent(ContentManager content)
        {
            animator.CreateAnimation(content, "Love", "Love", 1, 1, 1, true);
            animator.CreateAnimation(content, "Ball", "Ball", 1, 1, 1, true);
            animator.CreateAnimation(content, "Eye", "Eye", 1, 1, 1, true);
            animator.SetDefaultAnimation("Love");
            itemList.AddItem(new Item { Name = "Love", TexturePath = "Love" });
            itemList.AddItem(new Item { Name = "Ball", TexturePath = "Ball" });
            itemList.AddItem(new Item { Name = "Eye", TexturePath = "Eye" });
        }
        public override void Update(GameTime gameTime)
        {

            currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.IsKeyDown(Keys.I) && previousKeyboardState.IsKeyUp(Keys.I))
            {
                // 切换到下一个物品（这里简化为循环切换）  
                currentItemIndex = (currentItemIndex + 1) % itemList.items.Count;
                // 加载新物品的纹理（注意：你应该在物品列表初始化时加载所有纹理，而不是在Update中）  
                // 假设你有一个方法来获取当前物品的纹理，这里只是一个示意性的代码  
                //currentItemTexture = GetItemTexture(currentItemIndex); 
                animator.ChangeAnimation(itemList.items[currentItemIndex].Name);

            }
            else if (currentKeyboardState.IsKeyDown(Keys.U) && previousKeyboardState.IsKeyUp(Keys.U))
            {
                // 切换到下一个物品（这里简化为循环切换）  
                currentItemIndex = (currentItemIndex - 1 + itemList.items.Count) % itemList.items.Count;
                // 加载新物品的纹理（注意：你应该在物品列表初始化时加载所有纹理，而不是在Update中）  
                // 假设你有一个方法来获取当前物品的纹理，这里只是一个示意性的代码  
                //currentItemTexture = GetItemTexture(currentItemIndex); 
                animator.ChangeAnimation(itemList.items[currentItemIndex].Name);
            }
            // 将当前状态保存为下一帧的“之前”状态  
            previousKeyboardState = currentKeyboardState;
        }
    }
}
