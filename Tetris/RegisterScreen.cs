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
    public class RegisterScreen : Screen
    {
        TextButton submitButton;
        TextButton backButton;
        TextBox usernameTextBox;
        TextBox passwordTextBox;
        TextBox passwordConfirmTextBox;
        TextBox firstnameTextBox;
        TextBox lastnameTextBox;
        TextBox emailTextBox;
        TextBox monthTextBox;
        TextBox dayTextBox;
        TextBox yearTextBox;

        public RegisterScreen()
            :base()
        {
            Texture2D pixel = new Texture2D(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White});
            usernameTextBox = new TextBox(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, new Rectangle(100, 50, 200, 50), GraphicsManager.ContentManager.Load<SpriteFont>("font"));
            passwordTextBox = new TextBox(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, new Rectangle(100, 125, 200, 50), GraphicsManager.ContentManager.Load<SpriteFont>("font")) { isPassword = true };
            passwordConfirmTextBox = new TextBox(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, new Rectangle(100, 200, 200, 50), GraphicsManager.ContentManager.Load<SpriteFont>("font")) { isPassword = true };
            firstnameTextBox = new TextBox(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, new Rectangle(100, 275, 200, 50), GraphicsManager.ContentManager.Load<SpriteFont>("font"));
            lastnameTextBox = new TextBox(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, new Rectangle(100, 350, 200, 50), GraphicsManager.ContentManager.Load<SpriteFont>("font"));
            emailTextBox = new TextBox(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, new Rectangle(100, 425, 200, 50), GraphicsManager.ContentManager.Load<SpriteFont>("font"));
            monthTextBox = new TextBox(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, new Rectangle(100, 500, 50, 50), GraphicsManager.ContentManager.Load<SpriteFont>("font"));
            dayTextBox = new TextBox(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, new Rectangle(160, 500, 50, 50), GraphicsManager.ContentManager.Load<SpriteFont>("font"));
            yearTextBox = new TextBox(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, new Rectangle(220, 500, 80, 50), GraphicsManager.ContentManager.Load<SpriteFont>("font"));

            submitButton = new TextButton(pixel, null, null, null, Color.Green, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Submit", Color.Black);
            submitButton.Size = new Vector2(150, 50);
            submitButton.Position = new Vector2(400, 425);
            submitButton.LeftClicked += submitButton_LeftClicked;
            backButton = new TextButton(pixel, null, null, null, Color.Green, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Back", Color.Black);
            backButton.Size = new Vector2(150, 50);
            backButton.Position = new Vector2(400, 500);
            backButton.LeftClicked += backButton_LeftClicked;

            UIComponents.Add(usernameTextBox);
            UIComponents.Add(passwordTextBox);
            UIComponents.Add(passwordConfirmTextBox);
            UIComponents.Add(firstnameTextBox);
            UIComponents.Add(lastnameTextBox); 
            UIComponents.Add(emailTextBox);
            UIComponents.Add(monthTextBox);
            UIComponents.Add(dayTextBox); 
            UIComponents.Add(yearTextBox);

            UIComponents.Add(submitButton);
            UIComponents.Add(backButton);
        }

        void backButton_LeftClicked(object sender, EventArgs e)
        {
            ScreenManager.SetScreen("LoginScreen");
        }

        void submitButton_LeftClicked(object sender, EventArgs e)
        {
            ScreenManager.SetScreen("TitleScreen");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
