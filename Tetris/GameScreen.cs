using FMOD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiLib.CoreTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class GameScreen : Screen
    {
        Random random = new Random(1);

        Color?[][] board;
        int width;
        int height;

        Piece currentPiece;
        Piece shadowPiece;
        Queue<Piece> nextPieces;
        Piece onHoldPiece;
        List<int> pieceCounter;
        TimeSpan fallTime;
        TimeSpan offsetFallTime;
        TimeSpan elapsedFallTime;
        TimeSpan comboTime;
        TimeSpan elapsedComboTime;
        Vector2 boardPosition;
        bool hasHeld;
        public bool isPaused;
        bool lose;
        ulong score;
        public int level;
        int combo;
        float multiplier;
        int lastDeleted;
        float lastY;

        int closestRow;
        SpriteFont font;
        Texture2D pixel;

        SoundSystem soundSystem;


        public GameScreen(int width, int height)
            : base()
        {
            this.width = width;
            this.height = height;
            pixel = new Texture2D(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            font = GraphicsManager.ContentManager.Load<SpriteFont>("font");
            boardPosition = new Vector2(300, 100);
            board = new Color?[this.height][];
            soundSystem = new SoundSystem();
            init();
        }
        private void init()
        {
            for (int h = 0; h < height; h++)
            {
                board[h] = new Color?[width];
                for (int w = 0; w < width; w++)
                {
                    board[h][w] = null;
                }
            }
            pieceCounter = new List<int>();
            nextPieces = new Queue<Piece>();
            for (int i = 0; i < 7; i++)
            {
                pieceCounter.Add(0);
            }
            for (int i = 0; i < 5; i++)
            {
                nextPieces.Enqueue(newPiece());
            }
            currentPiece = newPiece();
            onHoldPiece = null;
            shadowCreate();
            shadowUpdate();
            hasHeld = false;
            lose = false;
            isPaused = false;
            elapsedFallTime = new TimeSpan();
            comboTime = new TimeSpan();
            elapsedComboTime = new TimeSpan();
            offsetFallTime = new TimeSpan();
            level = 1;
            fallTime = new TimeSpan(0, 0, 0, 0, 1001 - level);
            combo = 0;
            lastDeleted = 0;
            score = 0;
            multiplier = 1;
            lastY = -5;
            closestRow = height;
        }

        private void shadowUpdate()
        {
            shadowPiece.CanRotate = currentPiece.CanRotate;
            shadowPiece.Position = currentPiece.Position;
            while (canMoveDown(shadowPiece))
            {
                shadowPiece.Position.Y++;
            }
        }

        private void shadowCreate()
        {
            Color?[][] temp = new Color?[5][];
            for (int i = 0; i < currentPiece.blocks.Length; i++)
            {
                temp[i] = new Color?[5];
                currentPiece.blocks[i].CopyTo(temp[i], 0);
            }

            shadowPiece = new Piece(temp, currentPiece.ID);

            for (int y = 0; y < shadowPiece.blocks.Length; y++)
            {
                for (int x = 0; x < shadowPiece.blocks[y].Length; x++)
                {
                    if (shadowPiece.blocks[y][x].HasValue)
                    {
                        shadowPiece.blocks[y][x] = Color.Lerp(Color.Black, Color.Transparent, 0.1f);
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            soundSystem.Update(gameTime);
            if (lose)
            {
                if (InputManager.IsKeyPressed(Keys.Enter))
                {
                    init();
                    lose = false;
                }
            }
            else
            {
                if (InputManager.IsKeyPressed(Keys.Escape))
                {
                    isPaused = !isPaused;
                    if (isPaused)
                    {
                        //TetrisTitleEvent.setVolume(.25f);
                    }
                    else
                    {
                        //TetrisTitleEvent.setVolume(1f);
                    }
                }
                if (!isPaused)
                {
                    if (InputManager.IsKeyPressed(Keys.A))
                    {
                        addRow();
                    }
                    if (InputManager.IsKeyPressed(Keys.Left))
                    {
                        if (canMoveLeft(currentPiece))
                        {
                            currentPiece.Position.X--;
                        }
                    }
                    if (InputManager.IsKeyPressed(Keys.Right))
                    {
                        if (canMoveRight(currentPiece))
                        {
                            currentPiece.Position.X++;
                        }
                    }
                    if (InputManager.IsKeyPressed(Keys.Up))
                    {
                        if (canRotateLeft(currentPiece))
                        {
                            currentPiece.RotateLeft();
                            correctPosition(currentPiece);
                            shadowPiece.RotateLeft();
                            if (!canMoveDown(currentPiece))
                            {
                                elapsedFallTime = new TimeSpan();
                            }
                        }
                    }
                    if (InputManager.IsKeyPressed(Keys.Down))
                    {
                        if (canMoveDown(currentPiece))
                        {
                            currentPiece.Position.Y++;
                            elapsedFallTime = new TimeSpan();
                            offsetFallTime = TimeSpan.FromMilliseconds(500);
                        }
                        else
                        {
                            StickToMatrix();
                            checkRemove(currentPiece);
                            hasHeld = false;
                        }
                    }
                    if (InputManager.IsKeyPressed(Keys.Space))
                    {
                        while (canMoveDown(currentPiece))
                        {
                            currentPiece.Position.Y++;
                        }
                        StickToMatrix();
                        checkRemove(currentPiece);
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
                            Piece tempPiece = onHoldPiece;
                            onHoldPiece = currentPiece;
                            currentPiece = tempPiece;
                            onHoldPiece.Position = Vector2.Zero;
                            currentPiece.Position = new Vector2((board[0].Length - 5) / 2, -2);
                        }

                        shadowCreate();
                        hasHeld = true;
                    }
                    shadowUpdate();
                    elapsedComboTime += gameTime.ElapsedGameTime;
                    if (elapsedComboTime >= comboTime)
                    {
                        elapsedComboTime = new TimeSpan();
                        comboTime = new TimeSpan();
                        combo = 0;
                    }
                    elapsedFallTime += gameTime.ElapsedGameTime;
                    if (!canMoveDown(currentPiece) && currentPiece.CanRotate)
                    {
                        offsetFallTime = TimeSpan.FromMilliseconds(500);
                    }
                    if (lastY > currentPiece.Position.Y)
                    {
                        elapsedFallTime = new TimeSpan();
                    }
                    if (elapsedFallTime >= fallTime + offsetFallTime)
                    {
                        offsetFallTime = new TimeSpan();
                        elapsedFallTime = new TimeSpan();
                        if (canMoveDown(currentPiece))
                        {
                            currentPiece.Position.Y++;
                        }
                        else
                        {
                            StickToMatrix();
                            checkRemove(currentPiece);
                            hasHeld = false;
                        }
                    }
                    lastY = currentPiece.Position.Y;
                }
            }
            base.Update(gameTime);
        }

        private void StickToMatrix()
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (currentPiece.blocks[y][x].HasValue)
                    {
                        if (currentPiece.Position.Y + y < 0)
                        {
                            lose = true;
                            return;
                        }
                        else
                        {
                            board[y + (int)currentPiece.Position.Y][x + (int)currentPiece.Position.X] = currentPiece.blocks[y][x];
                            if (y + (int)currentPiece.Position.Y < closestRow)
                            {
                                closestRow = y + (int)currentPiece.Position.Y;
                            }
                        }
                    }
                }
            }
            currentPiece = nextPieces.Dequeue();
            nextPieces.Enqueue(newPiece());
            correctPosition(currentPiece);
            shadowCreate();
            shadowUpdate();
            lastY = -5;
        }

        private bool canRotateLeft(Piece piece)
        {
            Color?[][] tempColors = new Color?[5][];
            for (int i = 0; i < piece.blocks.Length; i++)
            {
                tempColors[i] = new Color?[5];
                piece.blocks[i].CopyTo(tempColors[i], 0);
            }
            Piece tempPiece = new Piece(tempColors, piece.ID);
            tempPiece.Position = piece.Position;
            tempPiece.RotateLeft();
            correctPosition(tempPiece);
            for (int y = 0; y < tempPiece.blocks.Length; y++)
            {
                for (int x = 0; x < tempPiece.blocks[y].Length; x++)
                {
                    if (tempPiece.blocks[y][x].HasValue && tempPiece.Position.Y + y >= 0)
                    {
                        if (tempPiece.Position.X + x >= 0 && tempPiece.Position.X + x < board[y].Length)
                        {
                            if (board[(int)tempPiece.Position.Y + y][(int)tempPiece.Position.X + x].HasValue)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool canMoveLeft(Piece piece)
        {
            if (piece.Position.X <= 0)
            {
                int x = -(int)piece.Position.X;
                for (int y = 0; y < piece.blocks.Length; y++)
                {
                    if (piece.blocks[y][x].HasValue)
                    {
                        return false;
                    }
                }
            }
            for (int y = 0; y < piece.blocks.Length; y++)
            {
                for (int x = 0; x < piece.blocks[y].Length; x++)
                {
                    if (piece.blocks[y][x].HasValue && piece.Position.Y + y >= 0)
                    {
                        if (board[(int)piece.Position.Y + y][(int)piece.Position.X + x - 1].HasValue == piece.blocks[y][x].HasValue)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool canMoveRight(Piece piece)
        {
            if (piece.Position.X >= board[0].Length - 5)
            {
                int x = board[0].Length - (int)piece.Position.X;
                for (int y = 0; y < piece.blocks.Length; y++)
                {
                    if (piece.blocks[y][x - 1].HasValue)
                    {
                        return false;
                    }
                }
            }
            for (int y = 0; y < piece.blocks.Length; y++)
            {
                for (int x = 0; x < piece.blocks[y].Length; x++)
                {
                    if (piece.blocks[y][x].HasValue && piece.Position.Y + y >= 0)
                    {
                        if (board[(int)piece.Position.Y + y][(int)piece.Position.X + x + 1].HasValue == piece.blocks[y][x].HasValue)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool canMoveDown(Piece piece)
        {
            if (piece.Position.Y >= board.Length - 5)
            {
                int y = board.Length - (int)piece.Position.Y;
                for (int x = 0; x < piece.blocks[y - 1].Length; x++)
                {
                    if (piece.blocks[y - 1][x].HasValue)
                    {
                        return false;
                    }
                }
            }
            for (int y = 0; y < piece.blocks.Length; y++)
            {
                for (int x = 0; x < piece.blocks[y].Length; x++)
                {
                    if (piece.blocks[y][x].HasValue && piece.Position.Y + y >= 0 && piece.Position.X + x >= 0 && piece.Position.X + x <= board[y].Length)
                    {
                        if (board[(int)piece.Position.Y + y + 1][(int)piece.Position.X + x].HasValue == piece.blocks[y][x].HasValue)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void correctPosition(Piece piece)
        {
            if (piece.Position.X <= -1)
            {
                int x = -(int)piece.Position.X;
                for (int y = 0; y < piece.blocks.Length; y++)
                {
                    if (x <= 0)
                    {
                        break;
                    }
                    if (piece.blocks[y][x - 1].HasValue)
                    {
                        piece.Position.X++;
                        x = -(int)piece.Position.X;
                        y = 0;
                    }
                }
            }
            if (piece.Position.X >= board[0].Length - 4)
            {
                int x = board[0].Length - (int)piece.Position.X;
                for (int y = 0; y < piece.blocks.Length; y++)
                {
                    if (x >= 4)
                    {
                        break;
                    }
                    if (piece.blocks[y][x - 1].HasValue)
                    {
                        piece.Position.X--;
                        x = board[0].Length - (int)piece.Position.X;
                        y = 0;
                    }
                }
            }
            if (piece.Position.Y >= board.Length - 4)
            {
                int y = board.Length - (int)piece.Position.Y;
                for (int x = 0; x < piece.blocks[0].Length; x++)
                {
                    if (y > 4)
                    {
                        break;
                    }
                    if (piece.blocks[y - 1][x].HasValue)
                    {
                        piece.Position.Y--;
                        y = board.Length - (int)piece.Position.Y;
                        x = 0;
                    }
                }
            }
            for (int y = 0; y < piece.blocks.Length; y++)
            {
                for (int x = 0; x < piece.blocks[y].Length; x++)
                {
                    if (piece.blocks[y][x].HasValue && piece.Position.Y + y >= 0)
                    {
                        if (x + piece.Position.X >= 0 && piece.Position.X + x < board[y].Length)
                        {
                            if (board[(int)piece.Position.Y + y][(int)piece.Position.X + x].HasValue)
                            {
                                if (y <= 2)
                                {
                                    piece.Position.Y--;
                                }
                                if (x > 2)
                                {
                                    piece.Position.X--;
                                }
                                else if (x < 2)
                                {
                                    piece.Position.X++;
                                }
                            }
                        }
                        else
                        {
                            if (x + piece.Position.X < 0)
                            {
                                piece.Position.X++;
                            }
                            else
                            {
                                piece.Position.X--;
                            }
                        }
                    }
                }
            }
        }

        private void checkRemove(Piece piece)
        {
            int totalScore = 0;
            int rowsDeleted = 0;
            for (int y = 0; y < board.Length; y++)
            {
                int fullcounter = 0;
                for (int x = 0; x < board[y].Length; x++)
                {
                    if (board[y][x].HasValue)
                    {
                        fullcounter++;
                    }
                }
                if (fullcounter == board[y].Length)
                {
                    rowsDeleted++;
                    for (int x = 0; x < board[y].Length; x++)
                    {
                        board[y][x] = null;
                    }
                    for (int yy = y; yy > 0; yy--)
                    {
                        for (int x = 0; x < board[yy].Length; x++)
                        {
                            board[yy][x] = board[yy - 1][x];
                            totalScore += 1;
                        }
                    }
                    for (int x = 0; x < board[y].Length; x++)
                    {
                        board[0][x] = null;
                    }
                }
            }
            if (rowsDeleted > 0)
            {
                if (rowsDeleted >= lastDeleted - 1)
                {
                    multiplier += .1f * rowsDeleted;
                }
                else
                {
                    multiplier = 1;
                    lastDeleted = 0;
                }
                combo += rowsDeleted;
                comboTime += TimeSpan.FromSeconds((Math.Pow(rowsDeleted * multiplier, 2) * 2.0f / combo));
                elapsedComboTime = new TimeSpan();
                score += (ulong)(totalScore * Math.Pow(rowsDeleted * multiplier, combo / 50.0f));
                level = MathHelper.Clamp(level + rowsDeleted, 1, 1000);
                fallTime -= TimeSpan.FromMilliseconds(rowsDeleted);
                if (lastDeleted < rowsDeleted)
                {
                    lastDeleted = rowsDeleted;
                }
               // TetrisTitleEvent.setParameterValue("Level", level);
            }
        }

        private Piece newPiece()
        {
            Piece newPiece;
            bool reloop = false;
            int number = 0;
            do
            {
                reloop = false;
                int pieceCount = 0;
                number = random.Next(7);
                for (int i = 0; i < nextPieces.ToArray().Length; i++)
                {
                    if (number == nextPieces.ToArray()[i].ID)
                    {
                        pieceCount++;
                        if (pieceCount >= 2)
                        {
                            i = -1;
                            pieceCount = 0;
                            number = random.Next(7);
                            continue;
                        }
                    }
                }
                for (int i = 0; i < pieceCounter.Count; i++)
                {
                    if (number == i)
                    {
                        continue;
                    }
                    if (pieceCounter[i] <= pieceCounter[number] - 7)
                    {
                        reloop = true;
                        break;
                    }
                }
            } while (reloop);
            switch (number)
            {
                case 0:
                    newPiece = PieceFactory.Straight(Color.Cyan);
                    break;
                case 1:
                    newPiece = PieceFactory.L(Color.Orange);
                    break;
                case 2:
                    newPiece = PieceFactory.ReverseL(Color.Blue);
                    break;
                case 3:
                    newPiece = PieceFactory.Square(Color.Yellow);
                    break;
                case 4:
                    newPiece = PieceFactory.ReverseZ(Color.Red);
                    break;
                case 5:
                    newPiece = PieceFactory.Z(Color.Green);
                    break;
                case 6:
                    newPiece = PieceFactory.T(Color.Purple);
                    break;
                default:
                    newPiece = PieceFactory.Square(Color.Yellow);
                    break;
            }
            pieceCounter[number]++;
            newPiece.Position = new Vector2((board[0].Length - 5) / 2, -3);
            if (canMoveDown(newPiece))
            {
                newPiece.Position.Y++;
            }
            else
            {
                lose = true;
            }
            elapsedFallTime = new TimeSpan();
            return newPiece;
        }

        private void addRow()
        {
            for (int y = 0; y < height - 1; y++)
            {
                board[y] = board[y + 1];
            }
            board[height - 1] = new Color?[width];
            int gap = random.Next(width);
            for (int i = 0; i < board[height - 1].Length; i++)
            {
                if (i == gap)
                {
                    continue;
                }
                board[height - 1][i] = Color.Gray;
            }
            correctPosition(currentPiece);
        }

        public override void UnloadContent()
        {

            base.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    spriteBatch.Draw(pixel, new Rectangle((int)boardPosition.X + x * 35, (int)boardPosition.Y + y * 35, 35, 35), Color.Black);
                    Color color = board[y][x].HasValue ? board[y][x].Value : Color.Lerp(Color.White, Color.Transparent, .25f);
                    spriteBatch.Draw(pixel, new Rectangle((int)boardPosition.X + x * 35 + 1, (int)boardPosition.Y + y * 35 + 1, 33, 33), color);
                    if (board[y][x].HasValue)
                    {
                        for (int cross = 0; cross < 35; cross++)
                        {
                            spriteBatch.Draw(pixel, new Vector2(boardPosition.X + x * 35 + cross, boardPosition.Y + y * 35 + cross), Color.Black);
                            spriteBatch.Draw(pixel, new Vector2(boardPosition.X + x * 35 + 35 - cross, boardPosition.Y + y * 35 + cross), Color.Black);
                        }
                        spriteBatch.Draw(pixel, new Rectangle((int)boardPosition.X + x * 35 + 8, (int)boardPosition.Y + y * 35 + 8, 20, 20), Color.Black);
                        spriteBatch.Draw(pixel, new Rectangle((int)boardPosition.X + x * 35 + 9, (int)boardPosition.Y + y * 35 + 9, 18, 18), board[y][x].Value);
                    }
                    //spriteBatch.DrawString(font, text, boardPosition + new Vector2(x * font.MeasureString(text).X, y * font.MeasureString(text).Y), color);
                }
            }
            shadowPiece.Draw(spriteBatch, pixel, boardPosition);
            currentPiece.Draw(spriteBatch, pixel, boardPosition);
            if (onHoldPiece != null)
            {
                onHoldPiece.Draw(spriteBatch, pixel, new Vector2(25, 50));
            }
            for (int i = 0; i < nextPieces.Count; i++)
            {
                nextPieces.ToArray()[i].Draw(spriteBatch, pixel, new Vector2(600, 100 + i * 35 * 5));
            }
            spriteBatch.DrawString(font, "Multiplier : X" + multiplier.ToString("0.00") + "\nCombo: " + combo.ToString() + "\nComboTime: " + elapsedComboTime.TotalSeconds.ToString("0.0") + "/" + comboTime.TotalSeconds.ToString("0.0"), new Vector2(10, 250), Color.Black);
            spriteBatch.DrawString(font, "Level:" + level + "\nScore: " + score.ToString(), new Vector2(150, 0), Color.Black);
            if (isPaused)
            {
                spriteBatch.Draw(pixel, new Rectangle(0, 0, GraphicsManager.ScreenWidth, GraphicsManager.ScreenHeight), Color.Lerp(Color.Black, Color.Transparent, 0.75f));
                spriteBatch.DrawString(font, "PAUSED", (new Vector2(GraphicsManager.ScreenWidth, GraphicsManager.ScreenHeight) - font.MeasureString("PAUSED")) / 2, Color.Black);
            }
            if (lose)
            {
                spriteBatch.Draw(pixel, new Rectangle(0, 0, GraphicsManager.ScreenWidth, GraphicsManager.ScreenHeight), Color.Lerp(Color.Black, Color.Transparent, 0.75f));
                spriteBatch.DrawString(font, "LOSE", (new Vector2(GraphicsManager.ScreenWidth, GraphicsManager.ScreenHeight) - font.MeasureString("PAUSED")) / 2, Color.Black);
            }
            base.Draw(spriteBatch);
        }
    }
}
