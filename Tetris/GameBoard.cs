using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiLib.CoreTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class GameBoard
    {
        bool[][] board;
        Piece currentPiece;
        TimeSpan elapsedTime;

        public GameBoard(int width, int height)
        {
            currentPiece = PieceFactory.Straight();
            currentPiece.Position = new Vector2((width - 5) / 2, -5);
            elapsedTime = new TimeSpan();

            board = new bool[height][];
            for (int i = 0; i < height; i++)
            {
                board[i] = new bool[width];
                for (int w = 0; w < width; w++)
                {
                    board[i][w] = false;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyPressed(Keys.Left))
            {
                if (canMoveLeft())
                {
                    currentPiece.Position.X--;
                }
            }
            if (InputManager.IsKeyPressed(Keys.Right))
            {
                if (canMoveRight())
                {
                    currentPiece.Position.X++;
                }
            }
            if (InputManager.IsKeyPressed(Keys.Up))
            {
                currentPiece.RotateLeft();
                correctPosition();
            }
            if (InputManager.IsKeyPressed(Keys.Down))
            {
                if (canMoveDown())
                {
                    currentPiece.Position.Y++;
                }
            }
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime >= TimeSpan.FromSeconds(1))
            {
                elapsedTime = new TimeSpan();
                if (canMoveDown())
                {
                    currentPiece.Position.Y++;
                }
                else
                {
                    //board[y + (int)currentPiece.Position.Y][x + (int)currentPiece.Position.X] = currentPiece.blocks[y][x];
                }
            }
        }

        private void correctPosition()
        {
            if (currentPiece.Position.X <= -1)
            {
                int x = -(int)currentPiece.Position.X;
                for (int y = 0; y < currentPiece.blocks.Length; y++)
                {
                    if(x <= 0)
                    {
                        break;
                    }
                    if (currentPiece.blocks[y][x - 1])
                    {
                        currentPiece.Position.X++;
                        x = -(int)currentPiece.Position.X;
                        y = 0;
                    }
                }
            }
            if (currentPiece.Position.X >= board[0].Length - 4)
            {
                int x = board[0].Length - (int)currentPiece.Position.X;
                for (int y = 0; y < currentPiece.blocks.Length; y++)
                {
                    if(x > 4)
                    {
                        break;
                    }
                    if (currentPiece.blocks[y][x - 1])
                    {
                        currentPiece.Position.X--;
                        x = board[0].Length - (int)currentPiece.Position.X;
                        y = 0;
                    }
                }
            }
            if (currentPiece.Position.Y >= board.Length - 4)
            {
                int y = board.Length - (int)currentPiece.Position.Y;
                for (int x = 0; x < currentPiece.blocks[0].Length; x++)
                {
                    if(y > 4)
                    {
                        break;
                    }
                    if (currentPiece.blocks[y - 1][x])
                    {
                        currentPiece.Position.Y--;
                        y = board.Length - (int)currentPiece.Position.Y;
                        x = 0;
                    }
                }
            }
        }

        private bool canMoveLeft()
        {
            if (currentPiece.Position.X <= 0)
            {
                int x = -(int)currentPiece.Position.X;
                for (int y = 0; y < currentPiece.blocks.Length; y++)
                {
                    if (currentPiece.blocks[y][x])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool canMoveRight()
        {
            if (currentPiece.Position.X >= board[0].Length - 5)
            {
                int x = board[0].Length - (int)currentPiece.Position.X;
                for (int y = 0; y < currentPiece.blocks.Length; y++)
                {
                    if (currentPiece.blocks[y][x - 1])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool canMoveDown()
        {
            if (currentPiece.Position.Y >= board.Length - 5)
            {
                int y = board.Length - (int)currentPiece.Position.Y;
                for (int x = 0; x < currentPiece.blocks[y - 1].Length; x++)
                {
                    if (currentPiece.blocks[y - 1][x])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    string text = board[y][x] == true ? "1" : "0";
                    Color color = Color.Black;
                    if (text == "1")
                    {
                        color = Color.White;
                    }
                    spriteBatch.DrawString(font, text, new Vector2(x * font.MeasureString(text).X, y * font.MeasureString(text).Y), color);
                }
            }
            currentPiece.Draw(spriteBatch, font);
        }
    }
}
