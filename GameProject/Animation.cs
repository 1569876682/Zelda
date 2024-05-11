using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameProject
{
    internal class Animation
    {
        public event EventHandler OnAnimationOvered;
        int currentFrame;
        Texture2D texture;
        protected int columnsPerRow;
        int row;
        int column;
        protected int pictureNum;
        protected int framesPerPicture;
        bool isLoop;
        public string name {  get; private set; }
        public Animation(ContentManager content,string pictureName,string animationName, int columnsPerRow, int pictureNums,int framesPerPicture,bool isLoop)
        {

            texture = content.Load<Texture2D>(pictureName);
            name = animationName;
            this.columnsPerRow = columnsPerRow;
            this.pictureNum=pictureNums;
            this.framesPerPicture = framesPerPicture;
            this.isLoop = isLoop;
        }
        public void InitFrame()
        {
            currentFrame = 0;
        }
        public void Draw(SpriteBatch spriteBatch,int x,int y)
        {
            int width = texture.Width / columnsPerRow;
            int height;
            if (pictureNum % columnsPerRow != 0)
            {
                height = texture.Height / (pictureNum / columnsPerRow+1);
            }
            else
            {
                height = texture.Height / (pictureNum / columnsPerRow);
            }
            row = 0;
            int k = Math.Min(columnsPerRow, pictureNum);
            if (column == k - 1)
            {
                column = (currentFrame % (k * framesPerPicture)) / framesPerPicture;
                if(column==0)
                {
                    if(!isLoop)
                        OnAnimationOvered?.Invoke(this,EventArgs.Empty);
                }
            }
            else
            {
                column = (currentFrame % (k * framesPerPicture)) / framesPerPicture;
            }

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle(x, y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            
            spriteBatch.End();
        }
        public void Update()
        {
            currentFrame++;
        }
    }
}
