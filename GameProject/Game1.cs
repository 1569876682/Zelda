using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<Entity> entityList;
        private Animation2 animation;
        private Vector2 animationPosition;
        private float animationSpeed;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            entityList = new List<Entity>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            entityList.Add(new Block(new Vector2(500, 300)));
            entityList.Add(new Sword(new Vector2(500, 100)));
            entityList.Add(new Mono(new Vector2(200, 200)));
            entityList.Add(new Player(new Vector2(400,400)));
            entityList.Add(new Fire(new Vector2(0, 0)));
            entityList.Add(new Arrow(new Vector2(0, 0)));
            entityList.Add(new Boomerang(new Vector2(0, 0)));
            foreach (var t in entityList)
            {
                t.Initialize();
                
            }
            animationPosition = new Vector2(100, 100);
            animationSpeed = 100f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach(var t in entityList)
            {
                t.LoadContent(Content);
            }
            List<Texture2D> animationFrames = new List<Texture2D>();

            animationFrames.Add(Content.Load<Texture2D>("enemy11"));
            animationFrames.Add(Content.Load<Texture2D>("enemy22"));
            animationFrames.Add(Content.Load<Texture2D>("enemy33"));
            animationFrames.Add(Content.Load<Texture2D>("enemy44"));
            animationFrames.Add(Content.Load<Texture2D>("enemy55"));
            animationFrames.Add(Content.Load<Texture2D>("enemy6"));
            // ... 加载更多帧 ...  

            // 创建一个新的Animation实例  
            animation = new Animation2(animationFrames, 0.2f); // 假设每帧持续0.2秒  
            // TODO: use this.Content to load your game content here
        }
        private float moveDirection = 1f; // 1表示向下，-1表示向上  
        private List<Fireball> fireballs = new List<Fireball>();
        private float fireRate = 3.0f; // 每秒发射一个火球  
        private float lastFireTime = 0.0f;
        protected override void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();

            foreach (var t in entityList)
            {
                t.Update(gameTime);
            }
            for(int i=0;i<entityList.Count;i++)
            {
                if (entityList[i].isDestroyed)
                {
                    entityList.RemoveAt(i);
                    i--;
                }
            }
            if (currentKeyboardState.IsKeyDown(Keys.P) && previousKeyboardState.IsKeyUp(Keys.P))
            {
                if (animation.currentFrame % 2 == 0)
                    animation.nextone();

                else
                {
                    animation.currentFrame--;
                    animation.nextone();
                }
            }


            if (currentKeyboardState.IsKeyDown(Keys.O) && previousKeyboardState.IsKeyUp(Keys.O))
            {
                if (animation.currentFrame % 2 == 0)
                    animation.lastone();

                else
                {
                    animation.currentFrame--;
                    animation.lastone();
                }
            }
            previousKeyboardState = currentKeyboardState;
            animation.Update(gameTime);

            float elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // 更新动画位置  
            animationPosition.Y += 0.3f * moveDirection;
            animationPosition.X += 1.5f * 0.3f * moveDirection;


            if (animationPosition.Y <= 10 ||
                animationPosition.Y >= 200 || animationPosition.X <= 10)
            {
                moveDirection *= -1; // 反转移动方向  

            }




            lastFireTime += elapsedSeconds;

            if (lastFireTime >= fireRate) // 检查是否需要发射新的火球  
            {
                // 在这里假设动画的当前位置就是发射火球的位置  
                Vector2 fireballPosition = animationPosition;
                Vector2 fireballVelocity = new Vector2(50f, 0); // 假设火球向右上方发射  
                float maxLifetime = 4.0f; // 火球存在4秒  

                fireballs.Add(new Fireball(fireballPosition, fireballVelocity, maxLifetime));

                lastFireTime -= fireRate; // 重置计时器  
            }

            // 更新所有火球的位置和状态  
            for (int i = fireballs.Count - 1; i >= 0; i--)
            {
                fireballs[i].Update(gameTime);

                if (fireballs[i].Lifetime >= fireballs[i].MaxLifetime)
                {
                    fireballs.RemoveAt(i); // 移除过期的火球  
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            foreach (var t in entityList)
            {
                if (t.isEnabled)
                {
                    t.Draw(_spriteBatch, gameTime);
                }
            }
            _spriteBatch.Begin();

            animation.Draw(_spriteBatch, animationPosition, Color.White);

            foreach (var fireball in fireballs)
            {
                // 假设这里有一个用于绘制火球的Texture2D  
                Texture2D fireballTexture = Content.Load<Texture2D>("fire1"); // 你需要加载一个火球的纹理  
                fireball.Draw(_spriteBatch, fireballTexture);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }


    public class Animation2
    {
        public List<Texture2D> frames;
        public int currentFrame;
        public float frameDuration;
        public float elapsedTime;

        public Animation2(List<Texture2D> frames, float frameDuration)
        {
            this.frames = frames;
            this.currentFrame = 0;
            this.frameDuration = frameDuration;
            this.elapsedTime = 0;
        }

        public void nextone()
        {
            this.currentFrame = (this.currentFrame + 2) % frames.Count;
        }
        public void lastone()
        {
            this.currentFrame = (this.currentFrame - 2 + frames.Count) % frames.Count;
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime > frameDuration)
            {
                if (currentFrame % 2 == 0)
                {
                    currentFrame = (currentFrame + 1) % frames.Count;

                    elapsedTime -= frameDuration;
                }
                else
                {
                    currentFrame = (currentFrame - 1) % frames.Count;

                    elapsedTime -= frameDuration;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(frames[currentFrame], position, color);
        }
    }


    public class Fireball
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Lifetime { get; set; }
        public float MaxLifetime { get; set; }

        public Fireball(Vector2 position, Vector2 velocity, float maxLifetime)
        {
            Position = position;
            Velocity = velocity;
            MaxLifetime = maxLifetime;
        }

        public void Update(GameTime gameTime)
        {
            Lifetime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Lifetime >= MaxLifetime)
            {
                // 火球已过期，需要被移除  
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}


