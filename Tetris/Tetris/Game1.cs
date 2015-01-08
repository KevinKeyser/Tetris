using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiLib.CoreTypes;

namespace Tetris
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Piece straight;
        Piece l;
        Piece reverseL;
        Piece square;
        Piece z;
        Piece reverseZ;
        Piece t;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
            graphics.PreferredBackBufferWidth = 1050;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            straight = new Piece(new bool[5][] { new bool[]{ false, false, true, false, false }, new bool[]{ false, false, true, false, false }, new bool[]{ false, false, true, false, false }, new bool[]{ false, false, true, false, false }, new bool[]{ false, false, false, false, false } }, Content.Load<SpriteFont>("font"));
            l = new Piece(new bool[5][] { new bool[] { false, false, false, false, false }, new bool[] { false, false, true, false, false }, new bool[] { false, false, true, false, false }, new bool[] { false, false, true, true, false }, new bool[] { false, false, false, false, false } }, Content.Load<SpriteFont>("font"));
            l.Position = new Vector2(150, 0);
            reverseL = new Piece(new bool[5][] { new bool[] { false, false, false, false, false }, new bool[] { false, false, true, false, false }, new bool[] { false, false, true, false, false }, new bool[] { false, true, true, false, false }, new bool[] { false, false, false, false, false } }, Content.Load<SpriteFont>("font"));
            reverseL.Position = new Vector2(300, 0);
            square = new Piece(new bool[5][] { new bool[] { false, false, false, false, false }, new bool[] { false, false, false, false, false }, new bool[] { false, false, true, true, false }, new bool[] { false, false, true, true, false }, new bool[] { false, false, false, false, false } }, Content.Load<SpriteFont>("font"));
            square.Position = new Vector2(450, 0);
            square.CanRotate = false;
            z = new Piece(new bool[5][] { new bool[] { false, false, false, false, false }, new bool[] { false, false, false, false, false }, new bool[] { false, true, true, false, false }, new bool[] { false, false, true, true, false }, new bool[] { false, false, false, false, false } }, Content.Load<SpriteFont>("font"));
            z.Position = new Vector2(600, 0);
            reverseZ = new Piece(new bool[5][] { new bool[] { false, false, false, false, false }, new bool[] { false, false, false, false, false }, new bool[] { false, false, true, true, false }, new bool[] { false, true, true, false, false }, new bool[] { false, false, false, false, false } }, Content.Load<SpriteFont>("font"));
            reverseZ.Position = new Vector2(750, 0);
            t = new Piece(new bool[5][] { new bool[] { false, false, false, false, false }, new bool[] { false, false, false, false, false }, new bool[] { false, true, true, true, false }, new bool[] { false, false, true, false, false }, new bool[] { false, false, false, false, false } }, Content.Load<SpriteFont>("font"));
            t.Position = new Vector2(900, 0);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();

            if(InputManager.IsKeyPressed(Keys.Left))
            {
                straight.RotatedLeft();
                l.RotatedLeft();
                reverseL.RotatedLeft();
                square.RotatedLeft();
                z.RotatedLeft();
                reverseZ.RotatedLeft();
                t.RotatedLeft();
            }
            if(InputManager.IsKeyPressed(Keys.Right))
            {
                straight.RotatedRight();
                l.RotatedRight();
                reverseL.RotatedRight();
                square.RotatedRight();
                z.RotatedRight();
                reverseZ.RotatedRight();
                t.RotatedRight();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            straight.Draw(spriteBatch);
            l.Draw(spriteBatch);
            reverseL.Draw(spriteBatch);
            square.Draw(spriteBatch);
            z.Draw(spriteBatch);
            reverseZ.Draw(spriteBatch);
            t.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
