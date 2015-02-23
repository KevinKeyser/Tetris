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
    class OptionScreen : Screen
    {
        TextButton saveButton;
        TextButton backButton;

        public OptionScreen()
            : base()
        {
            Texture2D pixel = new Texture2D(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            saveButton = new TextButton(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, pixel, null, null, null, Color.Green, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Save", Color.Black);
            saveButton.Size = new Vector2(200, 50);
            saveButton.Position = new Vector2(350, 400);
            saveButton.LeftClicked += saveButton_LeftClicked;

            backButton = new TextButton(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, pixel, null, null, null, Color.Green, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Back", Color.Black);
            backButton.Size = new Vector2(200, 50);
            backButton.Position = new Vector2(100, 400);
            backButton.LeftClicked += backButton_LeftClicked;

            UIComponents.Add(saveButton);
            UIComponents.Add(backButton);
        }

        void saveButton_LeftClicked(object sender, EventArgs e)
        {
            ScreenManager.SetScreen("TitleScreen");
        }

        void backButton_LeftClicked(object sender, EventArgs e)
        {
            ScreenManager.SetScreen("TitleScreen");
        }
    }
}
