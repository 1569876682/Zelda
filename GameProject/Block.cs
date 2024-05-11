using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace GameProject
{
    public class Item
    {
        public string Name { get; set; }
        public string TexturePath { get; set; } // 假设我们使用纹理路径来引用物品图像  
                                                // ... 其他属性，如描述、属性等  
    }

    // ItemList 类，用于管理物品列表              
    public class ItemList
    {
        public List<Item> items = new List<Item>();

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public Item GetCurrentItem()
        {
            // 这里简化为只返回第一个物品，实际应用中可能需要根据索引或其他逻辑来选择  
            return items.FirstOrDefault();
        }

        // 其他方法，如切换物品、获取物品列表等  
    }
    internal class Block:Entity
    {
        ItemList itemList;
        int currentItemIndex = 0;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        
        public Block(Vector2 position) : base(position)
        {

        }
        public override void Initialize()
        {
            animator = new Animator(this);
            itemList = new ItemList();
        }
        public override void LoadContent(ContentManager content)
        {
            animator.CreateAnimation(content, "barrier1", "barrier1", 1, 1, 1, true);
            animator.CreateAnimation(content, "barrier2", "barrier2", 1, 1, 1, true);
            animator.CreateAnimation(content, "barrier3", "barrier3", 1, 1, 1, true);
            animator.SetDefaultAnimation("barrier1");
            itemList.AddItem(new Item { Name = "barrier1", TexturePath = "barrier1" });
            itemList.AddItem(new Item { Name = "barrier2", TexturePath = "barrier2" });
            itemList.AddItem(new Item { Name = "barrier3", TexturePath = "barrier3" });
        }
        public override void Update(GameTime gameTime)
        {

            currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.IsKeyDown(Keys.Y) && previousKeyboardState.IsKeyUp(Keys.Y))
            {
                // 切换到下一个物品（这里简化为循环切换）  
                currentItemIndex = (currentItemIndex + 1) % itemList.items.Count;
                // 加载新物品的纹理（注意：你应该在物品列表初始化时加载所有纹理，而不是在Update中）  
                // 假设你有一个方法来获取当前物品的纹理，这里只是一个示意性的代码  
                //currentItemTexture = GetItemTexture(currentItemIndex); 
                animator.ChangeAnimation(itemList.items[currentItemIndex].Name);
                
            }
            else if (currentKeyboardState.IsKeyDown(Keys.T) && previousKeyboardState.IsKeyUp(Keys.T))
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
