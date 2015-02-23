using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiLib.CoreTypes;
using System;

namespace Tetris
{
    public class GameApplication : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameApplication()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GraphicsManager.Init(graphics, Content);
        }

        public static bool isExiting = false;

        protected override void Initialize()
        {
            IsMouseVisible = true;
            GraphicsManager.ScreenHeight = 1000;
            GraphicsManager.ScreenWidth = 900;

            base.Initialize();
           
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.AddScreen("TitleScreen", new TitleScreen());
            ScreenManager.AddScreen("GameScreen", new GameScreen(10, 20));
            ScreenManager.AddScreen("LoginScreen", new LoginScreen());
            ScreenManager.AddScreen("RegisterScreen", new RegisterScreen());
            ScreenManager.AddScreen("OptionScreen", new OptionScreen());
            ScreenManager.AddScreen("HiscoreScreen", new HiscoreScreen());
            ScreenManager.SetScreen("LoginScreen");
        }

        protected override void UnloadContent()
        {
            ScreenManager.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if(isExiting)
            {
                Exit();
            }
            InputManager.Update();
            ScreenManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            ScreenManager.Render(spriteBatch);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            ScreenManager.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
