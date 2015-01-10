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
        Random random = new Random();

        public GameBoard(int width, int height)
        {
            elapsedTime = new TimeSpan();
            

            //bool[,] test = new bool[,] { {true, true }, {false, false} };
            


            board = new bool[height][];
            for (int i = 0; i < height; i++)
            {
                board[i] = new bool[width];
                for (int w = 0; w < width; w++)
                {
                    board[i][w] = false;
                }
            }
            currentPiece = newPiece();
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
                else
                {
                    StickToMatrix();
                    checkremove();
                }
            }
            if(InputManager.IsKeyPressed(Keys.Space))
            {
                while(canMoveDown())
                {
                    currentPiece.Position.Y++;
                }
                StickToMatrix();
                checkremove();
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
                    StickToMatrix();
                    checkremove();
                }
            }
        }

        private void StickToMatrix()
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (currentPiece.blocks[y][x])
                    {
                        if (currentPiece.Position.Y + y < 0)
                        {
                            //lose
                        }
                        else
                        {
                            board[y + (int)currentPiece.Position.Y][x + (int)currentPiece.Position.X] = currentPiece.blocks[y][x];
                        }
                    }
                }
            }
            currentPiece = newPiece();
        }

        private void correctPosition()
        {
            if (currentPiece.Position.X <= -1)
            {
                int x = -(int)currentPiece.Position.X;
                for (int y = 0; y < currentPiece.blocks.Length; y++)
                {
                    if (x <= 0)
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
                    if (x > 4)
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
                    if (y > 4)
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
            for (int y = 0; y < currentPiece.blocks.Length; y++)
            {
                for (int x = 0; x < currentPiece.blocks[y].Length; x++)
                {
                    if (currentPiece.blocks[y][x] && currentPiece.Position.Y + y >= 0)
                    {
                        if (board[(int)currentPiece.Position.Y + y][(int)currentPiece.Position.X + x] == currentPiece.blocks[y][x])
                        {
                            if(y < 1)
                            {
                                currentPiece.Position.Y--;
                            }
                            else if (x > 2)
                            {
                                currentPiece.Position.X--;
                            }
                            else if( x < 2)
                            {
                                currentPiece.Position.X++;
                            }
                        }
                    }
                }
            }
        }

        private void checkremove()
        {
            for(int y = 0; y < board.Length; y++)
            {
                int fullcounter = 0;
                for(int x = 0; x < board[y].Length; x++)
                {
                    if(board[y][x])
                    {
                        fullcounter++;
                    }
                }
                if(fullcounter == board[y].Length)
                {
                    for(int x = 0; x < board[y].Length; x++)
                    {
                        board[y][x] = false;
                    }
                    for(int yy = y; yy > 0; yy--)
                    {
                        for(int x = 0; x < board[yy].Length; x++)
                        {
                            board[yy][x] = board[yy - 1][x];
                        }
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
            for (int y = 0; y < currentPiece.blocks.Length; y++)
            {
                for (int x = 0; x < currentPiece.blocks[y].Length; x++)
                {
                    if (currentPiece.blocks[y][x] && currentPiece.Position.Y + y >= 0)
                    {
                        if (board[(int)currentPiece.Position.Y + y][(int)currentPiece.Position.X + x - 1] == currentPiece.blocks[y][x])
                        {
                            return false;
                        }
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
            for (int y = 0; y < currentPiece.blocks.Length; y++)
            {
                for (int x = 0; x < currentPiece.blocks[y].Length; x++)
                {
                    if (currentPiece.blocks[y][x] && currentPiece.Position.Y + y >= 0)
                    {
                        if (board[(int)currentPiece.Position.Y + y][(int)currentPiece.Position.X + x + 1] == currentPiece.blocks[y][x])
                        {
                            return false;
                        }
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
            for (int y = 0; y < currentPiece.blocks.Length; y++)
            {
                for (int x = 0; x < currentPiece.blocks[y].Length; x++)
                {
                    if (currentPiece.blocks[y][x] && currentPiece.Position.Y + y >= 0)
                    {
                        if(board[(int)currentPiece.Position.Y + y + 1][(int)currentPiece.Position.X + x] == currentPiece.blocks[y][x])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private Piece newPiece()
        {
            Piece newPiece;
            int number = random.Next(7);
            switch (number)
            {
                case 0:
                    newPiece = PieceFactory.Straight();
                    break;
                case 1:
                    newPiece = PieceFactory.L();
                    break;
                case 2:
                    newPiece = PieceFactory.ReverseL();
                    break;
                case 3:
                    newPiece = PieceFactory.Square();
                    break;
                case 4:
                    newPiece = PieceFactory.Z();
                    break;
                case 5:
                    newPiece = PieceFactory.ReverseZ();
                    break;
                case 6:
                    newPiece = PieceFactory.T();
                    break;
                default:
                    newPiece = PieceFactory.Square();
                    break;
            }
            newPiece.Position = new Vector2((board[0].Length - 5) / 2, -2);
            elapsedTime = new TimeSpan();
            return newPiece;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    string text = board[y][x] == true ? " 1 " : " 0 ";
                    Color color = Color.Lerp(Color.Black,Color.CornflowerBlue, .9f);
                    if (text == " 1 ")
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
