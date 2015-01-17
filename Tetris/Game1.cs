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
        FMOD.System soundSystem;
        FMOD.Sound titleMusic;
        FMOD.Channel channel;
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

            FMOD.Factory.System_Create(out soundSystem);
            soundSystem.init(1, INITFLAGS.NORMAL, IntPtr.Zero);
            soundSystem.createSound(Environment.CurrentDirectory + "/TetrisTitle.mp3", MODE.LOOP_NORMAL, out titleMusic);
            soundSystem.playSound(titleMusic, null, false, out channel);
            channel.getFrequency(out frequency);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            board = new GameBoard(10, 20);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            soundSystem.update();
            board.Update(gameTime);
            if (board.isPaused)
            {
                channel.setVolume(.25f);
            }
            else
            {
                channel.setVolume(1f);
            }
            channel.setFrequency(frequency + frequency * board.level / 1000);
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
