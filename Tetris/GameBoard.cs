using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        Queue<Piece> nextPieces;
        Piece onHoldPiece;
        TimeSpan elapsedTime;
        Random random = new Random();
        Vector2 boardPosition;
        bool hasHeld = false;
        long score = 0;
        bool isPaused = false;
        int combo = 0;
        TimeSpan comboTime = new TimeSpan();
        TimeSpan elapsedComboTime = new TimeSpan();
        int lastDeleted = 0;
        float multiplier = 1;

        public GameBoard(int width, int height)
        {
            elapsedTime = new TimeSpan();
            boardPosition = new Vector2(300, 25);
            board = new bool[height][];
            for (int h = 0; h < height; h++)
            {
                board[h] = new bool[width];
                for (int w = 0; w < width; w++)
                {
                    board[h][w] = false;
                }
            }
            nextPieces = new Queue<Piece>();
            for (int i = 0; i < 5; i++)
            {
                nextPieces.Enqueue(newPiece());
            }
            currentPiece = newPiece();
        }

        public void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyPressed(Keys.Escape))
            {
                isPaused = !isPaused;
            }
            if (isPaused)
            {
                MediaPlayer.Volume = .25f;
            }
            else
            {
                MediaPlayer.Volume = 1f;
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
                        hasHeld = false;
                    }
                }
                if (InputManager.IsKeyPressed(Keys.Space))
                {
                    while (canMoveDown())
                    {
                        currentPiece.Position.Y++;
                    }
                    StickToMatrix();
                    checkremove();
                    hasHeld = false;
                }
                if ((InputManager.IsKeyPressed(Keys.LeftShift) || InputManager.IsKeyPressed(Keys.RightShift)) && !hasHeld)
                {
                    if (onHoldPiece == null)
                    {
                        onHoldPiece = currentPiece;
                        onHoldPiece.Position = Vector2.Zero;
                        currentPiece = nextPieces.Dequeue();
                        nextPieces.Enqueue(newPiece());

                    }
                    else
                    {
                        Piece temp = onHoldPiece;
                        onHoldPiece = currentPiece;
                        currentPiece = temp;
                        onHoldPiece.Position = Vector2.Zero;
                        currentPiece.Position = new Vector2((board[0].Length - 5) / 2, -2);
                    }
                    hasHeld = true;
                }
                elapsedComboTime += gameTime.ElapsedGameTime;
                if(elapsedComboTime >= comboTime)
                {
                    elapsedComboTime = new TimeSpan();
                    comboTime = new TimeSpan();
                    combo = 0;
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
                        hasHeld = false;
                    }
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
            currentPiece = nextPieces.Dequeue();
            nextPieces.Enqueue(newPiece());
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
                    if (x >= 4)
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
            int totalScore = 0;
            int rowsDeleted = 0;
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
                    rowsDeleted++;
                    for(int x = 0; x < board[y].Length; x++)
                    {
                        board[y][x] = false;
                    }
                    for(int yy = y; yy > 0; yy--)
                    {
                        for(int x = 0; x < board[yy].Length; x++)
                        {
                            board[yy][x] = board[yy - 1][x];
                            totalScore += 1;
                        }
                    }
                }
            }
            if (rowsDeleted > 0)
            {
                if(rowsDeleted >= lastDeleted)
                {
                    multiplier += .1f * rowsDeleted;
                }
                else
                {
                    multiplier = 1;
                }
                combo += rowsDeleted;
                comboTime += TimeSpan.FromSeconds((Math.Pow(rowsDeleted*multiplier, 3) * 2.0f/combo));
                elapsedComboTime = new TimeSpan();
                score += (int)(totalScore * Math.Pow(rowsDeleted*multiplier, combo / 50.0f));
                lastDeleted = rowsDeleted;
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
                    spriteBatch.DrawString(font, text, boardPosition + new Vector2(x * font.MeasureString(text).X, y * font.MeasureString(text).Y), color);
                }
            }
            currentPiece.Draw(spriteBatch, font, boardPosition);
            if(onHoldPiece != null)
            {
                onHoldPiece.Draw(spriteBatch, font, new Vector2(25, 50));
            }
            for (int i = 0; i < nextPieces.Count; i++)
            {
                nextPieces.ToArray()[i].Draw(spriteBatch, font, new Vector2(600, 100 + i * 125));
            }
            spriteBatch.DrawString(font, "Multiplier : X" + multiplier.ToString("0.00") + "\nCombo: " + combo.ToString() + "\nComboTime: " + elapsedComboTime.TotalSeconds + "/" + comboTime.TotalSeconds, new Vector2(25, 200), Color.Black);
            spriteBatch.DrawString(font, "Score: " + score.ToString(), new Vector2(50, 25), Color.Black);
            if(isPaused)
            {
                spriteBatch.DrawString(font, "PAUSED", (new Vector2(GraphicsManager.ScreenWidth, GraphicsManager.ScreenHeight) - font.MeasureString("PAUSED") )/2, Color.Gray);
            }
        }
    }
}
