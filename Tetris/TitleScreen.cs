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
        FMOD.Studio.System soundSystem;
        FMOD.Studio.Bank soundBank;
        FMOD.Studio.Bank stringBank;
        FMOD.Studio.EventDescription[] events;
        FMOD.Studio.EventInstance TetrisTitleEvent;
        RESULT result;

        TextButton startButton;
        TextButton optionButton;
        TextButton exitButton;


        public TitleScreen()
            :base()
        {
           /* FMOD.Studio.System.create(out soundSystem);
            soundSystem.initialize(1, FMOD.Studio.INITFLAGS.NORMAL, INITFLAGS.NORMAL, System.IntPtr.Zero);
            result = soundSystem.loadBankFile(Environment.CurrentDirectory + @"\Content\Master Bank.bank", FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out soundBank);
            result = soundSystem.loadBankFile(Environment.CurrentDirectory + @"\Content\Master Bank.strings.bank", FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out stringBank);
            result = soundBank.getEventList(out events);
            events[0].createInstance(out TetrisTitleEvent);
            TetrisTitleEvent.setParameterValue("Level", 1);
            TetrisTitleEvent.setVolume(1);
            TetrisTitleEvent.start();*/
                

            Texture2D pixel = new Texture2D(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White } );
            startButton = new TextButton(pixel, null, null, null, Color.Blue, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Start", Color.Black);
            startButton.Size = new Vector2(200, 100);
            startButton.Position = new Vector2(GraphicsManager.ScreenWidth / 2 - 50, GraphicsManager.ScreenHeight / 4);
            startButton.LeftClicked += startButton_LeftClicked;

            optionButton = new TextButton(pixel, null, null, null, Color.Blue, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Options", Color.Black);
            optionButton.Size = new Vector2(200, 100);
            optionButton.Position = new Vector2(GraphicsManager.ScreenWidth / 2 - 50, GraphicsManager.ScreenHeight / 2);
            optionButton.LeftClicked += optionButton_LeftClicked;

            exitButton = new TextButton(pixel, null, null, null, Color.Blue, GraphicsManager.ContentManager.Load<SpriteFont>("font"), "Exit", Color.Black);
            exitButton.Size = new Vector2(200, 100);
            exitButton.Position = new Vector2(GraphicsManager.ScreenWidth / 2 - 50, 3*GraphicsManager.ScreenHeight / 4);
            exitButton.LeftClicked += exitButton_LeftClicked;
        }

        void exitButton_LeftClicked(object sender, EventArgs e)
        {
            GameApplication.isExiting = true;
        }

        void optionButton_LeftClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void startButton_LeftClicked(object sender, EventArgs e)
        {
            ScreenManager.SetScreen("GameScreen");    
        }

        public override void UnloadContent()
        {/*
            TetrisTitleEvent.release();
            for (int i = 0; i < events.Length; i++)
            {
                events[i].releaseAllInstances();
            }
            stringBank.unload();
            soundBank.unload();
            soundSystem.release();
            base.UnloadContent();*/
        }

        public override void Update(GameTime gameTime)
        {
            //soundSystem.update();

            startButton.Update(gameTime);
            optionButton.Update(gameTime);
            exitButton.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            startButton.Draw(spriteBatch);
            optionButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
            spriteBatch.DrawString(GraphicsManager.ContentManager.Load<SpriteFont>("font"), result.ToString(), Vector2.Zero, Color.Black);
            base.Draw(spriteBatch);
        }
    }
}
