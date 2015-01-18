using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiLib.CoreTypes;
using System;
using FMOD;

namespace Tetris
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameBoard board;
        FMOD.Studio.System soundSystem;
        FMOD.Studio.Bank soundBank;
        FMOD.Studio.Bank stringBank;
        FMOD.Studio.EventDescription[] events;
        FMOD.Studio.EventInstance TetrisTitleEvent;
        RESULT result;
        float frequency;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GraphicsManager.Init(graphics, Content);
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
            GraphicsManager.ScreenHeight = 1000;
            GraphicsManager.ScreenWidth = 900;
            FMOD.Studio.System.create(out soundSystem);
            soundSystem.initialize(1, FMOD.Studio.INITFLAGS.NORMAL, INITFLAGS.NORMAL, System.IntPtr.Zero);
            result = soundSystem.loadBankFile(Environment.CurrentDirectory + @"\Content\Master Bank.bank", FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out soundBank);
            result = soundSystem.loadBankFile(Environment.CurrentDirectory + @"\Content\Master Bank.strings.bank", FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out stringBank);
            result = soundBank.getEventList(out events);
            events[0].createInstance(out TetrisTitleEvent);
            TetrisTitleEvent.setParameterValue("Level", 1);
            TetrisTitleEvent.setVolume(1);
            TetrisTitleEvent.start();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            board = new GameBoard(10, 20);
        }

        protected override void UnloadContent()
        {
            TetrisTitleEvent.release();
            for (int i = 0; i < events.Length; i++)
            {
                events[i].releaseAllInstances();
            }
            stringBank.unload();
            soundBank.unload();
            soundSystem.release();
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            soundSystem.update();
            
            board.Update(gameTime);
            TetrisTitleEvent.setParameterValue("Level", board.level);
            if (board.isPaused)
            {
                TetrisTitleEvent.setVolume(.25f);
            }
            else
            {
                TetrisTitleEvent.setVolume(1f);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            board.Draw(spriteBatch, Content.Load<SpriteFont>("font"));
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
