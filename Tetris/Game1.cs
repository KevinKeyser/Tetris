using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MiLib.CoreTypes;
using System;

namespace Tetris
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameBoard board;
        Song TitleSong;
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
            GraphicsManager.ScreenHeight = 800;
            GraphicsManager.ScreenWidth = 900;
            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            board = new GameBoard(10, 20);
            TitleSong = Content.Load<Song>("TetrisTitle");
            MediaPlayer.Play(TitleSong);
            MediaPlayer.IsRepeating = true;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            board.Update(gameTime);
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
