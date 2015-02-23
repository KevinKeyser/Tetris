using FMOD;
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
    public class TitleScreen : Screen
    {
        Button startButton;
        Button hiscoresButton;
        Button optionButton;
       // TextButton exitButton;

        Window hiscoresWindow;
        Window optionsWindow; 
        TextButton exitButton;


        public TitleScreen()
            :base()
        {
            DimColor = Color.Lerp(Color.Black, Color.Transparent, .5f);

            Texture2D pixel = new Texture2D(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White } );
            startButton = new Button(GraphicsManager.GraphicsDeviceManager.GraphicsDevice,  pixel, null, null, null, Color.Green);
            startButton.Size = new Vector2(50, 50);
            startButton.Position = new Vector2(GraphicsManager.ScreenWidth - 100, 300);
            startButton.LeftClicked +=startButton_LeftClicked;

            hiscoresButton = new Button(GraphicsManager.GraphicsDeviceManager.GraphicsDevice,  pixel, null, null, null, Color.Red);
            hiscoresButton.Size = new Vector2(50, 50);
            hiscoresButton.Position = new Vector2(GraphicsManager.ScreenWidth - 100, 100);
            hiscoresButton.LeftClicked += hiscoresButton_LeftClicked;

            optionButton = new Button(GraphicsManager.GraphicsDeviceManager.GraphicsDevice,  pixel, null, null, null, Color.Blue);
            optionButton.Size = new Vector2(50, 50);
            optionButton.Position = new Vector2(GraphicsManager.ScreenWidth - 100, 200);
            optionButton.LeftClicked += optionButton_LeftClicked;

            /*exitButton = new TextButton(pixel, null, null, null, Color.Blue, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Exit", Color.Black);
            exitButton.Size = new Vector2(200, 100);
            exitButton.Position = new Vector2(GraphicsManager.ScreenWidth / 2 - 50, 475);
            exitButton.LeftClicked += exitButton_LeftClicked;
            */

            hiscoresWindow = new Window(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, new Vector2(100), new Vector2(GraphicsManager.ScreenWidth - 200, GraphicsManager.ScreenHeight - 200), GraphicsManager.ContentManager.Load<SpriteFont>("font"), pixel);
            hiscoresWindow.WindowColor = Color.Blue;
            hiscoresWindow.IsVisible = false;
            hiscoresWindow.IsDraggable = false;

            optionsWindow = new Window(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, new Vector2(100, 100), new Vector2(GraphicsManager.ScreenWidth - 200, GraphicsManager.ScreenHeight - 200), GraphicsManager.ContentManager.Load<SpriteFont>("font"), pixel);
            optionsWindow.WindowColor = Color.White;
            optionsWindow.IsVisible = false;
            optionsWindow.IsDraggable = false;


            exitButton = new TextButton(GraphicsManager.GraphicsDeviceManager.GraphicsDevice,  pixel, null, null, null, Color.Red, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "X", Color.Black);
            exitButton.Size = new Vector2(50, 50);
            exitButton.Position = new Vector2(GraphicsManager.ScreenWidth - 250, 0);
            exitButton.LeftClicked += exitButton_LeftClicked;

            optionsWindow.Items.Add(exitButton);
            hiscoresWindow.Items.Add(exitButton);

            UIComponents.Add(startButton);
            UIComponents.Add(hiscoresButton);
            UIComponents.Add(optionButton);
           // UIComponents.Add(exitButton);
        }

        void exitButton_LeftClicked(object sender, EventArgs e)
        {
            optionsWindow.IsVisible = false;
            hiscoresWindow.IsVisible = false;
            Enabled = true;
        }

        void hiscoresButton_LeftClicked(object sender, EventArgs e)
        {
            hiscoresWindow.IsVisible = true;
            Enabled = false;
        }

        void optionButton_LeftClicked(object sender, EventArgs e)
        {
            optionsWindow.IsVisible = true;
            Enabled = false;
        }

        void startButton_LeftClicked(object sender, EventArgs e)
        {
            ScreenManager.SetScreen("GameScreen");    
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            hiscoresWindow.Update(gameTime);
            optionsWindow.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);
            hiscoresWindow.Render(spriteBatch);
            optionsWindow.Render(spriteBatch);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            hiscoresWindow.Draw(spriteBatch);
            optionsWindow.Draw(spriteBatch);
        }
    }
}
