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
        TextButton startButton;
        TextButton hiscoresButton;
        TextButton optionButton;
        TextButton exitButton;


        public TitleScreen()
            :base()
        {
                

            Texture2D pixel = new Texture2D(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White } );
            startButton = new TextButton(pixel, null, null, null, Color.Blue, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Start", Color.Black);
            startButton.Size = new Vector2(200, 100);
            startButton.Position = new Vector2(GraphicsManager.ScreenWidth / 2 - 50, 100);
            startButton.LeftClicked +=startButton_LeftClicked;

            hiscoresButton = new TextButton(pixel, null, null, null, Color.Blue, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Hiscores", Color.Black);
            hiscoresButton.Size = new Vector2(200, 100);
            hiscoresButton.Position = new Vector2(GraphicsManager.ScreenWidth / 2 - 50, 225);
            hiscoresButton.LeftClicked += hiscoresButton_LeftClicked; 

            optionButton = new TextButton(pixel, null, null, null, Color.Blue, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Options", Color.Black);
            optionButton.Size = new Vector2(200, 100);
            optionButton.Position = new Vector2(GraphicsManager.ScreenWidth / 2 - 50, 350);
            optionButton.LeftClicked += optionButton_LeftClicked;

            exitButton = new TextButton(pixel, null, null, null, Color.Blue, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Exit", Color.Black);
            exitButton.Size = new Vector2(200, 100);
            exitButton.Position = new Vector2(GraphicsManager.ScreenWidth / 2 - 50, 475);
            exitButton.LeftClicked += exitButton_LeftClicked;

            UIComponents.Add(startButton);
            UIComponents.Add(hiscoresButton);
            UIComponents.Add(optionButton);
            UIComponents.Add(exitButton);
        }

        void hiscoresButton_LeftClicked(object sender, EventArgs e)
        {
            ScreenManager.SetScreen("HiscoreScreen");
        }

        void exitButton_LeftClicked(object sender, EventArgs e)
        {
            ScreenManager.SetScreen("LoginScreen");
            //GameApplication.isExiting = true;
        }

        void optionButton_LeftClicked(object sender, EventArgs e)
        {
            ScreenManager.SetScreen("OptionScreen");
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
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
