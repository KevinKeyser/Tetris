using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiLib.CoreTypes;
using MiLib.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class LoginScreen : Screen
    {
        TextButton loginButton;
        TextButton registerButton;
        TextBox usernameTextBox;
        TextBox passwordTextBox;

        public LoginScreen()
        {
            Texture2D pixel = new Texture2D(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, 5, 1);
            pixel.SetData<Color>(new Color[] { Color.Black, Color.Gray, Color.DarkGray, Color.White, Color.White });
            loginButton = new TextButton(pixel, null, null, null, Color.Blue, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Login", Color.Black);
            registerButton = new TextButton(pixel, null, null, null, Color.Blue, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Register", Color.Black);
            loginButton.Position = new Vector2(600, 100);
            loginButton.Size = new Vector2(200, 75);
            loginButton.LeftClicked += loginButton_LeftClicked;
            registerButton.Position = new Vector2(600, 225);
            registerButton.Size = new Vector2(200, 75);
            registerButton.LeftClicked += registerButton_LeftClicked;

            usernameTextBox = new TextBox(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, new Rectangle(100, 100, 450, 75), GraphicsManager.ContentManager.Load<SpriteFont>("font"));
            passwordTextBox = new TextBox(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, new Rectangle(100, 225, 450, 75), GraphicsManager.ContentManager.Load<SpriteFont>("font"));
        }

        void registerButton_LeftClicked(object sender, EventArgs e)
        {
            ScreenManager.SetScreen("RegisterScreen");  
        }

        void loginButton_LeftClicked(object sender, EventArgs e)
        {
            ScreenManager.SetScreen("TitleScreen");
        }

        public override void Update(GameTime gameTime)
        {
            passwordTextBox.Update(gameTime);
            usernameTextBox.Update(gameTime);
            loginButton.Update(gameTime);
            registerButton.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            usernameTextBox.Render(spriteBatch);
            passwordTextBox.Render(spriteBatch);
            base.Render(spriteBatch);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            passwordTextBox.Draw(spriteBatch);
            usernameTextBox.Draw(spriteBatch);  
            loginButton.Draw(spriteBatch);
            registerButton.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
