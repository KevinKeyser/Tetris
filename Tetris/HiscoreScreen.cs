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
    public class HiscoreScreen : Screen
    {
        TextButton classicButton;
        TextButton timeAttackButton;
        TextButton exitButton;

        public HiscoreScreen()
            : base()
        {
            Texture2D pixel = new Texture2D(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            classicButton = new TextButton(pixel, null, null, null, Color.Green, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Classic", Color.Black);
            classicButton.Size = new Vector2(200, 50);
            classicButton.Position = new Vector2(100, 50);
            classicButton.LeftClicked += classicButton_LeftClicked;
            
            timeAttackButton = new TextButton(pixel, null, null, null, Color.Green, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Time Attack", Color.Black);
            timeAttackButton.Size = new Vector2(200, 50);
            timeAttackButton.Position = new Vector2(350, 50);
            timeAttackButton.LeftClicked += timeAttackButton_LeftClicked;

            exitButton = new TextButton(pixel, null, null, null, Color.Red, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "X", Color.Black);
            exitButton.Size = new Vector2(50, 50);
            exitButton.Position = new Vector2(GraphicsManager.ScreenWidth + 50, 0);
            exitButton.LeftClicked += exitButton_LeftClicked;

            UIComponents.Add(classicButton);
            UIComponents.Add(timeAttackButton);
            UIComponents.Add(exitButton);
        }

        void exitButton_LeftClicked(object sender, EventArgs e)
        {
            ScreenManager.SetScreen("TitleScreen");
        }

        void timeAttackButton_LeftClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void classicButton_LeftClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
